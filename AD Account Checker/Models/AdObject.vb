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
        Me.Entry = entry
        Me.CanonicalName = entry.SchemaClassName
        Me.Path = entry.Path
        Me.Name = IIf(Mid(ValToStr(entry.Name), 3, 1) = "=", Mid(entry.Name, 4), entry.Name).ToString()
        Me.ObjectClass = entry.SchemaClassName
        Me.ObjectGUID = entry.Guid.ToString()
        If parent Is Nothing Then
            AdRepository.AdObjects.Add(Me)
        Else
            parent.Сhildren.Add(Me)
        End If
    End Sub

    Public Property Entry As DirectoryEntry
    Public ReadOnly Property Сhildren As New List(Of AdObject)

    Public Property CanonicalName As String

    Public Property Path As String

    Public Property Name As String

    Public Property ObjectClass As String

    Public Property ObjectGUID As String

    ''' <summary>
    ''' Пользователь отметил эдемент птичкой.
    ''' </summary>
    ''' <returns></returns>
    Public Property Checked As Boolean

End Class
