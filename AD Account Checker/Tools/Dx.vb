Imports DevExpress.Utils
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout.Utils
Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools
Imports System.Reflection

''' <summary>
''' Инструменты завязанные на DX, которые на этом основании не могут распологаться в Agent.
''' </summary>
Public NotInheritable Class Dx
	''' <summary>
	''' Основной и единственный экземпляр OFD для всего проекта.
	''' </summary>
	Public Shared ReadOnly OpenFileDialog As New XtraOpenFileDialog

	''' <summary>
	''' Основной и единственный экземпляр SFD для всего проекта.
	''' </summary>
	Public Shared ReadOnly SaveFileDialog As New XtraSaveFileDialog

	''' <summary>
	''' Основной и единственный экземпляр FBD для всего проекта.
	''' </summary>
	Public Shared ReadOnly FolderBrowserDialog As New XtraFolderBrowserDialog With {
			.RootFolder = System.Environment.SpecialFolder.MyComputer}

	''' <summary>
	''' Настройки по умолчанию для GridView.
	''' </summary>
	''' <param name="gv"></param>
	''' <param name="allowPixelScrolling">Влияет на возможность отображения Master-Detail.</param>
	Public Shared Sub SetDefaultSettingsForGridView(gv As GridView,
				Optional allowPixelScrolling As Boolean? = True,
				Optional allowRowSizing As Boolean = True,
				Optional selectorColore As KnownColor = KnownColor.ForestGreen)
		gv.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False ' Запретить удаление строк по Delete
		'gv.OptionsSelection.MultiSelect = True
		'gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
		'gv.OptionsView.NewItemRowPosition = NewItemRowPosition.None
		gv.OptionsCustomization.AllowRowSizing = allowRowSizing ' Разрешить изменять высоту строк
		gv.OptionsView.ShowFooter = False ' Отключить ногу
		gv.OptionsView.EnableAppearanceEvenRow = True ' Полосатость таблиц
		gv.OptionsView.ShowGroupPanel = False ' Отключить панель группировки
		gv.OptionsBehavior.AllowPixelScrolling = CastDefaultBoolean(allowPixelScrolling)  ' Плавный скролл
		gv.OptionsView.ShowViewCaption = False ' Скрыть заголовок
		gv.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown ' Выделяет содержимое ячейки при получении фокуса
		gv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways ' Показать конструктор фильтра
		gv.OptionsFind.AlwaysVisible = True
		gv.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.NeverAnimate ' Иначе при завершении работы падает исключение.
		gv.OptionsMenu.ShowConditionalFormattingItem = True ' Показать меню форматирования

		' Раскрасить селектор
		Dim lightBlack = _Graphic.GetClarifierColor(Color.Black, 1, 60)
		Dim lightForest = _Graphic.GetClarifierColor(Color.FromKnownColor(selectorColore), 1.18, 40)
		gv.Appearance.Row.ForeColor = lightBlack
		gv.Appearance.FocusedRow.ForeColor = Color.Black
		gv.Appearance.FocusedRow.BackColor = lightForest
		gv.Appearance.FocusedRow.BackColor2 = Color.Transparent

		gv.Appearance.FocusedCell.ForeColor = Color.Black
		gv.Appearance.FocusedCell.BackColor = Color.FromArgb(200, 180, 255, 185)
		gv.Appearance.FocusedCell.BorderColor = Color.FromArgb(0, 180, 255, 190)

		gv.Appearance.SelectedRow.ForeColor = Color.Black
		gv.Appearance.SelectedRow.BackColor = lightForest
		gv.Appearance.SelectedRow.BackColor2 = Color.Transparent

		gv.Appearance.HideSelectionRow.ForeColor = Color.Black
		gv.Appearance.HideSelectionRow.BackColor = lightForest
		gv.Appearance.HideSelectionRow.BackColor2 = Color.Transparent

		' BestFitColumns выполняется синхронно, после установки источника данных.
		AddHandler gv.DataSourceChanged, Sub() gv.BestFitColumns() ' Авто выравнивание ширины колонок.
	End Sub

	''' <summary>
	''' Определить GV Selector Appearance.
	''' </summary>
	''' <param name="gv"></param>
	''' <param name="focusedRowColore"></param>
	''' <param name="selectedRowColore"></param>
	Public Shared Sub SetDefaultSettingsForGridViewSelector(gv As GridView,
				Optional focusedRowColore As KnownColor = KnownColor.ForestGreen,
				Optional selectedRowColore As KnownColor = KnownColor.ForestGreen)
		' Раскрасить селектор
		Dim lightBlack = _Graphic.GetClarifierColor(Color.Black, 1, 60)
		Dim focusedRowLightForest = _Graphic.GetClarifierColor(Color.FromKnownColor(focusedRowColore), 1.18, 40)
		Dim selectedRowLightForest = _Graphic.GetClarifierColor(Color.FromKnownColor(selectedRowColore), 1.18, 40)
		gv.Appearance.Row.ForeColor = lightBlack
		gv.Appearance.FocusedRow.ForeColor = Color.Black
		gv.Appearance.FocusedRow.BackColor = focusedRowLightForest
		gv.Appearance.FocusedRow.BackColor2 = Color.Transparent

		gv.Appearance.FocusedCell.ForeColor = Color.Black
		gv.Appearance.FocusedCell.BackColor = Color.FromArgb(200, 180, 255, 185)
		gv.Appearance.FocusedCell.BorderColor = Color.FromArgb(0, 180, 255, 190)

		gv.Appearance.SelectedRow.ForeColor = Color.Black
		gv.Appearance.SelectedRow.BackColor = selectedRowLightForest
		gv.Appearance.SelectedRow.BackColor2 = Color.Transparent

		gv.Appearance.HideSelectionRow.ForeColor = Color.Black
		gv.Appearance.HideSelectionRow.BackColor = selectedRowLightForest
		gv.Appearance.HideSelectionRow.BackColor2 = Color.Transparent

		gv.Appearance.HotTrackedRow.ForeColor = Color.Black
		gv.Appearance.HotTrackedRow.BackColor = selectedRowLightForest
		gv.Appearance.HotTrackedRow.BackColor2 = Color.Transparent

		' BestFitColumns выполняется синхронно, после установки источника данных.
		AddHandler gv.DataSourceChanged, Sub() gv.BestFitColumns() ' Авто выравнивание ширины колонок.
	End Sub

	''' <summary>
	''' Прописать внешние репозитории. Обратить внимание на репозитории события CustomRowCellEdit!
	''' Важный ритуал DX: https://supportcenter.devexpress.com/ticket/details/q387748/repository-question
	''' Команда поддержки DX сообщает: Если вы не добавите элемент репозитория в эту коллекцию,
	''' в большинстве случаев элемент управления будет работать правильно.
	''' Однако нам сообщили о проблемах, связанных с этим. Таким образом, 
	''' безопаснее добавить элемент репозитория в один из контейнеров.
	''' </summary>
	''' <param name="gc"></param>
	Public Shared Sub AddExternalRepsitories(gc As GridControl, repositoryItems As RepositoryItem())
		If repositoryItems Is Nothing Then Exit Sub

		Dim externalRepository As New PersistentRepository()
		externalRepository.Items.AddRange(repositoryItems)
		gc.ExternalRepository = externalRepository
	End Sub

	''' <summary>
	''' Добавить в контейнер GC все репозитории подвязанные к колонкам (важный ритуал DX).
	''' Предварительно добавленные тоже сохраняются.
	''' Динамические репозитории события CustomRowCellEdit должны быть добавлены вручную!
	''' </summary>
	''' <param name="gc"></param>
	Public Shared Sub AddInternalRepsitories(gc As GridControl, Optional repositoryItems As RepositoryItem() = Nothing)
		If repositoryItems IsNot Nothing Then
			gc.BeginInit()
			For Each item In repositoryItems
				If item IsNot Nothing AndAlso Not gc.RepositoryItems.Contains(item) Then
					Debug.Print($"GridControl.Name: {gc.Name}, New Repository added: {item.Name}")
					gc.RepositoryItems.Add(item)
				End If
			Next
			gc.EndInit()
		Else
			For Each gv As GridView In gc.Views
				AddInternalRepsitories(gv)
			Next
		End If
	End Sub

	''' <summary>
	''' Добавить в контейнер GC все репозитории подвязанные к колонкам (важный ритуал DX).
	''' Предварительно добавленные тоже сохраняются.
	''' Динамические репозитории события CustomRowCellEdit должны быть добавлены вручную!
	''' </summary>
	''' <param name="gv"></param>
	Public Shared Sub AddInternalRepsitories(gv As GridView)
		Dim gc = gv.GridControl
		Log.Write($"Call, GridView.Name: {gv.Name}, gc.RepositoryItems.Count: {gc.RepositoryItems.Count}")
		Dim existingRepositories As New List(Of RepositoryItem)
		Dim newRepositories As New List(Of RepositoryItem)
		If gc.RepositoryItems?.Count > 0 Then ' Помнить репозитории добавленые ранее.
			For Each item As RepositoryItem In gc.RepositoryItems
				existingRepositories.Add(item)
			Next
		End If

		For Each column As GridColumn In gv.Columns
			If column.ColumnEdit IsNot Nothing AndAlso Not existingRepositories.Contains(column.ColumnEdit) Then
				Debug.Print($"GridView.Name: {gv.Name}, New Repository added: {column.ColumnEdit.Name}")
				newRepositories.Add(column.ColumnEdit)
			End If
		Next

		If newRepositories.Count > 0 Then
			Debug.Print($"GridView.Name: {gv.Name}, New Repositories Count: {newRepositories.Count}")
			gc.BeginInit()
			gc.RepositoryItems.AddRange(newRepositories.Distinct().ToArray())
			gc.EndInit()
		End If
	End Sub

	''' <summary>
	''' Запретить редактирование GV
	''' </summary>
	''' <param name="gv"></param>
	Public Shared Sub GridViewLock(gv As GridView)
		gv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.[False]
		gv.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None
		gv.OptionsBehavior.ReadOnly = True
		gv.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False
	End Sub

	Public Shared Function CastLayoutVisibility(value As Boolean?) As LayoutVisibility
		If value Then
			Return LayoutVisibility.Always
		Else
			Return LayoutVisibility.Never
		End If
	End Function

	Public Shared Function CastDefaultBoolean(value As Boolean?) As DefaultBoolean
		If value Is Nothing Then
			Return DefaultBoolean.Default
		ElseIf value Then
			Return DefaultBoolean.True
		Else
			Return DefaultBoolean.False
		End If
	End Function

	Public Shared Function CastBarItemVisibility(value As Boolean?) As BarItemVisibility
		If value Then
			Return BarItemVisibility.Always
		Else
			Return BarItemVisibility.Never
		End If
	End Function

	Public Shared Function CastCheckState(value As Boolean?) As CheckState
		If value Then
			Return CheckState.Checked
		Else
			Return CheckState.Unchecked
		End If
	End Function

	''' <summary>
	''' Построить представление выделенной строки таблицы.
	''' </summary>
	''' <param name="obj"></param>
	''' <param name="RowHandle"></param>
	''' <returns></returns>
	Public Shared Function PreviewGridViewRow(obj As GridView, RowHandle As Integer) As String
		If obj.SelectedRowsCount < 1 Then Return "NA"
		Dim s As String = String.Empty
		For Each col As DevExpress.XtraGrid.Columns.GridColumn In obj.Columns
			If s.Length > 0 Then s &= ", "
			If IsNothing(obj.GetRowCellValue(RowHandle, col.FieldName)) Then
				s &= "NA"
			Else
				s &= "<" & obj.GetRowCellValue(RowHandle, col.FieldName).ToString() & ">"
			End If
		Next
		Return s
	End Function

	''' <summary>
	''' Проверить, отображается ли подсказка в данный момент.
	''' </summary>
	''' <param name="controller"></param>
	Public Shared Function IsHintVisible(controller As DevExpress.Utils.ToolTipController) As Boolean
		'Private bool IsHintVisible(ToolTipController controller)  
		'{  
		'    System.Reflection.FieldInfo fi = TypeOf(ToolTipController).GetField("toolWindow", BindingFlags.NonPublic | BindingFlags.Instance);  
		'    DevExpress.Utils.Win.ToolTipControllerBaseWindow window = fi.GetValue(controller) as DevExpress.Utils.Win.ToolTipControllerBaseWindow;  
		'    Return window! = null && !window.IsDisposed && window.Visible;  
		'} 
		Dim fi = controller.GetType.GetField("toolWindow", BindingFlags.NonPublic Or BindingFlags.Instance)
		Dim window = TryCast(fi.GetValue(controller), DevExpress.Utils.Win.ToolTipControllerBaseWindow)
		Return window IsNot Nothing AndAlso Not window.IsDisposed AndAlso window.Visible
	End Function

