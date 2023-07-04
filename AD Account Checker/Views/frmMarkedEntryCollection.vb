Imports DevExpress.XtraBars.Navigation
Imports DevExpress.XtraSplashScreen
Imports ProcessBank.Agent

Public Class frmMarkedEntryCollection
    Const FILE_EXTENSION As String = "xml"
    Private ReadOnly collectionsDirPath As String = FileSystem.Path.Combine(Core.APP_DIR_PATH, "Collections")

    Private Sub frmMarkedEntryCollection_Load(sender As Object, e As EventArgs) Handles Me.Load
        FileSystem.CreateDir(collectionsDirPath)
        Dim filePaths = IO.Directory.GetFiles(collectionsDirPath, $"*.{FILE_EXTENSION}", IO.SearchOption.TopDirectoryOnly)
        lbcCollections.Items.Clear()
        For Each filePath In filePaths.OrderByDescending(Function(s) s)
            lbcCollections.Items.Add(FileSystem.Path.GetFileNameWithoutExtension(filePath))
        Next
    End Sub

    Private Sub sbAddCurrentCollection_Click(sender As Object, e As EventArgs) Handles sbAddCurrentCollection.Click
        If AdRepository.CheckedAdObjects?.Count > 0 Then
            ' Отобразить диалоговое окно сохранения файла
            Dim sfd = Dx.SaveFileDialog
            sfd.Title = "Save Collection of marked Entries"
            sfd.Filter = $"Extensible Markup Language (*.xml)|*.xml"
            sfd.FileName = Nothing
            sfd.CheckPathExists = True
            sfd.InitialDirectory = collectionsDirPath
            If sfd.ShowDialog() = DialogResult.OK Then
                Dim filePath = sfd.FileName
                If FileSystem.FileExists(filePath) Then
                    Message.Show("File already exists!", "Save Collection of marked Entries")
                    Exit Sub
                End If
                DbXmlWizard.SaveArray(AdRepository.CheckedAdObjects.Select(Function(o) o.NativePath).ToArray(), filePath)
                lbcCollections.Items.Add(FileSystem.Path.GetFileNameWithoutExtension(filePath))
            End If
        Else
            Message.Show("No Entries checked.", "Save Collection of marked Entries")
        End If
    End Sub

    Private Sub sbDelete_Click(sender As Object, e As EventArgs) Handles sbDelete.Click
        If lbcCollections.SelectedValue Is Nothing Then Exit Sub
        Dim filePath = FileSystem.Path.Combine(collectionsDirPath, $"{lbcCollections.SelectedValue}.{FILE_EXTENSION}")
        If FileSystem.DeleteFile(filePath) <> FileSystem.Status.Fail Then
            lbcCollections.Items.Remove(lbcCollections.SelectedItem)
        End If
    End Sub

    Private Sub lbcCollections_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lbcCollections.MouseDoubleClick
        sbApply_Click(sbApply, Nothing)
    End Sub

    Private Sub sbApply_Click(sender As Object, e As EventArgs) Handles sbApply.Click
        Dim filePath = FileSystem.Path.Combine(collectionsDirPath, $"{lbcCollections.SelectedValue}.{FILE_EXTENSION}")
        If Not FileSystem.FileExists(filePath) Then
            Message.Show("File does not exist.", "Open Collection of marked Entries")
            Exit Sub
        End If

        Using SplashScreenManager.ShowOverlayForm(Me,,,,,,,,,,,, WaitAnimationType.Line,, True)
            frmMain.UncheckAllEntries()
            frmMain.acAdEntries.BeginUpdate()
            Dim paths = DbXmlWizard.LoadArray(filePath)
            For Each path In paths
                Dim sp = Split(path, "\")
                Dim isTopLevel = True
                Dim parentAce As AccordionControlElement = Nothing

                For Each s In sp
                    If isTopLevel Then ' Верхний уровень всегда загружен
                        ' Найти целевой AdController
                        For Each el In frmMain.aceDomainControllers.Elements
                            If el.Text = s Then
                                parentAce = el ' Найден целевой AdController
                                Exit For
                            End If
                        Next

                        If parentAce Is Nothing Then
                            frmMain.acAdEntries.EndUpdate()
                            Message.Show($"AD Controller '{s}' does not exist.", "Open Collection of marked Entries")
                            Exit For
                        End If
                    End If

                    If Not isTopLevel Then
                        ' Найти и развернуть элементы (которые могут быть не загружены)
                        For Each el In parentAce.Elements
                            If el.Text = s Then
                                parentAce = el
                                frmMain.AddTopLevelAceElements(el)
                            End If
                        Next
                    End If

                    isTopLevel = False
                Next

                ' Отметить последний найденный элемент в пути
                If TypeOf parentAce?.Tag Is AdObject Then frmMain.CheckEntry(parentAce)
            Next
            frmMain.acAdEntries.EndUpdate()
        End Using
        Me.Close()
    End Sub

End Class