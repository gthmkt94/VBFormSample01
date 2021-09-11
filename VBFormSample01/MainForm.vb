Public Class MainForm
    Private _model As New MainModel()

    Public Sub New()
        InitializeComponent()

        TextBox1.DataBindings.Add(NameOf(TextBox1.Text), _model, NameOf(MainModel.LeftOperand))
        TextBox2.DataBindings.Add(NameOf(TextBox2.Text), _model, NameOf(MainModel.RightOperand))
        TextBox3.DataBindings.Add(NameOf(TextBox3.Text), _model, NameOf(MainModel.CalcResult))
    End Sub

    Private Sub OnKeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back _
            AndAlso e.KeyChar <> "-"c AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub OnKeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown, TextBox2.KeyDown, TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim forward As Boolean = e.Modifiers <> Keys.Shift
            Me.SelectNextControl(ActiveControl, forward, True, True, True)
            e.Handled = True
        End If
    End Sub
End Class

Public Class MainModel
    Inherits BindingBase

    Private _leftOperand As String
    Public Property LeftOperand As String
        Get
            Return _leftOperand
        End Get
        Set(value As String)
            If Not Equals(_leftOperand, value) Then
                _leftOperand = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Private _rightOperand As String
    Public Property RightOperand As String
        Get
            Return _rightOperand
        End Get
        Set(value As String)
            If Not Equals(_rightOperand, value) Then
                _rightOperand = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Public ReadOnly Property CalcResult As String
        Get
            Dim res As String = ""
            Try
                res = CInt(LeftOperand) + CInt(RightOperand)
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
            Return res
        End Get
    End Property
End Class