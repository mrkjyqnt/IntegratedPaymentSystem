Imports System

Module PaymentControl

    Public Property ThisMonthWholeDays As Integer
    Public Property ThisMonthConnectedDays As Integer
    Public Property ThisMonth As Date
    Public Property ThisMonthSpan As Date
    Private Property Total As Double

    Private ReadOnly InternalTransactionDal As New InternalTransactionsDAL
    Private ReadOnly ConnectionsDal As New ConnectionsDAL

    Private _UserTransactionModel As New UserTransactionsModel()
    Private _InternalTransactionsModel As New InternalTransactionsModel()

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
        _InternalTransactionsModel = InternalTransactionDal.Create(transaction)
        Models.InternalTransactions.Add(_InternalTransactionsModel)

        _UserTransactionModel = UserTransactionsModel.GetTransactionByID(_InternalTransactionsModel.ID)
        Models.UserTransactions.Add(_UserTransactionModel)

        Return _InternalTransactionsModel
    End Function

    Public Function UpdateUserConnection(connection As ConnectionsModel)

        ConnectionsDal.Update(connection)
        Models.Connections = ConnectionsDal.GetAll()

        Return Nothing
    End Function 

    Public Function IsEligible() As String

        If ThisMonth.Day >= ThisMonthSpan.Day Then
            Return True
        Else 
            Return False
        End If

    End Function
End Module
