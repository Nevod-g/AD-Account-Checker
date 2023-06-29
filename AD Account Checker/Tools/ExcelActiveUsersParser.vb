Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools
Imports System.Data.OleDb
Imports System.Threading.Tasks


Public NotInheritable Class ExcelActiveUsersParser
	' Ключевыве наименования колонок в файле Excel, ECN - Excel Column Name
	'! В нижнем регистре!
	Private Const ECN_NUMBER = "pers.nr."
	Private Const ECN_NUMBER_ALT1 = "nr."
	Private Const ECN_NUMBER_ALT2 = "nr#"
	Private Const ECN_NUMBER_ALT3 = "pers#nr#"
	Private Const ECN_NUMBER_ALT4 = "employeeid"
	Private Const ECN_NAME = "vorname"
	Private Const ECN_NAME_ALT1 = "givenname"
	Private Const ECN_SURNAME = "nachname"
	Private Const ECN_SURNAME_ALT1 = "surname"
	Private Const ECN_FUNCTION = "funktion"
	Private Const ECN_DEPARTMENT = "department"
	Private Const ECN_DATE_OF_ENTRY = "date of entry"
	Private Const ECN_DATE_OF_TERMINATION = "date of termination"

	Public Shared ReadOnly DataSource As New List(Of UserAccount)

