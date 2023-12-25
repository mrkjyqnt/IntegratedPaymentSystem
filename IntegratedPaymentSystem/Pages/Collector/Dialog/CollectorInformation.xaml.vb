Imports Microsoft.Win32

Public Class CollectorInformation

    Private ReadOnly _accountInformation As New AccountInformationsModel
    Private ReadOnly _account As New AccountsModel

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ApplyBlurEffect()

        TextName.Text = Models.UserInformation.FullName
        TextNumber.Text = Models.UserInformation.ContactNumber
        TextEmail.Text = Models.UserInformation.Email
        TextUsername.Text = Models.User.Username

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim code As String = GenerateRandomCode()
        Dim newPassword As String = TextPassword.Text.ToString() 
        Dim reNewPassword As String = TextRePassword.Text.ToString() 
        Dim oldPassword As String = HashPassword(TextOldPassword.Text.ToString())

        If sender Is ButtonEnter Then

            If SectionChangePassword.Visibility = Visibility.Visible Then
                If Not oldPassword.Equals(Models.User.Password) Then
                    Message.Show("Error", "Your <bold>old password</bold> does not match with the database password")
                    ApplyBlurEffect()
                    Return
                End If

                If Not newPassword.Equals(reNewPassword) Then
                    Message.Show("Error", "Your <bold>new password</bold> and <bold>repeated new password</bold> do not match")
                    ApplyBlurEffect()
                    Return
                End If

                If TextEmail.Text = "" OrElse TextNumber.Text = "" Then
                    Message.Show("Error", "Please fill all the content")
                    ApplyBlurEffect()
                    Return
                End If

                PrepareMail($"GeekXFiber System", "system.geekxfiber@gmail.com", Models.UserInformation.FullName, Models.UserInformation.Email, $"Information Changes Request ({Date.Today:MM/dd/yyyy})")
                AddBody(GenerateConfirmationCode(code))
                SendMail()

                Dim result As Boolean = Message.Confirm("Code Sent", "A code has been sent to your email</newline>please enter it in the box to confirm", code)
                If Not result Then
                    Message.Show("Failed", "Failed to enter a code")
                    ApplyBlurEffect()
                    Return
                End If

                With _account
                    .Username = Models.User.Username
                    .Password = HashPassword(newPassword)
                End With

                With _accountInformation
                    .AccountID = Models.UserInformation.AccountID
                    .Email = TextEmail.Text.ToString()
                    .ContactNumber = TextNumber.Text.ToString()
                End With

                UpdateAccount("me", _account)
                UpdateInformation("me", _accountInformation)
            Else
                PrepareMail($"GeekXFiber System", "system.geekxfiber@gmail.com", Models.UserInformation.FullName, Models.UserInformation.Email, $"Information Changes Request ({Date.Today:MM/dd/yyyy})")
                AddBody(GenerateConfirmationCode(code))
                SendMail()

                Dim result As Boolean = Message.Confirm("Code Sent", "A code has been sent to your email</newline>please enter it in the box to confirm", code)
                If result Then
                    With _accountInformation
                        .AccountID = Models.UserInformation.AccountID
                        .Email = TextEmail.Text.ToString()
                        .ContactNumber = TextNumber.Text.ToString()
                    End With

                    UpdateInformation("me", _accountInformation)
                Else
                    Message.Show("Failed", "Failed to enter a code")
                    ApplyBlurEffect()
                    Return
                End If
            End If

        End If

        If sender Is ButtonChangePassword Then
            
        SectionChangePassword.Visibility = Visibility.Visible
        ButtonChangePassword.Visibility = Visibility.Collapsed
        Return

        End If

        RemoveBlurEffect()
        Close()
    End Sub

    Private Sub NumericOnlyTextBox(sender As Object, e As TextCompositionEventArgs)
        If Not IsNumeric(e.Text) Then
            e.Handled = True
        End If
    End Sub
End Class
