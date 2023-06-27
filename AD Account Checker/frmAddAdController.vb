Imports Adac.AdRepository

Public Class frmAddAdController
    Private AdController As AdController

    Public Overloads Sub ShowDialog(Optional adController As AdController = Nothing)
        If adController Is Nothing Then
            Me.AdController = New AdController With {
                .Id = AdControllers.Count + 1
            }
        Else
            Me.AdController = adController
        End If

        DbEditorController.Binding(Me.Controls, Me.AdController)

        MyBase.ShowDialog()
    End Sub

    Private Sub sbAdd_Click(sender As Object, e As EventArgs) Handles sbAdd.Click
        AdControllers.Add(AdController)
        DbXmlWizard.SaveList(AdControllers, Core.APP_DIR_PATH, AdController.DATA_FILE_NAME, Now)
        frmMain.AddAdController(AdController) ' Добавить новую Клавишу-Сервер в акордеон
        Me.Close()
    End Sub
End Class