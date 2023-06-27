Imports System.DirectoryServices
Imports Adac.AdRepository
Imports DevExpress.Data.Helpers
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraSplashScreen
Imports ProcessBank.Agent.Tools

Public Class frmMain
    Private Const ACB_ADD_FOLDER_NAME = "acbAddDomainController"
    Private isAccordionControlAllElementExpanded As Boolean

    'Dim maxEntryCount As Integer = 1000
    Dim entryCount As Integer = 0

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Using SplashScreenManager.ShowOverlayForm(Me,,,, Color.LightGreen,,,,,,,, WaitAnimationType.Line,, True)
            ' Загрузить список контроллеров домена
            DbXmlWizard.LoadList(AdControllers, Core.APP_DIR_PATH, AdController.DATA_FILE_NAME)
            ' Создать клавиши
            For Each adController In AdControllers
                Dim aceAdController = AddAdController(adController)
                acAdEntries.Refresh()
                Application.DoEvents() ' Отобразить визуальный эффект
                ReloadAdControllerElements(aceAdController)
            Next
        End Using
    End Sub

    Private Sub ReloadAdControllerElements(aceAdController As AccordionControlElement)
        aceAdController.Elements.Clear()
        Dim adController = CType(aceAdController.Tag, AdController)
        Try
            BuildControllerTree(aceAdController, New DirectoryEntry("LDAP://" & adController.ServerAddress))
        Catch ex As Exception
            aceAdController.Hint = ex.Message
            aceAdController.Image = My.Resources.breakingchange_16x16
        End Try
    End Sub

    ''' <summary>
    ''' Добавить новую Клавишу-Сервер в акордеон.
    ''' </summary>
    ''' <param name="adController"></param>
    Public Function AddAdController(adController As AdController) As AccordionControlElement
        Dim ace As New AccordionControlElement(ElementStyle.Group) With {
            .Text = adController.Name,
            .Hint = adController.ServerAddress,
            .Image = My.Resources.servermode_16x16,
            .Tag = adController,
            .Expanded = True
        }
        ' Добавить контекстные кнопки
        Dim acbDelete As New AccordionContextButton()
        acbDelete.Name = "acbDelete"
        acbDelete.ToolTip = "Delete" : acbDelete.Tag = "Delete"
        acbDelete.AlignmentOptions.Panel = ContextItemPanel.Center
        acbDelete.AlignmentOptions.Position = ContextItemPosition.Far
        acbDelete.ImageOptionsCollection.ItemNormal.ImageUri = "icon%20builder/actions_deletecircled"
        acbDelete.ImageOptionsCollection.ItemNormal.SvgImageSize = New Size(16, 16)
        acbDelete.Visibility = ContextItemVisibility.Auto
        ace.ContextButtons.Add(acbDelete)

        Dim acbRefresh As New AccordionContextButton()
        acbRefresh.Name = "acbRefresh"
        acbRefresh.ToolTip = "Refresh" : acbRefresh.Tag = "Refresh"
        acbRefresh.AlignmentOptions.Panel = ContextItemPanel.Center
        acbRefresh.AlignmentOptions.Position = ContextItemPosition.Far
        acbRefresh.ImageOptionsCollection.ItemNormal.ImageUri = "icon%20builder/actions_refresh"
        acbRefresh.ImageOptionsCollection.ItemNormal.SvgImageSize = New Size(16, 16)
        acbRefresh.Visibility = ContextItemVisibility.Auto
        ace.ContextButtons.Add(acbRefresh)

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
        'If entryCount >= maxEntryCount Then Return
        Dim adObject As AdObject
        If TypeOf parentAce.Tag Is AdObject Then
            adObject = New AdObject(entry, CType(parentAce.Tag, AdObject))
        Else
            adObject = New AdObject(entry)
        End If

        Dim hint = $"Path: {adObject.Path}"
        hint &= vbCrLf & $"Class: {adObject.ObjectClass}"
        hint &= vbCrLf & $"GUID: {adObject.ObjectGUID}"

        Dim ace As New AccordionControlElement() With {
            .Text = adObject.Name,
            .Hint = hint,
            .Tag = adObject
        }
        RefreshAceView(ace)

        ' Добавить контекстные кнопки
        Dim acbCheck As New AccordionContextButton()
        acbCheck.Name = "acbCheck"
        acbCheck.ToolTip = "Check" : acbCheck.Tag = "Check"
        acbCheck.AlignmentOptions.Panel = ContextItemPanel.Center
        acbCheck.AlignmentOptions.Position = ContextItemPosition.Far
        acbCheck.ImageOptionsCollection.ItemNormal.ImageUri = "content/checkbox"
        acbCheck.ImageOptionsCollection.ItemNormal.SvgImageSize = New Size(16, 16)
        acbCheck.Visibility = ContextItemVisibility.Auto
        ace.ContextButtons.Add(acbCheck)

        parentAce.Elements.Add(ace) : entryCount += 1
    End Sub

    ''' <summary>
    ''' Контроллер контекстных кнопок аккордеона
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub acAdEntries_ContextButtonClick(sender As Object, e As ContextItemClickEventArgs) Handles acAdEntries.ContextButtonClick
        Select Case e.Item.Tag.ToString()
            Case "AddDomainController"
                Dim form As New frmAddAdController
                form.ShowDialog()

            Case "Delete"
                Dim ac = CType(sender, AccordionControl)
                Dim aceAdController = CType(e.DataItem, AccordionControlElement)
                Dim adController = CType(aceAdController.Tag, AdController)
                ac.Elements.Remove(aceAdController) ' Удалить клавишу
                aceAdController?.Dispose() ' Пинает отрисовку аккордеона
                AdControllers.Remove(adController)
                DbXmlWizard.SaveList(AdControllers, Core.APP_DIR_PATH, AdController.DATA_FILE_NAME, Now)

            Case "Refresh"
                Dim aceAdController = CType(e.DataItem, AccordionControlElement)
                Dim adController = CType(aceAdController.Tag, AdController)
                ReloadAdControllerElements(aceAdController)
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)

            Case "Check"
                Dim aceAdObject = CType(e.DataItem, AccordionControlElement)
                Dim adObject = CType(aceAdObject.Tag, AdObject)
                adObject.Checked = Not adObject.Checked
                RefreshAceView(aceAdObject)
        End Select
    End Sub

    Private Sub acAdEntries_ElementClick(sender As Object, e As ElementClickEventArgs) Handles acAdEntries.ElementClick

    End Sub

    Private Sub RefreshAceView(ace As AccordionControlElement)
        Dim adObject = CType(ace.Tag, AdObject)
        Dim entry = adObject.Entry

        If adObject.Checked Then
            ace.ImageUri = "actions/apply;Size16x16;Office2013"
        ElseIf entry.SchemaClassName <> "organizationalUnit" And entry.SchemaClassName <> "container" Then
            ace.Style = ElementStyle.Item
            ace.ImageUri = "miscellaneous/cube;Size16x16;GrayScaled"
        Else
            ace.Style = ElementStyle.Group
            ace.ImageUri = "actions/open;Size16x16;Office2013"
        End If
    End Sub

    Private Sub acAdEntries_ExpandStateChanging(sender As Object, e As ExpandStateChangingEventArgs) Handles acAdEntries.ExpandStateChanging
        ' Загрузить подчинённые элементы
        If e.Element.Expanded Then Exit Sub
        If e.Element.Elements.Count > 0 Then Exit Sub
        If TypeOf e.Element.Tag IsNot AdObject Then Exit Sub
        Dim ace = e.Element
        AddTopLevelAceElements(ace)
    End Sub

    Private Sub acAdEntries_FilterContent(sender As Object, e As FilterContentEventArgs) Handles acAdEntries.FilterContent
        ' Пост: Раскрывать каталоги однократно
        If isAccordionControlAllElementExpanded Then Exit Sub  ' False - Есть свёрнутые элементы в аккордеоне, поиск не будет полноценным
        If Not Me.Visible Then Exit Sub ' Пропустить загрузку формы
        isAccordionControlAllElementExpanded = True

        frmWait.ShowOnOwner(Me, "Reading Directory Entries...")
        acAdEntries.BeginUpdate()
        For Each aceAdController In aceDomainControllers.Elements
            AddAllAceElements(aceAdController)
        Next
        acAdEntries.EndUpdate()
        frmWait.InvokeHide()
    End Sub

    ''' <summary>
    ''' Добавить элементы первого уровня для родительской клавиши.
    ''' Triggered on expanding.
    ''' </summary>
    ''' <param name="aceParent"></param>
    Private Sub AddTopLevelAceElements(aceParent As AccordionControlElement)
        Dim adObject = CType(aceParent.Tag, AdObject)
        For Each children As DirectoryEntry In adObject.Entry.Children
            AddAdObject(aceParent, children)
        Next
    End Sub

    ''' <summary>
    ''' Добавить все элементы для родительской клавиши.
    ''' Triggered on finding elements.
    ''' </summary>
    ''' <param name="aceParent"></param>
    Private Sub AddAllAceElements(aceParent As AccordionControlElement)
        For Each ace In aceParent.Elements
            If ace.Expanded Then Continue For ' Только не раскрытые
            AddTopLevelAceElements(ace)
            frmWait.ShowOnOwner(Me, "Reading Directory Entries...", $"Number of Entries: {entryCount}")
            AddAllAceElements(ace)
        Next
    End Sub

End Class
