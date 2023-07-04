Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools
Imports ProcessBank.Xpo
Imports System
Imports System.Reflection

''' <summary>
''' Инструмент для работы с файлами XML. Объекты в листе должны быть однотипными.
''' v.1.1
''' </summary>
Public Class DbXmlWizard
    Public Const ELEMENT_NAME_LIST As String = "List"
    Public Const ELEMENT_NAME_OBJECT As String = "Object"
    Public Const ATTRIBUTE_NAME_INDEX As String = "Index"
    Public Const ELEMENT_NAME_FIELD As String = "Field"
    Public Const ELEMENT_NAME_ATTRIBUTE As String = "Attribute"
    Public Const ATTRIBUTE_NAME_TYPE As String = "Type"
    Public Const ATTRIBUTE_NAME_CREATE_DT As String = "CreateDt"
    Public Const ATTRIBUTE_NAME_СDB_LAST_UPDATE As String = "СdbLastUpdate"
    Public Const ATTRIBUTE_NAME_NAME As String = "Name"
    Public Const ATTRIBUTE_NAME_VALUE As String = "Value"
    Public Const ATTRIBUTE_NAME_IMAGEGREYSCALE As String = "ImageGrayscale"
    Public Const TYPE_NAME_IMAGEFILEPATH As String = "My.ImageFilePath"

    ''' <summary>
    ''' Сохранить list в файл XML.
    ''' </summary>
    ''' <param name="dataList"></param>
    ''' <param name="filePath"></param>
    ''' <param name="cdbLastUpdate">Дата последнего обновления данных в CDB, используется для проверки актуальности данных.</param>
    ''' <param name="bytesInverting">Инвертировать байты перед сохранением.</param>
    Public Shared Sub SaveList(dataList As IEnumerable(Of Object), filePath As String, cdbLastUpdate As Date,
                               Optional sourceImgToGrayscaleFieldName As String = "")
        ' Пост
        If dataList Is Nothing OrElse dataList.Count = 0 Then Exit Sub

        ' Удалить файл перед записью
        Dim dirPath = FileSystem.Path.GetParentDirPath(filePath)
        FileSystem.CreateDir(dirPath)
        FileSystem.DeleteFile(filePath, False)

        ' Инициализация
        Dim i As Integer = 0 ' Индекс объекта в листе
        Dim xDoc As New XDocument(New XElement(ELEMENT_NAME_LIST)) ' XML документ
        Dim properties As PropertyInfo() = dataList(0).GetType().GetProperties()
        xDoc.Element(ELEMENT_NAME_LIST).Add(New XAttribute(ATTRIBUTE_NAME_TYPE, dataList.GetType.Name)) ' Указать дату создания файла
        xDoc.Element(ELEMENT_NAME_LIST).Add(New XAttribute(ATTRIBUTE_NAME_CREATE_DT, Now)) ' Указать дату создания файла
        xDoc.Element(ELEMENT_NAME_LIST).Add(New XAttribute(ATTRIBUTE_NAME_СDB_LAST_UPDATE, cdbLastUpdate)) ' Указать дату последнего обновления данных в CDB

        For Each obj In dataList
            Dim objectIdPropertyInfo = properties.FirstOrDefault(Function(p) p.Name = NameOf(DbMetaObject.Id))
            Dim objectId = ValToInt(objectIdPropertyInfo.GetValue(obj))
            Dim xObj As New XElement(ELEMENT_NAME_OBJECT)
            xObj.Add(New XAttribute(ATTRIBUTE_NAME_INDEX, i)) : i += 1

            ' Определить существенные поля Модели, которые требуется сохранить (доступные для чтения и записи)
            Dim persistentProperties = properties.Where(
                Function(p)
                    Return p.CanRead And p.CanWrite And
                    Not p.GetAccessors(True)(0).IsStatic And ' пропустить Shared свойства класса
                    p.Name <> NameOf(DbMetaObject.MeOld) And
                    p.Name <> NameOf(DbMetaObject.IsMeOld)
                End Function)

            ' Перебрать существенные поля Модели
            For Each propertyInfo In persistentProperties
                'Dim isStatic = propertyInfo.GetAccessors(True)(0).IsStatic

                ' Описать поле экземпляра для XML
                Dim xField As New XElement(ELEMENT_NAME_FIELD)
                xField.Add(New XAttribute(ATTRIBUTE_NAME_NAME, propertyInfo.Name))

                Dim value = propertyInfo.GetValue(obj)
                If value IsNot Nothing Then
                    Select Case propertyInfo.PropertyType
                        Case GetType(Drawing.Image)
                            Dim img = CType(value, Drawing.Image)
                            If propertyInfo.Name = ATTRIBUTE_NAME_IMAGEGREYSCALE And Not String.IsNullOrEmpty(sourceImgToGrayscaleFieldName) Then
                                ' Создать изображение в оттенках серого, по данным sourceImgToGrayscaleFieldName
                                Dim propertyInfoSource = properties.FirstOrDefault(Function(p) p.Name = sourceImgToGrayscaleFieldName)
                                Dim imgSource = propertyInfoSource?.GetValue(obj)
                                img = Graphics.ImageToGrayscale(imgSource, Graphics.TILE_IMAGE_MAX_SIZE_224)
                            End If

                            ' Некоторые форматы картинок не удаётся созранить в Base64, по этому в худшем случае создаём файл и указываем его путь, как значение.
                            Dim wasException As Boolean = False ' Ошибка созранения в формате Base64

                            Try ' Пытаться сохранить в формате Base64
                                Log.Write($"{NameOf(objectId)}: {objectId} Path: {filePath}")
                                Dim imgBase64Value = Graphics.ImageToBase64(img)
                                xField.Add(New XAttribute(ATTRIBUTE_NAME_TYPE, propertyInfo.PropertyType))
                                xField.Add(New XAttribute(ATTRIBUTE_NAME_VALUE, imgBase64Value))
                            Catch ex As Exception
                                wasException = True
                                Log.Write(Log.Level.Trace, $"{ex}")
                            End Try

                            If wasException Then ' В худшем случае создаём файл и имя файла, как значение. Файл пишется в корневой каталог.
                                Dim imgFileName As String = $"{propertyInfo.Name}{objectId}.img"
                                Log.Write(Log.Level.Trace, $"Saving Image as file...")
                                Dim imgFilePath = FileSystem.Path.Combine(dirPath, imgFileName)
                                FileSystem.SaveFile(Graphics.ImageToBytes(img), FileSystem.Path.Combine(dirPath, imgFileName))
                                xField.Add(New XAttribute(ATTRIBUTE_NAME_TYPE, TYPE_NAME_IMAGEFILEPATH))
                                xField.Add(New XAttribute(ATTRIBUTE_NAME_VALUE, imgFileName))
                            End If

                        Case Else ' Остальные пишем как есть
                            xField.Add(New XAttribute(ATTRIBUTE_NAME_TYPE, propertyInfo.PropertyType))
                            xField.Add(New XAttribute(ATTRIBUTE_NAME_VALUE, value))
                    End Select

                Else
                    ' Пустые значения не записываем
                End If
                xObj.Add(xField)
            Next
            xDoc.Element(ELEMENT_NAME_LIST).Add(xObj)
        Next

        xDoc.Save(filePath) ' Сохранить документ в файл
    End Sub

    ''' <summary>
    ''' Сохранить list в файл XML.
    ''' </summary>
    ''' <param name="dataList"></param>
    ''' <param name="dirPath"></param>
    ''' <param name="fileName"></param>
    ''' <param name="cdbLastUpdate">Дата последнего обновления данных в CDB, используется для проверки актуальности данных.</param>
    Public Shared Sub SaveList(dataList As IEnumerable(Of Object), dirPath As String, fileName As String,
                               cdbLastUpdate As Date,
                               Optional ByVal sourceImgToGrayscaleFieldName As String = "")
        Dim filePath = FileSystem.Path.Combine(dirPath, fileName)
        SaveList(dataList, filePath, cdbLastUpdate, sourceImgToGrayscaleFieldName)
    End Sub

    ''' <summary>
    ''' Загрузить данные в list из файла XML.
    ''' Наполняется лист, указанный в параметре.
    ''' </summary>
    ''' <param name="dataList"></param>
    ''' <param name="filePath"></param>
    Public Shared Function LoadList(Of T)(ByRef dataList As List(Of Object),
                                          filePath As String) As List(Of Object)
        dataList = LoadList(Of T)(filePath)
        Return dataList
    End Function

    ''' <summary>
    ''' Загрузить данные в list из файла XML.
    ''' </summary>
    ''' <param name="filePath"></param>
    Public Shared Function LoadList(documentType As Type,
                                    filePath As String) As List(Of Object)
        Dim dataList As New List(Of Object)
        If Not FileSystem.FileExists(filePath) Then Return dataList

        ' Инициализация
        Dim dirPath = FileSystem.Path.GetParentDirPath(filePath)
        Dim xDoc As XDocument ' XML документ
        Dim properties As PropertyInfo() = documentType.GetProperties()
        Dim propertyInfo As PropertyInfo
        xDoc = XDocument.Load(filePath) ' Читать файл

        ' Перебрать все объекты в списке
        For Each xObj In xDoc.Elements(ELEMENT_NAME_LIST).Elements(ELEMENT_NAME_OBJECT)
            ' Создать новый экземпляр объекта
            Dim document = Activator.CreateInstance(documentType)

            ' Перебрать поля экземпляра
            For Each xField In xObj.Elements(ELEMENT_NAME_FIELD)
                ' Если есть запись о значении поля
                If xField.Attribute(ATTRIBUTE_NAME_VALUE) IsNot Nothing Then
                    ' Получить значения полей
                    Dim fieldName = xField.Attribute(ATTRIBUTE_NAME_NAME)?.Value
                    Dim fieldValue = xField.Attribute(ATTRIBUTE_NAME_VALUE).Value
                    propertyInfo = properties.FirstOrDefault(Function(p) p.Name = fieldName)

                    ' Пост
                    If propertyInfo Is Nothing Then
                        Throw New Exception($"Field '{fieldName}' of class instance '{documentType.Name}' not found!")
                        Continue For
                    End If

                    Select Case propertyInfo.PropertyType
                        Case GetType(Drawing.Image)
                            ' Картинка может быть сохранена в одном из форматов: Base64 или как файл. Формат проверяем по типу.
                            Dim img As Drawing.Image
                            Dim fieldValueType = xField.Attribute(ATTRIBUTE_NAME_TYPE)?.Value
                            Debug.Print($"fieldName: {fieldName}, fieldValueType: {fieldValueType}")
                            If fieldValueType = TYPE_NAME_IMAGEFILEPATH Then
                                Debug.Print($"Read image from file: {fieldValue}")
                                Dim imageFilePath = FileSystem.Path.Combine(dirPath, fieldValue)
                                Dim bFile = FileSystem.LoadFileAsByte(imageFilePath)
                                img = Graphics.BytesToImage(bFile)
                            Else ' Base64
                                Debug.Print($"Read image from field value.")
                                img = Graphics.Base64ToImage(fieldValue)
                            End If
                            propertyInfo.SetValue(document, img)

                        Case Else ' Остальные пишем как есть
                            propertyInfo.SetValue(document, Cast(fieldValue, propertyInfo.PropertyType))
                    End Select
                Else
                    ' Пустые поля не читаем
                End If
            Next

            dataList.Add(document)
        Next

        Return dataList
    End Function

    ''' <summary>
    ''' Загрузить данные в list из файла XML.
    ''' </summary>
    ''' <param name="filePath"></param>
    Public Shared Function LoadList(Of T)(filePath As String) As List(Of Object)
        Return LoadList(GetType(T), filePath)
    End Function

    ''' <summary>
    ''' Загрузить данные в list из файла XML.
    ''' Наполняется лист, указанный в параметре.
    ''' </summary>
    ''' <param name="dataList"></param>
    ''' <param name="dirPath"></param>
    ''' <param name="fileName"></param>
    Public Shared Function LoadList(Of T)(ByRef dataList As List(Of T),
                                          dirPath As String,
                                          fileName As String) As List(Of T)
        Dim filePath = FileSystem.Path.Combine(dirPath, fileName)
        dataList = LoadList(GetType(T), filePath).OfType(Of T).ToList()
        Return dataList
    End Function

#Region "Process String Array"
    ''' <summary>
    ''' Сохранить Array в файл XML.
    ''' </summary>
    ''' <param name="strings"></param>
    ''' <param name="filePath"></param>
    Public Shared Sub SaveArray(strings As String(), filePath As String)
        ' Пост
        If strings Is Nothing OrElse strings.Count() = 0 Then Exit Sub

        ' Удалить файл перед записью
        Dim dirPath = FileSystem.Path.GetParentDirPath(filePath)
        FileSystem.CreateDir(dirPath)
        FileSystem.DeleteFile(filePath, False)

        ' Инициализация
        Dim xDoc As New XDocument(New XElement(ELEMENT_NAME_LIST)) ' XML документ
        xDoc.Element(ELEMENT_NAME_LIST).Add(New XAttribute(ATTRIBUTE_NAME_TYPE, GetType(String()).Name))
        xDoc.Element(ELEMENT_NAME_LIST).Add(New XAttribute(ATTRIBUTE_NAME_CREATE_DT, Now)) ' Указать дату создания файла

        For Each value In strings
            Dim xObj As New XElement(ELEMENT_NAME_OBJECT)
            xObj.Add(New XAttribute(ATTRIBUTE_NAME_VALUE, value))
            xDoc.Element(ELEMENT_NAME_LIST).Add(xObj)
        Next

        xDoc.Save(filePath) ' Сохранить документ в файл
    End Sub

    ''' <summary>
    ''' Загрузить данные в Array из файла XML.
    ''' </summary>
    ''' <param name="filePath"></param>
    Public Shared Function LoadArray(filePath As String) As String()
        If Not FileSystem.FileExists(filePath) Then Return Nothing

        ' Инициализация
        Dim dirPath = FileSystem.Path.GetParentDirPath(filePath)
        Dim xDoc As XDocument ' XML документ
        xDoc = XDocument.Load(filePath) ' Читать файл
        Dim strings As New List(Of String)

        ' Перебрать все объекты в списке
        For Each xObj In xDoc.Elements(ELEMENT_NAME_LIST).Elements(ELEMENT_NAME_OBJECT)
            strings.Add(xObj.Attribute(ATTRIBUTE_NAME_VALUE).Value)
        Next

        Return strings.ToArray()
    End Function
#End Region
End Class
