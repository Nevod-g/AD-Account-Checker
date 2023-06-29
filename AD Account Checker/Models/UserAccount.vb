Imports ProcessBank.Xpo

''' <summary>
''' Результат сопоставления AdEntries и HR UserAccounts.
''' </summary>
Public Class UserAccount
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "NA"
    Public Overloads Shared ReadOnly Property DboDescription As String = "NA"
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Directory
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = True
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = False
    Public Sub New() : End Sub

	Public Property ImportNumber As String
	Public Property ImportGivenName As String
	Public Property ImportSurname As String
	Public Property ImportFunction As String
	Public Property ImportDateOfEntry As Date?
	Public Property ImportDateOfTermination As Date?
	Public Property ImportDepartment As String

	Public Property AdObject As AdObject

	Public Property ExcelFileName As String
	Public Property ExcelSheetName As String
	Public Property ExcelRowNumber As Integer

	Public ReadOnly Property Compared As Boolean
		Get
			Return AdObject IsNot Nothing
		End Get
	End Property

	Public Property ValidateDescription As String
	Public Property ValidateStatus As Status

	Enum Status
		''' <summary>
		''' Данные подготовлены к импорту. (Прозрачный)
		''' </summary>
		Compared = 1

		''' <summary>
		''' Ошибка чтения. (Серый)
		''' </summary>
		[Error] = -1

	End Enum

#Region "Enrty Properties"
	Public ReadOnly Property SamAccountName As String
		Get
			Return AdObject?.SamAccountName
		End Get
	End Property

	Public ReadOnly Property GivenName As String
		Get
			Return AdObject?.GivenName
		End Get
	End Property

	Public ReadOnly Property Surname As String
		Get
			Return AdObject?.Surname
		End Get
	End Property

	Public ReadOnly Property Department As String
		Get
			Return AdObject?.Department
		End Get
	End Property

	Public ReadOnly Property Title As String
		Get
			Return AdObject?.Title
		End Get
	End Property

	Public ReadOnly Property Company As String
		Get
			Return AdObject?.Company
		End Get
	End Property

	Public ReadOnly Property EmployeeId As String
		Get
			Return AdObject?.EmployeeId
		End Get
	End Property

	Public ReadOnly Property Enabled As Boolean?
		Get
			Return AdObject?.Enabled
		End Get
	End Property

	Public ReadOnly Property AccountExpirationDate As Date?
		Get
			Return AdObject?.AccountExpirationDate
		End Get
	End Property

	Public ReadOnly Property DistinguishedName As String
		Get
			Return AdObject?.DistinguishedName
		End Get
	End Property

	Public ReadOnly Property LastLogon As Date?
		Get
			Return AdObject?.LastLogon
		End Get
	End Property

#End Region
End Class

