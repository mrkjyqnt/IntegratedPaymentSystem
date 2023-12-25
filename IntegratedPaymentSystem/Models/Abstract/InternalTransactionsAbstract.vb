Public Class InternalTransactionsAbstract
    Inherits DefaultModelFunction

    Public Property ID As Long 
    Public Property Status As String
    Public Property Type As String
    Public Property Description As String
    Public Property Others As String
    Public Property TransactionDate As Date
    Public Property Amount As Decimal
    Public Property CollectorID As Long 
    Public Property CustomerID As Long 
    Public Property PlanID As Long 

End Class
