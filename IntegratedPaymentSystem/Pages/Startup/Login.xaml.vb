Imports System.Text.RegularExpressions

Public Class Login
    Private ReadOnly loginControl As New LoginControl()

    Private Sub AuthenticateUser()
        Dim username As String = TextUsername.Text
        Dim password As String = GetPasswordFromPasswordBox(TextPassword)

        loginControl.LoginAuthentication(Username, Password)
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        
        If TextPassword.Password IsNot "" Then

            AuthenticateUser()

        Else 

            Message.Show("Error", "Please enter a password")

        End If

    End Sub

    Private Sub Pressed_Enter(sender As Object, e As KeyEventArgs) Handles TextPassword.PreviewKeyDown
        If e.Key = Key.Enter Then
            Button_Click(sender, New RoutedEventArgs())
        End If
    End Sub

    Private Sub Pressed_Space(sender As Object, e As KeyEventArgs) Handles TextUsername.PreviewKeyDown
        If e.Key = Key.Space Then
            e.Handled = True
        End If
    End Sub

    Private Sub Input_preview(sender As Object, e As TextCompositionEventArgs) Handles TextUsername.PreviewTextInput
        Dim regex As New Regex("[^a-zA-Z0-9]+")

        If regex.IsMatch(e.Text) Then
            e.Handled = True
        End If
    End Sub
End Class
