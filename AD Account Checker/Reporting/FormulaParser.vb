Imports ProcessBank.Agent
Imports ProcessBank.Agent.Tools
Imports System.Reflection

Namespace Reporting

	''' <summary>
	''' Контейнер методов для парсинга аргументов функций RTS.
	''' https://malashkin.atlassian.net/wiki/spaces/PB/pages/154927407
	''' </summary>
	Public Class FormulaParser

#Region "Formula separation to args"
		''' <summary>
		''' Получить список всех вычисляемых аргументов [arg] из формулы.
		''' Input: formula = "Тест [[a1] = [a2]] + [2] @ [3] слово [iif([4] = 2, [5], [6])]", 
		''' Output: list([[a1] = [a2]], [2], [3], [iif([4] = 2, [5], [6])])
		''' </summary>
		''' <param name="formula"></param>
		''' <returns> Лист аргументов для последующего вычисления и подстановки значений. </returns>
		Public Shared Function GetFormulaArguments(formula As String) As List(Of String)
			' Пост
			Dim args As New List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return args
			If IsNumeric(formula) Then Return args

			Dim beginBracketIndex As Integer = 0
			Dim endBracketIndex As Integer

			Do
				' Найти открывающую скобку
				beginBracketIndex = InStr(endBracketIndex + 1, formula, "[", CompareMethod.Text)
				If beginBracketIndex = 0 Then Return args
				If beginBracketIndex + 1 >= formula.Length Then Return args ' Нарушение структуры

				' Проверить, есть ли хотя бы одна закрывающая скобка
				If InStr(beginBracketIndex + 1, formula, "]", CompareMethod.Text) = 0 Then Return args

				' Найти закрывающую скобку, агрументы могут быть вложенными, например: [iif([4] = 2, [5], [6])] - читаем как один аргумент
				Dim nestingLevel = 0
				For i = beginBracketIndex To formula.Length - 1 ' Перебрать хвост формулы
					Dim c = formula.Chars(i)
					If c = "[" Then nestingLevel += 1
					If c = "]" Then nestingLevel -= 1
					If c = "]" And nestingLevel = -1 Then ' Найдена закрывающая скобка на нужном уровне вложенности
						endBracketIndex = i + 1
						Exit For
					End If
				Next
				If endBracketIndex = 0 Then Return args

				' Добавить аргумент в лист, если он имеет значение
				Dim argLength = endBracketIndex - beginBracketIndex + 1
				If argLength > 2 Then ' Нечто большее чем []
					Dim arg = Mid(formula, beginBracketIndex, argLength)
					If arg.Length > 2 Then args.Add(arg)
				End If

			Loop While endBracketIndex + 1 < formula.Length

			Return args
		End Function

		''' <summary>
		''' Возвращает набор аргументов для функции Not.
		''' Если в формуле обнаружены признаки этой конструкции.
		''' </summary>
		''' <param name="formula"> Формула аргумента, для поиска признаков использования данной функции. </param>
		''' <returns> Набор аргументов функции. </returns>
		Public Shared Function GetArgsForNot(formula As String) As List(Of String)
			' Пост
			Dim key = "[not "
			Dim args = New List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return args
			If InStr(LCase(formula), key) <> 1 Then Return args ' key
			If formula.Length < key.Length + 2 Then Return args

			' Вырезать один аргумент до закрывающейся скобки [not arg1]
			Dim dirtyArg = Right(formula, formula.Length - key.Length) ' arg1]
			Dim argLength = GetCharIndexOnLevel(dirtyArg, "]"c, -1)
			If argLength < 1 Then Return args
			Dim arg = Mid(formula, key.Length + 1, argLength)

			' Заключить аргумент в страховочные скобки для вычисления его значения,
			' это позволяет в целом писать меньше скобок в RTS.
			' Страховочные скобки снимаются на выходе.
			If arg.First() <> "["c Then arg = $"[{arg}]"

			args.Add(arg)
			Return args
		End Function

		''' <summary>
		''' Возвращает набор аргументов для функции [arg1 In (arg2, arg3, ..., argN)].
		''' Если в формуле обнаружены признаки этой конструкции.
		''' </summary>[1 in (1, 2)]
		''' <param name="formula"> Формула аргумента, для поиска признаков использования данной функции. </param>
		''' <returns> Набор аргументов функции. </returns>
		Public Shared Function GetArgsForIn(formula As String) As List(Of String)
			' Пост
			Dim key = " in ("
			Dim args = New List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return args
			Dim fTrim = formula.Trim()
			If fTrim.First() <> "["c Then Return args
			Dim keyIndex = GetTextIndexOnLevel(LCase(fTrim), key, 1) + 1
			If keyIndex < 3 Then Return args
			If formula.Length < 13 Then Return args ' Минимум: [1 in (1, 2)]

			' Вырезать первый аргумент, arg1 = 1
			args.Add(Mid(fTrim, 2, keyIndex - 2))


			Dim endBracketIndex = InStr(keyIndex + key.Length, formula, ")")

			' Вырезать перечисление '(1, 2) dirty'
			Dim dirtyArg = Right(formula, formula.Length - key.Length - keyIndex + 1) ' 1, 2)]
			' Найти конец ')' содержимого перечисления '(1, 2) dirty'
			Dim argLength = GetCharIndexOnLevel(dirtyArg, ")"c, 0) ' 1, 2)]
			If argLength < 1 Then Return args
			If argLength < dirtyArg.Length - 2 Then Return args ' символ ')' НЕ является последним или предпоследним  
			Dim sEnumeration = Mid(formula, keyIndex + key.Length, argLength)
			Dim enumeration() As String = Split(sEnumeration, ", ")

			' Заключить аргумент в страховочные скобки для вычисления его значения,
			' это позволяет в целом писать меньше скобок в RTS.
			' Страховочные скобки снимаются на выходе.
			args.AddRange(enumeration)

			Return args
		End Function

		''' <summary>
		''' Возвращает набор аргументов формулы конструкции: 
		''' [iif([arg1] = [arg2], [arg3], [arg4])]
		''' [iif([IsNullOrEmpty([arg0])], [arg1])]
		''' Если в формуле обнаружены признаки данной конструкции.
		''' </summary>
		''' <param name="formula">Формула аргумента, для поиска признаков использования данной функции.</param>
		''' <returns> Набор аргументов функции. </returns>
		Public Shared Function GetArgsForIif(formula As String) As List(Of String)
			' Пост
			Dim args = New List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return args
			If InStr(LCase(formula), "[iif(") <> 1 Then Return args ' key
			If formula.Length < 12 Then Return args

			' Парсить на аргументы. Формула должна содержать, как минимум два аргумента: условие и значение для True,
			' например, [iif(1 = 1, Yeah)]
			Dim bracketIndex = InStr(LCase(formula), "(")
			If bracketIndex < 4 Or bracketIndex > 5 Then Return args ' Нарушение конструкции, нет iif(body)
			Dim filling = Mid(formula, bracketIndex + 1, formula.Length - bracketIndex - 2) ' Усечь имя функции
			Dim comma = CChar(",")
			Dim splitByComma = FormulaParser.SplitBy(filling, comma)

			If splitByComma.Count > 1 And splitByComma.Count < 4 Then ' Два или три аргумента через запятую
				args.Add(Trim(splitByComma(0)))
				args.Add(Trim(splitByComma(1)))

				' Добавить третий аргумент, если он не был введён
				If splitByComma.Count = 3 Then args.Add(Trim(splitByComma(2))) Else args.Add(String.Empty)
			End If
			Return args
		End Function

		''' <summary>
		''' Возвращает набор аргументов формулы конструкции: [IsNullOrEmpty([arg1])]
		''' Если в формуле обнаружены признаки данной конструкции.
		''' </summary>
		''' <param name="formula"> Формула аргумента, для поиска признаков использования данной функции. </param>
		''' <returns> Набор аргументов функции. </returns>
		Public Shared Function GetArgsForIsNullOrEmpty(formula As String) As List(Of String)
			' Пост
			Dim args = New List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return args
			If InStr(LCase(formula), "[isnullorempty(") <> 1 Then Return args ' key
			If formula.Length < 17 Then Return args ' минимум: [IsNullOrEmpty()]
			If formula.Length = 17 Then args.Add(Nothing) : Return args ' [IsNullOrEmpty()]

			' Парсить на аргументы. Формула должна содержать, только один аргумент
			Dim bracketIndex = InStr(LCase(formula), "(")
			If bracketIndex < 14 Or bracketIndex > 15 Then Return args ' Нарушение конструкции, isnullorempty(body)
			Dim filling = Mid(formula, bracketIndex + 1, formula.Length - bracketIndex - 2) ' Усечь имя функции
			args.Add(Trim(filling))

			Return args
		End Function

		''' <summary>
		''' Возвращает набор аргументов формулы конструкции: "[coalesce(arg1, arg2, …, argN)]"
		''' Аргументов может быть множество.
		''' Если в формуле обнаружены признаки данной конструкции.
		''' </summary>
		''' <param name="formula"> Формула аргумента, для поиска признаков использования данной функции. </param>
		''' <returns> Набор аргументов функции. </returns>
		Public Shared Function GetArgsForCoalesce(formula As String) As List(Of String)
			' Пост
			Dim args = New List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return args
			If InStr(LCase(formula), "[isnull(") = 0 And InStr(LCase(formula), "[coalesce(") = 0 Then Return args
			If InStr(LCase(formula), ",") = 0 Then Return args
			If formula.Length < 12 Then Return args

			' Парсить на аргументы.
			Dim bracketIndex = InStr(LCase(formula), "(")
			If bracketIndex = 0 Then Return args
			Dim filling = Mid(formula, bracketIndex + 1, formula.Length - bracketIndex - 2) ' Усечь имя функции
			Dim comma = CChar(",")
			args = FormulaParser.SplitBy(filling, comma)
			If args.Count < 2 Then Return args ' Аргументов меньше, чем должно быть
			Return TrimList(args)
		End Function

		''' <summary>
		''' Возвращает набор аргументов формулы конструкции: "[isnull(arg1, arg2)]"
		''' Если в формуле обнаружены признаки данной конструкции.
		''' </summary>
		''' <param name="formula"> Формула аргумента, для поиска признаков использования данной функции. </param>
		''' <returns> Набор аргументов функции. </returns>
		Public Shared Function GetArgsForIsNull(formula As String) As List(Of String)
			If String.IsNullOrWhiteSpace(formula) Then Return New List(Of String)
			If InStr(LCase(formula), "[isnull(") = 0 Then Return New List(Of String)
			If InStr(LCase(formula), ",") = 0 Then Return New List(Of String)
			If formula.Length < 12 Then Return New List(Of String)
			Return GetArgsForCoalesce(formula)
		End Function


		''' <summary>
		''' Выполнить сплит по символу на одном уровне квадратных скобок.
		''' Аргументы могут быть представлены абсолютными значениями,
		''' Input: SplitBy("[obj.field]=2", "=") Output: {[obj.field], 2}
		''' Input: SplitBy("[2 * 2]", "*") Output: {2, 2}
		''' </summary>
		''' <param name="formula"> Любая языковая конструкция. </param>
		''' <returns> Лист аргументов на одном уровне. </returns>
		Public Shared Function SplitBy(formula As String, delimeter As Char) As List(Of String)
			' Пост
			Dim args As New List(Of String)
			If String.IsNullOrEmpty(formula) Then Return args
			If IsNumeric(formula) Then Return args
			If InStr(formula, delimeter) = 0 Then Return {formula}.ToList()
			If formula.Length < 3 Then Return Split(formula, delimeter).ToList
			If InStr(formula, "[") = 0 Then Return Split(formula, delimeter).ToList
			If InStr(formula, "]") = 0 Then Return args ' Нарушение структуры

			' Если знак находится на первом уровне вложенности в формуле, например вход, [1=2] или [[1]=[2]]
			' то раскрыть скобки. Тогда на выходе тут будет: 1=2 или [1]=[2]
			formula = RemoveOuterBrackets(formula)

			Dim beginIndex As Integer = 0
			Dim endIndex As Integer

			Do
				' Найти разделитель на первом уровне вложенности
				Dim nestingLevel = 0
				endIndex = 0
				For i = beginIndex To formula.Length - 1 ' Перебрать хвост формулы
					Dim c = formula.Chars(i)
					If c = "[" Then nestingLevel += 1
					If c = "]" Then nestingLevel -= 1
					If c = delimeter And nestingLevel = 0 Then
						endIndex = i
						Exit For
					End If
				Next
				If endIndex = 0 Then Exit Do

				' Добавить аргумент в лист, если он имеет значение
				Dim arg = Mid(formula, beginIndex + 1, endIndex - beginIndex)
				args.Add(arg)

				beginIndex = endIndex + 1
			Loop While beginIndex < formula.Length

			' Добавить последний аргумент
			args.Add(Mid(formula, beginIndex + 1, formula.Length - beginIndex))

			Return args
		End Function

		''' <summary>
		''' Найти символ на определённом уровне вложенности квадратных скобок.
		''' Индекс первого символа: 0
		''' Символ не найден: -1
		''' </summary>
		''' <param name="formula"> Любая языковая конструкция. </param>
		''' <param name="symbol"> Искомый символ. </param>
		''' <param name="level"> Уровень вложенности. </param>
		''' <returns> Позиция символа на уровне. </returns>
		Public Shared Function GetCharIndexOnLevel(formula As String, symbol As Char, level As Integer) As Integer
			' Пост
			If String.IsNullOrEmpty(formula) Then Return 0
			If InStr(formula, "[") = 0 And level > 0 Then Return 0
			If InStr(formula, "]") = 0 And level < 0 Then Return 0

			' Поиск символа на уровне
			Dim nestingLevel = 0
			For i = 0 To formula.Length - 1 ' Перебрать хвост формулы
				Dim c = formula.Chars(i)
				If c = "[" Then nestingLevel += 1
				If c = "]" Then nestingLevel -= 1
				If c = symbol And nestingLevel = level Then Return i
			Next

			Return -1 ' символ не найден
		End Function

		''' <summary>
		''' Найти Текст на определённом уровне вложенности квадратных скобок.
		''' Индекс первого символа = 0.
		''' </summary>
		''' <param name="formula"> Любая языковая конструкция. </param>
		''' <param name="key"> Искомый text. </param>
		''' <param name="level"> Уровень вложенности. </param>
		''' <returns> Позиция символа на уровне. </returns>
		Public Shared Function GetTextIndexOnLevel(formula As String, key As String, level As Integer) As Integer
			' Пост
			If String.IsNullOrEmpty(formula) Then Return 0
			If InStr(formula, "[") = 0 And level > 0 Then Return 0
			If InStr(formula, "]") = 0 And level < 0 Then Return 0

			' Поиск символа на уровне
			Dim nestingLevel = 0
			For i = 0 To formula.Length - 1 ' Перебрать хвост формулы
				Dim tail = Right(formula, formula.Length - i)
				Dim c = tail.First()
				If c = "[" Then nestingLevel += 1
				If c = "]" Then nestingLevel -= 1
				If Left(tail, key.Length()) = key And nestingLevel = level Then Return i
			Next

			Return 0
		End Function

		''' <summary>
		''' Подровнять элементы листа.
		''' </summary>
		''' <param name="args"></param>
		Public Shared Function TrimList(ByRef args As List(Of String)) As List(Of String)
			If args Is Nothing OrElse args.Count = 0 Then Return args
			For i = 0 To args.Count - 1
				args(i) = Trim(args(i))
			Next
			Return args
		End Function

		''' <summary>
		''' Убрать внешние скобки аргумента.
		''' Input: [true], [1=2], [[1]=[2]], [1]=[2]
		''' Output: true,	1=2,   [1]=[2],  [1]=[2]
		''' </summary>
		Public Shared Function RemoveOuterBrackets(arg As String) As String
			Dim argTrim = arg.Trim()
			Dim args = GetFormulaArguments(argTrim)
			If args.Count() = 1 AndAlso argTrim?.First() = "["c AndAlso argTrim?.Last() = "]"c Then
				Return Mid(argTrim, 2, argTrim.Length - 2)
			End If

			Return arg
		End Function
