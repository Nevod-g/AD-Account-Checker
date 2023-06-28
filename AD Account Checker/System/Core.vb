Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools

''' <summary>
''' Системные величины окружения: статусы, измерение состояния.
''' Отслеживает состояние системы, реализует глобальную логику.
''' </summary>
Public NotInheritable Class Core
    ''' <summary>
    ''' Счетчик релизов (минорная версия приложения), определяет необходимость обновления системы, указывает на количество релизов.
    ''' </summary>
    Public Shared ReadOnly RELEASE_NUMBER As Integer = My.Application.Info.Version.Minor

    ''' <summary>
    ''' Макеровка версии приложения
    ''' </summary>
    Public Shared ReadOnly APP_VERSION_MARKER As String = $"v.{My.Application.Info.Version.Major}.{RELEASE_NUMBER}"

#Region "Global Paths"
    Public Shared ReadOnly APP_DIR_PATH As String = FileSystem.APP_DIR_PATH
#End Region

End Class
