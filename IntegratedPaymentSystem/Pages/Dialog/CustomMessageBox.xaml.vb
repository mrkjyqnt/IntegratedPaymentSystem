Imports System.Text.RegularExpressions
Imports System.Windows.Media.Animation

Public Class CustomMessageBox

    Public Sub New(title As String, message As String, ownerWindow As Window)

        InitializeComponent()
        ApplyBlurEffect()

        Owner = ownerWindow 
        FormatTextblock(TextError, message)
        TextTitle.Text = title

        ' Register the Loaded event handler
        AddHandler Me.Loaded, AddressOf Animate

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        RemoveBlurEffect()
        Close()
    End Sub

    Private Sub Animate(sender As Object, e As RoutedEventArgs)
        ' Create a fade-in animation
        Dim fadeInAnimation As New DoubleAnimation(0, 1, New Duration(TimeSpan.FromSeconds(0.25)))

        ' Apply the animation to the window
        Me.BeginAnimation(Window.OpacityProperty, fadeInAnimation)
    End Sub

    Public Property EmptyArgs As RoutedEventArgs

    Private Sub Pressed_Enter(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Key = Key.Enter Then
            Button_Click(sender, EmptyArgs)
            e.Handled = True
        End If
    End Sub
End Class
