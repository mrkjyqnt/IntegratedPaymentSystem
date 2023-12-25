Imports System.ComponentModel
Imports System.Net
Imports System.Transactions

Public Class CustomerView
    Inherits UserControl
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _billingData As UserBillingModel
    Private _adminBillingData As BillingsModel
    Private _userData As UserInformationModel
    Private _transactionsData As List(Of UserTransactionsModel)
    Private _activitiesData As List(Of UserActivitiesModel)
    Private _connectionData As UserConnectionModel
    Public Property BillingData As UserBillingModel
        Get
            Return _billingData
        End Get
        Set(value As UserBillingModel)
            If _billingData IsNot value Then
                _billingData = value
                OnPropertyChanged("BillingData")
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
    Public Property ConnectionData As UserConnectionModel
        Get
            Return _connectionData
        End Get
        Set(value As UserConnectionModel)
            If _connectionData IsNot value Then
                _connectionData = value
                OnPropertyChanged("ConnectionData")
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

    Public Property ActivitiesData As List(Of UserActivitiesModel)
        Get
            Return _activitiesData
        End Get
        Set(value As List(Of UserActivitiesModel))
            If _activitiesData IsNot value Then
                _activitiesData = value
                OnPropertyChanged("ActivitiesData")
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
        ChangeView(mainView ,New CustomerDashboard())

        Dim _thisActivity As New ActivitiesModel

        _thisActivity.AccountID = Models.UserInformation.AccountID
        _thisActivity.Description = $"User logged (IP: {Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(Function(ip) ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork)?.ToString()}, Computer: {Environment.MachineName})"
        _thisActivity.Amount = 0
        _thisActivity.ActivityDate = DateTime.Today
        _thisActivity.Type = "User Login"
        _thisActivity.Category = "System"

        NewActivity(_thisActivity)

        Me.DataContext = Models.UserInformation

        UserData = Models.UserInformation
        TransactionsData = Models.UserTransactions
        ConnectionData = Models.UserConnection
        BillingData = Models.UserBilling
        AdminBillingData = Models.AdminBilling
        ActivitiesData = Models.UserActivities

    End Sub

    ' Navigation Buttons
    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim clickedButton As Button = TryCast(sender, Button)

        If clickedButton Is ButtonDashboard Then

            ChangeView(mainView ,New CustomerDashboard())
            NavigationChange("Dashboard")

        ElseIf clickedButton Is ButtonTransactions Then

            ChangeView(mainView ,New CustomerTransaction)
            NavigationChange("Transactions")

            
        ElseIf clickedButton Is ButtonSupport Then

            MsgBox("Not available")
            NavigationChange("Support")

        ElseIf clickedButton Is ButtonAccount Then

            ChangeView(mainView ,New CustomerAccount)
            NavigationChange("Account")

        ElseIf clickedButton Is ButtonLogout Then

            If Message.Confirm("Are you sure?", "You are about to log-out</newline>Press <bold>Enter</bold> to confirm", Nothing) Then
                Message.Show("Success", "Successfully logged-out")
                ViewControl.Instance.ChangeView(New Login)
            End If

        End If

    End Sub

    Public Sub NavigationChange(navigationName As String)
        ' Simulate button click based on buttonName
        If navigationName = "Dashboard" Then

            ButtonDashboard.Style = DirectCast(FindResource("NavButtonActive"), Style)
            ButtonTransactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonSupport.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonAccount.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Transactions" Then
            
            ButtonDashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonTransactions.Style = DirectCast(FindResource("NavButtonActive"), Style)
            ButtonSupport.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonAccount.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Support" Then
            
            ButtonDashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonTransactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonSupport.Style = DirectCast(FindResource("NavButtonActive"), Style)
            ButtonAccount.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Account" Then
            
            ButtonDashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonTransactions.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonSupport.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            ButtonAccount.Style = DirectCast(FindResource("NavButtonActive"), Style)

        End If
    End Sub

    Private Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class
