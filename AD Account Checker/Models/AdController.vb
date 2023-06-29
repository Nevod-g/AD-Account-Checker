Imports System.DirectoryServices
Imports ProcessBank.Xpo

''' <summary>
''' Сервер - Контроллер домена.
''' </summary>
Public Class AdController
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "NA"
    Public Overloads Shared ReadOnly Property DboDescription As String = "NA"
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Directory
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = True
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = False
    Public Sub New() : End Sub

    Public Const DATA_FILE_NAME As String = "AdControllers.xml"
    Public ReadOnly Property Сhildren As New List(Of AdObject)

    Public Property Name As String

    Public Property ServerAddress As String

    Public Property RootEntry As DirectoryEntry

End Class
