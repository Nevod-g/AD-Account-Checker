<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AccordionContextButton1 As DevExpress.XtraBars.Navigation.AccordionContextButton = New DevExpress.XtraBars.Navigation.AccordionContextButton()
        Me.FluentDesignFormContainer1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer()
        Me.gcUserAccounts = New DevExpress.XtraGrid.GridControl()
        Me.cmsOnRow = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmiEntryInformation = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmiDeleteRows = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmiClearAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTipController = New DevExpress.Utils.ToolTipController(Me.components)
        Me.gvUserAccounts = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
        Me.gbId = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.bgcHrNumber = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcEmployeeId = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcEnabled = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gbName = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.bgcHrName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcGivenName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gbSurname = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.bgcHrSurname = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcSurname = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gbDepartment = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.bgcHrDepartment = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcDepartment = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gbOther = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.bgcHrFunction = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcHrDateOfEntry = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcCompany = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcHrDateOfTermination = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.gbDetail = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
        Me.bgcSamAccountName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcLastLogonDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcAccountExpirationDate = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcDistinguishedName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcExcelFileName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcExcelSheetName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.bgcExcelRowNumber = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        Me.FluentFormDefaultManager = New DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(Me.components)
        Me.bbiCompare = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiImport = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiImportExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiCompareAccounts = New DevExpress.XtraBars.BarButtonItem()
        Me.acAdEntries = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.aceDomainControllers = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.FluentDesignFormControl1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl()
        Me.FluentDesignFormContainer1.SuspendLayout()
        CType(Me.gcUserAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsOnRow.SuspendLayout()
        CType(Me.gvUserAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentFormDefaultManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.acAdEntries, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FluentDesignFormContainer1
        '
        Me.FluentDesignFormContainer1.Controls.Add(Me.gcUserAccounts)
        Me.FluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FluentDesignFormContainer1.Location = New System.Drawing.Point(280, 30)
        Me.FluentDesignFormContainer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FluentDesignFormContainer1.Name = "FluentDesignFormContainer1"
        Me.FluentDesignFormContainer1.Size = New System.Drawing.Size(1120, 770)
        Me.FluentDesignFormContainer1.TabIndex = 0
        '
        'gcUserAccounts
        '
        Me.gcUserAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcUserAccounts.EmbeddedNavigator.ContextMenuStrip = Me.cmsOnRow
        Me.gcUserAccounts.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gcUserAccounts.EmbeddedNavigator.ToolTipController = Me.ToolTipController
        Me.gcUserAccounts.Location = New System.Drawing.Point(0, 0)
        Me.gcUserAccounts.MainView = Me.gvUserAccounts
        Me.gcUserAccounts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gcUserAccounts.MenuManager = Me.FluentFormDefaultManager
        Me.gcUserAccounts.Name = "gcUserAccounts"
        Me.gcUserAccounts.Size = New System.Drawing.Size(1120, 770)
        Me.gcUserAccounts.TabIndex = 0
        Me.gcUserAccounts.ToolTipController = Me.ToolTipController
        Me.gcUserAccounts.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.[True]
        Me.gcUserAccounts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvUserAccounts})
        '
        'cmsOnRow
        '
        Me.cmsOnRow.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiEntryInformation, Me.ToolStripSeparator1, Me.tsmiDeleteRows, Me.tsmiClearAll})
        Me.cmsOnRow.Name = "cmsOnRow"
        Me.cmsOnRow.Size = New System.Drawing.Size(181, 98)
        '
        'tsmiEntryInformation
        '
        Me.tsmiEntryInformation.Image = Global.Adac.My.Resources.Resources.info_16x16
        Me.tsmiEntryInformation.Name = "tsmiEntryInformation"
        Me.tsmiEntryInformation.Size = New System.Drawing.Size(180, 22)
        Me.tsmiEntryInformation.Text = "Entry Information"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(177, 6)
        '
        'tsmiDeleteRows
        '
        Me.tsmiDeleteRows.Image = Global.Adac.My.Resources.Resources.deleterows_16x16
        Me.tsmiDeleteRows.Name = "tsmiDeleteRows"
        Me.tsmiDeleteRows.Size = New System.Drawing.Size(180, 22)
        Me.tsmiDeleteRows.Text = "Delete Row"
        '
        'tsmiClearAll
        '
        Me.tsmiClearAll.Image = Global.Adac.My.Resources.Resources.clear_16x16
        Me.tsmiClearAll.Name = "tsmiClearAll"
        Me.tsmiClearAll.Size = New System.Drawing.Size(180, 22)
        Me.tsmiClearAll.Text = "Clear All"
        '
        'ToolTipController
        '
        Me.ToolTipController.AutoPopDelay = 15000
        '
        'gvUserAccounts
        '
        Me.gvUserAccounts.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() {Me.gbId, Me.gbName, Me.gbSurname, Me.gbDepartment, Me.gbOther, Me.gbDetail})
        Me.gvUserAccounts.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.bgcEmployeeId, Me.bgcGivenName, Me.bgcSurname, Me.bgcDepartment, Me.bgcCompany, Me.bgcSamAccountName, Me.bgcAccountExpirationDate, Me.bgcDistinguishedName, Me.bgcLastLogonDate, Me.bgcEnabled, Me.bgcHrNumber, Me.bgcHrName, Me.bgcHrSurname, Me.bgcHrFunction, Me.bgcHrDateOfEntry, Me.bgcHrDateOfTermination, Me.bgcHrDepartment, Me.bgcExcelFileName, Me.bgcExcelSheetName, Me.bgcExcelRowNumber})
        Me.gvUserAccounts.DetailHeight = 284
        Me.gvUserAccounts.GridControl = Me.gcUserAccounts
        Me.gvUserAccounts.GroupCount = 2
        Me.gvUserAccounts.Name = "gvUserAccounts"
        Me.gvUserAccounts.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
        Me.gvUserAccounts.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsBehavior.AutoExpandAllGroups = True
        Me.gvUserAccounts.OptionsBehavior.ReadOnly = True
        Me.gvUserAccounts.OptionsClipboard.AllowCopy = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsClipboard.AllowCsvFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsClipboard.AllowHtmlFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsClipboard.AllowRtfFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsClipboard.AllowTxtFormat = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsCustomization.AllowChangeColumnParent = True
        Me.gvUserAccounts.OptionsDetail.EnableMasterViewMode = False
        Me.gvUserAccounts.OptionsFilter.AllowAutoFilterConditionChange = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsLayout.StoreAllOptions = True
        Me.gvUserAccounts.OptionsLayout.StoreAppearance = True
        Me.gvUserAccounts.OptionsLayout.StoreFormatRules = True
        Me.gvUserAccounts.OptionsMenu.EnableGroupRowMenu = True
        Me.gvUserAccounts.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsMenu.ShowFooterItem = True
        Me.gvUserAccounts.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.gvUserAccounts.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvUserAccounts.OptionsView.ShowBands = False
        Me.gvUserAccounts.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.bgcExcelFileName, DevExpress.Data.ColumnSortOrder.Ascending), New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.bgcExcelSheetName, DevExpress.Data.ColumnSortOrder.Ascending)})
        '
        'gbId
        '
        Me.gbId.Caption = "ID"
        Me.gbId.Columns.Add(Me.bgcHrNumber)
        Me.gbId.Columns.Add(Me.bgcEmployeeId)
        Me.gbId.Columns.Add(Me.bgcEnabled)
        Me.gbId.Name = "gbId"
        Me.gbId.OptionsBand.AllowMove = False
        Me.gbId.VisibleIndex = 0
        Me.gbId.Width = 76
        '
        'bgcHrNumber
        '
        Me.bgcHrNumber.Caption = "HR Pers. Nr."
        Me.bgcHrNumber.FieldName = "HrNumber"
        Me.bgcHrNumber.Name = "bgcHrNumber"
        Me.bgcHrNumber.Visible = True
        Me.bgcHrNumber.Width = 76
        '
        'bgcEmployeeId
        '
        Me.bgcEmployeeId.Caption = "Employee ID"
        Me.bgcEmployeeId.FieldName = "EmployeeId"
        Me.bgcEmployeeId.MinWidth = 21
        Me.bgcEmployeeId.Name = "bgcEmployeeId"
        Me.bgcEmployeeId.RowIndex = 1
        Me.bgcEmployeeId.Visible = True
        Me.bgcEmployeeId.Width = 76
        '
        'bgcEnabled
        '
        Me.bgcEnabled.FieldName = "Enabled"
        Me.bgcEnabled.Name = "bgcEnabled"
        Me.bgcEnabled.RowIndex = 1
        '
        'gbName
        '
        Me.gbName.Caption = "Name"
        Me.gbName.Columns.Add(Me.bgcHrName)
        Me.gbName.Columns.Add(Me.bgcGivenName)
        Me.gbName.Name = "gbName"
        Me.gbName.OptionsBand.AllowMove = False
        Me.gbName.VisibleIndex = 1
        Me.gbName.Width = 99
        '
        'bgcHrName
        '
        Me.bgcHrName.Caption = "HR Name"
        Me.bgcHrName.FieldName = "HrName"
        Me.bgcHrName.Name = "bgcHrName"
        Me.bgcHrName.Visible = True
        Me.bgcHrName.Width = 99
        '
        'bgcGivenName
        '
        Me.bgcGivenName.FieldName = "GivenName"
        Me.bgcGivenName.Name = "bgcGivenName"
        Me.bgcGivenName.RowIndex = 1
        Me.bgcGivenName.Visible = True
        Me.bgcGivenName.Width = 99
        '
        'gbSurname
        '
        Me.gbSurname.Caption = "Surname"
        Me.gbSurname.Columns.Add(Me.bgcHrSurname)
        Me.gbSurname.Columns.Add(Me.bgcSurname)
        Me.gbSurname.Name = "gbSurname"
        Me.gbSurname.OptionsBand.AllowMove = False
        Me.gbSurname.VisibleIndex = 2
        Me.gbSurname.Width = 117
        '
        'bgcHrSurname
        '
        Me.bgcHrSurname.Caption = "HR Surname"
        Me.bgcHrSurname.FieldName = "HrSurname"
        Me.bgcHrSurname.Name = "bgcHrSurname"
        Me.bgcHrSurname.Visible = True
        Me.bgcHrSurname.Width = 117
        '
        'bgcSurname
        '
        Me.bgcSurname.FieldName = "Surname"
        Me.bgcSurname.Name = "bgcSurname"
        Me.bgcSurname.RowIndex = 1
        Me.bgcSurname.Visible = True
        Me.bgcSurname.Width = 117
        '
        'gbDepartment
        '
        Me.gbDepartment.Caption = "Department"
        Me.gbDepartment.Columns.Add(Me.bgcHrDepartment)
        Me.gbDepartment.Columns.Add(Me.bgcDepartment)
        Me.gbDepartment.Name = "gbDepartment"
        Me.gbDepartment.VisibleIndex = 3
        Me.gbDepartment.Width = 127
        '
        'bgcHrDepartment
        '
        Me.bgcHrDepartment.Caption = "HR Department"
        Me.bgcHrDepartment.FieldName = "HrDepartment"
        Me.bgcHrDepartment.Name = "bgcHrDepartment"
        Me.bgcHrDepartment.Visible = True
        Me.bgcHrDepartment.Width = 127
        '
        'bgcDepartment
        '
        Me.bgcDepartment.FieldName = "Department"
        Me.bgcDepartment.Name = "bgcDepartment"
        Me.bgcDepartment.RowIndex = 1
        Me.bgcDepartment.Visible = True
        Me.bgcDepartment.Width = 127
        '
        'gbOther
        '
        Me.gbOther.Caption = "Other"
        Me.gbOther.Columns.Add(Me.bgcHrFunction)
        Me.gbOther.Columns.Add(Me.bgcHrDateOfEntry)
        Me.gbOther.Columns.Add(Me.bgcCompany)
        Me.gbOther.Columns.Add(Me.bgcHrDateOfTermination)
        Me.gbOther.Name = "gbOther"
        Me.gbOther.VisibleIndex = 4
        Me.gbOther.Width = 240
        '
        'bgcHrFunction
        '
        Me.bgcHrFunction.Caption = "HR Function"
        Me.bgcHrFunction.FieldName = "HrFunction"
        Me.bgcHrFunction.Name = "bgcHrFunction"
        Me.bgcHrFunction.Visible = True
        Me.bgcHrFunction.Width = 107
        '
        'bgcHrDateOfEntry
        '
        Me.bgcHrDateOfEntry.Caption = "HR Date of Entry"
        Me.bgcHrDateOfEntry.FieldName = "HrDateOfEntry"
        Me.bgcHrDateOfEntry.Name = "bgcHrDateOfEntry"
        Me.bgcHrDateOfEntry.Visible = True
        Me.bgcHrDateOfEntry.Width = 133
        '
        'bgcCompany
        '
        Me.bgcCompany.FieldName = "Company"
        Me.bgcCompany.Name = "bgcCompany"
        Me.bgcCompany.RowIndex = 1
        Me.bgcCompany.Visible = True
        Me.bgcCompany.Width = 107
        '
        'bgcHrDateOfTermination
        '
        Me.bgcHrDateOfTermination.Caption = "HR Date of Termination"
        Me.bgcHrDateOfTermination.FieldName = "HrDateOfTermination"
        Me.bgcHrDateOfTermination.Name = "bgcHrDateOfTermination"
        Me.bgcHrDateOfTermination.RowIndex = 1
        Me.bgcHrDateOfTermination.Visible = True
        Me.bgcHrDateOfTermination.Width = 133
        '
        'gbDetail
        '
        Me.gbDetail.Caption = "Detail"
        Me.gbDetail.Columns.Add(Me.bgcSamAccountName)
        Me.gbDetail.Columns.Add(Me.bgcLastLogonDate)
        Me.gbDetail.Columns.Add(Me.bgcAccountExpirationDate)
        Me.gbDetail.Columns.Add(Me.bgcDistinguishedName)
        Me.gbDetail.Name = "gbDetail"
        Me.gbDetail.VisibleIndex = 5
        Me.gbDetail.Width = 365
        '
        'bgcSamAccountName
        '
        Me.bgcSamAccountName.Caption = "Sam Account Name"
        Me.bgcSamAccountName.FieldName = "SamAccountName"
        Me.bgcSamAccountName.MinWidth = 21
        Me.bgcSamAccountName.Name = "bgcSamAccountName"
        Me.bgcSamAccountName.Visible = True
        Me.bgcSamAccountName.Width = 99
        '
        'bgcLastLogonDate
        '
        Me.bgcLastLogonDate.FieldName = "LastLogonDate"
        Me.bgcLastLogonDate.Name = "bgcLastLogonDate"
        Me.bgcLastLogonDate.Visible = True
        Me.bgcLastLogonDate.Width = 133
        '
        'bgcAccountExpirationDate
        '
        Me.bgcAccountExpirationDate.Caption = "Expiration Date"
        Me.bgcAccountExpirationDate.FieldName = "AccountExpirationDate"
        Me.bgcAccountExpirationDate.Name = "bgcAccountExpirationDate"
        Me.bgcAccountExpirationDate.ToolTip = "Account Expiration Date"
        Me.bgcAccountExpirationDate.Visible = True
        Me.bgcAccountExpirationDate.Width = 133
        '
        'bgcDistinguishedName
        '
        Me.bgcDistinguishedName.FieldName = "DistinguishedName"
        Me.bgcDistinguishedName.Name = "bgcDistinguishedName"
        Me.bgcDistinguishedName.RowIndex = 1
        Me.bgcDistinguishedName.Visible = True
        Me.bgcDistinguishedName.Width = 365
        '
        'bgcExcelFileName
        '
        Me.bgcExcelFileName.Caption = "File Name"
        Me.bgcExcelFileName.FieldName = "ExcelFileName"
        Me.bgcExcelFileName.Name = "bgcExcelFileName"
        Me.bgcExcelFileName.Visible = True
        '
        'bgcExcelSheetName
        '
        Me.bgcExcelSheetName.Caption = "Sheet Name"
        Me.bgcExcelSheetName.FieldName = "ExcelSheetName"
        Me.bgcExcelSheetName.Name = "bgcExcelSheetName"
        Me.bgcExcelSheetName.Visible = True
        '
        'bgcExcelRowNumber
        '
        Me.bgcExcelRowNumber.Caption = "#"
        Me.bgcExcelRowNumber.FieldName = "ExcelRowNumber"
        Me.bgcExcelRowNumber.Name = "bgcExcelRowNumber"
        Me.bgcExcelRowNumber.ToolTip = "Data Set Row Number"
        '
        'FluentFormDefaultManager
        '
        Me.FluentFormDefaultManager.Form = Me
        Me.FluentFormDefaultManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbiCompare, Me.bbiImport, Me.bbiImportExcel, Me.bbiCompareAccounts})
        Me.FluentFormDefaultManager.MaxItemId = 6
        '
        'bbiCompare
        '
        Me.bbiCompare.Caption = "Compare"
        Me.bbiCompare.Id = 0
        Me.bbiCompare.ImageOptions.SvgImage = Global.Adac.My.Resources.Resources.productquickcomparisons
        Me.bbiCompare.Name = "bbiCompare"
        '
        'bbiImport
        '
        Me.bbiImport.Caption = "Import"
        Me.bbiImport.Id = 1
        Me.bbiImport.ImageOptions.SvgImage = Global.Adac.My.Resources.Resources.lowimportance
        Me.bbiImport.Name = "bbiImport"
        '
        'bbiImportExcel
        '
        Me.bbiImportExcel.Caption = "Import Excel file"
        Me.bbiImportExcel.Id = 2
        Me.bbiImportExcel.ImageOptions.SvgImage = Global.Adac.My.Resources.Resources.lowimportance
        Me.bbiImportExcel.Name = "bbiImportExcel"
        '
        'bbiCompareAccounts
        '
        Me.bbiCompareAccounts.Caption = "Compare Accounts"
        Me.bbiCompareAccounts.Id = 3
        Me.bbiCompareAccounts.ImageOptions.SvgImage = Global.Adac.My.Resources.Resources.productquickcomparisons
        Me.bbiCompareAccounts.Name = "bbiCompareAccounts"
        '
        'acAdEntries
        '
        Me.acAdEntries.Dock = System.Windows.Forms.DockStyle.Left
        Me.acAdEntries.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.aceDomainControllers})
        Me.acAdEntries.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple
        Me.acAdEntries.Location = New System.Drawing.Point(0, 30)
        Me.acAdEntries.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.acAdEntries.Name = "acAdEntries"
        Me.acAdEntries.ResizeMode = DevExpress.XtraBars.Navigation.AccordionControlResizeMode.InnerResizeZone
        Me.acAdEntries.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Fluent
        Me.acAdEntries.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always
        Me.acAdEntries.Size = New System.Drawing.Size(280, 770)
        Me.acAdEntries.TabIndex = 1
        Me.acAdEntries.ToolTipController = Me.ToolTipController
        Me.acAdEntries.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.[True]
        Me.acAdEntries.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu
        '
        'aceDomainControllers
        '
        AccordionContextButton1.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Center
        AccordionContextButton1.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far
        AccordionContextButton1.Id = New System.Guid("c8ae9975-ab2e-4b0b-aa8b-681d7d4cca7b")
        AccordionContextButton1.ImageOptionsCollection.ItemNormal.SvgImage = Global.Adac.My.Resources.Resources.actions_addcircled
        AccordionContextButton1.ImageOptionsCollection.ItemNormal.SvgImageSize = New System.Drawing.Size(16, 16)
        AccordionContextButton1.Name = "acbAddDomainController"
        AccordionContextButton1.Tag = "AddDomainController"
        AccordionContextButton1.ToolTip = "Add new Domain Controller"
        AccordionContextButton1.Visibility = DevExpress.Utils.ContextItemVisibility.Visible
        Me.aceDomainControllers.ContextButtons.Add(AccordionContextButton1)
        Me.aceDomainControllers.Expanded = True
        Me.aceDomainControllers.Name = "aceDomainControllers"
        Me.aceDomainControllers.Text = "Domain Controllers"
        '
        'FluentDesignFormControl1
        '
        Me.FluentDesignFormControl1.FluentDesignForm = Me
        Me.FluentDesignFormControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbiCompare, Me.bbiImport, Me.bbiImportExcel, Me.bbiCompareAccounts})
        Me.FluentDesignFormControl1.Location = New System.Drawing.Point(0, 0)
        Me.FluentDesignFormControl1.Manager = Me.FluentFormDefaultManager
        Me.FluentDesignFormControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FluentDesignFormControl1.Name = "FluentDesignFormControl1"
        Me.FluentDesignFormControl1.Size = New System.Drawing.Size(1400, 30)
        Me.FluentDesignFormControl1.TabIndex = 2
        Me.FluentDesignFormControl1.TabStop = False
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.bbiImportExcel)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.bbiCompareAccounts)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1400, 800)
        Me.ControlContainer = Me.FluentDesignFormContainer1
        Me.Controls.Add(Me.FluentDesignFormContainer1)
        Me.Controls.Add(Me.acAdEntries)
        Me.Controls.Add(Me.FluentDesignFormControl1)
        Me.FluentDesignFormControl = Me.FluentDesignFormControl1
        Me.IconOptions.Image = Global.Adac.My.Resources.Resources.AD
        Me.IconOptions.LargeImage = Global.Adac.My.Resources.Resources.AD
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmMain"
        Me.NavigationControl = Me.acAdEntries
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AD Account Checker"
        Me.FluentDesignFormContainer1.ResumeLayout(False)
        CType(Me.gcUserAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsOnRow.ResumeLayout(False)
        CType(Me.gvUserAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentFormDefaultManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.acAdEntries, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FluentDesignFormContainer1 As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer
    Friend WithEvents acAdEntries As DevExpress.XtraBars.Navigation.AccordionControl
    Friend WithEvents aceDomainControllers As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents FluentDesignFormControl1 As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl
    Friend WithEvents FluentFormDefaultManager As DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager
    Friend WithEvents gcUserAccounts As DevExpress.XtraGrid.GridControl
    Friend WithEvents bbiCompare As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiImport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiImportExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiCompareAccounts As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents ToolTipController As DevExpress.Utils.ToolTipController
    Friend WithEvents gvUserAccounts As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    Friend WithEvents bgcEmployeeId As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcSamAccountName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcSurname As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcDepartment As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcCompany As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcAccountExpirationDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcDistinguishedName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcLastLogonDate As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcEnabled As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrNumber As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrSurname As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrFunction As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrDateOfEntry As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrDateOfTermination As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcHrDepartment As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcGivenName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents gbId As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gbName As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gbSurname As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gbDepartment As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gbOther As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents gbDetail As DevExpress.XtraGrid.Views.BandedGrid.GridBand
    Friend WithEvents bgcExcelFileName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcExcelSheetName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents bgcExcelRowNumber As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
    Friend WithEvents cmsOnRow As ContextMenuStrip
    Friend WithEvents tsmiDeleteRows As ToolStripMenuItem
    Friend WithEvents tsmiClearAll As ToolStripMenuItem
    Friend WithEvents tsmiEntryInformation As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
End Class
