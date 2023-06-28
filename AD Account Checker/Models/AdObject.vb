Imports System.DirectoryServices
Imports ProcessBank.Xpo
Imports ProcessBank.Agent.Tools

Public Class AdObject
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "NA"
    Public Overloads Shared ReadOnly Property DboDescription As String = "NA"
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Directory
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = True
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = False
    Public Sub New() : End Sub

    Public Sub New(entry As DirectoryEntry, Optional parent As AdObject = Nothing)
        Me.Parent = parent
        Me.Entry = entry
        Me.Path = entry.Path
        Me.Name = IIf(Mid(ValToStr(entry.Name), 3, 1) = "=", Mid(entry.Name, 4), entry.Name).ToString()
        Me.ObjectClass = entry.SchemaClassName
        Me.ObjectGUID = entry.Guid.ToString()
        Me.IsUser = entry.SchemaClassName = "user"

        If parent Is Nothing Then
            AdRepository.AdObjects.Add(Me)
        Else
            parent.Children.Add(Me)
        End If
    End Sub

    Public Property Parent As AdObject
    Public Property Entry As DirectoryEntry
    Public ReadOnly Property Children As New List(Of AdObject)
    Public Property Path As String
    Public Property Name As String
    Public Property ObjectClass As String
    Public Property ObjectGUID As String

    ''' <summary>
    ''' Пользователь отметил эдемент птичкой.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsChecked As Boolean

    ''' <summary>
    ''' Элемент или его родитель отмечен птичкой.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IsParentOrMeChecked As Boolean
        Get
            ' Вернуть свойство первого елемента отмеченного птичкой.
            Dim target = Me
            Do While Not target.IsChecked And target.Parent IsNot Nothing
                target = target.Parent
            Loop
            Return target.IsChecked
        End Get
    End Property

    ''' <summary>
    ''' Дочерние Entry были загружены, созданы подчинённые клавиши в аккордеоне.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsChildLoaded As Boolean

    Public Property IsUser As Boolean

#Region "Properties"
    Public Property SamAccountName As String
    Public Property GivenName As String
    Public Property Surname As String
    Public Property Department As String
    Public Property Company As String
    Public Property EmployeeId As String
    Public Property Enabled As Boolean
    Public Property AccountExpirationDate As Date?
    Public Property DistinguishedName As String
    Public Property LastLogonDate As Date?
#End Region

    Private IsLoadPropertiesData As Boolean

    ''' <summary>
    ''' Загрузить из AD Entry значения свойств.
    ''' </summary>
    Public Sub LoadPropertiesData()
        If IsLoadPropertiesData Then Exit Sub
        SamAccountName = ValToStr(Entry.Properties(NameOf(SamAccountName)).Value)
        GivenName = ValToStr(Entry.Properties(NameOf(GivenName)).Value)
        Surname = ValToStr(Entry.Properties(NameOf(Surname)).Value)
        Department = ValToStr(Entry.Properties(NameOf(Department)).Value)
        Company = ValToStr(Entry.Properties(NameOf(Company)).Value)
        EmployeeId = ValToStr(Entry.Properties(NameOf(EmployeeId)).Value)
        Enabled = ValToBool(Entry.Properties(NameOf(Enabled)).Value)
        Dim accountExpirationDateValue = Entry.Properties(NameOf(AccountExpirationDate)).Value
        AccountExpirationDate = ValToDateN(Entry.Properties(NameOf(AccountExpirationDate)).Value)
        DistinguishedName = ValToStr(Entry.Properties(NameOf(DistinguishedName)).Value)
        LastLogonDate = ValToDateN(Entry.Properties(NameOf(LastLogonDate)).Value)
        IsLoadPropertiesData = True
        If AccountExpirationDate > New Date(2000, 1, 1) Or accountExpirationDateValue IsNot Nothing Then
            Beep()
        End If
    End Sub

    Public Function GetPropertyValue(propertyName As String) As String
        Return ValToStr(Entry.Properties(propertyName).Value)
    End Function

End Class
