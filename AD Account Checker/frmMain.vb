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
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.Data

Public Class frmMain
    Private Const ACB_ADD_FOLDER_NAME = "acbAddDomainController"

    'Dim maxEntryCount As Integer = 1000
    Dim entryCount As Integer = 0

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Применить корпоративный стиль
        Dx.SetDefaultSettingsForGridView(gvUserAccounts, True, False)
        Dx.AddFormatRules(gvUserAccounts)

        ' Добавить агрегацию в надпись Группировки
        gvUserAccounts.GroupSummary.Add(New GridGroupSummaryItem() With {
          .FieldName = NameOf(UserAccount.ExcelRowNumber),
          .SummaryType = SummaryItemType.Count,
          .ShowInGroupColumnFooter = Nothing})

        ' Определить предназначение конролов
        gvUserAccounts.Tag = UserAccount.DboCodeName
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Using SplashScreenManager.ShowOverlayForm(Me,,,, Color.LightGreen,,,,,,,, WaitAnimationType.Line,, True)
            ' Загрузить список контроллеров домена
            DbXmlWizard.LoadList(AdControllers, Core.APP_DIR_PATH, AdController.DATA_FILE_NAME)
            ' Создать клавиши
            For Each adController In AdControllers
                Dim aceAdController = AddAceAdController(adController)
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
            adController.RootEntry = New DirectoryEntry("LDAP://" & adController.ServerAddress)
            adController.RootEntry.AuthenticationType = AuthenticationTypes.Secure
            BuildControllerTree(aceAdController)
        Catch ex As Exception
            aceAdController.Hint = ex.Message
            aceAdController.Image = My.Resources.breakingchange_16x16
        End Try
    End Sub

    ''' <summary>
    ''' Построить дерево зависимостей елементов контроллера.
    ''' </summary>
    Private Sub BuildControllerTree(aceAdController As AccordionControlElement)
        Dim adController = CType(aceAdController.Tag, AdController)
        For Each children As DirectoryEntry In adController.RootEntry.Children
            Dim adObject = AddAdObject(aceAdController, children)
            adObject.RootEntry = adController.RootEntry
        Next
    End Sub

    ''' <summary>
    ''' Добавить новую Клавишу-Сервер в акордеон.
    ''' </summary>
    ''' <param name="adController"></param>
    Public Function AddAceAdController(adController As AdController) As AccordionControlElement
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
    Public Function AddAdObject(parentAce As AccordionControlElement, entry As DirectoryEntry) As AdObject
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
        For Each children In entry.Children ' Есои есть хотябы один елемент.
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
        acbCheck.ToolTip = "Check/uncheck Entry and load Structure data." : acbCheck.Tag = "Check"
        acbCheck.AlignmentOptions.Panel = ContextItemPanel.Center
        acbCheck.AlignmentOptions.Position = ContextItemPosition.Far
        acbCheck.ImageOptionsCollection.ItemNormal.ImageUri = "content/checkbox"
        acbCheck.ImageOptionsCollection.ItemNormal.SvgImageSize = New Size(16, 16)
        acbCheck.Visibility = ContextItemVisibility.Auto
        ace.ContextButtons.Add(acbCheck)

        parentAce.Elements.Add(ace) : entryCount += 1
        Return adObject
    End Function

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
            ace.ImageUri = "content/checkbox"
        ElseIf adObject.IsUser Then
            ace.ImageUri = "people/employee;Size16x16;Office2013"
        ElseIf adObject.ObjectClass = "group" Then
            ace.ImageUri = "people/team;Size16x16;Office2013"
        ElseIf adObject.ObjectClass = "contact" Then
            ace.ImageUri = "business%20objects/bo_contact"
        ElseIf adObject.ObjectClass = "builtinDomain" Then
            ace.ImageUri = "business%20objects/bolocalization;Size16x16;Colored"
        ElseIf adObject.ObjectClass = "computer" Then
            ace.ImageUri = "icon%20builder/electronics_laptopwindows;Size16x16"
        ElseIf entry.SchemaClassName <> "organizationalUnit" And entry.SchemaClassName <> "container" Then
            ace.ImageUri = "miscellaneous/cube;Size16x16;GrayScaled"
        Else
            ace.ImageUri = "actions/open;Size16x16;Office2013"
        End If
        ace.ImageOptions.SvgImageSize = New Size(16, 16)
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
            Application.DoEvents()
        Next
        If aceParent.Style = ElementStyle.Group Then
            aceParent.Text = $"{adObject.Name} [{adObject.Children?.Count}]"
        End If

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

    Private Sub bbiImportExcel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiImportExcel.ItemClick
        ' Отобразить диалоговое окно выбора файла
        Dim ofd = Dx.OpenFileDialog
        ofd.Title = "Choose Excel Files"
        ofd.FileName = String.Empty
        ofd.Multiselect = True
        ofd.Filter = $"Excel|*.XLSX;*.XLSM"
        If ofd.ShowDialog() = DialogResult.OK Then
            Using SplashScreenManager.ShowOverlayForm(gcUserAccounts,,,,,,,,,,,, WaitAnimationType.Line,, True)
                ' Прочитать данные из файла Excel
                For Each filePath In ofd.FileNames
                    ExcelActiveUsersParser.ReadExcelFile(filePath)
                Next
            End Using

            AddAdEntriesData() ' Добавить данные существующих AdEntries и сопоставить с HR UserAccounts.
        End If
    End Sub

    Private Sub bbiCompareAccounts_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiCompareAccounts.ItemClick
        AddAdEntriesData() ' Добавить данные существующих AdEntries и сопоставить с HR UserAccounts.
    End Sub

    ''' <summary>
    ''' Добавить данные существующих AdEntries и сопоставить с HR UserAccounts.
    ''' </summary>
    Private Async Sub AddAdEntriesData()
        Using SplashScreenManager.ShowOverlayForm(gcUserAccounts,,,,,,,,,,,, WaitAnimationType.Line,, True)
            Dim dataSource = ExcelActiveUsersParser.DataSource
            ' Загрузить данные выделенных Entries на всю глубину вложенности.
            Dim deepCheckedAdObjects = GetAllCheckedAdObjects()

            ' Загрузить из AD Entry значения свойств Users.
            For Each adObject In deepCheckedAdObjects.Where(Function(o) o.IsUser)
                Await Task.Run(AddressOf adObject.LoadPropertiesData)
            Next

            ' Сопоставить записи, которые не были сопоставлены ранее.
            gcUserAccounts.BeginUpdate()
            Dim adUsers = deepCheckedAdObjects.Where(Function(o) ValToBool(o.IsUser))
            For Each userAccount In dataSource.Where(Function(u) ValToBool(u.AdObject Is Nothing))
                userAccount.AdObject = adUsers.FirstOrDefault(Function(p) p.EmployeeId = userAccount.ImportNumber)
            Next

            ExcelActiveUsersParser.ValidateDataSource()

            ' Подключить данные и обновить View
            gcUserAccounts.DataSource = New BindingSource(dataSource, String.Empty)
            gcUserAccounts.EndUpdate()
            'gcUserAccounts.RefreshDataSource()
        End Using
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
        Dim hi = gvUserAccounts.CalcHitInfo(e.Location)
        If hi.RowHandle = GridControl.AutoFilterRowHandle Then Exit Sub
        If hi.RowHandle = GridControl.InvalidRowHandle Then Exit Sub
        If hi.RowHandle = GridControl.NewItemRowHandle Then Exit Sub
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

    Private Sub acAdEntries_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles acAdEntries.MouseDoubleClick

        Dim hi As AccordionControlHitInfo = acAdEntries.CalcHitInfo(e.Location)
        Dim aceAdObject = CType(hi.ItemInfo.Element, AccordionControlElement)
        If aceAdObject Is Nothing Then Exit Sub
        If TypeOf aceAdObject.Tag IsNot AdObject Then Exit Sub
        Dim adObject = CType(aceAdObject.Tag, AdObject)

        adObject.LoadPropertiesData()
        frmAdObject.ShowDialog(adObject)
    End Sub

    ''' <summary>
    ''' Рисовать иконку на индикаторе строки.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub gvUserAccounts_CustomDrawRowIndicator(sender As Object, e As RowIndicatorCustomDrawEventArgs) Handles gvUserAccounts.CustomDrawRowIndicator
        e.DefaultDraw()

        If e.RowHandle < 0 Then Exit Sub
        If e.RowHandle = GridControl.AutoFilterRowHandle Then Exit Sub
        If e.RowHandle = GridControl.InvalidRowHandle Then Exit Sub
        If e.RowHandle = GridControl.NewItemRowHandle Then Exit Sub

        Dim userAccount = TryCast(gvUserAccounts.GetRow(e.RowHandle), UserAccount)
        Dim centerByHeight = CInt(e.Info.Bounds.Height / 2)
        Dim rect As New Rectangle(e.Info.Bounds.Location.X + 4, e.Info.Bounds.Location.Y + centerByHeight - 8, 16, 16)

        Dim notComparedImage = My.Resources.Resources.updownbarsnone_16x16
        Dim enableUndefinedImage = My.Resources.Resources.question_16x16
        Dim enableImage = My.Resources.Resources.apply_16x16
        Dim disableImage = My.Resources.Resources.cancel_16x16

        If userAccount.AdObject Is Nothing Then
            e.Cache.DrawImage(notComparedImage, rect)
        ElseIf userAccount.Enabled Is Nothing Then
            e.Cache.DrawImage(enableUndefinedImage, rect)
        ElseIf userAccount.Enabled Then
            e.Cache.DrawImage(enableImage, rect)
        Else
            e.Cache.DrawImage(disableImage, rect)
        End If
    End Sub

    Private Sub gvUserAccounts_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles gvUserAccounts.CustomDrawCell
        e.DefaultDraw()
        Dim gv = CType(sender, GridView)
        ' Рисовать Акцент - рамку вокруг ячейки, имеющей новое значение.
        Dim needDrawEmphasis As Boolean = False
        Select Case e.Column.FieldName
            Case NameOf(UserAccount.GivenName)
                Dim userAccount = TryCast(gv.GetRow(e.RowHandle), UserAccount)
                needDrawEmphasis = userAccount.GivenName <> userAccount.ImportGivenName And
                    userAccount.GivenName IsNot Nothing And
                    userAccount.ImportGivenName IsNot Nothing

            Case NameOf(UserAccount.Surname)
                Dim userAccount = TryCast(gv.GetRow(e.RowHandle), UserAccount)
                needDrawEmphasis = userAccount.Surname <> userAccount.ImportSurname And
                    userAccount.Surname IsNot Nothing And
                    userAccount.ImportSurname IsNot Nothing

            Case NameOf(UserAccount.Title)
                Dim userAccount = TryCast(gv.GetRow(e.RowHandle), UserAccount)
                needDrawEmphasis = userAccount.Title <> userAccount.ImportFunction And
                    userAccount.Title IsNot Nothing And
                    userAccount.ImportFunction IsNot Nothing

            Case NameOf(UserAccount.Department)
                Dim userAccount = TryCast(gv.GetRow(e.RowHandle), UserAccount)
                needDrawEmphasis = userAccount.Department <> userAccount.ImportDepartment And
                    userAccount.Department IsNot Nothing And
                    userAccount.ImportDepartment IsNot Nothing
        End Select

        If needDrawEmphasis Then
            ' Настройки рисования
            e.Cache.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            e.Cache.Graphics.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
            e.Cache.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

            e.Cache.DrawRectangle(e.Bounds, Color.PaleVioletRed, 1)
        End If
    End Sub
End Class
