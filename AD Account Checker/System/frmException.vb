Imports DevExpress.Utils.Html
Imports ProcessBank.Agent
Imports ProcessBank.Agent.Marker
Imports System.ComponentModel

Public Class frmException
    Private dataSource As New ExceptionModel
    Private Class ExceptionModel
        ''' <summary> Значение как есть. </summary>
        Property Message As String

        ''' <summary> Представление посредством Html. </summary>
        Property MessageHtml As String

        ''' <summary> Значение как есть. </summary>
        Property Description As String

        ''' <summary> Представление посредством Html. </summary>
        Property DescriptionHtml As String
    End Class

    Private Const TITLE_BAR_HEIGHT = 38
    Private Const FOOTER_BAR_HEIGHT = 24
    Private Const BODY_PADDING = 14

    ''' <summary>
    ''' Основной шаблон представления формы со ссылками на поля данных.
    ''' </summary>
    Private primitiveHtmlTemplate As String
    Private primitivefrmMinSize As Size

    Public Sub New()
        InitializeComponent()
        primitiveHtmlTemplate = Me.HtmlTemplate.Template
        primitivefrmMinSize = Me.MinimumSize
    End Sub

    Private Sub frmException_Load(sender As Object, e As EventArgs) Handles Me.Load
        'HtmlContentControl.DataContext = dataSource
    End Sub

    Private Sub frmException_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ' Оптимизировать размер формы, для отображения всего текста
        Dim fontMessage As New Font("Segoe UI", 14)
        Dim fontDescription As New Font("Segoe UI", 12)
        Dim scr = Screen.FromControl(Me)
        Dim screenSize = New Size(scr.Bounds.Width, scr.Bounds.Height)
        Dim maxFormWidth = CInt(scr.Bounds.Width * 0.8)
        Dim maxFormHeight = CInt(scr.Bounds.Height * 0.9)
        Using g = System.Drawing.Graphics.FromHwnd(Me.Handle)
            ' Определить размер надписи
            Dim msgSize = Windows.Forms.TextRenderer.MeasureText(g, dataSource.Message, fontMessage)
            Dim descSize = Windows.Forms.TextRenderer.MeasureText(g, dataSource.Description, fontDescription)
            ' Вычислить оптимальный размер формы.
            ' Найти максимальный/достаточный размер
            Dim width = CInt(IIf(msgSize.Width > descSize.Width, msgSize.Width, descSize.Width * 0.8)) ' 0.8 - коэфициент на глаз
            'width += BODY_PADDING * 2
            Dim height = CInt(IIf(msgSize.Height > descSize.Height, msgSize.Height, descSize.Height))
            height += TITLE_BAR_HEIGHT + FOOTER_BAR_HEIGHT + BODY_PADDING * 2
            If width > maxFormWidth Then
                width = maxFormWidth
                height += 25 ' добавить высоту одной строки (реально не известно сколько длинных строк в тексте, можно посчтитать...)
            End If
            If width < primitivefrmMinSize.Width Then
                width = primitivefrmMinSize.Width
            End If
            If height > maxFormHeight Then
                height = maxFormHeight
            End If
            If height < primitivefrmMinSize.Height Then
                height = primitivefrmMinSize.Height
            End If
            Me.Size = New Size(width, height)
        End Using
        ' Зафиксировать новый минимальный размер
        Me.MinimumSize = Me.Size

        ' Разместить в центре
        Me.Left = scr.Bounds.Left + CInt((screenSize.Width - Me.Width) / 2)
        Me.Top = scr.Bounds.Top + CInt((screenSize.Height - Me.Height) / 2)
    End Sub

    Private Sub frmException_HtmlElementMouseDown(sender As Object, e As DxHtmlElementMouseEventArgs) Handles Me.HtmlElementMouseDown
        If e.ElementId.StartsWith("title") Then
            ' События мыши для элементов в строке заголовка
            'Dim args = TryCast(e.MouseArgs, DXMouseEventArgs)
            'args.Handled = True
        Else
            OnButtonClickController(sender, e)
        End If
    End Sub

    Private Sub OnButtonClickController(ByVal sender As Object, ByVal args As DxHtmlElementMouseEventArgs)
        If args.ElementId.StartsWith("copyBtn") Then
            ' Копировать текст в буффер обмена
            Dim subject = $"{dataSource.Message}{vbCrLf}{vbCrLf}{dataSource.Description}"
            Clipboard.SetText(subject, True)
            Me.TopMost = False

        ElseIf args.ElementId.StartsWith("abortBtn") Then
            Me.DialogResult = DialogResult.Abort
            Me.Hide()

        ElseIf args.ElementId.StartsWith("mailBtn") Then
            ' Сформировать письмо администраторам.
            Dim subject = Me.Text
            Dim htmlMessage = dataSource.MessageHtml
            Dim htmlDescription = dataSource.DescriptionHtml
            Dim body = $"Please take a look at this problem as a priority.<p>"
            body &= $"{htmlMessage}<p>{htmlDescription}"
            'Dim mailFrom = GetEmailFullName(AppUser.Name, AppUser.Email)
            Dim mailto = GetEmailFullName("Dmitrii Medintsev", "dmedintsev@ipgphotonics.com")
            Dim cc = GetEmailFullName("Sergei Malashkin", "smalashkin@ipgphotonics.com")
            Dim atachmentFilePath = Log.GetZipLogFilePath()
            Me.TopMost = False
            Outlook.CreateEmail(subject, body, mailto, cc, Nothing, atachmentFilePath)

        ElseIf args.ElementId.StartsWith("cancelBtn") Or
            args.ElementId.StartsWith("closeBtn") Then
            Me.DialogResult = DialogResult.Cancel
            Me.Hide()
        End If
    End Sub

    Public Overloads Function ShowDialog(title As String, message As String, description As String) As DialogResult
        Me.Text = title
        dataSource.Message = message
        dataSource.MessageHtml = ClearHtmlFormat(message)
        dataSource.Description = description
        dataSource.DescriptionHtml = ClearHtmlFormat(description)

        ' Заменить ссылки на данные в шаблоне HtmlTemplate
        Dim html = primitiveHtmlTemplate
        html = Replace(html, "${Message}", dataSource.MessageHtml)
        html = Replace(html, "${Description}", dataSource.DescriptionHtml)
        HtmlTemplate.Template = html

        Return Me.ShowDialog()
    End Function

    ''' <summary>
    ''' Привести код Html в формат Html для отображения текстом в полях значений.
    ''' </summary>
    ''' <param name="html"></param>
    ''' <returns> Html Text </returns>
    Private Function ClearHtmlFormat(html As String) As String
        html = Replace(html, "&", "&amp")
        html = Replace(html, "<", "&lt")
        html = Replace(html, ">", "&gt")
        html = Replace(html, vbCrLf, "<br>")
        Return html
    End Function

    Private Sub frmException_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' Скрыть форму ожидания, т.к. целостность процесса могла быть нарушена.
        frmWait.InvokeHide()
    End Sub

    Private Sub frmException_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose() ' Это так же убирает белые рамки вокруг формы при повторном показе.
    End Sub
End Class