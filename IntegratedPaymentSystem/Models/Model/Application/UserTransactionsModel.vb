Imports System.ComponentModel

Public Class UserTransactionsModel
    Inherits UserTransactionsAbstract
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _id As Integer
    Public Property ID() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            If _id <> value Then
                _id = value
                RaisePropertyChanged("ID")
            End If
        End Set
    End Property

    Private _status As String
    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(value As String)
            If _status <> value Then
                _status = value
                RaisePropertyChanged("Status")
            End If
        End Set
    End Property

    Private _type As String
    Public Property Type() As String
        Get
            Return _type
        End Get
        Set(value As String)
            If _type <> value Then
                _type = value
                RaisePropertyChanged("Type")
            End If
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(value As String)
            If _description <> value Then
                _description = value
                RaisePropertyChanged("Description")
            End If
        End Set
    End Property

    Private _others As String
    Public Property Others() As String
        Get
            Return _others
        End Get
        Set(value As String)
            If _others <> value Then
                _others = value
                RaisePropertyChanged("Others")
            End If
        End Set
    End Property

    Private _transactionDate As Date
    Public Property TransactionDate() As Date
        Get
            Return _transactionDate
        End Get
        Set(value As Date)
            If _transactionDate <> value Then
                _transactionDate = value
                RaisePropertyChanged("TransactionDate")
            End If
        End Set
    End Property

    Private _amount As Decimal
    Public Property Amount() As Decimal
        Get
            Return _amount
        End Get
        Set(value As Decimal)
            If _amount <> value Then
                _amount = value
                RaisePropertyChanged("Amount")
            End If
        End Set
    End Property

    Private _customer As String
    Public Property Customer() As String
        Get
            Return _customer
        End Get
        Set(value As String)
            If _customer <> value Then
                _customer = value
                RaisePropertyChanged("Customer")
            End If
        End Set
    End Property

    Private _collector As String
    Public Property Collector() As String
        Get
            Return _collector
        End Get
        Set(value As String)
            If _collector <> value Then
                _collector = value
                RaisePropertyChanged("Collector")
            End If
        End Set
    End Property

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub New(ID As Integer, Status As String, Type As String, Description As String, Others As String, TransactionDate As Date, Amount As Decimal, Customer As String, Collector As String)
        With Me
            .ID = ID
            .Status = Status
            .Type = Type
            .Description = Description
            .Others = Others
            .TransactionDate = TransactionDate
            .Amount = Amount
            .Customer = Customer
            .Collector = Collector
        End With
    End Sub

    Public Sub New()
    End Sub

    Public Shared Function GetAllByCustomerID(ID As Integer) As List(Of UserTransactionsModel)
        Dim TransactionList = From it In Models.InternalTransactions
                              Join customer In Models.AccountInformations On it.CustomerID Equals customer.ID
                              Join collector In Models.AccountInformations On it.CollectorID Equals collector.ID
                              Join ip In Models.InternetPlans On it.PlanID Equals ip.ID
                              Order By it.ID Descending
                              Where it.CustomerID = ID
                              Select New UserTransactionsModel(
                              it.ID,
                              it.Status,
                              it.Type,
                              it.Description,
                              it.Others,
                              it.TransactionDate,
                              it.Amount.ToString("0.00"),
                              Customer:=$"{customer.FirstName} {customer.LastName}",
                              Collector:=$"{collector.FirstName} {collector.LastName}"
                          )

        Return TransactionList.ToList()
    End Function

    Public Shared Function GetAllByCollectorID(ID As Integer) As List(Of UserTransactionsModel)
        Dim TransactionList = From it In Models.InternalTransactions
                Join customer In Models.AccountInformations On it.CustomerID Equals customer.ID
                Join collector In Models.AccountInformations On it.CollectorID Equals collector.ID
                Join ip In Models.InternetPlans On it.PlanID Equals ip.ID
                Order By it.ID Descending
                Where it.CollectorID = ID
                Select New UserTransactionsModel(
                    it.ID,
                    it.Status,
                    it.Type,
                    it.Description,
                    it.Others,
                    it.TransactionDate,
                    it.Amount.ToString("0.00"),
                    Customer:=$"{customer.FirstName} {customer.LastName}",
                    Collector:=$"{collector.FirstName} {collector.LastName}"
                    )

        Return TransactionList.ToList()
    End Function

    Public Shared Function GetTransactionByID(ID As Integer) As UserTransactionsModel
        Dim Transaction = (From it In Models.InternalTransactions
                Join customer In Models.AccountInformations On it.CustomerID Equals customer.ID
                Join collector In Models.AccountInformations On it.CollectorID Equals collector.ID
                Join ip In Models.InternetPlans On it.PlanID Equals ip.ID
                Where it.ID = ID
                Select New UserTransactionsModel(
                    it.ID,
                    it.Status,
                    it.Type,
                    it.Description,
                    it.Others,
                    it.TransactionDate,
                    it.Amount.ToString("0.00"),
                    Customer:=$"{customer.FirstName} {customer.LastName}",
                    Collector:=$"{collector.FirstName} {collector.LastName}"
                    )).FirstOrDefault()

        Return Transaction
    End Function

    Public Shared Function GetAllByDate(ID As Integer, FromDate As Date, ToDate As Date) As List(Of UserTransactionsModel)
        Dim TransactionList = From it In Models.InternalTransactions
                              Join customer In Models.AccountInformations On it.CustomerID Equals customer.ID
                              Join collector In Models.AccountInformations On it.CollectorID Equals collector.ID
                              Join ip In Models.InternetPlans On it.PlanID Equals ip.ID
                              Order By it.ID Descending
                              Where it.CustomerID = ID AndAlso it.TransactionDate >= FromDate AndAlso it.TransactionDate <= ToDate
                              Select New UserTransactionsModel(
                                it.ID,
                                it.Status,
                                it.Type,
                                it.Description,
                                it.Others,
                                it.TransactionDate,
                                it.Amount.ToString("0.00"),
                                Customer:=$"{customer.FirstName} {customer.LastName}",
                                Collector:=$"{collector.FirstName} {collector.LastName}"
                                )

        Return TransactionList.ToList()
    End Function
End Class
