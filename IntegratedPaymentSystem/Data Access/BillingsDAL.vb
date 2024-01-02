Imports System.Data

Public Class BillingsDAL
    Public Function GetAll() As List(Of BillingsModel)
        Dim list As New List(Of BillingsModel)

        Query("SELECT * FROM Billings")

        If DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New BillingsModel(row))
            Next

        End If

        Return list
    End Function

     Public Function Read(billing As BillingsModel) As BillingsModel
        Dim _billing As New BillingsModel

        Query($"SELECT * FROM Billings WHERE ID = {billing.ID}")

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            _billing = New BillingsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .AccountID = Convert.ToInt64(row("AccountID")),
                .Name = row("Name").ToString(),
                .Number = row("Number").ToString(),
                .Type = row("Type").ToString(),
                .IsAdmin = Convert.ToBoolean(row("IsAdmin")),
                .IsEnabled = Convert.ToBoolean(row("IsEnabled"))
                }
        End If

    Return _billing
    End Function

    Public Function Create(billing As BillingsModel) As BillingsModel
        Dim _billing As BillingsModel = Nothing
        Dim query As String = "INSERT INTO Billings (AccountID, Name, Number, Type, IsAdmin, IsEnabled) 
                              OUTPUT INSERTED.* 
                              VALUES (@AccountID, @Name, @Number, @Type, @IsAdmin, @IsEnabled);"

        Prepare(query)
        AddParam("@ID", billing.ID)
        AddParam("@AccountID", billing.AccountID)
        AddParam("@Name", billing.Name)
        AddParam("@Number", billing.Number)
        AddParam("@Type", billing.Type)
        AddParam("@IsAdmin", billing.IsAdmin)
        AddParam("@IsEnabled", billing.IsEnabled)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            _billing = New BillingsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .AccountID = Convert.ToInt64(row("AccountID")),
                .Name = row("Name").ToString(),
                .Number = row("Number").ToString(),
                .Type = row("Type").ToString(),
                .IsAdmin = Convert.ToBoolean(row("IsAdmin")),
                .IsEnabled = Convert.ToBoolean(row("IsEnabled"))
                }
        End If

    Return _billing
    End Function

    Public Function Update(billing As BillingsModel) As BillingsModel
        Dim _billing As BillingsModel = Nothing
        Dim query As String = "UPDATE Billings 
                                    SET AccountID = @AccountID,
                                        Name = @Name,
                                        Number = @Number,
                                        Type = @Type,
                                        IsAdmin = @IsAdmin,
                                        IsEnabled = @IsEnabled
                                    WHERE ID = @ID;
                                    SELECT * FROM Billings WHERE ID = @ID;"

        Prepare(query)
        AddParam("@ID", billing.ID)
        AddParam("@AccountID", billing.AccountID)
        AddParam("@Name", billing.Name)
        AddParam("@Number", billing.Number)
        AddParam("@Type", billing.Type)
        AddParam("@IsAdmin", billing.IsAdmin)
        AddParam("@IsEnabled", billing.IsEnabled)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            _billing = New BillingsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .AccountID = Convert.ToInt64(row("AccountID")),
                .Name = row("Name").ToString(),
                .Number = row("Number").ToString(),
                .Type = row("Type").ToString(),
                .IsAdmin = Convert.ToBoolean(row("IsAdmin")),
                .IsEnabled = Convert.ToBoolean(row("IsEnabled"))
                }
        End If

        Return _billing
    End Function

    Public Function Delete(billing As BillingsModel)

        Query($"DELETE FROM Billings WHERE ID = {billing.ID}")

        Return Nothing
    End Function

End Class