#End Region

#Region "Value calculation"
		''' <summary>
		''' Получить значение аргумента при помощи вычисления формулы.
		''' Если DataSource представлен набором экземпляров.
		''' Ссылки на свойства и методы чувствительны к регистру!
		''' </summary>
		''' <param name="formula"></param>
		''' <param name="data"> Карта Наименований и Источников данных. Регистр не учитывается. </param>
		''' <returns> Вычисленное значение аргумента. </returns>
		Public Shared Function GetArgValue(formula As String,
										   data As (SourceName As String, DataSource As Object)()) As Object
			'Log.Write(Log.Level.Trace, $"Calc arg '{formula}'")

			' Пост
			If String.IsNullOrWhiteSpace(formula) Then Return formula ' Пусто
			If IsNumeric(formula) Then Return formula ' Представлена числом
			Dim firstBracketIndex = InStr(formula, "[")
			If firstBracketIndex = 0 Then Return formula ' Нет тегов в формуле, вернуть без вычислений
			Dim lastBracketIndex = GetCharIndexOnLevel(formula, "]"c, 0)
			If firstBracketIndex >= lastBracketIndex Then Return formula ' Нет закрывающейся скобки

			'' Если источник данных представлен одним DS, то считаеть его набором строк
			'Dim ffeTypeName = String.Empty ' Наименование типа данных первого эелемента первого DS.
			'If data.Count() = 1 Then
			'	ffeTypeName = data(0).SourceName
			'End If

			'! Порядок вычислений определяется вероятной глубиной языковой конструкции,
			'! т.е. от внешних к внутренним, например от функций к логике и далее к арифметике

			' (0) Константы
			Select Case formula
				Case "[n]" : Return vbCrLf
				Case "[t]" : Return vbTab
				Case "[r]" ' Вернуть экземпляр строки
					Return data.FirstOrDefault(Function(d) d.SourceName = "r").DataSource
			End Select

			' (1) Пост, формула не содержит аргументов, все вычисления произведены
			Dim formulaArguments = FormulaParser.GetFormulaArguments(formula)
			If formulaArguments.Count = 0 Then Return formula

