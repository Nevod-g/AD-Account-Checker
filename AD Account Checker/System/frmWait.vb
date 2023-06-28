Imports ProcessBank.Agent

Public Class frmWait
    Sub New()
        InitializeComponent()
        Me.pp.AutoHeight = True
    End Sub

    Public Overrides Sub SetCaption(ByVal caption As String)
        MyBase.SetCaption(caption)
        Me.pp.Caption = caption
    End Sub

    Public Overrides Sub SetDescription(ByVal description As String)
        MyBase.SetDescription(description)
        Me.pp.Description = description
        Application.DoEvents()
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum WaitFormCommand
        SomeCommandId
    End Enum

    ''' <summary>
    ''' Главный метод для отображения формы в модальном режиме.
    ''' Заполнять Description чтобы форма могла корректно масштабироваться.
    ''' caption устанавливается в tag формы, для контроля управления, можно проверить via InvokeGetTag 
    ''' </summary>
    ''' <param name="owner"></param>
    ''' <param name="caption"></param>
    Public Shared Sub ShowOnOwner(Optional ByVal owner As Form = Nothing,
                                  Optional ByVal caption As String = "Loading...",
                                  Optional ByVal description As String = "",
        <System.Runtime.CompilerServices.CallerMemberName> Optional caller As String = "NA")

        Log.Write(Log.Level.Info, $"Caller: {caller}, Message: {caption} | {description}", $"{NameOf(frmWait)}.{NameOf(ShowOnPeak)}")

        Try
            ' Иногда DX падает при попытке AutoWidth
            frmWait.pp.Caption = caption
            frmWait.pp.Description = description
        Catch ex As Exception

        End Try
        If Not frmWait.Visible Then frmWait.Show(owner)
        InvokeSetTag(caption)
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Менее приоритетный метод для отображения формы.
    ''' </summary>
    ''' <param name="caption"></param>
    ''' <param name="description"></param>
    Public Shared Sub ShowOnPeak(Optional ByVal caption As String = "Loading...",
                                 Optional ByVal description As String = "",
        <System.Runtime.CompilerServices.CallerMemberName> Optional caller As String = "NA")

        Log.Write(Log.Level.Info, $"Caller: {caller}, Message: {caption} | {description}", $"{NameOf(frmWait)}.{NameOf(ShowOnPeak)}")
        frmWait.pp.Caption = caption
        frmWait.pp.Description = description
        If Not frmWait.Visible Then frmWait.Show()
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Установить описание хода процесса.
    ''' </summary>
    ''' <param name="description"></param>
    Public Shared Sub InvokeSetDescription(description As String)
        If frmWait.InvokeRequired Then ' Определить поток
            frmWait?.Invoke(New MethodInvoker(Sub() frmWait.SetDescription(description)))
        Else
            frmWait?.SetDescription(description)
        End If
    End Sub

    ''' <summary>
    ''' Потокобезопасный вызов
    ''' </summary>
    Public Shared Sub InvokeHide()
        If frmWait.InvokeRequired Then ' Определить поток
            frmWait?.Invoke(New MethodInvoker(AddressOf frmWait.Hide))
        Else
            frmWait.Hide()
        End If
    End Sub

    Public Function GetTag() As Object
        Return Tag
    End Function

    Public Sub SetTag(tag As Object)
        Me.Tag = tag
    End Sub

    ''' <summary>
    ''' Потокобезопасный вызов
    ''' </summary>
    Public Shared Function InvokeGetTag() As Object
        If frmWait.InvokeRequired Then ' Определить поток
            Return frmWait?.Invoke(New MethodInvoker(Sub() frmWait.GetTag()))
        Else
            Return frmWait.GetTag()
        End If
    End Function

    ''' <summary>
    ''' Потокобезопасный вызов
    ''' </summary>
    Public Shared Sub InvokeSetTag(tag As Object)
        If frmWait.InvokeRequired Then ' Определить поток
            frmWait?.Invoke(New MethodInvoker(Sub() frmWait.SetTag(tag)))
        Else
            frmWait.SetTag(tag)
        End If
    End Sub
End Class
