Namespace Reporting
    Namespace ExportAdEntries
        <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
        Partial Class frmParameters
            Inherits DevExpress.XtraEditors.XtraForm

            'Form overrides dispose to clean up the component list.
            <System.Diagnostics.DebuggerNonUserCode()>
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
            <System.Diagnostics.DebuggerStepThrough()>
            Private Sub InitializeComponent()
                Me.ceExceptComparedEntries = New DevExpress.XtraEditors.CheckEdit()
                Me.sbCancel = New DevExpress.XtraEditors.SimpleButton()
                Me.sbOk = New DevExpress.XtraEditors.SimpleButton()
                Me.meExceptCompanies = New DevExpress.XtraEditors.MemoEdit()
                Me.ceExceptCompanies = New DevExpress.XtraEditors.CheckEdit()
                CType(Me.ceExceptComparedEntries.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
                CType(Me.meExceptCompanies.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
                CType(Me.ceExceptCompanies.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
                Me.SuspendLayout()
                '
                'ceExceptComparedEntries
                '
                Me.ceExceptComparedEntries.Location = New System.Drawing.Point(12, 12)
                Me.ceExceptComparedEntries.Name = "ceExceptComparedEntries"
                Me.ceExceptComparedEntries.Properties.Caption = "Except compared Entries"
                Me.ceExceptComparedEntries.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
                Me.ceExceptComparedEntries.Size = New System.Drawing.Size(137, 20)
                Me.ceExceptComparedEntries.TabIndex = 0
                '
                'sbCancel
                '
                Me.sbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                Me.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
                Me.sbCancel.Location = New System.Drawing.Point(207, 205)
                Me.sbCancel.Name = "sbCancel"
                Me.sbCancel.Size = New System.Drawing.Size(75, 23)
                Me.sbCancel.TabIndex = 3
                Me.sbCancel.Text = "Cancel"
                '
                'sbOk
                '
                Me.sbOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                Me.sbOk.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.sbOk.Location = New System.Drawing.Point(126, 205)
                Me.sbOk.Name = "sbOk"
                Me.sbOk.Size = New System.Drawing.Size(75, 23)
                Me.sbOk.TabIndex = 2
                Me.sbOk.Text = "Ok"
                '
                'meExceptCompanies
                '
                Me.meExceptCompanies.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                Me.meExceptCompanies.Location = New System.Drawing.Point(12, 64)
                Me.meExceptCompanies.Name = "meExceptCompanies"
                Me.meExceptCompanies.Size = New System.Drawing.Size(270, 131)
                Me.meExceptCompanies.TabIndex = 4
                '
                'ceExceptCompanies
                '
                Me.ceExceptCompanies.Location = New System.Drawing.Point(12, 38)
                Me.ceExceptCompanies.Name = "ceExceptCompanies"
                Me.ceExceptCompanies.Properties.Caption = "Except Companies"
                Me.ceExceptCompanies.Properties.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
                Me.ceExceptCompanies.Size = New System.Drawing.Size(137, 20)
                Me.ceExceptCompanies.TabIndex = 0
                '
                'frmParameters
                '
                Me.AcceptButton = Me.sbOk
                Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
                Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
                Me.CancelButton = Me.sbCancel
                Me.ClientSize = New System.Drawing.Size(294, 240)
                Me.Controls.Add(Me.meExceptCompanies)
                Me.Controls.Add(Me.sbCancel)
                Me.Controls.Add(Me.sbOk)
                Me.Controls.Add(Me.ceExceptCompanies)
                Me.Controls.Add(Me.ceExceptComparedEntries)
                Me.IconOptions.Image = Global.Adac.My.Resources.Resources.properties_16x16
                Me.MaximizeBox = False
                Me.MinimizeBox = False
                Me.Name = "frmParameters"
                Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Me.Text = "Parameters"
                Me.TopMost = True
                CType(Me.ceExceptComparedEntries.Properties, System.ComponentModel.ISupportInitialize).EndInit()
                CType(Me.meExceptCompanies.Properties, System.ComponentModel.ISupportInitialize).EndInit()
                CType(Me.ceExceptCompanies.Properties, System.ComponentModel.ISupportInitialize).EndInit()
                Me.ResumeLayout(False)

            End Sub

            Friend WithEvents ceExceptComparedEntries As DevExpress.XtraEditors.CheckEdit
            Friend WithEvents sbCancel As DevExpress.XtraEditors.SimpleButton
            Friend WithEvents sbOk As DevExpress.XtraEditors.SimpleButton
            Friend WithEvents meExceptCompanies As DevExpress.XtraEditors.MemoEdit
            Friend WithEvents ceExceptCompanies As DevExpress.XtraEditors.CheckEdit
        End Class
    End Namespace
End Namespace