#Region "Output data"
			Dim hasDot = InStr(firstBracketIndex, formula.ToString(), ".") > 0
			If data?.Count() > 0 And hasDot Then
				' Если есть источники данных, и точки (признаки ссылок), то вычислить значения аргументов.
				For Each argFormula In formulaArguments
					Dim dotIndex As Integer = GetCharIndexOnLevel(argFormula, "."c, 1)
					If dotIndex < 1 Then Continue For ' Нет признака ссылки

					' (4) Если это ссылка формата: [SourceName.FieldName]
					' Найти источник данных
					Dim dataSource = data.FirstOrDefault(Function(d) InStr(LCase(argFormula), $"[{LCase(d.SourceName)}.") = 1 And
																  argFormula.Length > (d.SourceName.Length + 3)).DataSource
					Dim objectFieldValue As Object = Nothing ' Возвращаемое значение, если формула содержит лишь один аргумент

					If dataSource IsNot Nothing Then
						' Источник данных определён
						argFormula = argFormula.Trim() ' Чувствовать регистр в формулах аргументов!

						' Вычислить ссылку на поле из формулы: [Project.AppsLab.Country.Name] -> Project и AppsLab.Country.Name
						Dim squareBracketIndex = GetCharIndexOnLevel(argFormula, "]"c, 0)
						Dim link As String = Mid(argFormula, dotIndex + 2, squareBracketIndex - dotIndex - 1)
						objectFieldValue = GetLinkValue(dataSource, link, data)
					End If

					' Если входная формула состоит лишь из одного элемента,
					' то сразу вернуть результат, не кастуя значение в String
					If formulaArguments?.Count = 1 AndAlso formula.Length() = argFormula.Length() Then
						Return objectFieldValue
					Else ' Заменить формулы аргументов на значения
						formula = Replace(formula, argFormula, ValToStr(objectFieldValue), 1, 1)
					End If
				Next
			End If
