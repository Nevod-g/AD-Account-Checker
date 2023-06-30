Imports System.Threading.Tasks
Imports ProcessBank.Agent

Namespace Reporting
    Namespace ExportAdEntries
        Public Class ExcelTechnology
#Region "Report Parameters"
            Public Shared IsExceptComparedEntries As Boolean
            Public Shared IsExceptCompanies As Boolean

            ''' <summary>
            ''' Список ExceptCompanies в формате CSV.
            ''' </summary>
            Public Shared Property ExceptCompanies As String
#End Region

            Private Shared ReadOnly Property reportTemplateFileName As String = "AD Entries.xlsx"
            Private Shared ReadOnly Property reportFileExtension As String = ".xlsx"
            Private Shared ReadOnly Property reportFileNameWoExtension As String = "AD Entries"

            Private Shared dataSource As List(Of AdObject)

            Public Shared Async Sub CreateReport()
                Dim sw As New Stopwatch : sw.Start()
                Dim filePath As String
                Log.Write()

                ' Запросить параметры
                Dim result = frmParameters.ShowDialog()
                If result = DialogResult.OK Then
                    ' Получить и отсортировать данные (Сортировка строиться в обратном порядке!)
                    frmWait.ShowOnOwner(frmMain, "Loading report data...")

                    ' Выбрать пул данных
                    Dim adObjects = frmMain.GetAllCheckedAdObjects().Where(
                        Function(o) o.IsUser AndAlso ' только Users
                        (Not IsExceptComparedEntries OrElse o.UserAccount Is Nothing)) ' исключить сопоставленные, если включена опция

                    ' Исключить Companies, если включена опция
                    If IsExceptCompanies Then
                        Dim exceptedCompanies = Split(ExceptCompanies.ToLower(), ", ")
                        If exceptedCompanies.Count() > 0 Then
                            adObjects = adObjects.Where(Function(o) String.IsNullOrWhiteSpace(o.Company) OrElse
                                                            Not exceptedCompanies.Contains(o.Company.ToLower()))
                        End If
                    End If
                    dataSource = adObjects?.
                        OrderByDescending(Function(r) r.SamAccountName).
                        OrderBy(Function(r) r.Department).
                        ToList()

                    ' Загрузить из AD Entry значения свойств Users.
                    For Each adObject In dataSource.Where(Function(o) o.IsUser)
                        Await Task.Run(AddressOf adObject.LoadPropertiesData)
                    Next

                    frmWait.InvokeHide()
                Else
                    Exit Sub
                End If

                ' Аборт, если нет данных для сохранения.
                If dataSource Is Nothing OrElse dataSource.Count = 0 Then
                    Message.Show("Not data matching the specified parameters.", "Create Report",, MessageBoxIcon.Information)
                    Exit Sub
                End If

                ' Отобразить диалоговое окно сохранения файла
                Dim sfd = Dx.SaveFileDialog
                sfd.Title = "Save Report"
                sfd.FileName = reportFileNameWoExtension
                sfd.Filter = $"Excel Workbook (*{reportFileExtension})|*{reportFileExtension}"
                If sfd.ShowDialog() = DialogResult.OK Then
                    filePath = sfd.FileName
                Else
                    Exit Sub
                End If

                ' Сборка отчета
                frmWait.ShowOnOwner(frmMain, "Build Report")
                ' определить шаблон
                Dim templateFilePath As String = FileSystem.Path.Combine(Core.REPORTS_TEMPLATES_DIR_PATH, reportTemplateFileName)
                ' наполнить данными и сохранить
                ExcelEngine.FillAndSaveFile(templateFilePath, filePath, dataSource)

                sw.Stop()
                Log.Write($"Time of report creation: {sw.Elapsed.TotalSeconds} s")
            End Sub
        End Class
    End Namespace
End Namespace