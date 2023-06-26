<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AccordionContextButton3 As DevExpress.XtraBars.Navigation.AccordionContextButton = New DevExpress.XtraBars.Navigation.AccordionContextButton()
        Me.FluentDesignFormContainer1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer()
        Me.gcAccounts = New DevExpress.XtraGrid.GridControl()
        Me.gvAccounts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FluentFormDefaultManager = New DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(Me.components)
        Me.bbiCompare = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiImport = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiImportExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiCompareAccounts = New DevExpress.XtraBars.BarButtonItem()
        Me.AccordionControl = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.aceDomainControllers = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.FluentDesignFormControl1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl()
        Me.FluentDesignFormContainer1.SuspendLayout()
        CType(Me.gcAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentFormDefaultManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AccordionControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FluentDesignFormContainer1
        '
        Me.FluentDesignFormContainer1.Controls.Add(Me.gcAccounts)
        Me.FluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FluentDesignFormContainer1.Location = New System.Drawing.Point(223, 30)
        Me.FluentDesignFormContainer1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FluentDesignFormContainer1.Name = "FluentDesignFormContainer1"
        Me.FluentDesignFormContainer1.Size = New System.Drawing.Size(711, 519)
        Me.FluentDesignFormContainer1.TabIndex = 0
        '
        'gcAccounts
        '
        Me.gcAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcAccounts.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gcAccounts.Location = New System.Drawing.Point(0, 0)
        Me.gcAccounts.MainView = Me.gvAccounts
        Me.gcAccounts.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gcAccounts.MenuManager = Me.FluentFormDefaultManager
        Me.gcAccounts.Name = "gcAccounts"
        Me.gcAccounts.Size = New System.Drawing.Size(711, 519)
        Me.gcAccounts.TabIndex = 0
        Me.gcAccounts.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.[True]
        Me.gcAccounts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvAccounts})
        '
        'gvAccounts
        '
        Me.gvAccounts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.gvAccounts.DetailHeight = 284
        Me.gvAccounts.GridControl = Me.gcAccounts
        Me.gvAccounts.Name = "gvAccounts"
        Me.gvAccounts.OptionsBehavior.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.[True]
        Me.gvAccounts.OptionsDetail.EnableMasterViewMode = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "SID"
        Me.GridColumn1.MinWidth = 21
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 81
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Name"
        Me.GridColumn2.MinWidth = 21
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 81
        '
        'FluentFormDefaultManager
        '
        Me.FluentFormDefaultManager.Form = Me
        Me.FluentFormDefaultManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbiCompare, Me.bbiImport, Me.bbiImportExcel, Me.bbiCompareAccounts})
        Me.FluentFormDefaultManager.MaxItemId = 4
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
        'AccordionControl
        '
        Me.AccordionControl.Dock = System.Windows.Forms.DockStyle.Left
        Me.AccordionControl.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.aceDomainControllers})
        Me.AccordionControl.ExpandElementMode = DevExpress.XtraBars.Navigation.ExpandElementMode.Multiple
        Me.AccordionControl.Location = New System.Drawing.Point(0, 30)
        Me.AccordionControl.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.AccordionControl.Name = "AccordionControl"
        Me.AccordionControl.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Fluent
        Me.AccordionControl.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always
        Me.AccordionControl.Size = New System.Drawing.Size(223, 519)
        Me.AccordionControl.TabIndex = 1
        Me.AccordionControl.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.[True]
        Me.AccordionControl.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu
        '
        'aceDomainControllers
        '
        AccordionContextButton3.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Center
        AccordionContextButton3.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far
        AccordionContextButton3.Id = New System.Guid("c8ae9975-ab2e-4b0b-aa8b-681d7d4cca7b")
        AccordionContextButton3.ImageOptionsCollection.ItemNormal.SvgImage = Global.Adac.My.Resources.Resources.actions_addcircled
        AccordionContextButton3.ImageOptionsCollection.ItemNormal.SvgImageSize = New System.Drawing.Size(16, 16)
        AccordionContextButton3.Name = "acbAddDomainController"
        AccordionContextButton3.Visibility = DevExpress.Utils.ContextItemVisibility.Visible
        Me.aceDomainControllers.ContextButtons.Add(AccordionContextButton3)
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
        Me.FluentDesignFormControl1.Size = New System.Drawing.Size(934, 30)
        Me.FluentDesignFormControl1.TabIndex = 2
        Me.FluentDesignFormControl1.TabStop = False
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.bbiImportExcel)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.bbiCompareAccounts)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(934, 549)
        Me.ControlContainer = Me.FluentDesignFormContainer1
        Me.Controls.Add(Me.FluentDesignFormContainer1)
        Me.Controls.Add(Me.AccordionControl)
        Me.Controls.Add(Me.FluentDesignFormControl1)
        Me.FluentDesignFormControl = Me.FluentDesignFormControl1
        Me.IconOptions.Image = Global.Adac.My.Resources.Resources.leftright_32x32
        Me.IconOptions.LargeImage = Global.Adac.My.Resources.Resources.leftright_32x32
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "frmMain"
        Me.NavigationControl = Me.AccordionControl
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AD Account Checker"
        Me.FluentDesignFormContainer1.ResumeLayout(False)
        CType(Me.gcAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentFormDefaultManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AccordionControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FluentDesignFormContainer1 As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer
    Friend WithEvents AccordionControl As DevExpress.XtraBars.Navigation.AccordionControl
    Friend WithEvents aceDomainControllers As DevExpress.XtraBars.Navigation.AccordionControlElement
    Friend WithEvents FluentDesignFormControl1 As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl
    Friend WithEvents FluentFormDefaultManager As DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager
    Friend WithEvents gcAccounts As DevExpress.XtraGrid.GridControl
    Friend WithEvents gvAccounts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents bbiCompare As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiImport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiImportExcel As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents bbiCompareAccounts As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