#End Region

#Region "Functions"
			' (5) Функции формата: [FunctionName(arguments)]
			' Not - логическое отрицание
			Dim notArgs = FormulaParser.GetArgsForNot(formula)
			If notArgs.Count = 1 Then
				' Получить значение аргумента
				Dim notArg = GetArgValue(notArgs(0), data)

				' Снять страховочные квадратные скобки, если они остались
				If notArg?.GetType().Equals(GetType(String)) Then notArg = RemoveOuterBrackets(notArg.ToString())

				' Вычислить значение конструкции Not
				Dim expression As Boolean = ValToBool(notArg)

				Return Not expression
			End If

			' In - поглощение значения перечислением (после Not)
			Dim inArgs = FormulaParser.GetArgsForIn(formula)
			If inArgs.Count > 2 Then
				' Вычислить значения аргументов
				Dim desired = GetArgValue(inArgs(0), data)?.ToString()
				Dim argValues As New List(Of Object)

				For i = 1 To inArgs.Count - 1
					argValues.Add(GetArgValue(inArgs(i), data)?.ToString())
				Next

				Return argValues.Contains(desired)
			End If

			' IIf
			Dim iifArgs = FormulaParser.GetArgsForIif(formula)
			If iifArgs.Count = 3 Then
				' Вычислить значение конструкции IIf
				Dim expression As Boolean = ValToBool(GetArgValue(iifArgs(0), data))
				Dim valueIfTrue = GetArgValue(iifArgs(1), data)
				Dim valueIfFalse = GetArgValue(iifArgs(2), data)
				Return Interaction.IIf(expression, valueIfTrue, valueIfFalse)
			End If

			' IsNull, Coalesce
			Dim coalesceArgs = FormulaParser.GetArgsForCoalesce(formula) ' В том числе обрабатывает конструкцию isnull
			If coalesceArgs.Count > 1 Then
				' Вычислить значение конструкции Coalesce
				Dim result As Object = Nothing
				Log.Write(Log.Level.Trace, $"Coalesce: Calculation... Formula: {formula}, Args count: {ValToInt(coalesceArgs?.Count)}")
				Dim sArg = String.Empty ' Вычисленный аргумент
				For Each arg In coalesceArgs
					sArg = arg
					Dim sValue = arg
					Do While InStr(sValue, "[") > 0 ' Вычислить значения всех аргументов
						result = GetArgValue(arg, data)
						sValue = ValToStr(result)
					Loop
					' Если найдено первое не пустое значение, то вернуть
					If sValue.Length > 0 Then Exit For
					If result IsNot Nothing AndAlso result.GetType.Equals(GetType(Byte())) AndAlso DirectCast(result, Byte()).Length > 0 Then Exit For
					If result IsNot Nothing AndAlso result.GetType.Equals(GetType(Bitmap)) AndAlso DirectCast(result, Bitmap).Size.Width > 0 Then Exit For
					If result IsNot Nothing AndAlso result.GetType.Equals(GetType(Image)) AndAlso DirectCast(result, Image).Size.Width > 0 Then Exit For
				Next
				Log.Write(Log.Level.Trace, $"Coalesce: Calculation result: {sArg} = {result}")
				Return result
			End If

			' IsNullOrEmpty
			Dim isNullOrEmptyArgs = FormulaParser.GetArgsForIsNullOrEmpty(formula)
			If isNullOrEmptyArgs.Count = 1 Then
				' Вычислить значение конструкции IIf
				Dim argValue = GetArgValue(isNullOrEmptyArgs(0), data)
				Return String.IsNullOrEmpty(ValToStr(argValue))
			End If
