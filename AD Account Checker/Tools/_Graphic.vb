Imports System.Drawing

''' <summary>
''' Graphics container.
''' v.1.0
''' </summary>
Public Module _Graphic
	Public Sub DrawText(Image As Image, Comment As String, ShiftX As Integer, ShiftY As Integer, TextSize As Integer,
	ForeColor As Color,
	Optional ByVal FontName As String = "Calibri")

		If ShiftY < 0 Then ' Отступить от нижнего края
			ShiftY = Image.Height + ShiftY
			If ShiftY < 0 Then ShiftY = 0
		End If

		If Comment Is Nothing OrElse Comment.Length = 0 OrElse Image Is Nothing Then Exit Sub
		Using g = Drawing.Graphics.FromImage(Image)
			'Dim k As Byte = 40 '% тени
			'Dim sPen As New Pen(Color.FromArgb(Math.Round(255 * k / 100), BorderColor.R, BorderColor.G, BorderColor.B)) 'Тень
			'Dim ssPen As New Pen(Color.FromArgb(Math.Round(255 * k / 2 / 100), BorderColor.R, BorderColor.G, BorderColor.B)) 'Тень2
			g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
			g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
			g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
			g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
			g.TextRenderingHint = Text.TextRenderingHint.AntiAliasGridFit

			' Рассчитать длину выбранных сторон
			Dim drawFormat As New StringFormat With {.FormatFlags = StringFormatFlags.NoWrap, .Alignment = StringAlignment.Near}
			Dim fontBrush As New SolidBrush(ForeColor) ' Подпись
			Dim backBrush As New SolidBrush(Color.Gray) ' Подпись
			Dim fontText As New Font(FontName, TextSize)
			'Dim sizeText As Size
			'sizeText = TextRenderer.MeasureText(g, Comment, fontText)

			g.DrawString(Comment, fontText, backBrush, ShiftX + 2, ShiftY + 2)
			g.DrawString(Comment, fontText, fontBrush, ShiftX, ShiftY)
		End Using
	End Sub

	Public Sub DrawComment(image As Image, comment As String, Optional ByVal fontName As String = "Calibri")
		' Пост
		If comment Is Nothing OrElse comment.Length = 0 OrElse image Is Nothing Then Exit Sub

		Dim foreColor As Color = Color.White
		Dim textSize As Integer = CInt(28 / 800 * image.Height) ' Вычислить размер текста по высоте

		Using g = Drawing.Graphics.FromImage(image)
			'Dim k As Byte = 40 '% тени
			'Dim sPen As New Pen(Color.FromArgb(Math.Round(255 * k / 100), BorderColor.R, BorderColor.G, BorderColor.B)) 'Тень
			'Dim ssPen As New Pen(Color.FromArgb(Math.Round(255 * k / 2 / 100), BorderColor.R, BorderColor.G, BorderColor.B)) 'Тень2
			g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
			g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
			g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
			g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
			g.TextRenderingHint = Text.TextRenderingHint.AntiAliasGridFit

			' Позиционировать текст по нижнему краю
			Dim fontText As New Font(fontName, textSize)
			Dim sizeText = Windows.Forms.TextRenderer.MeasureText(g, comment, fontText) ' Определить размер надписи
			Dim shiftX As Integer = 2, shiftY As Integer
			shiftY = image.Height - sizeText.Height - 2

			' Отобразить чёрный текст на светлом фоне
			Dim backBrush As New SolidBrush(Color.Black) ' Подпись
			Dim btm = New Bitmap(image)
			If GetBlackOrWhite(btm.GetPixel(shiftX, shiftY)) = Color.White Then
				foreColor = Color.Black
				backBrush = New SolidBrush(Color.White)
			End If

			' Рассчитать длину выбранных сторон
			Dim drawFormat As New StringFormat With {.FormatFlags = StringFormatFlags.NoWrap, .Alignment = StringAlignment.Near}
			Dim fontBrush As New SolidBrush(foreColor) ' Подпись

			g.DrawString(comment, fontText, backBrush, shiftX + 1, shiftY + 1)
			g.DrawString(comment, fontText, fontBrush, shiftX, shiftY)
		End Using
	End Sub

	''' <summary>
	''' Преобразовать цвет в черный или белый
	''' </summary>
	''' <param name="c"></param>
	Function GetBlackOrWhite(c As Color) As Color
		Dim d As Byte = CByte(c.R * 0.3 + c.G * 0.59 + c.B * 0.11)
		If d > 127 Then Return Color.White Else Return Color.Black
	End Function

	''' <summary>
	''' Получить более светлый или более тёмный цвет.
	''' </summary>
	''' <param name="color">Базовый цвет</param>
	''' <param name="multiplier">Множитель, значение от 0 до 2.</param>
	''' <param name="addendum">Слагаемое, значение от -255 до +255</param>
	Function GetClarifierColor(color As Color, multiplier As Double, Optional ByVal addendum As Integer = 0) As Color
		Dim r As Integer = CInt(color.R * multiplier) + addendum
		Dim g As Integer = CInt(color.G * multiplier) + addendum
		Dim b As Integer = CInt(color.B * multiplier) + addendum
		If r > 255 Then r = 255
		If g > 255 Then g = 255
		If b > 255 Then b = 255
		If r < 0 Then r = 0
		If g < 0 Then g = 0
		If b < 0 Then b = 0
		Return Color.FromArgb(r, g, b)
	End Function


	''' <summary>
	''' Прочитать картинку, не блокируя файл на диске. К сожалению этот метод искажает некоторые картинки (обычно терятся фон у PNG).
	''' </summary>
	''' https://stackoverflow.com/questions/3661799/file-delete-failing-when-image-fromfile-was-called-prior-it-despite-making-copy/3661892#3661892
	''' <param name="filePath"></param>
	Public Function ReadImageFromFile(filePath As String) As Image
		Using img = Image.FromFile(filePath)
			Dim result = New Bitmap(img.Width, img.Height, img.PixelFormat)
			Using g = Drawing.Graphics.FromImage(result)
				g.DrawImageUnscaled(img, 0, 0) ' Копировать изображение
			End Using
			Return CType(result, Image)
		End Using

		' Типа более быстрый вариант с индексированием
		'[DllImport("Kernel32.dll", EntryPoint = "CopyMemory")] 
		'Private extern Static void CopyMemory(IntPtr dest, IntPtr src, uint length); 

		'Public Static Image CreateIndexedImage(String path) { 
		'  Using (var sourceImage = (Bitmap)Image.FromFile(path)) { 
		'    var targetImage = New Bitmap(sourceImage.Width, sourceImage.Height,
		'      sourceImage.PixelFormat); 
		'    var sourceData = sourceImage.LockBits(
		'      New Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
		'      ImageLockMode.ReadOnly, sourceImage.PixelFormat); 
		'    var targetData = targetImage.LockBits(
		'      New Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
		'      ImageLockMode.WriteOnly, targetImage.PixelFormat); 
		'    CopyMemory(targetData.Scan0, sourceData.Scan0, 
		'      (uint)sourceData.Stride * (uint)sourceData.Height); 
		'    sourceImage.UnlockBits(sourceData); 
		'    targetImage.UnlockBits(targetData); 
		'    targetImage.Palette = sourceImage.Palette;
		'    Return targetImage; 
		'  } 
		'} 
	End Function

	''' <summary>
	''' Нарисовать треугольник с вершиной вправо.
	''' </summary>
	''' <param name="width"></param>
	''' <param name="height"></param>
	Public Function TriangleRight(location As Point, width As Integer, height As Integer) As Point()
		Dim result(0 To 3) As Point
		result(0) = New Point(location.X, location.Y)
		result(1) = New Point(location.X, location.Y + height)
		result(2) = New Point(location.X + width, location.Y + CInt(height / 2))
		result(3) = New Point(location.X, location.Y)
		Return result
	End Function

	''' <summary>
	''' Нарисовать треугольник с вершиной влево.
	''' </summary>
	''' <param name="width"></param>
	''' <param name="height"></param>
	Public Function TriangleLeft(location As Point, width As Integer, height As Integer) As Point()
		Dim result(0 To 3) As Point
		result(0) = New Point(location.X + width, location.Y)
		result(1) = New Point(location.X + width, location.Y + height)
		result(2) = New Point(location.X, location.Y + CInt(height / 2))
		result(3) = New Point(location.X + width, location.Y)
		Return result
	End Function
End Module