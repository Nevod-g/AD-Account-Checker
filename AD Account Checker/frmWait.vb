Imports ProcessBank.Agent


Public Class frmWait
#Region "Singleton"
    Private Shared ReadOnly instance As New frmWait() ' + единственный приватный конструктор!
    Public Shared ReadOnly Property Current As frmWait
        Get
            Return instance
        End Get
    End Property

    Private Sub New()
        InitializeComponent()
        Me.pp.AutoHeight = True
    End Sub
#End Region

    Private Shared description As String

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
        If owner Is Nothing Then owner = frmMain

        Try
            ' Иногда DX падает при попытке AutoWidth
            Current.pp.Caption = caption
            Current.pp.Description = description
        Catch ex As Exception

        End Try
        If Not Current.Visible Then Current.Show(owner)
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
        Current.pp.Caption = caption
        Current.pp.Description = description
        If Not Current.Visible Then Current.Show()
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Установить описание хода процесса.
    ''' </summary>
    ''' <param name="description"></param>
    Private Sub SetDescription()
        Log.Write(Log.Level.Info, description, $"{NameOf(frmWait)}.{NameOf(SetDescription)}")
        Current.pp.Description = description
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Установить описание хода процесса.
    ''' </summary>
    ''' <param name="description"></param>
    Public Shared Sub InvokeSetDescription(description As String)
        frmWait.description = description
        If frmWait.Current.InvokeRequired Then ' Определить поток
            frmWait.Current?.Invoke(New MethodInvoker(AddressOf frmWait.Current.SetDescription))
        Else
            frmWait.Current?.SetDescription()
        End If
    End Sub

    ''' <summary>
    ''' Потокобезопасный вызов
    ''' </summary>
    Public Shared Sub InvokeHide()
        If frmWait.Current.InvokeRequired Then ' Определить поток
            frmWait.Current?.Invoke(New MethodInvoker(AddressOf frmWait.Current.Hide))
        Else
            frmWait.Current.Hide()
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
        If frmWait.Current.InvokeRequired Then ' Определить поток
            Return frmWait.Current?.Invoke(New MethodInvoker(Sub() frmWait.Current.GetTag()))
        Else
            Return frmWait.Current.GetTag()
        End If
    End Function

    ''' <summary>
    ''' Потокобезопасный вызов
    ''' </summary>
    Public Shared Sub InvokeSetTag(tag As Object)
        If frmWait.Current.InvokeRequired Then ' Определить поток
            frmWait.Current?.Invoke(New MethodInvoker(Sub() frmWait.Current.SetTag(tag)))
        Else
            frmWait.Current.SetTag(tag)
        End If
    End Sub

    Protected Overrides Sub Finalize()
		MyBase.Finalize()
	End Sub
End Class