#End Region

#Region "Logic operators"
			' (10) Логические операторы: =, and, or (вычисляются после функций)
			' Проверить количество аргументов в формуле,
			' допускаются только парные варианты, например, [[1 = 2] = 3]
			' т.е. каждую пару требуется оборачивать в квадратные скобки (можно улучшить)
			For Each symbol In {"="c, "&"c, "|"c}
				Dim args = FormulaParser.SplitBy(formula, symbol)

				If args.Count = 2 Then ' Если кейс типа: [arg]=2
					' Аргументы могут быть не заключены в квадратные скобки, заключить для вычисления.
					Dim objA = GetArgValue($"[{RemoveOuterBrackets(args(0)?.Trim())}]", data)
					Dim objB = GetArgValue($"[{RemoveOuterBrackets(args(1)?.Trim())}]", data)
					Select Case symbol
						Case "="c
							' Сравнивать строковое представление значений объектов
							If args(0)?.Length > 1 AndAlso args(0)?.Chars(args(0).Length - 1) = "!"c Then
								Dim sObjA = ValToStr(objA)
								sObjA = Strings.Left(sObjA, sObjA.Length - 1).Trim() ' Убрать восклицалку из хвоста
								Dim sObjB = ValToStr(objB).Trim()
								Return IsNotEquals(sObjA, sObjB) ' !=
							Else
								Return IsEquals(ValToStr(objA).Trim(), ValToStr(objB).Trim()) ' =
							End If

						Case "&"c : Return ValToBool(objA) And ValToBool(objB)
						Case "|"c : Return ValToBool(objA) Or ValToBool(objB)
					End Select
				End If
			Next
