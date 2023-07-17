<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddAdController
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
        Me.sbCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.clName = New DevExpress.XtraEditors.LabelControl()
        Me.lcServerAddress = New DevExpress.XtraEditors.LabelControl()
        Me.sbAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.teServerAddress = New DevExpress.XtraEditors.TextEdit()
        Me.teName = New DevExpress.XtraEditors.TextEdit()
        CType(Me.teServerAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.teName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sbCancel
        '
        Me.sbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.sbCancel.Location = New System.Drawing.Point(220, 70)
        Me.sbCancel.Name = "sbCancel"
        Me.sbCancel.Size = New System.Drawing.Size(75, 23)
        Me.sbCancel.TabIndex = 3
        Me.sbCancel.Text = "Cancel"
        '
        'clName
        '
        Me.clName.Location = New System.Drawing.Point(12, 15)
        Me.clName.Name = "clName"
        Me.clName.Size = New System.Drawing.Size(27, 13)
        Me.clName.TabIndex = 3
        Me.clName.Text = "Name"
        '
        'lcServerAddress
        '
        Me.lcServerAddress.Location = New System.Drawing.Point(12, 41)
        Me.lcServerAddress.Name = "lcServerAddress"
        Me.lcServerAddress.Size = New System.Drawing.Size(74, 13)
        Me.lcServerAddress.TabIndex = 3
        Me.lcServerAddress.Text = "Server Address"
        '
        'sbAdd
        '
        Me.sbAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbAdd.Location = New System.Drawing.Point(139, 70)
        Me.sbAdd.Name = "sbAdd"
        Me.sbAdd.Size = New System.Drawing.Size(75, 23)
        Me.sbAdd.TabIndex = 2
        Me.sbAdd.Text = "Add"
        '
        'teServerAddress
        '
        Me.teServerAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.teServerAddress.EditValue = ""
        Me.teServerAddress.Location = New System.Drawing.Point(92, 38)
        Me.teServerAddress.Name = "teServerAddress"
        Me.teServerAddress.Size = New System.Drawing.Size(203, 20)
        Me.teServerAddress.TabIndex = 1
        Me.teServerAddress.Tag = "ServerAddress"
        '
        'teName
        '
        Me.teName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.teName.Location = New System.Drawing.Point(92, 12)
        Me.teName.Name = "teName"
        Me.teName.Size = New System.Drawing.Size(203, 20)
        Me.teName.TabIndex = 0
        Me.teName.Tag = "Name"
        '
        'frmAddAdController
        '
        Me.AcceptButton = Me.sbAdd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.sbCancel
        Me.ClientSize = New System.Drawing.Size(307, 105)
        Me.Controls.Add(Me.lcServerAddress)
        Me.Controls.Add(Me.clName)
        Me.Controls.Add(Me.teServerAddress)
        Me.Controls.Add(Me.teName)
        Me.Controls.Add(Me.sbCancel)
        Me.Controls.Add(Me.sbAdd)
        Me.IconOptions.Image = Global.Adac.My.Resources.Resources.servermode_16x16
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddAdController"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Domain Controller"
        CType(Me.teServerAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.teName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents sbAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents teName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents teServerAddress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents clName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lcServerAddress As DevExpress.XtraEditors.LabelControl
End Class
