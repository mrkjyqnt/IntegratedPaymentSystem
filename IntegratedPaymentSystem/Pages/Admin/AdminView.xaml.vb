Imports System.ComponentModel

Public Class AdminView
    Inherits UserControl

    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ' <summary>
    '
    ' List of the user data from the database
    '
    ' <summary>

    Private _user As AccountsModel
    Private _userData As UserInformationModel

    Public Property User As AccountsModel
        Get
            Return _user
        End Get
        Set(value As AccountsModel)
            If _user IsNot value Then
                _user = value
                OnPropertyChanged("User")
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

    ' <summary>
    '
    ' List of the data from the database
    '
    ' <summary>

    Private _users As List(Of AccountsModel)
    Private _usersData As List(Of UserInformationModel)
    Private _usersConnectionData As List(Of UserConnectionModel)
    Private _usersTransactionsData As List(Of UserTransactionsModel)
    Private _usersActivitiesData As List(Of UserActivitiesModel)

    Public Property Users As List(Of AccountsModel)
        Get
            Return _users
        End Get
        Set(value As List(Of AccountsModel))
            If _users IsNot value Then
                _users = value
                OnPropertyChanged("Users")
            End If
        End Set
    End Property

    Public Property UsersData As List(Of UserInformationModel)
        Get
            Return _usersData
        End Get
        Set(value As List(Of UserInformationModel))
            If _usersData IsNot value Then
                _usersData = value
                OnPropertyChanged("UsersData")
            End If
        End Set
    End Property

    Public Property UsersConnectionData As List(Of UserConnectionModel)
        Get
            Return _usersConnectionData
        End Get
        Set(value As List(Of UserConnectionModel))
            If _usersConnectionData IsNot value Then
                _usersConnectionData = value
                OnPropertyChanged("UsersConnectionData")
            End If
        End Set
    End Property

    Public Property UsersTransactionsData As List(Of UserTransactionsModel)
        Get
            Return _usersTransactionsData
        End Get
        Set(value As List(Of UserTransactionsModel))
            If _usersTransactionsData IsNot value Then
                _usersTransactionsData = value
                OnPropertyChanged("UsersTransactionsData")
            End If
        End Set
    End Property

    Public Property UsersActivitiesData As List(Of UserActivitiesModel)
        Get
            Return _usersActivitiesData
        End Get
        Set(value As List(Of UserActivitiesModel))
            If _usersActivitiesData IsNot value Then
                _usersActivitiesData = value
                OnPropertyChanged("UsersActivitiesData")
            End If
        End Set
    End Property

    ' <summary>
    '
    ' List of the customer data from the database
    '
    ' <summary>

    Private _customer As AccountsModel
    Private _customerData As UserInformationModel
    Private _customerConnectionData As UserConnectionModel
    Private _customerTransactionsData As List(Of UserTransactionsModel)
    Private _customerActivitiesData As List(Of UserActivitiesModel)


    Public Property Customer As AccountsModel
        Get
            Return _customer
        End Get
        Set(value As AccountsModel)
            If _customer IsNot value Then
                _customer = value
                OnPropertyChanged("Customer")
            End If
        End Set
    End Property

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

    Public Property CustomerActivitiesData As List(Of UserActivitiesModel)
        Get
            Return _customerActivitiesData
        End Get
        Set(value As List(Of UserActivitiesModel))
            If _customerActivitiesData IsNot value Then
                _customerActivitiesData = value
                OnPropertyChanged("CustomerActivitiesData")
            End If
        End Set
    End Property

    ' <summary>
    '
    ' List of the collector data from the database
    '
    ' <summary>

    Private _collector As AccountsModel
    Private _collectorData As UserInformationModel
    Private _collectorTransactionsData As List(Of UserTransactionsModel)
    Private _collectorActivitiesData As List(Of UserActivitiesModel)


    Public Property Collector As AccountsModel
        Get
            Return _collector
        End Get
        Set(value As AccountsModel)
            If _collector IsNot value Then
                _collector = value
                OnPropertyChanged("Collector")
            End If
        End Set
    End Property

    Public Property CollectorData As UserInformationModel
        Get
            Return _collectorData
        End Get
        Set(value As UserInformationModel)
            If _collectorData IsNot value Then
                _collectorData = value
                OnPropertyChanged("CollectorData")
            End If
        End Set
    End Property

    Public Property CollectorTransactionsData As List(Of UserTransactionsModel)
        Get
            Return _collectorTransactionsData
        End Get
        Set(value As List(Of UserTransactionsModel))
            If _collectorTransactionsData IsNot value Then
                _collectorTransactionsData = value
                OnPropertyChanged("CollectorTransactionsData")
            End If
        End Set
    End Property

    Public Property CollectorActivitiesData As List(Of UserActivitiesModel)
        Get
            Return _collectorActivitiesData
        End Get
        Set(value As List(Of UserActivitiesModel))
            If _collectorActivitiesData IsNot value Then
                _collectorActivitiesData = value
                OnPropertyChanged("CollectorActivitiesData")
            End If
        End Set
    End Property

    ' <summary>
    '
    ' List of the Admins data from the database
    '
    ' <summary>

    Private _administrator As AccountsModel
    Private _administratorData As UserInformationModel
    Private _administratorActivitiesData As List(Of UserActivitiesModel)

    Public Property Administrator As AccountsModel
        Get
            Return _administrator
        End Get
        Set(value As AccountsModel)
            If _administrator IsNot value Then
                _administrator = value
                OnPropertyChanged("Administrator")
            End If
        End Set
    End Property

    Public Property AdministratorData As UserInformationModel
        Get
            Return _administratorData
        End Get
        Set(value As UserInformationModel)
            If _administratorData IsNot value Then
                _administratorData = value
                OnPropertyChanged("AdministratorData")
            End If
        End Set
    End Property

    Public Property AdministratorActivitiesData As List(Of UserActivitiesModel)
        Get
            Return _administratorActivitiesData
        End Get
        Set(value As List(Of UserActivitiesModel))
            If _administratorActivitiesData IsNot value Then
                _administratorActivitiesData = value
                OnPropertyChanged("AdministratorActivitiesData")
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

        User = Models.User
        UserData = Models.UserInformation

        Users = Models.Users
        UsersData = Models.UsersInformation
        UsersTransactionsData = Models.UsersTransactions
        UsersConnectionData = Models.UsersConnection
        UsersActivitiesData = Models.UserActivities

        Customer = Models.CustomerAccount
        CustomerData = Models.CustomerInformation
        CustomerConnectionData = Models.CustomerConnection
        CustomerTransactionsData = Models.CustomerTransactions
        CustomerActivitiesData = Models.CustomerActivities

        Collector = Models.CollectorAccount
        CollectorData = Models.CustomerInformation
        CollectorTransactionsData = Models.CustomerTransactions
        CollectorActivitiesData = Models.CustomerActivities

        Administrator = Models.AdministratorAccount
        AdministratorData = Models.AdministratorInformation
        AdministratorActivitiesData = Models.AdministratorActivities

        ViewControl.SetCurrentView = Me
        Me.DataContext = UserData
        ChangeView(mainView, New AdminDashboard())
    End Sub

    ' Navigation Buttons
    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If sender Is Dashboard Then

            NavigationChange("Dashboard")
            ChangeView(mainView, New AdminDashboard)

        ElseIf sender Is Payments Then

            NavigationChange("Payments")
            ChangeView(mainView, New AdminPayments)

        ElseIf sender Is Financial Then

            NavigationChange("Transactions")
            'ChangeView(mainView, New AdminFinancial)

        ElseIf sender Is UserManagement Then
            
            NavigationChange("UserManagement")
            'ChangeView(mainView, New AdminUserManagement)

        ElseIf sender Is DataRecovery Then
            
            NavigationChange("DataRecovery")
            'ChangeView(mainView, New AdminDataRecovery)

        ElseIf sender Is Account Then

            NavigationChange("Account")
            'ChangeView(mainView, New CollectorAccount)

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
            Payments.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Financial.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            UserManagement.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            DataRecovery.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Payments" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payments.Style = DirectCast(FindResource("NavButtonActive"), Style)
            Financial.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            UserManagement.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            DataRecovery.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "Financial" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payments.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Financial.Style = DirectCast(FindResource("NavButtonActive"), Style)
            UserManagement.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            DataRecovery.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "UserManagement" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payments.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Financial.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            UserManagement.Style = DirectCast(FindResource("NavButtonActive"), Style)
            DataRecovery.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)

        ElseIf navigationName = "DataRecovery" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payments.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Financial.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            UserManagement.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            DataRecovery.Style = DirectCast(FindResource("NavButtonActive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonInactive"), Style)


        ElseIf navigationName = "Account" Then
            
            Dashboard.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Payments.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Financial.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            UserManagement.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            DataRecovery.Style = DirectCast(FindResource("NavButtonInactive"), Style)
            Account.Style = DirectCast(FindResource("NavButtonActive"), Style)

        End If
    End Sub

    Private Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

End Class
