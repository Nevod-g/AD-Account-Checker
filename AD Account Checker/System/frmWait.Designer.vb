<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWait
    Inherits DevExpress.XtraWaitForm.WaitForm

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
        Me.pp = New DevExpress.XtraWaitForm.ProgressPanel()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.tableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pp
        '
        Me.pp.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.pp.Appearance.Options.UseBackColor = True
        Me.pp.AppearanceCaption.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.pp.AppearanceCaption.Options.UseFont = True
        Me.pp.AppearanceDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.pp.AppearanceDescription.Options.UseFont = True
        Me.pp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pp.ImageHorzOffset = 20
        Me.pp.Location = New System.Drawing.Point(0, 17)
        Me.pp.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pp.MinimumSize = New System.Drawing.Size(300, 0)
        Me.pp.Name = "pp"
        Me.pp.Size = New System.Drawing.Size(300, 39)
        Me.pp.TabIndex = 0
        Me.pp.UseWaitCursor = True
        '
        'tableLayoutPanel1
        '
        Me.tableLayoutPanel1.AutoSize = True
        Me.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.tableLayoutPanel1.ColumnCount = 1
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.Controls.Add(Me.pp, 0, 0)
        Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.tableLayoutPanel1.MinimumSize = New System.Drawing.Size(300, 0)
        Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
        Me.tableLayoutPanel1.Padding = New System.Windows.Forms.Padding(0, 14, 0, 14)
        Me.tableLayoutPanel1.RowCount = 1
        Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tableLayoutPanel1.Size = New System.Drawing.Size(300, 73)
        Me.tableLayoutPanel1.TabIndex = 1
        Me.tableLayoutPanel1.UseWaitCursor = True
        '
        'frmWait
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(300, 73)
        Me.Controls.Add(Me.tableLayoutPanel1)
        Me.DoubleBuffered = True
        Me.MinimumSize = New System.Drawing.Size(300, 0)
        Me.Name = "frmWait"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Processing"
        Me.UseWaitCursor = True
        Me.tableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents pp As DevExpress.XtraWaitForm.ProgressPanel
    Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
End Class
