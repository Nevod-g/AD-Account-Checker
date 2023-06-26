Imports DevExpress.XtraEditors
Imports ProcessBank.Xpo

''' <summary>
''' Тег редактора UI, для Binding'а редактора с полем модели данных.
''' Предварительно в тег каждого элемента UI, Разработчик-Дизайнер UI вручную указывается FieldName.
''' Далее при Бандинге в тег устанавливается экземпляр DbEditorTag, для взаимодействия между Моделью и Редактором.
''' Обычно в хендлере формы Load вызывается процедура Binding.
''' Таким образом обеспечивается удобный доступ к эекземпляру модели из элементов UI.
''' Обеспечивает связь между элементами UI и зависимые вычисления.
''' </summary>
Public Class DbEditorTag
    Public Sub New(editor As BaseEdit, document As DbMetaObject, fieldName As String)
        If document Is Nothing Or String.IsNullOrWhiteSpace(fieldName) Then
            Throw New ArgumentException("The Argument 'fieldName' value is Null!")
        End If
        If editor Is Nothing Then
            Throw New ArgumentException($"The Editor '{fieldName}' is not set!")
        End If

        Me.Editor = editor
        Me.Document = document
        Me.FieldName = fieldName
        editor.Tag = Me '! До вызова события EditValueChanged уже должен быть установлен Tag (требует UnitButtonEdit_EditValueChanged)
        editor.EditValue = Me.DocumentGetFieldValue()
    End Sub

    ''' <summary>
    ''' Документ, экземпляр модели данных.
    ''' </summary>
    ''' <returns></returns>
    Public Property Document As DbMetaObject

    ''' <summary>
    ''' Имя поля
    ''' </summary>
    ''' <returns></returns>
    Public Property FieldName As String

    ''' <summary>
    ''' Редатор поля
    ''' </summary>
    ''' <returns></returns>
    Public Property Editor As BaseEdit

    ''' <summary>
    ''' Получить значение в экземпляр модели.
    ''' </summary>
    Public Function DocumentGetFieldValue() As Object
        Return Document?.GetFieldValue(FieldName)
    End Function

    ''' <summary>
    ''' Вывести значение модели в редактор.
    ''' </summary>
    Public Sub EditorRefresh()
        Editor.EditValue = Document?.GetFieldValue(FieldName)
        Editor.Refresh()
    End Sub

    ''' <summary>
    ''' Записать значение в экземпляр модели.
    ''' </summary>
    Public Sub DocumentSetFieldValue(value As Object, Optional ByVal softly As Boolean = False)
        If Document Is Nothing Then Exit Sub
        Document?.SetFieldValue(FieldName, value, softly)
    End Sub

    ''' <summary>
    ''' Если документ поддерживает интерфейс IChanging, то установить блокировку, данные были изменены.
    ''' </summary>
    ''' <param name="form"> Форма в заголовке которой требуется установить 'Звёздочку'. </param>
    Public Sub DocumentTryLock(Optional form As Form = Nothing)
        If Document Is Nothing Then Exit Sub
        If TypeOf (Document) Is IChanging Then CType(Document, IChanging).Lock(form) ' Установить блокировку документа
    End Sub

    ''' <summary>
    ''' Если документ поддерживает интерфейс IChanging, то снять блокировку, данные были изменены.
    ''' </summary>
    ''' <param name="form"> Форма в заголовке которой требуется снять 'Звёздочку'. </param>
    Public Sub DocumentTryUnLock(Optional form As Form = Nothing)
        If Document Is Nothing Then Exit Sub
        If TypeOf (Document) Is IChanging Then CType(Document, IChanging).Unlock(form) ' Установить блокировку документа
    End Sub

    Public Overrides Function ToString() As String
        Return Me?.FieldName
    End Function

    Public Shared Function GetEditorTag(control As Control) As DbEditorTag
        If control Is Nothing Then Throw New ArgumentException("The Control is Nothing!")
        If control.Tag Is Nothing Then Throw New ArgumentException($"The Control '{control.Name}' Tag is Nothing!")
        Dim controlTag = TryCast(control.Tag, DbEditorTag)
        If controlTag Is Nothing Then
            Throw New ArgumentException($"The Control '{control.Name}' Tag is not of type {NameOf(DbEditorTag)}! Current Control Type: {control.GetType.Name}, value type: '{control.Tag.GetType}'")
        End If
        Return controlTag
    End Function
End Class
