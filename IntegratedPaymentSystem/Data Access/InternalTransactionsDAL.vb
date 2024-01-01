Imports System.Data

Public Class InternalTransactionsDAL
    Public Function GetAll() As List(Of InternalTransactionsModel)
        Dim list As New List(Of InternalTransactionsModel)

        Query("SELECT * FROM InternalTransactions")

        If HASRECORD Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New InternalTransactionsModel(row))
            Next

        End If

        Return list
    End Function

    Public Function Create(transaction As InternalTransactionsModel) As InternalTransactionsModel
        Dim createdTransaction As InternalTransactionsModel = Nothing
        Dim query As String = "INSERT INTO InternalTransactions (Status, Type, Date, Description, Others, Amount, CollectorID, CustomerID, PlanID) 
                              OUTPUT INSERTED.* 
                              VALUES (@Status, @Type, @Date, @Description, @Others, @Amount, @CollectorID, @CustomerID, @PlanID);"

        Prepare(query)
        AddParam("@Status", transaction.Status)
        AddParam("@Type", transaction.Type)
        AddParam("@Date", transaction.TransactionDate)
        AddParam("@Description", transaction.Description)
        AddParam("@Others", transaction.Others)
        AddParam("@Amount", transaction.Amount)
        AddParam("@CollectorID", transaction.CollectorID)
        AddParam("@CustomerID", transaction.CustomerID)
        AddParam("@PlanID", transaction.PlanID)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            createdTransaction = New InternalTransactionsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .Status = row("Status").ToString(),
                .Type = row("Type").ToString(),
                .TransactionDate = Convert.ToDateTime(row("Date")),
                .Description = row("Description").ToString(),
                .Others = row("Others").ToString(),
                .Amount = Convert.ToDecimal(row("Amount")),
                .CollectorID = Convert.ToInt64(row("CollectorID")),
                .CustomerID = Convert.ToInt64(row("CustomerID")),
                .PlanID = Convert.ToInt64(row("PlanID"))
                }
        End If

    Return createdTransaction
    End Function

    Public Function Update(transaction As InternalTransactionsModel) As InternalTransactionsModel
        Dim updatedTransaction As InternalTransactionsModel = Nothing
        Dim query As String = "UPDATE InternalTransactions 
                                    SET Status = @Status,
                                        Type = @Type,
                                        Date = @Date,
                                        Description = @Description,
                                        Others = @Others,
                                        Amount = @Amount,
                                        CollectorID = @CollectorID,
                                        CustomerID = @CustomerID,
                                        PlanID = @PlanID
                                    WHERE ID = @ID;
                                    SELECT * FROM InternalTransactions WHERE ID = @ID;"

        Prepare(query)
        AddParam("@ID", transaction.ID)
        AddParam("@Status", transaction.Status)
        AddParam("@Type", transaction.Type)
        AddParam("@Date", transaction.TransactionDate)
        AddParam("@Description", transaction.Description)
        AddParam("@Others", transaction.Others)
        AddParam("@Amount", transaction.Amount)
        AddParam("@CollectorID", transaction.CollectorID)
        AddParam("@CustomerID", transaction.CustomerID)
        AddParam("@PlanID", transaction.PlanID)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            updatedTransaction = New InternalTransactionsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .Status = row("Status").ToString(),
                .Type = row("Type").ToString(),
                .TransactionDate = Convert.ToDateTime(row("Date")),
                .Description = row("Description").ToString(),
                .Others = row("Others").ToString(),
                .Amount = Convert.ToDecimal(row("Amount")),
                .CollectorID = Convert.ToInt64(row("CollectorID")),
                .CustomerID = Convert.ToInt64(row("CustomerID")),
                .PlanID = Convert.ToInt64(row("PlanID"))
                }
        Else
            MsgBox(ERRORMESSAGE)
        End If

        Return updatedTransaction
    End Function


End Class
