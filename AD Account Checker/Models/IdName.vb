Imports ProcessBank.Xpo

''' <summary>
''' Примитивная структура.
''' </summary>
Public Class IdName
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "NA"
    Public Overloads Shared ReadOnly Property DboDescription As String = "Typical generic model for supporting primitive structure objects."
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Abstract
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = False
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = False
    Public Sub New() : End Sub

    Public Sub New(id As Integer, name As String)
        Me.Id = id
        Me.Name = name
    End Sub

    Public Property Name As String
End Class
