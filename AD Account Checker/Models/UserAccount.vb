Imports ProcessBank.Xpo

Public Class UserAccount
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "NA"
    Public Overloads Shared ReadOnly Property DboDescription As String = "NA"
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Directory
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = True
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = False
    Public Sub New() : End Sub

	Public Property HrNumber As String
	Public Property HrName As String
	Public Property HrSurname As String
	Public Property HrFunction As String
	Public Property HrDateOfEntry As String
	Public Property HrDateOfTermination As String
	Public Property HrDepartment As String

	Public Property AdObject As AdObject

	Public Property ExcelFileName As String
	Public Property ExcelSheetName As String
	Public Property ExcelRowNumber As Integer

	Public Property CompareStatus As Status
	Public Property CompareDescription As String

	Enum Status
		''' <summary>
		''' Данные подготовлены к импорту. (Прозрачный)
		''' </summary>
		Compared = 1

		''' <summary>
		''' Ошибка чтения. (Серый)
		''' </summary>
		[Error] = -1

		''' <summary>
		''' Данные не сопоставлены с Directory Enries. (Красный)
		''' </summary>
		NotCompared = -2
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

	Public ReadOnly Property LastLogonDate As Date?
		Get
			Return AdObject?.LastLogonDate
		End Get
	End Property

#End Region
End Class

