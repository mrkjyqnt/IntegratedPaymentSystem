Imports System.Data

Public Class ExternalTransactionsDAL
    Public Function GetAll() As List(Of ExternalTransactionsModel)
        Dim list As New List(Of ExternalTransactionsModel)

        Query("SELECT * FROM ExternalTransactions")

        If HASRECORD Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New ExternalTransactionsModel(row))
            Next

        End If

        Return list
    End Function

    Public Function Create(transaction As ExternalTransactionsModel) As ExternalTransactionsModel
        Dim createdTransaction As ExternalTransactionsModel = Nothing
        Dim query As String = "INSERT INTO ExternalTransactions (Status, Type, Date, Description, Others, Amount, CollectorID, CustomerID, PlanID) 
                              OUTPUT INSERTED.* 
                              VALUES (@TransactionDate, @Description, @ReferenceNumber, @Amount);"

        Prepare(query)
        AddParam("@TransactionDate", transaction.TransactionDate)
        AddParam("@Description", transaction.Description)
        AddParam("@ReferenceNumber", transaction.ReferenceNumber)
        AddParam("@Amount", transaction.Amount)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            createdTransaction = New ExternalTransactionsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .TransactionDate = row("TransactionDate").ToString(),
                .Description = row("Description").ToString(),
                .ReferenceNumber = row("ReferenceNumber").ToString(),
                .Amount = Convert.ToDecimal(row("Amount"))
                }
        End If

    Return createdTransaction
    End Function


End Class
