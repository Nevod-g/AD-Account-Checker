Imports ProcessBank.Xpo

''' <summary>
''' Регистр учёта возникновения Исключений в приложении.
''' </summary>
Public Class AppException
    Inherits DbMetaObject
    Public Overloads Shared ReadOnly Property DboCodeName As String = "AppExceptions"
    Public Overloads Shared ReadOnly Property DboDescription As String = "Register for the occurrence of Exceptions in the Client Application."
    Public Overloads Shared ReadOnly Property DboType As Db.ObjectType = Db.ObjectType.Register
    Public Overloads Shared ReadOnly Property HasInLineEditor As Boolean = False
    Public Overloads Shared ReadOnly Property HasLocalCache As Boolean = False

    Public Sub New() : End Sub

    Public Sub New(ex As Exception, exceptionDtUtc As DateTime, sessionId As Integer?, appVersion As String,
                   description As String, logFile As Byte())
        Me.ExceptionDtUtc = exceptionDtUtc
        Me.SessionId = sessionId
        Me.AppVersion = appVersion
        Me.HResult = ex?.HResult
        Me.Message = Strings.Left(ex?.Message, 4000)
        Me.StackTrace = Strings.Left(ex?.StackTrace?.Trim(), 4000)
        Me.Description = Strings.Left(description, 8000)
        Me.LogFile = logFile
    End Sub

#Region "Persistent fields"
    Public Property [ExceptionDtUtc] As DateTime
    Public Property [UserId] As Integer?
    Public Property [SessionId] As Integer?
    Public Property [AppVersion] As String
    Public Property [HResult] As Integer?
    Public Property [Message] As String
    Public Property [StackTrace] As String
    Public Property [Description] As String
    Public Property LogFile As Byte()
#End Region

End Class