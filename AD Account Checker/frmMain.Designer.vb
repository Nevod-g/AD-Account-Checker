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
        Dim AccordionContextButton2 As DevExpress.XtraBars.Navigation.AccordionContextButton = New DevExpress.XtraBars.Navigation.AccordionContextButton()
        Me.FluentDesignFormContainer1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer()
        Me.gcAccounts = New DevExpress.XtraGrid.GridControl()
        Me.gvAccounts = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FluentFormDefaultManager = New DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(Me.components)
        Me.bbiCompare = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiImport = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiImportExcel = New DevExpress.XtraBars.BarButtonItem()
        Me.bbiCompareAccounts = New DevExpress.XtraBars.BarButtonItem()
        Me.AccordionControl1 = New DevExpress.XtraBars.Navigation.AccordionControl()
        Me.AccordionControlElement1 = New DevExpress.XtraBars.Navigation.AccordionControlElement()
        Me.FluentDesignFormControl1 = New DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FluentDesignFormContainer1.SuspendLayout()
        CType(Me.gcAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gvAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentFormDefaultManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AccordionControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FluentDesignFormContainer1
        '
        Me.FluentDesignFormContainer1.Controls.Add(Me.gcAccounts)
        Me.FluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FluentDesignFormContainer1.Location = New System.Drawing.Point(260, 37)
        Me.FluentDesignFormContainer1.Name = "FluentDesignFormContainer1"
        Me.FluentDesignFormContainer1.Size = New System.Drawing.Size(830, 639)
        Me.FluentDesignFormContainer1.TabIndex = 0
        '
        'gcAccounts
        '
        Me.gcAccounts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gcAccounts.Location = New System.Drawing.Point(0, 0)
        Me.gcAccounts.MainView = Me.gvAccounts
        Me.gcAccounts.MenuManager = Me.FluentFormDefaultManager
        Me.gcAccounts.Name = "gcAccounts"
        Me.gcAccounts.Size = New System.Drawing.Size(830, 639)
        Me.gcAccounts.TabIndex = 0
        Me.gcAccounts.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.gvAccounts})
        '
        'gvAccounts
        '
        Me.gvAccounts.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.gvAccounts.GridControl = Me.gcAccounts
        Me.gvAccounts.Name = "gvAccounts"
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
        'AccordionControl1
        '
        Me.AccordionControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.AccordionControl1.Elements.AddRange(New DevExpress.XtraBars.Navigation.AccordionControlElement() {Me.AccordionControlElement1})
        Me.AccordionControl1.Location = New System.Drawing.Point(0, 37)
        Me.AccordionControl1.Name = "AccordionControl1"
        Me.AccordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch
        Me.AccordionControl1.Size = New System.Drawing.Size(260, 639)
        Me.AccordionControl1.TabIndex = 1
        Me.AccordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu
        '
        'AccordionControlElement1
        '
        AccordionContextButton2.AlignmentOptions.Panel = DevExpress.Utils.ContextItemPanel.Center
        AccordionContextButton2.AlignmentOptions.Position = DevExpress.Utils.ContextItemPosition.Far
        AccordionContextButton2.Id = New System.Guid("c8ae9975-ab2e-4b0b-aa8b-681d7d4cca7b")
        AccordionContextButton2.ImageOptionsCollection.ItemNormal.SvgImage = Global.Adac.My.Resources.Resources.actions_addcircled
        AccordionContextButton2.ImageOptionsCollection.ItemNormal.SvgImageSize = New System.Drawing.Size(16, 16)
        AccordionContextButton2.Name = "acbAddAd"
        Me.AccordionControlElement1.ContextButtons.Add(AccordionContextButton2)
        Me.AccordionControlElement1.Expanded = True
        Me.AccordionControlElement1.Name = "AccordionControlElement1"
        Me.AccordionControlElement1.Text = "Active Directories"
        '
        'FluentDesignFormControl1
        '
        Me.FluentDesignFormControl1.FluentDesignForm = Me
        Me.FluentDesignFormControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.bbiCompare, Me.bbiImport, Me.bbiImportExcel, Me.bbiCompareAccounts})
        Me.FluentDesignFormControl1.Location = New System.Drawing.Point(0, 0)
        Me.FluentDesignFormControl1.Manager = Me.FluentFormDefaultManager
        Me.FluentDesignFormControl1.Name = "FluentDesignFormControl1"
        Me.FluentDesignFormControl1.Size = New System.Drawing.Size(1090, 37)
        Me.FluentDesignFormControl1.TabIndex = 2
        Me.FluentDesignFormControl1.TabStop = False
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.bbiImportExcel)
        Me.FluentDesignFormControl1.TitleItemLinks.Add(Me.bbiCompareAccounts)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "SID"
        Me.GridColumn1.MinWidth = 25
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 94
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Name"
        Me.GridColumn2.MinWidth = 25
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 94
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 676)
        Me.ControlContainer = Me.FluentDesignFormContainer1
        Me.Controls.Add(Me.FluentDesignFormContainer1)
        Me.Controls.Add(Me.AccordionControl1)
        Me.Controls.Add(Me.FluentDesignFormControl1)
        Me.FluentDesignFormControl = Me.FluentDesignFormControl1
        Me.IconOptions.Image = Global.Adac.My.Resources.Resources.leftright_32x32
        Me.Name = "frmMain"
        Me.NavigationControl = Me.AccordionControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AD Account Checker"
        Me.FluentDesignFormContainer1.ResumeLayout(False)
        CType(Me.gcAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gvAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentFormDefaultManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AccordionControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FluentDesignFormControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FluentDesignFormContainer1 As DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer
    Friend WithEvents AccordionControl1 As DevExpress.XtraBars.Navigation.AccordionControl
    Friend WithEvents AccordionControlElement1 As DevExpress.XtraBars.Navigation.AccordionControlElement
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