#Region "GridView Format Rules"
	''' <summary>
	''' Добавить правила форматирования строк.
	''' Требуется чтобы тег содержал DboCodeName.
	''' </summary>
	''' <param name="gv"></param>
	''' <param name="column"></param>
	''' <param name="value"></param>
	Public Shared Sub AddFormatRules(gv As GridView)
		'Select Case ValToStr(gv.Tag)
		'	Case User.DboCodeName
		'		' Правило "Подсвечивать новичков зеленым"
		'		Dim fcrv As New FormatConditionRuleValue With {.Condition = FormatCondition.Expression}
		'		Dim fieldName = NameOf(User.RoleId)
		'		fcrv.Expression = $"[{fieldName}] = {Role.NEWBIE_ID}"
		'		fcrv.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		'		fcrv.Appearance.ForeColor = System.Drawing.Color.DimGray
		'		fcrv.Appearance.BackColor = System.Drawing.Color.LightGreen
		'		AddFormatRule(gv, fcrv, fieldName, NameOf(Role.NEWBIE_ID))

		'		' Правило "Подсвечивать отключенных серым"
		'		Dim fcrv2 As New FormatConditionRuleValue With {.Condition = FormatCondition.Equal}
		'		fcrv2.Value1 = Role.OFF_ID
		'		fcrv2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		'		fcrv2.Appearance.ForeColor = System.Drawing.Color.Gray
		'		AddFormatRule(gv, fcrv2, fieldName, NameOf(Role.OFF_ID))

		'	Case News.DboCodeName
		'		' Правило "Подсвечивать не прочитанные новости"
		'		Dim fcrv As New FormatConditionRuleValue With {.Condition = FormatCondition.NotEqual}
		'		Dim fieldName = NameOf(News.WasRead)
		'		fcrv.Value1 = True
		'		fcrv.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		'		fcrv.Appearance.ForeColor = System.Drawing.Color.Black
		'		fcrv.Appearance.BackColor = System.Drawing.Color.LightGreen
		'		AddFormatRule(gv, fcrv, fieldName, NameOf(News.WasRead))

		'	Case SessionActive.DboCodeName
		'		' Правило "Подсвечивать активные сессии"
		'		Dim fcrv As New FormatConditionRuleValue With {.Condition = FormatCondition.Equal}
		'		Dim fieldName = NameOf(SessionActive.Status)
		'		fcrv.Value1 = "running"
		'		fcrv.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		'		fcrv.Appearance.ForeColor = System.Drawing.Color.Black
		'		fcrv.Appearance.BackColor = System.Drawing.Color.LightGreen
		'		AddFormatRule(gv, fcrv, fieldName, fcrv.Value1.ToString())

		'		' Правило "Подсвечивать спящие сессии"
		'		Dim fcrv2 As New FormatConditionRuleValue With {.Condition = FormatCondition.Equal}
		'		fcrv2.Value1 = "sleeping"
		'		fcrv2.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		'		fcrv2.Appearance.ForeColor = System.Drawing.Color.DimGray
		'		fcrv2.Appearance.BackColor = System.Drawing.Color.LightBlue
		'		AddFormatRule(gv, fcrv2, fieldName, fcrv.Value1.ToString())
		'End Select
	End Sub

	''' <summary>
	''' Добавить форматирование логически удалённых строк.
	''' </summary>
	Public Shared Sub AddFormatRuleLogicalRemove(gv As GridView)
		'' Правило "Подсвечивать отключенных серым"
		'Dim fcrv As New FormatConditionRuleValue With {.Condition = FormatCondition.Expression}
		'Dim fieldName = NameOf(ILogicalRemove.DeletedDt)
		'fcrv.Expression = $"[{fieldName}] Is Not Null"
		'fcrv.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		'fcrv.Appearance.ForeColor = System.Drawing.Color.DimGray
		'fcrv.Appearance.BackColor = System.Drawing.Color.LightGray
		'AddFormatRule(gv, fcrv, fieldName, NameOf(ILogicalRemove))
	End Sub

	''' <summary>
	''' Добавить правило форматирования.
	''' </summary>
	''' <param name="gv"></param>
	''' <param name="fcrv"></param>
	''' <param name="fieldName"></param>
	Public Shared Sub AddFormatRule(gv As GridView, fcrv As FormatConditionRuleValue, fieldName As String, ruleNameSuffix As String)
		Dim column = gv.Columns(fieldName)
		fcrv.Appearance.Options.UseFont = True
		fcrv.Appearance.Options.UseBackColor = True
		fcrv.Appearance.Options.UseForeColor = True
		fcrv.Appearance.Options.UseTextOptions = True

		' Найти одноимённое правило
		Dim ruleName = $"Format_{fieldName}_{ruleNameSuffix}"
		Dim oldRule = gv.FormatRules?.FirstOrDefault(Function(r) r.Name = ruleName)
		If oldRule IsNot Nothing Then Exit Sub ' Пропустить, если правило создано ранее или было измененно пользователем
		If oldRule IsNot Nothing Then gv.FormatRules.Remove(oldRule) ' Удалить старое правило

		Dim gfr As New GridFormatRule With {
					.Name = ruleName,
					.Rule = fcrv,
					.StopIfTrue = True, ' Останосить обработку других правил, если применено это
					.Column = column,
					.ApplyToRow = True}
		gv.FormatRules.Add(gfr)
	End Sub

	''' <summary>
	''' Клонировать правила
	''' </summary>
	''' <param name="gfr"></param>
	''' <returns></returns>
	Public Shared Function CloneGfr(ByVal gfr As GridFormatRule) As GridFormatRule
		' Использовать серый цвет шрифта для нулевых значений
		'Dim fcrv As New FormatConditionRuleValue With {.Condition = FormatCondition.Between, .Value1 = "0"}
		Dim fcrv As New FormatConditionRuleValue With {.Condition = FormatCondition.Expression, .Expression = "[" & gfr.Column.FieldName & "] In ('0','','-1')"}

		fcrv.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
		fcrv.Appearance.ForeColor = System.Drawing.Color.DarkGray
		fcrv.Appearance.Options.UseFont = True
		fcrv.Appearance.Options.UseForeColor = True
		fcrv.Appearance.Options.UseTextOptions = True
		fcrv.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
		fcrv.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

		gfr.StopIfTrue = True 'Останосить обработку других правил, если применено это
		gfr.Rule = fcrv : Return gfr
	End Function
#End Region

End Class
