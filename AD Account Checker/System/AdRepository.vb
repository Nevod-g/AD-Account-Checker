Public NotInheritable Class AdRepository
    ''' <summary>
    ''' Список добавленных пользователем Контроллеров Домена.
    ''' </summary>
    Public Shared AdControllers As New List(Of AdController)

    ''' <summary>
    ''' Верхний уровень иерархии данных Directory Entries.
    ''' </summary>
    Public Shared AdObjects As New List(Of AdObject)

    ''' <summary>
    ''' отмеченные пользователем Directory Entries.
    ''' </summary>
    Public Shared CheckedAdObjects As New List(Of AdObject)
End Class
