<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmException
    Inherits DevExpress.XtraEditors.DirectXForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmException))
        Me.DirectXFormContainerControl = New DevExpress.XtraEditors.DirectXFormContainerControl()
        Me.SvgImageCollection = New DevExpress.Utils.SvgImageCollection(Me.components)
        CType(Me.SvgImageCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DirectXFormContainerControl
        '
        Me.DirectXFormContainerControl.Location = New System.Drawing.Point(15, 53)
        Me.DirectXFormContainerControl.Name = "DirectXFormContainerControl"
        Me.DirectXFormContainerControl.Size = New System.Drawing.Size(416, 136)
        Me.DirectXFormContainerControl.TabIndex = 0
        '
        'SvgImageCollection
        '
        Me.SvgImageCollection.Add("close", "image://svgimages/icon builder/actions_delete.svg")
        Me.SvgImageCollection.Add("cancel", "image://svgimages/outlook inspired/cancel.svg")
        Me.SvgImageCollection.Add("mail", "image://svgimages/icon builder/actions_send.svg")
        Me.SvgImageCollection.Add("abort", "image://svgimages/icon builder/security_stop.svg")
        Me.SvgImageCollection.Add("copy", "image://svgimages/edit/copy.svg")
        '
        'frmException
        '
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ChildControls.Add(Me.DirectXFormContainerControl)
        Me.ClientSize = New System.Drawing.Size(446, 282)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HtmlImages = Me.SvgImageCollection
        Me.HtmlTemplate.Styles = resources.GetString("frmException.HtmlTemplate.Styles")
        Me.HtmlTemplate.Template = resources.GetString("frmException.HtmlTemplate.Template")
        Me.IconOptions.SvgImage = Global.Adac.My.Resources.Resources.bo_attention
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(446, 282)
        Me.Name = "frmException"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Exception: NA"
        Me.TopMost = True
        CType(Me.SvgImageCollection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DirectXFormContainerControl As DevExpress.XtraEditors.DirectXFormContainerControl
    Friend WithEvents SvgImageCollection As DevExpress.Utils.SvgImageCollection
End Class
