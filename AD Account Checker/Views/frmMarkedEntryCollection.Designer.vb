<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMarkedEntryCollection
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbcCollections = New DevExpress.XtraEditors.ListBoxControl()
        Me.sbAddCurrentCollection = New DevExpress.XtraEditors.SimpleButton()
        Me.sbApply = New DevExpress.XtraEditors.SimpleButton()
        Me.sbDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        CType(Me.lbcCollections, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbcCollections
        '
        Me.lbcCollections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbcCollections.Location = New System.Drawing.Point(0, 0)
        Me.lbcCollections.Name = "lbcCollections"
        Me.lbcCollections.Size = New System.Drawing.Size(221, 356)
        Me.lbcCollections.TabIndex = 0
        '
        'sbAddCurrentCollection
        '
        Me.sbAddCurrentCollection.Location = New System.Drawing.Point(6, 5)
        Me.sbAddCurrentCollection.Name = "sbAddCurrentCollection"
        Me.sbAddCurrentCollection.Size = New System.Drawing.Size(125, 23)
        Me.sbAddCurrentCollection.TabIndex = 1
        Me.sbAddCurrentCollection.Text = "Add current Collection"
        '
        'sbApply
        '
        Me.sbApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.sbApply.ImageOptions.Image = Global.Adac.My.Resources.Resources.apply_16x16
        Me.sbApply.Location = New System.Drawing.Point(6, 321)
        Me.sbApply.Name = "sbApply"
        Me.sbApply.Size = New System.Drawing.Size(125, 23)
        Me.sbApply.TabIndex = 2
        Me.sbApply.Text = "Apply"
        '
        'sbDelete
        '
        Me.sbDelete.Location = New System.Drawing.Point(6, 34)
        Me.sbDelete.Name = "sbDelete"
        Me.sbDelete.Size = New System.Drawing.Size(125, 23)
        Me.sbDelete.TabIndex = 3
        Me.sbDelete.Text = "Delete Collection"
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.sbAddCurrentCollection)
        Me.PanelControl1.Controls.Add(Me.sbDelete)
        Me.PanelControl1.Controls.Add(Me.sbApply)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelControl1.Location = New System.Drawing.Point(221, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(137, 356)
        Me.PanelControl1.TabIndex = 4
        '
        'frmMarkedEntryCollection
        '
        Me.AcceptButton = Me.sbApply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 356)
        Me.Controls.Add(Me.lbcCollections)
        Me.Controls.Add(Me.PanelControl1)
        Me.IconOptions.Image = Global.Adac.My.Resources.Resources.issue_16x16
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMarkedEntryCollection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Collection of marked Entries"
        CType(Me.lbcCollections, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbcCollections As DevExpress.XtraEditors.ListBoxControl
    Friend WithEvents sbAddCurrentCollection As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbApply As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
End Class
