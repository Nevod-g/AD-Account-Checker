Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools
Imports System.Runtime.CompilerServices
Imports System.Threading.Tasks

''' <summary>
''' Facade Message system.
''' Контейнер методов для вывода диалоговых сообщений.
''' </summary>
Public Class Message
	Public Shared Function Show(description As String,
								title As String,
								Optional ByVal buttons As MessageBoxButtons = MessageBoxButtons.OK,
								Optional ByVal icon As MessageBoxIcon = MessageBoxIcon.Error,
								<CallerMemberName> Optional caller As String = "") As DialogResult
		' Скрыть окна, которые могу перекрывать сообщение.
		frmWait.InvokeHide()

		' Вывести сообщение пользователю и в лог
		Log.Write(title & " " & description, $"{caller}.{NameOf(Message)}.{NameOf(Show)}")
		Return XtraMessageBox.Show(description, title, buttons, icon)
	End Function

	''' <summary>
	''' Построить представление выделенных строк.
	''' </summary>
	''' <param name="gv"></param>
	''' <param name="mode"></param>
	''' <returns></returns>
	Public Shared Function GetPreviewSelectedRows(gv As GridView, Optional ByVal mode As Byte = 0) As String
		Dim s As String = String.Empty
		For Each col As DevExpress.XtraGrid.Columns.GridColumn In gv.Columns
			Dim dataType = gv.GetRowCellValue(gv.FocusedRowHandle, col.FieldName)?.GetType()
			If Not IsNothing(gv.GetRowCellValue(gv.FocusedRowHandle, col.FieldName)) Then
				Try
					Select Case dataType
						Case GetType(Byte()), GetType(Image), GetType(Bitmap)
							' Пропустить представления значений для перечисленных типов данных
						Case Else
							If InStr(Mid(gv.GetRowCellValue(gv.FocusedRowHandle, col.FieldName).ToString, 1, 7), "{\rtf", CompareMethod.Text) = 0 Then 'пропустить RTF текст
								Select Case mode
									Case 0 ' Вывести таблицу
										If col.Visible Then ' Только видимые столбцы
											If s.Length > 0 Then s &= vbCrLf
											s &= CStr(IIf(col.Caption.Length > 0, col.Caption, col.FieldName)) & ": " & gv.GetRowCellValue(gv.FocusedRowHandle, col.FieldName).ToString
										End If

									Case 1 ' Вывести в одну строку
										If s.Length > 0 Then s &= ", "
										s &= "<" & gv.GetRowCellValue(gv.FocusedRowHandle, col.FieldName).ToString & ">"
								End Select

							End If
					End Select
				Catch ex As Exception
					Message.Show($"Type '{dataType.Name}'{vbCrLf}{vbCrLf}{Err.Description}",
								 $"Exception #{Err.Number} [{NameOf(GetPreviewSelectedRows)}]")
					Err.Clear()
				End Try
			End If
		Next
		If gv.SelectedRowsCount > 1 Then s &= $"{vbCrLf}{vbCrLf}And others total rows count: {gv.SelectedRowsCount}."
		Return s
	End Function

	''' <summary>
	''' Вывод сообщения об исключении.
	''' Предпринимается попытка отправить Лог приложения в DB.
	''' В сообщении скрываются значения: login, SId
	''' </summary>
	''' <param name="ex">Исключение</param>
	''' <param name="caller">Метод в котором произошло исключение.</param>
	''' <param name="description">Дополнительное описание ошибки.</param>
	''' <param name="buttons"></param>
	''' <param name="icon"></param>
	''' <returns>Dialog result.</returns>
	Public Shared Function ShowException(ex As Exception,
										 <CallerMemberName> Optional caller As String = "",
										 Optional ByVal description As String = "") As DialogResult
		Dim exceptionDtUtc = DateTime.UtcNow
		Dim hResult = ex?.HResult
		Dim stackTrace As String = Nothing
		Dim title = $"Exception: {ex?.GetType()?.Name}"

		If ex?.Source <> caller Then
			caller = $"{ex?.Source}.{caller}" ' Source - имя приложения или объекта, вызывавшего ошибку
		End If

		' Начать сборку пользовательского ссообщения и описания исключения.
		Dim msg = $"{ex?.Message}"
		If Not String.IsNullOrEmpty(description) Then description &= vbCrLf & vbCrLf

		' Вывести дополнительную детализацию для Администратора
		' Трассировку стека в диалоговом окне видит только администратор.
		' Пользовательские трассировки нужно смотреть в логе.
		' Логи сыпятся в базу, таблица [AppExceptions]
		stackTrace = $"Stack Trace:{vbCrLf}{ex?.StackTrace}{vbCrLf}{vbCrLf}"
		Try ' пытаться логировать
			Log.Write(Log.Level.Error, ex?.StackTrace, caller)
		Catch
			Beep() : Beep()
		End Try

		' Экземпляр класса Exception, который вызвал текущее исключение.
		Dim innerException = ex?.InnerException
		If innerException IsNot Nothing Then
			Dim msgIex = $"The Inner Exception:{vbCrLf}"
			msgIex &= $"Exception Name:{innerException.GetType().Name}{vbCrLf}"
			msgIex &= $"Message:{innerException.Message}{vbCrLf}"
			msgIex &= $"Stack Trace:{innerException.StackTrace}"

			stackTrace &= $"{vbCrLf}{vbCrLf}{msgIex}"
			Try ' пытаться логировать
				Log.Write(Log.Level.Error, msgIex, caller)
			Catch
				Beep() : Beep()
			End Try
		End If

		' Вывести в подвал дополнительные данные
		' Добавить скрипт запроса, если есть
		If Not String.IsNullOrWhiteSpace(description) Then
			description = $"Description: {Strings.Left(ValToStr(description), 1000)}{vbCrLf}"
			Try ' Пытаться логировать
				Log.Write(Log.Level.Error, description, caller)
			Catch
				Beep() : Beep()
			End Try
		End If

		'! Не использовать значения требующие обращения к DB!
		description &= $"Source: {caller} {Core.APP_VERSION_MARKER}{vbCrLf}" &
						$"Target Site Type: {ex?.TargetSite?.DeclaringType}{vbCrLf}" &
						$"Exception Type: {ex?.GetType().FullName}{vbCrLf}" &
						$"{stackTrace}"

		' Попытаться отправить лог в DB.
		Dim logFile As Byte() = Nothing
		Try
			logFile = Log.GetTempCopyLogFile()
		Finally
			Dim aex = New AppException(ex, exceptionDtUtc, 0, Core.APP_VERSION_MARKER, msg, logFile)
			Task.Run(Sub() SendAppLogToDb(aex))
		End Try

		' Попытаться сохранить копию лога в каталог лаборатории текущего пользователя (устарело/отключено).
		'Task.Run(Sub() Log.CopyLogFileTo(AppUser.AppsLab?.Share, DbUser.Name))

		' Вывести типовое сообщение об исключении
		Return frmException.ShowDialog(title, msg, description)
	End Function

	''' <summary>
	''' Попытаться отправить лог в DB.
	''' </summary>
	Private Shared Sub SendAppLogToDb(aex As AppException)
		'Try
		'	Dim logFile = aex.LogFile
		'	aex.LogFile = Nothing
		'	aex.Save() ' сохранить основные данные
		'	aex.LogFile = logFile
		'	aex.Save() ' пытаться передать лог целиком
		'Catch ex As Exception

		'End Try
	End Sub
End Class
