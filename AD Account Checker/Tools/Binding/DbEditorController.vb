Imports DevExpress.XtraEditors
Imports ProcessBank.Agent
Imports ProcessBank.Xpo

''' <summary>
''' Типовая логика взаимодействия элементов UI/Model/DB.
''' Предварительно в тег элемента UI, дизайнер вручную вводит FieldName.
''' Далее при Бандинге в тег устанавливается экземпляр DbEditorTag, для взаимодействия между Моделью и Редактором.
''' Обычно в хендлере формы Load вызывается процедура Binding.
''' Таким образом обеспечивается удобный доступ к эекземпляру модели из элементов UI.
''' </summary>
Public Class DbEditorController

    ''' <summary>
    ''' Состояние загрузки или инициализации данных, исключающее действия пользователя.
    ''' Если IsLoading = True, то отключаются хендлеры: Editor_EditValueChanged, Editor_KeyDown.
    ''' Это полезно использовать, если хендлеры уже повешены, но их нужно отключить на время загрузки/вывода данных в UI.
    ''' </summary>
    Public Shared IsLoading As Boolean = True

    ''' <summary>
    ''' Метка - Получил ли редактор фокус.
    ''' </summary>
    Private Shared isGotFocus As Boolean = False

    ''' <summary>
    ''' Create a bind Tag, add handlers optional.
    ''' Отобразить значение свойства экземпляра модели данных.
    ''' </summary>
    ''' <param name="editor"></param>
    ''' <param name="document"></param>
    ''' <param name="softly">Не выдавать исключений.</param>
    ''' <param name="needAddHandlers"> Повесить типовые обработчики. </param>
    Public Shared Sub Binding(editor As BaseEdit, document As DbMetaObject,
                              Optional ByVal softly As Boolean = False,
                              Optional ByVal needAddHandlers As Boolean = True)
        If editor Is Nothing Then Throw New ArgumentException("The Editor is Nothing!")

        ' Преобразовывать только текстовые теги, указывающие на FieldName
        If editor.Tag IsNot Nothing AndAlso editor.Tag.GetType = GetType(String) AndAlso editor.Tag.ToString.Length > 0 Then
            ' Создать Bind Tag и вывести значение в редактор
            Dim fieldName = editor.Tag.ToString()
            editor.Tag = New DbEditorTag(editor, document, fieldName)
            editor.EditValue = document.GetFieldValue(fieldName)

            ' Определить возможность редактирования данных
            If Not editor.ReadOnly AndAlso TypeOf (document) Is IChanging Then
                ' Заблокировать ввод значения, если редактор не ReadOnly
                editor.ReadOnly = CType(document, IChanging).IsReadOnly
            End If

            ' Накинуть обработчики
            If needAddHandlers Then AddHandlers(editor)

        ElseIf editor.Tag?.GetType.Equals(GetType(DbEditorTag)) Then
            ' Тег уже обращён в тип DbEditorTag

        Else
            If Not softly Then ' Тег пуст или не был определён в UI
                Throw New Exception($"[{NameOf(DbEditorController)}.{NameOf(Binding)}]: Unprocessed control '{document.GetType.Name}.{editor.Name}' mast have FieldName!")
            Else
                Log.Write(Log.Level.Info, $"Unprocessed control '{document.GetType.Name}{editor.Name}' mast have FieldName!",
                            $"{NameOf(DbEditorController)}.{NameOf(Binding)}")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Create bind Tags. Отобразить значение из модели данных в редакторах.
    ''' Создать Tag для редакторов по указанным в них текстом FieldName.
    ''' </summary>
    ''' <param name="controls"></param>
    ''' <param name="document"></param>
    ''' <param name="softly">Не выдавать исключений.</param>
    ''' <param name="needAddHandlers">Повесить типовые обработчики.</param>
    Public Shared Sub Binding(controls As Control.ControlCollection, document As DbMetaObject,
                              Optional ByVal softly As Boolean = False,
                              Optional ByVal needAddHandlers As Boolean = True)
        IsLoading = True
        ' todo: Binding пока видит только первый уровень вложенности элементов,
        ' можно доработать чтобы в Group смотрел на SplitPanel и подобные контейнеры
        ' и глубже, но это ухудшает контроль или делает Binding менее явным (обдумать).
        For Each c In controls
            Select Case c.GetType() ' Список допустимых типов
                Case GetType(TextEdit), GetType(CalcEdit),
                     GetType(CheckEdit), GetType(PictureEdit), GetType(LookUpEdit),
                     GetType(ButtonEdit), GetType(ImageComboBoxEdit), GetType(ToggleSwitch),
                     GetType(MemoEdit), GetType(MemoExEdit), GetType(CheckedComboBoxEdit),
                     GetType(PopupContainerEdit)
                    Dim editor = CType(c, BaseEdit)
                    Binding(editor, document, softly, needAddHandlers)
            End Select
        Next
        IsLoading = False
    End Sub

    ''' <summary>
    ''' Подцепить типовые хендлеры:
    ''' 1) Транслировать значение в экземпляр модели.
    ''' 2) Обработать нажания клавиш: AnyKey => Lock, Delete, Ctrl+S or Ctrl+Enter.
    ''' 3) Специфка для TextEdit: Выделить весь текст в поле.
    ''' Do if editor is not ReadOnly!
    ''' </summary>
    ''' <param name="editor"></param>
    Public Shared Sub AddHandlers(editor As BaseEdit)
        If editor.ReadOnly Then Exit Sub

        ' Handlers of any editors
        AddHandler editor.EditValueChanged, AddressOf Editor_EditValueChanged ' Транслировать значение в экземпляр модели.
        AddHandler editor.KeyDown, AddressOf Editor_AnyKeyToLock_KeyDown ' Hot key - AnyKey => Lock
        AddHandler editor.KeyDown, AddressOf Editor_Save_KeyDown ' Hot key - Ctrl+S => Document.Save

        Select Case editor.GetType()
            Case GetType(LookUpEdit), GetType(ImageComboBoxEdit),
                 GetType(ButtonEdit), GetType(PictureEdit)
                ' Hot key - Delete => EditValue = Nothing
                AddHandler editor.KeyDown, AddressOf Editor_Delete_KeyDown
        End Select

        Select Case editor.GetType()
            Case GetType(TextEdit), GetType(CalcEdit), GetType(ButtonEdit)
                ' Select all text in the field
                AddHandler editor.GotFocus, AddressOf TextEdit_GotFocus
                AddHandler editor.Enter, AddressOf TextEdit_Enter
                AddHandler editor.MouseUp, AddressOf TextEdit_MouseUp

            Case GetType(PictureEdit)
                ' Drag & Drop Image file
                AddHandler editor.DragEnter, AddressOf PictureEdit_DragEnter
                AddHandler editor.DragDrop, AddressOf PictureEdit_DragDrop
        End Select
    End Sub


    ''' <summary>
    ''' Транслировать значение в экземпляр модели.
    ''' ImageComboBoxEdit - сохраняется в экземпляр модели.
    ''' PictureEdit - не сохраняется!
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Shared Sub Editor_EditValueChanged(sender As Object, e As EventArgs)
        If IsLoading Then Exit Sub
        Dim editor = CType(sender, BaseEdit)
        Dim editorTag = DbEditorTag.GetEditorTag(editor)
        'Dim args = DirectCast(e, ChangingEventArgs)
        'args.NewValue

        Select Case editor.GetType() ' Список проверяемых типов
            Case GetType(TextEdit),
                 GetType(CalcEdit),
                 GetType(CheckEdit),
                 GetType(LookUpEdit),
                 GetType(ImageComboBoxEdit),
                 GetType(MemoEdit),
                 GetType(MemoExEdit),
                 GetType(CheckedComboBoxEdit),
                 GetType(ToggleSwitch)
                ' Записать данные в экземпляр модели.
                editorTag.Document.SetFieldValue(editorTag.FieldName, editor.EditValue)

            Case GetType(ButtonEdit), GetType(PictureEdit) ' Вне закона
            Case Else
                Log.Write(Log.Level.Warn,
                          $"For control type '{editor.GetType.Name}' processing is not defined.",
                          $"{NameOf(DbEditorController)}.{NameOf(Editor_EditValueChanged)}")
        End Select
        editorTag?.DocumentTryLock(editor?.FindForm()) ' Установить блокировку документа

        ' Валидировать значение при вводе, мгновенно, чтобы произвести вычисления (пнуть не дожидаясь закрытия редактора).
        Select Case editor.GetType() ' Список проверяемых типов
            Case GetType(LookUpEdit), GetType(CalcEdit)
                editor.DoValidate()
        End Select
    End Sub

    ''' <summary>
    ''' Hot key - AnyKey => Lock
    ''' Обработчик редакторов.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Shared Sub Editor_AnyKeyToLock_KeyDown(sender As Object, e As KeyEventArgs)
        If IsLoading Then Exit Sub
        Dim editor = CType(sender, BaseEdit)
        Dim editorTag = DbEditorTag.GetEditorTag(editor)
        editorTag?.DocumentTryLock(editor?.FindForm()) ' Установить блокировку документа
    End Sub

    ''' <summary>
    ''' Hot key - Delete => EditValue = Nothing
    ''' Обработчик редакторов.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Shared Sub Editor_Delete_KeyDown(sender As Object, e As KeyEventArgs)
        If IsLoading Then Exit Sub
        Dim editor = CType(sender, BaseEdit)
        Dim editorTag = DbEditorTag.GetEditorTag(editor)

        If e.KeyCode = Keys.Delete Then
            ' Удалить значение в редакторе
            TryCast(sender, PopupBaseEdit)?.ClosePopup()
            editor.EditValue = Nothing
            editor.DoValidate()
            Log.Write(Log.Level.Trace,
                      $"Press key 'Delete'",
                      $"{NameOf(DbEditorController)}.{NameOf(Editor_Delete_KeyDown)}")
        End If
    End Sub

    ''' <summary>
    ''' Hot key - Ctrl+S or Ctrl+Enter => Document.Save
    ''' Обработчик для всех контролов формы.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Shared Sub Editor_Save_KeyDown(sender As Object, e As KeyEventArgs)
        If IsLoading Then Exit Sub
        Dim editor = CType(sender, BaseEdit)
        Dim editorTag = DbEditorTag.GetEditorTag(editor)

        If (My.Computer.Keyboard.CtrlKeyDown Or e.Control) AndAlso
               (e.KeyCode = Keys.Enter Or e.KeyCode = Keys.S) Then
            ' Ctrl+S or Ctrl+Enter - сохранить документ
            editorTag?.Document?.Save()
            Log.Write(Log.Level.Trace,
                      $"Press keys 'Ctrl+Enter' or 'Ctrl+S'",
                      $"{NameOf(DbEditorController)}.{NameOf(Editor_Save_KeyDown)}")
        End If
    End Sub

