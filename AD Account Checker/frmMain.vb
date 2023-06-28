Imports System.DirectoryServices
Imports Adac.AdRepository
Imports DevExpress.Data.Helpers
Imports DevExpress.Utils
Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraSplashScreen
Imports ProcessBank.Agent.Tools
Imports ProcessBank.Agent
Imports System.Threading.Tasks
Imports DevExpress.XtraGrid

Public Class frmMain
    Private Const ACB_ADD_FOLDER_NAME = "acbAddDomainController"

    'Dim maxEntryCount As Integer = 1000
    Dim entryCount As Integer = 0

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Применить корпоративный стиль
        Dx.SetDefaultSettingsForGridView(gvUserAccounts, True, False)
    End Sub

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
            Dim domain = New DirectoryEntry("LDAP://" & adController.ServerAddress)
            BuildControllerTree(aceAdController, domain)
        Catch ex As Exception
            aceAdController.Hint = ex.Message
            aceAdController.Image = My.Resources.breakingchange_16x16
        End Try
    End Sub

    ''' <summary>
    ''' Построить дерево зависимостей елементов контроллера.
    ''' </summary>
    Private Sub BuildControllerTree(aceAdController As AccordionControlElement, entry As DirectoryEntry)
        For Each children As DirectoryEntry In entry.Children
            AddAdObject(aceAdController, children)
        Next
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

        ' Определить наличие дочерних элементов и стиль клавиши.
        ace.Style = ElementStyle.Item
        For Each children In entry.Children
            ace.Style = ElementStyle.Group
            Exit For
        Next

        ' Добавить контекстные кнопки
        Dim acbInfo As New AccordionContextButton()
        acbInfo.Name = "acbInfo"
        acbInfo.ToolTip = "Info" : acbInfo.Tag = "Info"
        acbInfo.AlignmentOptions.Panel = ContextItemPanel.Center
        acbInfo.AlignmentOptions.Position = ContextItemPosition.Far
        acbInfo.ImageOptionsCollection.ItemNormal.ImageUri = "outlook%20inspired/about"
        acbInfo.ImageOptionsCollection.ItemNormal.SvgImageSize = New Size(16, 16)
        acbInfo.Visibility = ContextItemVisibility.Auto
        ace.ContextButtons.Add(acbInfo)

        ' Добавить контекстные кнопки
        Dim acbCheck As New AccordionContextButton()
        acbCheck.Name = "acbCheck"
        acbCheck.ToolTip = "Check Entry and load structure data." : acbCheck.Tag = "Check"
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
                If AdControllers.Count = 0 Then
                    FileSystem.DeleteFile(FileSystem.Path.Combine(Core.APP_DIR_PATH, AdController.DATA_FILE_NAME))
                Else
                    DbXmlWizard.SaveList(AdControllers, Core.APP_DIR_PATH, AdController.DATA_FILE_NAME, Now)
                End If

            Case "Refresh"
                Dim aceAdController = CType(e.DataItem, AccordionControlElement)
                Dim adController = CType(aceAdController.Tag, AdController)
                ReloadAdControllerElements(aceAdController)
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)

            Case "Check"
                Dim aceAdObject = CType(e.DataItem, AccordionControlElement)
                Dim adObject = CType(aceAdObject.Tag, AdObject)
                adObject.IsChecked = Not adObject.IsChecked
                RefreshAceView(aceAdObject)
                LoadAllCheckedAceElements()
                acAdEntries.Refresh()

                ' Дбавить или удалить из репозитория отмеченных элементов
                If adObject.IsChecked Then
                    CheckedAdObjects.Add(adObject)
                Else
                    CheckedAdObjects.Remove(adObject)
                End If

            Case "Info"
                Dim aceAdObject = CType(e.DataItem, AccordionControlElement)
                If TypeOf aceAdObject.Tag IsNot AdObject Then Exit Sub
                Dim adObject = CType(aceAdObject.Tag, AdObject)
                adObject.LoadPropertiesData()
                frmAdObject.ShowDialog(adObject)
        End Select
    End Sub

    ''' <summary>
    ''' Обновить представление клавиши по её состоянию.
    ''' </summary>
    ''' <param name="ace"></param>
    Private Sub RefreshAceView(ace As AccordionControlElement)
        Dim adObject = CType(ace.Tag, AdObject)
        Dim entry = adObject.Entry

        ' Определить иконки елементов.
        If adObject.IsChecked Then
            ace.ImageUri = "actions/apply;Size16x16;Office2013"
        ElseIf adObject.IsUser Then
            ace.ImageUri = "people/employee;Size16x16;Office2013"
        ElseIf adObject.ObjectClass = "group" Then
            ace.ImageUri = "people/team;Size16x16;Office2013"
        ElseIf adObject.ObjectClass = "builtinDomain" Then
            ace.ImageUri = "business%20objects/bolocalization;Size16x16;Colored"
        ElseIf adObject.ObjectClass = "computer" Then
            ace.ImageUri = "icon%20builder/electronics_laptopwindows;Size16x16"
            ace.ImageOptions.SvgImageSize = New Size(16, 16)
        ElseIf entry.SchemaClassName <> "organizationalUnit" And entry.SchemaClassName <> "container" Then
            ace.ImageUri = "miscellaneous/cube;Size16x16;GrayScaled"
        Else
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

    ''' <summary>
    ''' Добавить элементы первого уровня для родительской клавиши.
    ''' Triggered on expanding.
    ''' </summary>
    ''' <param name="aceParent"></param>
    Private Sub AddTopLevelAceElements(aceParent As AccordionControlElement)
        Dim adObject = CType(aceParent.Tag, AdObject)
        If adObject.IsChildLoaded Then Exit Sub ' Пропустить загрузку первого уровны, если дети были загружены ранее
        For Each children As DirectoryEntry In adObject.Entry.Children
            AddAdObject(aceParent, children)
        Next
        aceParent.Text = $"{adObject.Name} [{adObject.Children?.Count}]"
        adObject.IsChildLoaded = True
    End Sub

    Private Sub acAdEntries_FilterContent(sender As Object, e As FilterContentEventArgs) Handles acAdEntries.FilterContent
        ' Поиск выполняется по отмеченным позициям или по подгруженным на данный момент.
        If Not Me.Visible Then Exit Sub ' Пропустить при загрузке формы

    End Sub

    Private Sub LoadAllCheckedAceElements()
        frmWait.ShowOnOwner(Me, "Reading Directory Entries...")
        acAdEntries.BeginUpdate()
        For Each aceAdController In aceDomainControllers.Elements
            AddAllAceElements(aceAdController)
        Next
        acAdEntries.EndUpdate()
        frmWait.InvokeHide()
    End Sub

    ''' <summary>
    ''' Добавить все элементы для родительской клавиши на глубину всей структуры.
    ''' Подгружаются лишь отмеченные позициии или подгруженные ранее.
    ''' Triggered on finding elements.
    ''' </summary>
    ''' <param name="aceParent"></param>
    Private Sub AddAllAceElements(aceParent As AccordionControlElement)
        For Each ace In aceParent.Elements
            Dim adObject = CType(ace.Tag, AdObject)
            If adObject.IsParentOrMeChecked Then
                AddTopLevelAceElements(ace)
                frmWait.SetDescription($"Number of Loaded Entries: {entryCount}")
            End If

            AddAllAceElements(ace) ' Рекурсивно передбрать всю глубину вложенности.
        Next
    End Sub

    Private Async Sub bbiImportExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiImportExcel.ItemClick
        ' Отобразить диалоговое окно выбора файла
        Dim ofd = Dx.OpenFileDialog
        ofd.Title = "Choose Excel Files"
        ofd.FileName = String.Empty
        ofd.Multiselect = True
        ofd.Filter = $"Excel|*.XLSX;*.XLSM"
        If ofd.ShowDialog() = DialogResult.OK Then
            Using SplashScreenManager.ShowOverlayForm(gcUserAccounts,,,,,,,,,,,, WaitAnimationType.Line,, True)
                ' Прочитать данные из файла Excel
                Dim parser As New ExcelActiveUsersParser
                For Each filePath In ofd.FileNames
                    parser.ReadExcelFile(filePath)
                Next

                Await Task.Run(Sub() AddAdEntriesData(ExcelActiveUsersParser.DataSource))
                parser.ValidateDataSource()

                ' Подключить данные и обновить View
                gcUserAccounts.DataSource = New BindingSource(ExcelActiveUsersParser.DataSource, String.Empty)
                gcUserAccounts.RefreshDataSource()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Добавить данные существующих AdEntries.
    ''' </summary>
    Private Sub AddAdEntriesData(dataSource As List(Of UserAccount))
        ' Загрузить данные выделенных Entries на всю глубину вложенности.
        Dim deepCheckedAdObjects = GetAllCheckedAdObjects()

        ' Загрузить из AD Entry значения свойств Users.
        For Each adObject In deepCheckedAdObjects.Where(Function(o) o.IsUser)
            adObject.LoadPropertiesData()
        Next

        ' Сопоставить записи
        Dim adUsers = deepCheckedAdObjects.Where(Function(u) ValToBool(u.IsUser))
        For Each userAccount In dataSource
            userAccount.AdObject = adUsers.FirstOrDefault(Function(p) p.EmployeeId = userAccount.HrNumber)
        Next
    End Sub

    ''' <summary>
    ''' Получить список отмеченных записей на всю глубину вложенности.
    ''' </summary>
    Private Function GetAllCheckedAdObjects() As List(Of AdObject)
        Dim deepCheckedAdObjects As New List(Of AdObject)
        For Each adObject In CheckedAdObjects
            deepCheckedAdObjects.Add(adObject)
            GetAllCheckedAdObjectChildren(deepCheckedAdObjects, adObject)
        Next
        Return deepCheckedAdObjects
    End Function

    ''' <summary>
    ''' Рекурсивно добавить детей записи.
    ''' </summary>
    ''' <param name="deepCheckedAdObjects"></param>
    ''' <param name="adObject"></param>
    Private Sub GetAllCheckedAdObjectChildren(deepCheckedAdObjects As List(Of AdObject), adObject As AdObject)
        For Each adObj In adObject.Children
            deepCheckedAdObjects.Add(adObj)
            GetAllCheckedAdObjectChildren(deepCheckedAdObjects, adObj)
        Next
    End Sub

#Region "Context Menu on Row"

    Private Sub gcUserAccounts_MouseUp(sender As Object, e As MouseEventArgs) Handles gcUserAccounts.MouseUp
        If e.Button = MouseButtons.Right Then
            cmsOnRow.Show(CType(sender, GridControl), e.Location)
        End If
    End Sub

    Private Sub cmsOnRow_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsOnRow.Opening
        Dim rowHandle = gvUserAccounts.FocusedRowHandle
        Dim item = TryCast(gvUserAccounts.GetRow(rowHandle), UserAccount)
        tsmiDeleteRows.Visible = item IsNot Nothing
    End Sub

    Private Sub tsmiDeleteRows_Click(sender As Object, e As EventArgs) Handles tsmiDeleteRows.Click
        ' Пост
        Dim rowHandle = gvUserAccounts.FocusedRowHandle
        If rowHandle = GridControl.AutoFilterRowHandle Then Exit Sub
        If rowHandle = GridControl.InvalidRowHandle Then Exit Sub
        If rowHandle = GridControl.NewItemRowHandle Then Exit Sub

        Dim userAccount = TryCast(gvUserAccounts.GetRow(rowHandle), UserAccount)
        If userAccount Is Nothing Then
            Message.Show("Object is not defined!", "Delete Object", , MessageBoxIcon.Stop)
            Exit Sub
        End If

        gvUserAccounts.BeginDataUpdate()
        ExcelActiveUsersParser.DataSource.Remove(userAccount)
        gvUserAccounts.EndDataUpdate()
    End Sub

    Private Sub tsmiClearAll_Click(sender As Object, e As EventArgs) Handles tsmiClearAll.Click
        ExcelActiveUsersParser.DataSource.Clear()
        gcUserAccounts.RefreshDataSource()
    End Sub

    Private Sub tsmiEntryInformation_Click(sender As Object, e As EventArgs) Handles tsmiEntryInformation.Click
        gcUserAccounts_MouseDoubleClick(gcUserAccounts, Nothing)
    End Sub
#End Region

    Private Sub gcUserAccounts_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles gcUserAccounts.MouseDoubleClick
        Dim rowHandle = gvUserAccounts.FocusedRowHandle
        If rowHandle = GridControl.AutoFilterRowHandle Then Exit Sub
        If rowHandle = GridControl.InvalidRowHandle Then Exit Sub
        If rowHandle = GridControl.NewItemRowHandle Then Exit Sub

        Dim userAccount = TryCast(gvUserAccounts.GetRow(rowHandle), UserAccount)
        If userAccount.AdObject IsNot Nothing Then
            frmAdObject.ShowDialog(userAccount.AdObject)
        End If
    End Sub
End Class
