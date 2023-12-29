Module FinanceControl

    Public Property ThisWeekStart As Date
    Public Property ThisWeekEnd As Date
    Private Property ThisMonthStart As Date
    Private Property ThisMonthEnd As Date

    Public Property ThisWeekSales As Double
    Public Property ThisMonthSales As Double

    Public Property WeeksPercentage As Double
    Public Property MonthsPercentage As Double

    Public Property WeekResult As Boolean = True
    Public Property MonthResult As Boolean = True

    Private Property Recent As Double

    Private Property Total As Double

    Public Function CreateFinanceData()

        ThisWeekStart = Date.Today.AddDays(-(Date.Today.DayOfWeek - DayOfWeek.Sunday + 7) Mod 7)
        ThisWeekEnd = ThisWeekStart.AddDays(6)

        GetWeeklyData(Models.InternalTransactions)
        GetMonthlyData(Models.InternalTransactions)

        Return Nothing
    End Function

    Public Function GetWeeklyData(transaction As List(Of InternalTransactionsModel))

        ThisWeekSales = transaction _
            .Where(Function(data) data.Status = "Confirmed" AndAlso 
                                  data.TransactionDate.Date >= ThisWeekStart AndAlso 
                                  data.TransactionDate.Date <= ThisWeekEnd) _
            .Sum(Function(data) data.Amount)

        Recent = transaction _
            .Where(Function(data) data.Status = "Confirmed" AndAlso 
                                  data.TransactionDate.Date >= ThisWeekStart.AddDays(-7) AndAlso 
                                  data.TransactionDate.Date <= ThisWeekStart.AddDays(-7).AddDays(6)) _
            .Sum(Function(data) data.Amount)

        WeeksPercentage = (ThisWeekSales - Recent) / Recent * 100
        WeekResult = ThisWeekSales >= Recent

        Return Nothing
    End Function

    Public Function GetMonthlyData(transaction As List(Of InternalTransactionsModel))

        ThisMonthSales = transaction _
            .Where(Function(data) data.Status = "Confirmed" AndAlso 
                                  data.TransactionDate.Year = Date.Today.Year AndAlso
                                  data.TransactionDate.Month = Date.Today.Month) _
                   .Sum(Function(data) data.Amount)

        Recent = transaction _
                .Where(Function(data) data.Status = "Confirmed" AndAlso 
                                      data.TransactionDate >= Date.Today.AddMonths(-1).Date.AddDays(-Date.Today.Day + 1) AndAlso 
                                      data.TransactionDate < Date.Today.Date.AddDays(-Date.Today.Day).Date) _
                .Sum(Function(data) data.Amount)

        MonthsPercentage = (ThisMonthSales - Recent) / Recent * 100
        MonthResult = ThisMonthSales >= Recent

        Return Nothing
    End Function

End Module
