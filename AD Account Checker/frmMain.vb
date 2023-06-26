Imports System.DirectoryServices
Imports Adac.AdRepository
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraSplashScreen
Imports ProcessBank.Agent.Tools

Public Class frmMain
    Public Const ACB_ADD_FOLDER_NAME = "acbAddDomainController"
    Dim maxEntryCount As Integer = 1000
    Dim entryCount As Integer = 0

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Using SplashScreenManager.ShowOverlayForm(Me,,,, Color.LightGreen,,,,,,,, WaitAnimationType.Line,, True)
            ' Загрузить список контроллеров домена
            DbXmlWizard.LoadList(AdControllers, Core.APP_DIR_PATH, AdController.DATA_FILE_NAME)
            ' Создать клавиши
            For Each adController In AdControllers
                Dim ace = AddAdController(adController)
                Try
                    BuildControllerTree(ace, New DirectoryEntry("LDAP://" & adController.Address))
                Catch ex As Exception
                    ace.Hint = ex.Message
                    ace.Image = My.Resources.breakingchange_16x16
                End Try
            Next

        End Using
    End Sub

    ''' <summary>
    ''' Добавить новую Клавишу-Сервер в акордеон.
    ''' </summary>
    ''' <param name="adController"></param>
    Public Function AddAdController(adController As AdController) As AccordionControlElement
        Dim ace As New AccordionControlElement(ElementStyle.Group) With {
            .Text = adController.Name,
            .Hint = adController.Address,
            .Image = My.Resources.servermode_16x16,
            .Tag = adController
        }
        aceDomainControllers.Elements.Add(ace)
        Return ace
    End Function

    ''' <summary>
    ''' Построить дерево зависимостей елементов контроллера.
    ''' </summary>
    Private Sub BuildControllerTree(aceAdController As AccordionControlElement, entry As DirectoryEntry)
        For Each children As DirectoryEntry In entry.Children
            AddAdObject(aceAdController, children)
        Next
    End Sub

    ''' <summary>
    ''' Добавить новую Клавишу-Object в акордеон.
    ''' </summary>
    ''' <param name="parentAce"></param>
    ''' <param name="entry"></param>
    ''' <returns></returns>
    Public Sub AddAdObject(parentAce As AccordionControlElement, entry As DirectoryEntry)
        If entry.SchemaClassName <> "organizationalUnit" And entry.SchemaClassName <> "container" Then Return
        If entryCount >= maxEntryCount Then Return
        entryCount += 1
        Dim adObject = CreateAdObject(entry)

        Dim hint = $"Path: {adObject.Path}"
        hint &= vbCrLf & $"ObjectClass: {adObject.ObjectClass}"
        hint &= vbCrLf & $"ObjectGUID: {adObject.ObjectGUID}"

        Dim ace As New AccordionControlElement(ElementStyle.Item) With {
            .Text = adObject.Name,
            .Hint = hint,
            .Image = My.Resources.bofolder_16x16,
            .Tag = adObject
        }
        parentAce.Elements.Add(ace)
    End Sub

    Private Function CreateAdObject(entry As DirectoryEntry) As AdObject
        Dim adObject As New AdObject
        adObject.CanonicalName = entry.SchemaClassName
        adObject.Path = entry.Path
        adObject.Name = IIf(Mid(ValToStr(entry.Name), 3, 1) = "=", Mid(entry.Name, 4), entry.Name).ToString()
        adObject.ObjectClass = entry.SchemaClassName
        adObject.ObjectGUID = entry.Guid.ToString()
        Return adObject
    End Function

    Private Sub AccordionControl_ContextButtonClick(sender As Object, e As ContextItemClickEventArgs) Handles AccordionControl.ContextButtonClick
        Dim form As New frmAddAdController
        form.ShowDialog()
    End Sub
End Class
