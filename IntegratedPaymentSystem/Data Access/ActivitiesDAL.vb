Imports System.Data
Imports System.Windows.Interop

Public Class ActivitiesDAL
    Public Function GetAll() As List(Of ActivitiesModel)
        Dim list As New List(Of ActivitiesModel)

        Query("SELECT * FROM Activities")

        If DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New ActivitiesModel(row))
            Next

        End If

        Return list
    End Function

    Public Function Create(activity As ActivitiesModel) As ActivitiesModel
        Dim listRow As ActivitiesModel = Nothing
        Dim query As String = "INSERT INTO Activities (AccountID, Description, Amount, ActivityDate, Type, Category) 
                              OUTPUT INSERTED.* 
                              VALUES (@AccountID, @Description, @Amount, @ActivityDate, @Type, @Category);"

        Prepare(query)
        AddParam("@AccountID", activity.AccountID)
        AddParam("@Description", activity.Description)
        AddParam("@Amount", activity.Amount)
        AddParam("@ActivityDate", activity.ActivityDate)
        AddParam("@Type", activity.Type)
        AddParam("@Category", activity.Category)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            listRow = New ActivitiesModel() With {
                          .ID = Convert.ToInt64(row("ID")),
                          .AccountID = Convert.ToInt64(row("AccountID")),
                          .Description = row("Description").ToString(),
                          .Amount = Convert.ToDouble(row("Amount")),
                          .ActivityDate = Convert.ToDateTime(row("ActivityDate")),
                          .Type = row("Type").ToString(),
                          .Category = row("Category").ToString()
                }

        End If
        Return listRow
    End Function

End Class
