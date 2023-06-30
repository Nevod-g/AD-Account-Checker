Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools
Imports ProcessBank.Xpo
Imports System.Reflection
Imports Excel = Microsoft.Office.Interop.Excel

Namespace Reporting
    ''' <summary>
    ''' Движок обработки RTS в шаблонах формата Excel.
    ''' Инструкция: https://ipgphotonics.atlassian.net/wiki/spaces/PBTD/pages/63046533
    ''' </summary>
    Public Class ExcelEngine
        ''' <summary>
        ''' Сохранить файл отчёта.
        ''' </summary>
        ''' <param name="templateFilePath"></param>
        ''' <param name="filePath"></param>
        ''' <param name="dataSource"></param>
        ''' <param name="runMacrosName">После открытия книги выполнить макрос с указанным именем.</param>
        Public Shared Sub FillAndSaveFile(Of T As DbMetaObject)(templateFilePath As String, filePath As String,
                                    dataSource As List(Of T), Optional ByVal runMacrosName As String = "")
            ' Копировать отчёт в указанное пользователем место
            If FileSystem.CopyFile(templateFilePath, filePath) = FileSystem.Status.Fail Then Exit Sub

            ' Запустить внешнее Приложение и сделайте его окно видимым, но свернутым.
            Dim app As New Excel.Application()
            Dim book As Excel.Workbook
            app.Visible = True ' Сделать окно приложения видимым
            'app.Visible = False ' Сделать окно приложения не видимым, этот приём не даёт производительности
            app.WindowState = Excel.XlWindowState.xlMinimized ' Пользователю не желательно просматривать отчёт, его действия приводят к ошибкам
            app.ScreenUpdating = False

            ' Создать элемент отчёта из шаблона.
            Log.Write("Opening Excel file...")
            Try
                book = app.Workbooks.Open(filePath)
            Catch ex As Exception
                app.ScreenUpdating = True
                Throw New Exception($"Unable to open a report template in Excel!{vbCrLf _
                                    }HRESULT: 0x800A03EC => Pls check templates or MS Excel workability.{vbCrLf _
                                    }{vbCrLf}{ex}")
            End Try

            ' Получить метаданные типа строки отчёта
            Application.DoEvents() ' Проявить frmWait
            Dim properties As PropertyInfo() = GetType(T).GetProperties()
            Dim metaMarker = $"[{GetType(T).Name}." ' Маркер вывода данных в поле.

            ' Перебрать все листы книги
            frmWait.ShowOnOwner(frmMain, "Mapping...")
            For Each sheet As Excel.Worksheet In book.Sheets
                ' Если в наименовании листа первый символ решётка, то обрабатываем лист, иначе смотрим следующий
                If String.IsNullOrEmpty(sheet.Name) OrElse InStr(sheet.Name, "#") <> 1 Then Continue For
                'sheet.Visible = Excel.XlSheetVisibility.xlSheetHidden 'этот приём не даёт увеличения производительности
                sheet.Select()
                'Заменить теги General
                'For rowIndex = 1 To sheet.UsedRange.Rows.Count ' Перебрать все ячейки целевого листа
                '    For colIndex = 1 To sheet.UsedRange.Columns.Count

                '    Next
                'Next

                ' Составить карту привязки данных
                Dim templateRangeColumnsCount As Integer
                Dim templateRange(0 To sheet.UsedRange.Columns.Count - 1) As Object
                Dim templateRowIndex As Integer = 0 ' Целевая строка отчёта с набором тегов для мапинга
                Dim fillRangeColumnBeginIndex As Integer ' Начало диапазона заполнения
                Dim fillRangeColumnEndIndex As Integer ' Конец диапазона заполнения
                For rowIndex = 1 To sheet.UsedRange.Rows.Count ' Перебрать все ячейки целевого листа
                    For colIndex = 1 To sheet.UsedRange.Columns.Count
                        Dim cell = CType(sheet.Cells(rowIndex, colIndex), Excel.Range)
                        Dim formula = cell?.Formula.ToString()

                        ' Маппинг данных
                        If templateRowIndex = 0 AndAlso (InStr(formula, "[Row.") > 0 Or InStr(formula, metaMarker) > 0) Then
                            templateRowIndex = rowIndex ' Целевая строка отчёта с набором тегов для мапинга
                            fillRangeColumnBeginIndex = colIndex ' Начало диапазона заполнения

                            Dim j = 0
                            For cIndex = colIndex To sheet.UsedRange.Columns.Count
                                cell = CType(sheet.Cells(rowIndex, cIndex), Excel.Range)
                                formula = cell?.Formula.ToString
                                templateRange(j) = formula
                                If InStr(formula, metaMarker) > 0 Then fillRangeColumnEndIndex = cIndex
                                j += 1
                            Next
                            templateRangeColumnsCount = j

                            ' Мапинг завершён
                            rowIndex = sheet.UsedRange.Rows.Count
                            Exit For
                        End If
                    Next
                    Application.DoEvents() ' Крутить гифку
                Next

                ' Собрать массив данных
                frmWait.ShowOnOwner(frmMain, "Filling...")
                Log.Write("Prepare fill...")
                Dim fillRangeColumnsCount = fillRangeColumnEndIndex - fillRangeColumnBeginIndex + 1
                Dim dataSourceRange(0 To dataSource.Count - 1, 0 To fillRangeColumnsCount - 1) As Object
                Dim range = sheet.Range(sheet.Cells(templateRowIndex, fillRangeColumnBeginIndex), sheet.Cells(templateRowIndex + dataSource.Count - 1, fillRangeColumnEndIndex))
                Dim isMessageShown As Boolean = False
                For i = 0 To dataSource.Count - 1
                    If i Mod 3 = 0 Then ' Показать ход процесса
                        Dim p = CInt((i + 1) / dataSource.Count * 100)
                        frmWait.InvokeSetDescription($"Calculating row {i + 1}... {p}%")
                    End If

                    Dim dataSourceRow = dataSource(i)
                    For j = 0 To fillRangeColumnsCount - 1
                        Dim sFormula = ValToStr(templateRange(j)) ' представление значения для каждого поля столбца
                        Dim formula As Object = sFormula ' вычисляемое значение

                        ' Расстановка порядкового номера
                        If sFormula = "[Row.#]" Then
                            dataSourceRange(i, j) = i + 1
                            Continue For
                        ElseIf InStr(sFormula, "[Row.#]") > 0 Then
                            formula = Replace(sFormula, "[Row.#]", CStr(i + 1))
                        End If

                        Try ' Вычислить значение по формуле RTS
                            ' todo: следует передавать в GetArgValue весь dataSource целиком, и уже внутри определять источник по тегу!

                            Dim args = FormulaParser.GetFormulaArguments(sFormula)
                            If args.Count() = 1 AndAlso sFormula?.First() = "["c AndAlso sFormula?.Last() = "]"c Then
                                ' Если формула представлена лишь одним аргументом,
                                ' то возвращаемое значение будет иметь оригинальный тип данных (data, integer, double)
                                dataSourceRange(i, j) = GetValue(ValToStr(formula), dataSourceRow)

                            ElseIf args.Count() > 1 Then
                                ' Если формула содержит более одного аргумента, то вычислить значение как строку.
                                For Each arg In args
                                    ' Заменить аргументы в строке значениями
                                    Dim argValue = ValToStr(GetValue(arg, dataSourceRow))
                                    formula = Replace(formula?.ToString(), arg, argValue)
                                Next
                                dataSourceRange(i, j) = formula

                            Else
                                ' Иначе копировать значение поля первой строки
                                dataSourceRange(i, j) = templateRange(j)
                            End If

                        Catch ex As Exception
                            If Not isMessageShown Then
                                Message.ShowException(ex, $"{NameOf(ExcelEngine)}.{NameOf(FillAndSaveFile)}",
                                            $"{NameOf(FormulaParser.GetArgValue)} is fail, formula: {sFormula}" & vbCrLf &
                                            $"Cell R1C1: R[{templateRowIndex + i}]C[{j}]")
                            End If
                        End Try
                    Next
                Next

                ' Залить диапазон ячеек в Excel
                Log.Write("Filling...")
                range.Value(Excel.XlRangeValueDataType.xlRangeValueDefault) = dataSourceRange

                ' Протянуть формулы
                Dim marker As Char = "="c
                frmWait.ShowOnOwner(frmMain, "Copying Formulas")
                If dataSource.Count > 1 Then
                    For j = 0 To fillRangeColumnsCount - 1
                        Dim formula As Object = templateRange(j).ToString
                        If formula.ToString.Length > 2 AndAlso formula.ToString.Chars(0) = marker Then ' Найдена формула
                            Dim columnIndex = fillRangeColumnBeginIndex + j
                            Dim rangeSource = CType(sheet.Cells(templateRowIndex, columnIndex), Excel.Range)
                            Dim rangeTarget = sheet.Range(sheet.Cells(templateRowIndex + 1, columnIndex),
                                                          sheet.Cells(templateRowIndex + dataSource.Count - 1, columnIndex))
                            rangeSource.Copy(rangeTarget)
                        End If
                    Next
                End If

                ' Наполнить лист данными (очень медленно выполняется)
                'Dim no As Integer = 0 'Счётчик
                'Dim templateRowCopy As Excel.Range = Nothing
                'For rowIndex = targetRowIndex To targetRowIndex + dataSource.Count - 1
                '    Dim rowDataSource = dataSource(no) : no += 1
                '    Dim templateRow = CType(sheet.Rows(rowIndex), Excel.Range)
                '    templateRowCopy = CType(sheet.Rows(rowIndex + 1), Excel.Range)
                '    templateRow.Copy()
                '    Dim lastRow = (rowIndex = (targetRowIndex + dataSource.Count - 1))
                '    If Not lastRow Then templateRowCopy.Insert(Shift:=Excel.XlInsertShiftDirection.xlShiftDown)

                '    For colIndex = 1 To sheet.UsedRange.Columns.Count
                '        Dim cell = CType(sheet.Cells(templateRow.Row, colIndex), Excel.Range)
                '        Dim formula = cell?.Formula.ToString
                '        Dim formulaOld = formula
                '        If InStr(formula, "[Row.#]") > 0 Then formula = Replace(formula, "[Row.#]", no.ToString)
                '        formula = ReplaceTagOnData(formula, properties, rowDataSource)
                '        If formula <> formulaOld Then cell.Value = formula
                '    Next

                '    If no Mod 3 = 0 Then
                '        Dim progress = Math.Round(rowIndex / (targetRowIndex + dataSource.Count - 1) * 100)
                '        frmWait.SetDescription($"Data filling... {progress}%")
                '        Application.DoEvents() 'Крутить гифку
                '    End If
                'Next
            Next

            If Not String.IsNullOrWhiteSpace(runMacrosName) Then app.Run(runMacrosName) ' Выполнить макрос
            app.ScreenUpdating = True
            'app.Visible = True ' Сделать окно приложения видимым
            app.WindowState = Excel.XlWindowState.xlMaximized
            frmWait.InvokeHide()
            book.Save() 'https://answers.microsoft.com/en-us/msoffice/forum/all/be-careful-parts-of-your-document-may-include/fae98705-d078-4fc5-843a-908dda5be559
        End Sub

        ''' <summary>
        ''' Вычислить значение формулы вида [Row.Property], специфичное для формата Excel.
        ''' </summary>
        ''' <param name="formula"> Формула или значение ячейки.</param>
        ''' <param name="rowSource"> Экземпляр строки ресурса.</param>
        ''' <returns>Значение тега, может быть числом, текстом или датой.</returns>
        Shared Function GetValue(formula As Object, rowSource As DbMetaObject) As Object
            Return FormulaParser.GetArgValue(ValToStr(formula), rowSource.GetType().Name, rowSource)
        End Function
    End Class
End Namespace