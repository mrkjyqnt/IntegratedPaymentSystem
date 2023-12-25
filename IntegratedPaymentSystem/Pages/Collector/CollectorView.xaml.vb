Imports System.ComponentModel

Public Class CollectorView
    Inherits UserControl

    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _transactionsData As List(Of UserTransactionsModel)
    Private _adminBillingData As BillingsModel
    Private _userData As UserInformationModel
    Private _customersListData As List(Of AccountInformationsModel)

    Private _customerData As UserInformationModel
    Private _customerConnectionData As UserConnectionModel
    Private _customerTransactionsData As List(Of UserTransactionsModel)

    Public Property CustomerData As UserInformationModel
        Get
            Return _customerData
        End Get
        Set(value As UserInformationModel)
            If _customerData IsNot value Then
                _customerData = value
                OnPropertyChanged("CustomerData")
            End If
        End Set
    End Property

    Public Property CustomerConnectionData As UserConnectionModel
        Get
            Return _customerConnectionData
        End Get
        Set(value As UserConnectionModel)
            If _customerConnectionData IsNot value Then
                _customerConnectionData = value
                OnPropertyChanged("CustomerConnectionData")
            End If
        End Set
    End Property

    Public Property CustomerTransactionsData As List(Of UserTransactionsModel)
        Get
            Return _customerTransactionsData
        End Get
        Set(value As List(Of UserTransactionsModel))
            If _customerTransactionsData IsNot value Then
                _customerTransactionsData = value
                OnPropertyChanged("CustomerTransactionsData")
            End If
        End Set
    End Property

    Public Property TransactionsData As List(Of UserTransactionsModel)
        Get
            Return _transactionsData
        End Get
        Set(value As List(Of UserTransactionsModel))
            If _transactionsData IsNot value Then
                _transactionsData = value
                OnPropertyChanged("TransactionsData")
            End If
        End Set
    End Property

    Public Property CustomersListData As List(Of AccountInformationsModel)
        Get
            Return _customersListData
        End Get
        Set(value As List(Of AccountInformationsModel))
            If _customersListData IsNot value Then
                _customersListData = value
                OnPropertyChanged("CustomersListData")
            End If
        End Set
    End Property

    Public Property UserData As UserInformationModel
        Get
            Return _userData
        End Get
        Set(value As UserInformationModel)
            If _userData IsNot value Then
                _userData = value
                OnPropertyChanged("UserData")
            End If
        End Set
    End Property

    Public Property AdminBillingData As BillingsModel
        Get
            Return _adminBillingData
        End Get
        Set(value As BillingsModel)
            If _adminBillingData IsNot value Then
                _adminBillingData = value
                OnPropertyChanged("AdminBillingData")
            End If
        End Set
    End Property

    ' <summary>
    '
    ' Main function of the view
    '
    ' <summary>
    Public Sub New()

        InitializeComponent()

        ViewControl.SetCurrentView = Me
        ChangeView(mainView, New CollectorDashboard())
        Me.DataContext = Models.UserInformation

        UserData = Models.UserInformation
        TransactionsData = Models.UserTransactions
        AdminBillingData = Models.AdminBilling
        CustomersListData = Models.AccountInformations.Where(Function(data) data.AccountID > 0 AndAlso Models.Accounts.Any(function(account) account.Role = "customer" AndAlso account.ID = data.AccountID)).ToList()
        CustomerData = Models.CustomerInformation
        CustomerConnectionData = Models.CustomerConnection
        CustomerTransactionsData = Models.CustomerTransactions

    End Sub

    ' Navigation Buttons
    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If sender Is Dashboard Then

            NavigationChange("Dashboard")
            ChangeView(mainView, New CollectorDashboard)

        ElseIf sender Is Payment Then

            NavigationChange("Payments")
            ChangeView(mainView, New CollectorPayments)

        ElseIf sender Is Transactions Then

            NavigationChange("Transactions")
            ChangeView(mainView, New CollectorTransaction)

        ElseIf sender Is Support Then
            
            '

        ElseIf sender Is Account Then

            NavigationChange("Account")
            ChangeView(mainView, New CollectorAccount)

        ElseIf sender Is ButtonLogout Then

            If Message.Confirm("Are you sure?", "You are about to log-out</newline>Press <bold>Enter</bold> to confirm", Nothing) Then
                Message.Show("Success", "Successfully logged-out")
                ViewControl.Instance.ChangeView(New Login)
            End If

        End If

    End Sub

    Public Sub NavigationChange(navigationName As String)
        ' Simulate button click based on buttonName
        If navigationName = "Dashboard" Then

            Dashboard.Style = DirectCast(FindResource("NavButtonActive"), Style)
            Payment.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Transactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Support.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Payments" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payment.Style = DirectCast(FindResource("NavButtonActive"), Style)
            Transactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Support.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Transactions" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payment.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Transactions.Style = DirectCast(FindResource("NavButtonActive"), Style)
            Support.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Support" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payment.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Transactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Support.Style = DirectCast(FindResource("NavButtonActive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Account" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payment.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Transactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Support.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonActive"), Style)

        End If
    End Sub

    Private Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class
