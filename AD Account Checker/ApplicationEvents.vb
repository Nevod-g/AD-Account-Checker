Imports Microsoft.VisualBasic.ApplicationServices
Imports ProcessBank

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApp_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            'Randomize() ' 70-200 ms, включать по месту использования!
            Agent.Log.InitializeLog(True, "English",
                                    Core.APP_VERSION_MARKER, My.Application.Info.ProductName) ' 108 ms
        End Sub

		Private Sub MyApp_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
			Dim dialogResult = Message.ShowException(e.Exception, e.Exception.Source)

			' Нужно ли завершить работу приложения?
			Select Case dialogResult
				Case DialogResult.Yes, DialogResult.OK, DialogResult.Retry, DialogResult.Ignore, DialogResult.Cancel
					e.ExitApplication = False
				Case Else
					e.ExitApplication = True
			End Select
		End Sub

		Private Sub MyApp_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
			Agent.Log.Dispose()
		End Sub
	End Class
End Namespace
