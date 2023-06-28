<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdObject
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.meInfo = New DevExpress.XtraEditors.MemoEdit()
        Me.sbGetPropertyValue = New DevExpress.XtraEditors.SimpleButton()
        Me.tePropertyValue = New DevExpress.XtraEditors.TextEdit()
        Me.tePropertyName = New DevExpress.XtraEditors.TextEdit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.meInfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tePropertyValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tePropertyName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.sbGetPropertyValue)
        Me.PanelControl1.Controls.Add(Me.tePropertyValue)
        Me.PanelControl1.Controls.Add(Me.tePropertyName)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 221)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(594, 29)
        Me.PanelControl1.TabIndex = 4
        '
        'meInfo
        '
        Me.meInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.meInfo.Location = New System.Drawing.Point(0, 0)
        Me.meInfo.Name = "meInfo"
        Me.meInfo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.meInfo.Size = New System.Drawing.Size(594, 221)
        Me.meInfo.TabIndex = 0
        '
        'sbGetPropertyValue
        '
        Me.sbGetPropertyValue.ImageOptions.Image = Global.Adac.My.Resources.Resources.next_16x16
        Me.sbGetPropertyValue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter
        Me.sbGetPropertyValue.Location = New System.Drawing.Point(173, 3)
        Me.sbGetPropertyValue.Name = "sbGetPropertyValue"
        Me.sbGetPropertyValue.Size = New System.Drawing.Size(131, 23)
        Me.sbGetPropertyValue.TabIndex = 1
        Me.sbGetPropertyValue.Text = "Get Property Value"
        '
        'tePropertyValue
        '
        Me.tePropertyValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tePropertyValue.Location = New System.Drawing.Point(310, 5)
        Me.tePropertyValue.Name = "tePropertyValue"
        Me.tePropertyValue.Properties.NullText = "NA"
        Me.tePropertyValue.Properties.ReadOnly = True
        Me.tePropertyValue.Size = New System.Drawing.Size(279, 20)
        Me.tePropertyValue.TabIndex = 2
        '
        'tePropertyName
        '
        Me.tePropertyName.Location = New System.Drawing.Point(5, 5)
        Me.tePropertyName.Name = "tePropertyName"
        Me.tePropertyName.Properties.NullValuePrompt = "Input Property Name"
        Me.tePropertyName.Size = New System.Drawing.Size(162, 20)
        Me.tePropertyName.TabIndex = 3
        '
        'frmAdObject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 250)
        Me.Controls.Add(Me.meInfo)
        Me.Controls.Add(Me.PanelControl1)
        Me.IconOptions.Image = Global.Adac.My.Resources.Resources.new_16x16
        Me.Name = "frmAdObject"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Directory Entry Information"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.meInfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tePropertyValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tePropertyName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents meInfo As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents sbGetPropertyValue As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tePropertyValue As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tePropertyName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
End Class
