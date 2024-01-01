Imports System
Imports System.Security.RightsManagement

Module PaymentControl

    Public Property ThisMonthWholeDays As Integer
    Public Property ThisMonthConnectedDays As Integer
    Public Property ThisMonth As Date
    Public Property ThisMonthSpan As Date

    Private Property Total As Double

    Private ReadOnly InternalTransactionDal As New InternalTransactionsDAL
    Private ReadOnly ExternalTransactionDal As New ExternalTransactionsDAL
    Private ReadOnly ConnectionsDal As New ConnectionsDAL

    Private _UserTransactionsModel As New UserTransactionsModel()
    Private _UsersTransactionsModel As New List(Of UserTransactionsModel())
    Private _InternalTransactionModel As New InternalTransactionsModel()
    Private _InternalTransactionsModel As New List(Of InternalTransactionsModel)
    Private _ExternalTransactionModel As New ExternalTransactionsModel()
    Private _ExternalTransactionsModel As New List(Of ExternalTransactionsModel)
    Private _ConnectionModel As New ConnectionsModel()

    Public Function CreatePaymentDate(LastDayConnected As Date)
        ThisMonth = LastDayConnected.AddDays(1 - LastDayConnected.Day).AddMonths(1)
        ThisMonthSpan = ThisMonth.AddDays(2)
        ThisMonthWholeDays = Math.Abs((New Date(LastDayConnected.Year, LastDayConnected.Month, 6) - ThisMonth).Days)
        ThisMonthConnectedDays = Math.Abs((LastDayConnected - ThisMonth).Days) - 1

        Return Nothing
    End Function

    Public Function CalculatePayment(Cost As Double) As Double

        If ThisMonth.Day <= ThisMonthSpan.Day AndAlso ThisMonth.Day >= ThisMonthSpan.Day Then
            Total = Cost
        Else 
            Total = (Cost / ThisMonthWholeDays) * ThisMonthConnectedDays
        End If

        Return Total
    End Function

    Public Function CreatePayment(transaction As InternalTransactionsModel) As InternalTransactionsModel
        _InternalTransactionModel = InternalTransactionDal.Create(transaction)
        Models.InternalTransactions.Add(_InternalTransactionModel)

        _UserTransactionsModel = UserTransactionsModel.GetTransactionByID(_InternalTransactionModel.ID)
        Models.UserTransactions.Add(_UserTransactionsModel)

        Return _InternalTransactionModel
    End Function

    Public Function ConfirmPayments()

        _InternalTransactionsModel = Models.InternalTransactions.Where(Function(data) data.Status = "Pending").ToList

        For Each transaction As InternalTransactionsModel In _InternalTransactionsModel
            Dim foundTransaction As ExternalTransactionsModel = Models.ExternalTransactions.FirstOrDefault(Function(data) data.ReferenceNumber = transaction.Others AndAlso data.Status = "Unclaimed")
            Dim customerData As AccountInformationsModel = Models.AccountInformations.FirstOrDefault(function(data) data.AccountID = transaction.CustomerID)
            Dim customerTransactionData As UserTransactionsModel = Models.UsersTransactions.FirstOrDefault(function(data) data.ID = transaction.ID)
            Dim customerInformationsData As UserInformationModel = Models.UsersInformation.FirstOrDefault(function(data) data.AccountID = transaction.CustomerID)
            Dim customerConnection As UserConnectionModel = Models.UsersConnection.FirstOrDefault(Function(data) data.AccountID = transaction.CustomerID)
            Dim accountConnection As ConnectionsModel = Models.Connections.FirstOrDefault(Function(data) data.AccountID = customerConnection.AccountID)

            If foundTransaction IsNot Nothing Then

                _InternalTransactionModel = transaction
                With _InternalTransactionModel
                    .Status = "Confirmed"
                    .CollectorID = 0
                End With
                
                UpdateInternalTransaction(_InternalTransactionModel)

                 _UserTransactionsModel = customerTransactionData

                _ConnectionModel = accountConnection
                With _ConnectionModel
                    .Status = "Connected"
                End With

                UpdateUserConnection(_ConnectionModel)

                _ExternalTransactionModel = foundTransaction
                With _ExternalTransactionModel
                    .Status = "Claimed"
                End With

                UpdateExternalTransaction(_ExternalTransactionModel)
                UpdateUserConnection(_ConnectionModel)

                PrepareMail($"System" ,"system.geekxfiber@gmail.com", customerInformationsData.FullName, customerInformationsData.Email, $"Your payment on ({transaction.TransactionDate:MM/dd/yyyy}) is confirmed")
                AddBody(GenerateCustomerReceipt(_UserTransactionsModel, customerConnection, $"Your payment on ({transaction.TransactionDate:MM/dd/yyyy}) is confirmed"))
                SendMail()

            Else

                _InternalTransactionModel = transaction
                 With _InternalTransactionModel
                    .Status = "Declined"
                    .CollectorID = 0
                 End With

                UpdateInternalTransaction(_InternalTransactionModel)

                PrepareMail($"System" ,"system.geekxfiber@gmail.com", customerInformationsData.FullName, customerInformationsData.Email, $"Your payment on ({transaction.TransactionDate:MM/dd/yyyy}) has been declined")
                AddBody(GenerateCustomerReceipt(_UserTransactionsModel, customerConnection, $"Your payment on ({transaction.TransactionDate:MM/dd/yyyy}) has been declined"))
                SendMail()

            End If

        Next

        Return Nothing
    End Function

    Public Function UpdateUserConnection(connection As ConnectionsModel)

        ConnectionsDal.Update(connection)
        Models.Connections = ConnectionsDal.GetAll()

        Return Nothing
    End Function 

    Public Function IsEligible() As String

        If ThisMonth.Day >= ThisMonthSpan.Day Then
            Return False
        Else 
            Return True
        End If

    End Function
End Module
