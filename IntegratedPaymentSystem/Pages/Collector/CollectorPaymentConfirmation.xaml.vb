Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Security.Principal
Imports System.Windows.Controls.Primitives

Public Class CollectorPaymentConfirmation
    Inherits UserControl
    
    Private _transactionModel As New InternalTransactionsModel()
    Private _connectionModel As New ConnectionsModel()
    Private _userTransactionModel As New UserTransactionsModel()
    Private _activityModel As New ActivitiesModel()

    Private ReadOnly _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
    Private ReadOnly customerData As New CollectorView

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If customerData IsNot Nothing Then
            Me.DataContext = customerData
        End If

        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If

        TextAmount.Text = CollectorPaymentOverview.ToPay.ToString()

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim _collectorView As CollectorView = ViewControl.CurrentView

        If sender Is ButtonBack Then

            ChangeView(mainView, new CollectorPaymentOverview(customerData.CustomerData.AccountID))

        ElseIf sender Is ButtonConfirm Then

            Dim result As Boolean = Message.Confirm("Are you sure?", "You cannot undo after this</newline>Enter <bold>yes</bold> to enter", "yes")

            If result = True Then

                ' Populate the new transaction
                With _transactionModel
                    .Status = "Paid"
                    .Type = ComboBoxType.SelectedItem.Content.ToString()
                    .TransactionDate = Date.Today.Date
                    .Description = $"From {customerData.CustomerData.FullName} to {customerData.UserData.FullName} using "
                    .Others = "None"
                    .Amount = CollectorPaymentOverview.ToPay
                    .CollectorID = customerData.UserData.AccountID
                    .CustomerID = customerData.CustomerData.AccountID
                    .PlanID = customerData.CustomerConnectionData.PlanID
                End With

                ' Creating and adding the transaction to Models.InternalTransactions
                _transactionModel = CreatePayment(_transactionModel)
                _UserTransactionModel = Models.UserTransactions.FirstOrDefault(Function(data) data.ID = _transactionModel.ID)

                With _connectionModel
                    .AccountID = customerData.CustomerConnectionData.AccountID
                    .Status = "Connected"
                End With

                With _activityModel
                    .AccountID = Models.UserInformation.AccountID
                    .Description = $"{Models.UserInformation.FullName} confirmed a payment from {Models.CustomerInformation.FullName}"
                    .Amount = Models.CustomerConnection.PlanAmount
                    .ActivityDate = DateTime.Today.Date
                    .Type = "Customer Payment"
                    .Category = "Payment"
                End With
                
                NewActivity(_activityModel)
                UpdateUserConnection(_connectionModel)
                PrepareMail($"Collector: {Models.UserInformation.FullName}" ,"system.geekxfiber@gmail.com", Models.CustomerInformation.FullName, Models.CustomerInformation.Email, $"You made a payment to Fiber ({_transactionModel.TransactionDate:MM/dd/yyyy})")
                AddBody(GenerateCustomerReceipt(_UserTransactionModel, Models.CustomerConnection))
                SendMail()

                Message.Show("Success", "Payment has been completed")

            Else 

                Message.Show("Canceled", "Payment has been canceled</newline>Sending back to dashboard")

            End If

            CustomerPayment.ReferenceNumber = Nothing
            Models.CustomerInformation = Nothing
            Models.CustomerConnection = Nothing
            Models.CustomerTransactions = Nothing
            _collectorView.NavigationChange("Dashboard")
            ChangeView(mainView, New CollectorDashboard())

        End If

    End Sub

End Class
