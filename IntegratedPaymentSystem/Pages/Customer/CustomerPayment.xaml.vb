
Imports iText.StyledXmlParser.Jsoup.Select

Public Class CustomerPayment
    Inherits UserControl
    Public Shared Property ReferenceNumber As String
    Private _customerViewInstance As New CustomerView

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        RemoveBlurEffect()

        _customerViewInstance.AdminBillingData = Nothing
        Models.AdminBilling = Nothing

        Models.AdminBilling = Models.Billings.FirstOrDefault(Function(billing) billing.IsAdmin = True AndAlso billing.IsEnabled = True)
        _customerViewInstance.AdminBillingData = Models.AdminBilling

        DataContext = _customerViewInstance

        TextAdminName.Text = Models.AdminBilling.Name
        TextAdminNumber.Text = Models.AdminBilling.Number
        TextAdminType.Text = Models.AdminBilling.Type
        TextAmount.Text = Models.UserConnection.PlanAmount

        If ReferenceNumber IsNot Nothing Then

            TextReferenceNumber.Text = ReferenceNumber
            ButtonEditReference.Visibility = Visibility.Visible
            ButtonAddReference.Visibility = Visibility.Collapsed

        Else 

            ButtonEditReference.Visibility = Visibility.Collapsed
            ButtonAddReference.Visibility = Visibility.Visible

        End If

        If Models.UserBilling IsNot Nothing Then

            SectionBillingInformation.Visibility = Visibility.Visible
            SectionTwo.Visibility = Visibility.Visible
            SectionThree.Visibility = Visibility.Visible
            ButtonEditBilling.Visibility = Visibility.Collapsed
            ButtonAddBilling.Visibility = Visibility.Visible
        
        Else

            SectionBillingInformation.Visibility = Visibility.Collapsed
            SectionTwo.Visibility = Visibility.Collapsed
            SectionThree.Visibility = Visibility.Collapsed
            ButtonEditBilling.Visibility = Visibility.Visible
            ButtonAddBilling.Visibility = Visibility.Collapsed

        End If

        If TextReferenceNumber.Text = Nothing then

            ButtonConfirm.Visibility = Visibility.Collapsed

        Else 

            ButtonConfirm.Visibility = Visibility.Visible

        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim customerBillingInfo As New CustomerBillingInfo()
        Dim customerReference As New CustomerReference()
        
        If sender Is ButtonAddBilling Then

            customerBillingInfo.ShowDialog()
            ChangeView(mainView, new CustomerPayment)

        ElseIf sender Is ButtonEditBilling Then

            customerBillingInfo.ShowDialog()
            ChangeView(mainView, new CustomerPayment)

        ElseIf sender Is ButtonAddReference Then

            customerReference.ShowDialog()
            ChangeView(mainView, new CustomerPayment)

        ElseIf sender Is ButtonEditReference Then

            customerReference.ShowDialog()
            ChangeView(mainView, new CustomerPayment)
            
        ElseIf sender Is ButtonConfirm Then

            ChangeView(mainView, new CustomerPaymentConfirmation)

        End If
    End Sub
End Class
