Public Class LoginControl

    Private ReadOnly userDal As New AccountsDAL()
    Private ReadOnly modelControl As New ModelsControl()

    Public Sub LoginAuthentication(Username As String, Password As String)
        Dim user As AccountsModel = userDal.GetUserByUsername(Username)
        modelControl.BuildModels()

        If user IsNot Nothing Then

            If user.Password.Equals(HashPassword(Password)) Then
                

                Dim role As String = user.Role

                Message.Show("Success", $"Logged In as <bold>{role}</bold>")

                Select Case role.ToLower()
                    Case "customer"

                        Models.User = user
                        modelControl.BuildCustomerModels()

                        ViewControl.Instance.ChangeView(New CustomerView())

                    Case "collector"

                        Models.User = user
                        modelControl.BuildCollectorModels()

                        ViewControl.Instance.ChangeView(New CollectorView())

                    Case "administrator"

                        Models.User = user
                        modelControl.BuildAdministratorModels()

                        ViewControl.Instance.ChangeView(New AdminView())

                    Case Else
                        Message.Show("Invalid", "Unknown <bold>User Type</bold>")
                End Select

            Else

                Message.Show("Invalid", "Wrong <bold>Password</bold>")

            End If
            
        Else 

            Message.Show("Invalid", "<bold>Username</bold> not found")
            
        End If
    End Sub
End Class
