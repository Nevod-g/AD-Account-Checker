Imports ProcessBank.Xpo
''' <summary>
''' Сервер - Контроллер домена.
''' </summary>
Public Class AdController
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "Alloys"
    Public Overloads Shared ReadOnly Property DboDescription As String = "Dictionary of Alloy."
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Directory
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = True
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = True
    Public Sub New() : End Sub

    Public Const DATA_FILE_NAME As String = "AdControllers.xml"

    Public Property Name As String

    Public Property Address As String
End Class
