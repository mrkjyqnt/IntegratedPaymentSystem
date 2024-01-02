Imports Microsoft.Win32

Public Class AdminBillingInformation

    Private ReadOnly _billing As New BillingsModel

    Private result As Boolean

    Public Sub New(Optional billing As BillingsModel = Nothing)
        ' This call is required by the designer.
        InitializeComponent()
        ApplyBlurEffect()

        If billing IsNot Nothing Then

            _billing = billing
            TextName.Text = billing.Name
            TextNumber.Text = billing.Number
            ComboAvailability.SelectedIndex = If(billing.IsEnabled = True, "0", "1")
            ButtonDelete.Visibility = Visibility.Visible

        End If
        

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim code As String = GenerateRandomCode()

        If sender Is ButtonEnter Then

            result = True

            If Not _billing.ID.Equals(0) Then

                With _billing
                        .Name = TextName.Text.ToString()
                        .Number = TextNumber.Text.ToString()
                        .IsEnabled = ComboAvailability.Text = "TRUE"
                End With

                result = Message.Confirm("Warning", "After clicking <bold>OK</bold>, the screen might froze or stop warking, please do not force close or end the program to prevent further system errors.")
                    
                If result Then

                    UpdateBilling(_billing)

                Else

                    Message.Show("Failed", "User canceled the action")

                End If

            Else

                With _billing
                        .AccountID = Models.User.ID
                        .Name = TextName.Text.ToString()
                        .Number = TextNumber.Text.ToString()
                        .Type = ComboType.Text.ToString
                        .IsEnabled = ComboAvailability.Text = "TRUE"
                        .IsAdmin = True
                End With

                AddBilling(_billing)

            End If

        ElseIf sender Is ButtonDelete Then

            result = Message.Confirm("Warning", "Please enter <bold>agree</bold> on the textbox below", "agree")
                    
            If result Then

                DeleteBilling(_billing)

            Else

                Message.Show("Failed", "User canceled the action")

            End If

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
