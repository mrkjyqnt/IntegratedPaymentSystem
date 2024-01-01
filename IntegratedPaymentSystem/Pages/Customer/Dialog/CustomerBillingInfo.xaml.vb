Imports Microsoft.Win32

Public Class CustomerBillingInfo

    Private _customerView As New CustomerView
    Private _customerViewInstance As CustomerView

    Public Sub New()

        InitializeComponent()
        ApplyBlurEffect()

        If Models.UserBilling IsNot Nothing Then

            TextName.Text = Models.UserBilling.Name
            TextNumber.Text = Models.UserBilling.Number
            ComboBoxType.Text = Models.UserBilling.Type

        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If _customerView IsNot Nothing Then

            If TextName.Text IsNot "" AndAlso TextNumber.Text IsNot "" Then

                Models.UserBilling = New UserBillingModel(Models.Billings.Count + 1, Models.User.ID, TextName.Text, TextNumber.Text, DirectCast(ComboBoxType.SelectedItem, ComboBoxItem).Content.ToString(), False, False)
                _customerView.BillingData = Models.UserBilling
   
            Else 
                Message.Show("Error", "Please fill all the content")
                ApplyBlurEffect()
                Return
            End If

        Else
            Message.Show("Error","CustomerView is not initialized.")
            ApplyBlurEffect()
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