#End Region

#Region "Arithmetic operators"
			' (20) Арифметические операции: +,-,/,*,^
			' Проверить количество аргументов в формуле,
			' допускаются только парные варианты, например, [[1 + 2] + 3]
			' т.е. каждую пару требуется оборачивать в квадратные скобки (можно улучшить)
			For Each symbol In {"+"c, "-"c, "/"c, "*"c, "^"c}
				Dim args = TrimList(FormulaParser.SplitBy(formula, symbol))

				If args.Count = 2 Then ' Если кейс типа: [arg1] * [arg2]
					Dim objA = ValToDbl(GetArgValue(args(0), data))
					Dim objB = ValToDbl(GetArgValue(args(1), data))
					Select Case symbol
						Case "+"c : Return objA + objB
						Case "-"c : Return objA - objB
						Case "/"c : Return objA / objB
						Case "*"c : Return objA * objB
						Case "^"c : Return objA ^ objB
					End Select
				End If
			Next
#End Region

			Return RemoveOuterBrackets(formula)
		End Function

		''' <summary>
		''' Получить значение аргумента при помощи вычисления формулы.
		''' Если DataSource представлен лишь одним экземпляром.
		''' Представление аргумента может передаваться, как в скобках '[arg]', так и без 'arg'.
		''' При этом требование к вычислению диктуется перед использованием этой функции.
		''' </summary>
		''' <param name="formula"></param>
		''' <returns> Вычисленное значение аргумента. </returns>
		Public Shared Function GetArgValue(formula As String, sourceName As String, source As Object) As Object
			Dim data() As (DataSourceName As String, DataSource As Object) = {(sourceName, source)}
			Return GetArgValue(formula, data)
		End Function

		''' <summary>
		''' Получить значение по ссылке на метод или свойство экземрляра.
		''' Не чувствительно к регистру.
		''' </summary>
		''' <param name="instance"> Например, Project </param>
		''' <param name="link"> Например, AppsLab.Country.Name или List.Count() </param>
		''' <param name="data"> Источник данных отчёта, для вычисления значений RTS. </param>
		''' <returns> Значение поля экземпляра объекта. </returns>
		Public Shared Function GetLinkValue(instance As Object,
											link As String,
											data As (SourceName As String, DataSource As Object)()
										   ) As Object
			' Пост
			If instance Is Nothing Then Return Nothing

			' Определить, ссылка на Свойство или Метод
			Dim firstDotIndex = GetCharIndexOnLevel(link, "."c, 0)
			Dim firstBracketIndex = GetCharIndexOnLevel(link, "("c, 0)

			If firstDotIndex > 0 Or ' Относительная ссылка на поле, например, Obj.Obj.Prop
			   (firstDotIndex = -1 And firstBracketIndex = -1) Then ' Прямая ссылка на поле, например, Obj.Prop
				Return GetPropertyValue(instance, link, data)
			End If

			If firstBracketIndex > firstDotIndex Then Return GetMethodValue(instance, link, data)
			Return Nothing
		End Function

		''' <summary>
		''' Получить значение поля объекта по ссылке на него.
		''' Не чувствительно к регистру.
		''' </summary>
		''' <param name="instance"> Например, Project </param>
		''' <param name="fieldLink"> Например, AppsLab.Country.Name </param>
		''' <param name="data"> Источник данных отчёта, для вычисления значений RTS. </param>
		''' <returns> Значение поля экземпляра объекта. </returns>
		Public Shared Function GetPropertyValue(instance As Object,
												fieldLink As String,
												data As (SourceName As String, DataSource As Object)()
											   ) As Object
			' Пост
			If instance Is Nothing Then Return Nothing
			Dim properties As PropertyInfo() = instance.GetType().GetProperties()
			If properties Is Nothing Then Return Nothing
			Dim propertyInfo As PropertyInfo = Nothing

			' Debug: Вывести список всех свойств
			'For Each p In properties
			'	Debug.Print($"Property: {instance.GetType().Name}.{p.Name}")
			'Next

			Dim firstDotIndex = InStr(fieldLink, ".")
			If firstDotIndex > 0 Then
				' Обработать относительную ссылку на Имя поля, например, Obj.Obj.Prop
				Dim fieldName = Mid(fieldLink, 1, firstDotIndex - 1)
				Dim tipFieldLink = Mid(fieldLink, firstDotIndex + 1, fieldLink.Length - firstDotIndex)
				propertyInfo = properties.FirstOrDefault(Function(p) LCase(p.Name) = LCase(fieldName))
				If propertyInfo IsNot Nothing Then
					Dim obj = propertyInfo.GetValue(instance)
					Return GetLinkValue(obj, tipFieldLink, data) ' Продолжить итерацию
				End If

			Else
				' Обработать прямые ссылки на поля объекта, например, Obj.Prop
				propertyInfo = properties.FirstOrDefault(Function(p) LCase(p.Name) = LCase(fieldLink))
			End If

			If propertyInfo IsNot Nothing Then
				Return propertyInfo.GetValue(instance)

			Else
				Dim msg = $"[{NameOf(GetPropertyValue)}]: "
				msg &= $"Field Name '{fieldLink}' is not associated with '{instance.GetType().Name}' class property."
				msg &= $"{vbCrLf}Field Link: {fieldLink}"
				Throw New ArgumentException(msg)
			End If

			Return Nothing
		End Function

		''' <summary>
		''' Вызвать метод экземпляра по ссылке и вернуть значение.
		''' Не чувствительно к регистру.
		''' </summary>
		''' <param name="source"> Например, Project </param>
		''' <param name="methodLink"> Например, List.Count() </param>
		''' <param name="data"> Источник данных отчёта, для вычисления значений RTS. </param>
		''' <returns> Значение поля экземпляра объекта. </returns>
		Public Shared Function GetMethodValue(source As Object,
											  methodLink As String,
											  data As (SourceName As String, DataSource As Object)()
											 ) As Object
			' Пост
			If source Is Nothing Then Return Nothing
			Dim firstBracketIndex = InStr(methodLink, "(")
			Dim lastBracketIndex = InStr(firstBracketIndex, methodLink, ")")
			If firstBracketIndex = 0 Then Return Nothing
			If lastBracketIndex = 0 Then
				Dim msg = $"[{NameOf(GetMethodValue)}]: "
				msg &= $"Name '{methodLink}' is not associated with '{source.GetType().Name}' class Method."
				msg &= $"{vbCrLf}Method Link: {methodLink}"
				Throw New ArgumentException(msg)
			End If

			Dim methods As MethodInfo() = source.GetType().GetMethods()
			' Получить имя метода
			Dim methodName = Mid(methodLink, 1, firstBracketIndex - 1)
			' Определить наличие метода в экземпляре
			Dim methodInfo = methods.FirstOrDefault(Function(m) LCase(m.Name) = LCase(methodName))
			If methodInfo Is Nothing Then Return Nothing
			' Вычислить аргументы метода
			Dim parameters As Object() = Nothing
			Dim parametersRtxLength = lastBracketIndex - firstBracketIndex - 1
			If parametersRtxLength > 0 Then
				' Есть описание аргументов метода
				Dim parametersRts = Mid(methodLink, firstBracketIndex + 1, parametersRtxLength)
				parameters = GetMethodParameters(parametersRts, data)
			End If

			' Если в RTS указаны аргументы метода
			If parameters?.Count > 0 Then
				' Привести значения аргументов к требуемому типу данных
				Dim typedParameter As New List(Of Object)
				Dim methodParameters = methodInfo.GetParameters()
				Dim i = 0
				For Each parameter In parameters
					typedParameter.Add(Cast(parameter, methodParameters(i).ParameterType))
					i += 1
				Next
				parameters = typedParameter.ToArray()
			End If

			' Вызвать метод
			Return methodInfo.Invoke(source, parameters)
		End Function

		''' <summary>
		''' Вычислить аргументы метода
		''' </summary>
		''' <param name="formula"></param>
		''' <returns></returns>
		Private Shared Function GetMethodParameters(
													parametersRts As String,
													data As (SourceName As String, DataSource As Object)()
												   ) As Object()
			Dim splitParametersRts = SplitBy(parametersRts, ","c)
			If splitParametersRts?.Count = 0 Then Return Nothing

			Dim parameters As New List(Of Object)
			For Each rts In splitParametersRts
				parameters.Add(GetArgValue(rts, data))
			Next

			Return parameters.ToArray()
		End Function
#End Region

	End Class
End Namespace