#Region "Assigning an image by drag and drop"
    ''' <summary>
    ''' Отображать эффект перетаскивания файла.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Shared Sub PictureEdit_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' Положить перетаскиваемую картинку в редактор.
    ''' Если картинок больше одной, то обрабатывается лишь первая.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Shared Sub PictureEdit_DragDrop(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim filesPaths As String() = TryCast(e.Data.GetData(DataFormats.FileDrop), String())
            For Each path In filesPaths ' Перебрать список путей к файлам
                If FileSystem.IsImage(path) Then
                    ' Присвоить значение
                    Dim pe = CType(sender, PictureEdit)
                    Dim fileData = FileSystem.LoadFileAsByte(path)
                    pe.EditValue = fileData
                    Exit Sub ' Обработать только первый путь
                End If
            Next
        End If
    End Sub
#End Region

    ' Выделить весь текст в поле, только после получения фокуса, если текст ещё не был выделен
#Region "Select text on TextEdit"
    Public Shared Sub TextEdit_GotFocus(sender As Object, e As EventArgs)
        isGotFocus = True
    End Sub

    Public Shared Sub TextEdit_Enter(sender As Object, e As EventArgs)
        If isGotFocus Then
            CType(sender, TextEdit).SelectAll() ' Выделить весь текст в поле
        End If
    End Sub

    Public Shared Sub TextEdit_MouseUp(sender As Object, e As MouseEventArgs)
        If isGotFocus Then
            CType(sender, TextEdit).SelectAll() ' Выделить весь текст в поле
            isGotFocus = False
        End If
    End Sub
#End Region
End Class