#Region "Parse Data"
	''' <summary>
	''' Прочитать файл данных в формате Excel 'HR List'.
	''' </summary>
	''' <param name="filePath"></param>
	Public Shared Sub ReadExcelFile(filePath As String)
		Dim fileName = FileSystem.Path.GetName(filePath)
		Dim excelConnection As OleDbConnection
		Try
			excelConnection = Excel.GetConnection(filePath)
		Catch ex As Exception
			If ex.HResult = -2147467259 Then
				Message.Show(ex.Message & $"{vbCrLf}FileName: {fileName}", "Read Excel File")
			Else
				Throw ex
			End If
			Exit Sub
		End Try

		' Перебрать все таблицы на листах в книге
		If excelConnection.State <> ConnectionState.Open Then excelConnection.Open()
		Dim schemaTables As DataTable = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
																	New Object() {Nothing, Nothing, Nothing, "TABLE"})
		'Dim tables = excelConnection.GetSchema("Tables")
		' Получить и перебрать список наименований листов
		'Dim sheetNames = Excel.GetSheetNames(excelConnection)

		For Each schemaRow As DataRow In schemaTables.Rows
			Dim sheetName = schemaRow("TABLE_NAME").ToString()
			' Пропустить призрачные листы (Реально эти объекты пользователь не видит и они дублируют данные.)
			If InStr(sheetName, "_xlnm#_FilterDatabase") > 0 Then Continue For

			Dim cmd = New OleDbDataAdapter($"select * from [{sheetName}]", excelConnection)
			Debug.Print($"Sheet Name: {sheetName}")
			Dim ds = New DataSet(sheetName)
			Try
				cmd.Fill(ds)
			Catch ex As Exception
				Message.Show($"Unknown {sheetName} object format.{vbCrLf}{ex}" & vbCrLf &
							 $"File Name: {fileName}", "Read Excel File")
				Continue For
			End Try

			' Список обязательных колонок:
			Dim checkColumns = New List(Of IdName)
			checkColumns.AddRange({
				New IdName(0, ECN_NUMBER),
				New IdName(0, ECN_NAME),
				New IdName(0, ECN_SURNAME),
				New IdName(0, ECN_FUNCTION)
			})

			' Проверить доступность данных на листе	
			For Each col As DataColumn In ds.Tables.Item(0).Columns
				Dim item = checkColumns.FirstOrDefault(
					Function(c)
						Return c.Name.ToLower() = col.ColumnName.ToLower().Trim() OrElse ' Синонимы:
						(c.Name = ECN_NUMBER And col.ColumnName.ToLower().Trim() = ECN_NUMBER_ALT1) OrElse
						(c.Name = ECN_NUMBER And col.ColumnName.ToLower().Trim() = ECN_NUMBER_ALT2) OrElse
						(c.Name = ECN_NUMBER And col.ColumnName.ToLower().Trim() = ECN_NUMBER_ALT3) OrElse
						(c.Name = ECN_NUMBER And col.ColumnName.ToLower().Trim() = ECN_NUMBER_ALT4) OrElse
						(c.Name = ECN_NAME And col.ColumnName.ToLower().Trim() = ECN_NAME_ALT1) OrElse
						(c.Name = ECN_SURNAME And col.ColumnName.ToLower().Trim() = ECN_SURNAME_ALT1)
					End Function)
				If item IsNot Nothing Then item.Id = 1 ' Метка, колонка найдена.
			Next

			Dim uncheckedColumns = checkColumns.Where(Function(c) c.Id <> 1)
			If uncheckedColumns?.Count() > 0 Then
				Message.Show($"Unknown '{sheetName}' sheet data format. Key columns are missing." & vbCrLf &
								 $"The following key columns were not found: {vbCrLf}{ _
								 Tools.ListToString(uncheckedColumns.Select(Function(c) $"{vbTab}- {c.Name}").ToList())}" &
								 vbCrLf & $"File Name: {fileName}", "Read Excel File")
				Continue For
			End If

			' Добавить необязательные колонки данных:
			checkColumns.AddRange({
				New IdName(100, ECN_DEPARTMENT),
				New IdName(100, ECN_DATE_OF_ENTRY),
				New IdName(110, ECN_DATE_OF_TERMINATION)
			})

			' Парсить данные Excel в лист данных
			Dim excelRowNumber = 1
			For Each row As DataRow In ds.Tables.Item(0).Rows
				excelRowNumber += 1 ' со второй строки
				Dim item = New UserAccount() With {
					.Id = DataSource.Count() + 1,
					.ExcelFileName = fileName,
					.ExcelSheetName = Replace(sheetName, "$", ""),
					.ExcelRowNumber = excelRowNumber
				}
				Dim needAdd As Boolean = False
				For Each col As DataColumn In ds.Tables.Item(0).Columns
					Dim value = row.Item(col.ColumnName)
					If value IsNot Nothing And Not IsDBNull(value) Then
						Select Case col.ColumnName.ToLower().Trim()
							Case ECN_NUMBER, ECN_NUMBER_ALT1, ECN_NUMBER_ALT2, ECN_NUMBER_ALT3, ECN_NUMBER_ALT4
								item.ImportNumber = ValToStr(value).Trim()
							Case ECN_NAME, ECN_NAME_ALT1 : item.ImportGivenName = ValToStr(value).Trim()
							Case ECN_SURNAME, ECN_SURNAME_ALT1 : item.ImportSurname = ValToStr(value).Trim()
							Case ECN_FUNCTION : item.ImportFunction = ValToStr(value).Trim()
							Case ECN_DEPARTMENT : item.ImportDepartment = ValToStr(value).Trim()
							Case ECN_DATE_OF_ENTRY : item.ImportDateOfEntry = ParseOaDate(value)
							Case ECN_DATE_OF_TERMINATION : item.ImportDateOfTermination = ParseOaDate(value)
						End Select
					End If
				Next

				' Добавить строку в источник, если определён персональный номер.
				If Not String.IsNullOrWhiteSpace(item.ImportNumber) Then DataSource.Add(item)
			Next
		Next
		Task.Run(Sub() excelConnection.Close())
	End Sub

	Private Shared Function ParseOaDate(value As Object) As Date?
		'Debug.Print(value.GetType().Name)
		If TypeOf value Is DateTime Then Return CDate(value)

		Dim d = ValToDblN(value)
		If d Is Nothing Then Return Nothing
		Return DateTime.FromOADate(ValToDbl(d))
	End Function
#End Region

	''' <summary>
	''' Проверить все импортируемые данные, сообщить о проблемах.
	''' </summary>
	Public Shared Sub ValidateDataSource()
		For Each item In DataSource
			ValidateDataSource(item)
		Next
	End Sub

	''' <summary>
	''' Проверить импортируемые данные Project, сообщить о проблемах.
	''' </summary>
	Private Shared Sub ValidateDataSource(item As UserAccount)
		If Not item.Compared Then Exit Sub ' Записи не сопоставлены.

		' Нет примечаний
		item.ValidateStatus = UserAccount.Status.Compared
		item.ValidateDescription = String.Empty
	End Sub
End Class
