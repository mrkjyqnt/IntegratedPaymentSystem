Imports System.Data
Imports System.Data.Common
Imports System.Security.Principal
Imports System.Transactions

Public Class CustomerPaymentConfirmation
    Inherits UserControl
    Private baseData As New CustomerView

    Private _transactionModel As New InternalTransactionsModel()
    Private _UserTransactionModel As New UserTransactionsModel()
    Private _activityModel As New ActivitiesModel()
    
    Dim DateToday as Date = DateTime.Today.ToString("yyyy-MM-dd")

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        RemoveBlurEffect()

        ' Add any initialization after the InitializeComponent() call.
        DataContext = baseData
        TextDateNow.Text = DateToday
        TextReferenceNumber.Text = CustomerPayment.ReferenceNumber


    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim _customerView As CustomerView = ViewControl.CurrentView
        
        If sender Is ButtonBack Then

            ChangeView(mainView, new CustomerPayment)

        ElseIf sender Is ButtonConfirm Then

            Dim result As Boolean = Message.Confirm("Are you sure?", "You cannot undo after this</newline>Enter <bold>yes</bold> to enter", "confirm")

            If result = True Then

                ' Populate the new transaction
                With _transactionModel
                    .Status = "Pending"
                    .Type = baseData.BillingData.Type
                    .TransactionDate = DateToday
                    .Description = $"From {baseData.BillingData.Number} to {baseData.AdminBillingData.Number} using {baseData.BillingData.Type}"
                    .Others = CustomerPayment.ReferenceNumber
                    .Amount = baseData.ConnectionData.PlanAmount
                    .CollectorID = 0
                    .CustomerID = baseData.UserData.AccountID
                    .PlanID = baseData.ConnectionData.PlanID
                End With

                ' Creating and adding the transaction to Models.InternalTransactions
                _transactionModel = CreatePayment(_transactionModel)
                _UserTransactionModel = Models.UserTransactions.FirstOrDefault(Function(transaction) transaction.ID = _transactionModel.ID)

                With _activityModel
                    .AccountID = Models.UserInformation.AccountID
                    .Description = $"{Models.UserInformation.FullName} you sent a payment to {Models.AdminBilling.Number}"
                    .Amount = Models.CustomerConnection.PlanAmount
                    .ActivityDate = DateTime.Today.ToString("MM/dd/yyyy")
                    .Type = "Customer Payment"
                    .Category = "Payment"
                End With
                
                NewActivity(_activityModel)

                PrepareMail($"We receieved your payment as of {_transactionModel.TransactionDate:MM/dd/yyyy}" ,"system.geekxfiber@gmail.com", Models.UserInformation.FullName, Models.UserInformation.Email, $"You made a payment to Fiber ({_transactionModel.TransactionDate:MM/dd/yyyy})")
                AddBody(GenerateConfirmationReceipt(_UserTransactionModel, Models.CustomerConnection))
                SendMail()

                Message.Show("Success", "Payment has been sent and will be Confirmed by the admin")
                
            Else 

                Message.Show("Canceled", "Payment has been canceled</newline>Sending back to dashboard")

            End If

            CustomerPayment.ReferenceNumber = Nothing
            baseData.BillingData = Nothing
            baseData.AdminBillingData = Nothing
            Models.UserBilling = Nothing
            Models.AdminBilling = Nothing
            _customerView.NavigationChange("Dashboard")
            ChangeView(mainView, New CustomerDashboard())

        End If
    End Sub
End Class
