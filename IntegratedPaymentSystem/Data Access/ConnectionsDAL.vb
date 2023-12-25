Imports System.Data

Public Class ConnectionsDAL
    Public Function GetAll() As List(Of ConnectionsModel)
        Dim list As New List(Of ConnectionsModel)

        Query("SELECT * FROM Connections")

        If DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New ConnectionsModel(row))
            Next

        Else
            MessageBox.Show("No record found")
        End If

        Return list
    End Function

    Public Function Update(data As ConnectionsModel)

        Prepare("UPDATE Connections
                        SET Status = @Status
                        WHERE AccountID = @ID")
        AddParam("@Status", data.Status)
        AddParam("@ID", data.AccountID)
        Execute()

        Return Nothing
    End Function

End Class
