Imports System.Data
Imports System.Windows.Markup

Public Class AccountInformationsDAL

    Public Function GetAll() As List(Of AccountInformationsModel)
        Dim list As New List(Of AccountInformationsModel)

        Query("SELECT * FROM AccountInformations")

        If DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New AccountInformationsModel(row))
            Next

        Else
            MessageBox.Show("No record found")
        End If

        Return list
    End Function

    Public Function Update(accountInformation As AccountInformationsModel) As AccountInformationsModel
        Dim _data As AccountInformationsModel = Nothing 

        Dim _query As String = "UPDATE AccountInformations
                                SET ContactNumber = @ContactNumber, Email = @Email
                                OUTPUT INSERTED.*
                                WHERE AccountID = @AccountID;"

        Prepare(_query)
        AddParam("@AccountID", accountInformation.AccountID)
        AddParam("@Email", accountInformation.Email)
        AddParam("@ContactNumber", accountInformation.ContactNumber)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            _data = New AccountInformationsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .AccountID = Convert.ToInt64(row("AccountID")),
                .Email = row("Email").ToString(),
                .ContactNumber = row("ContactNumber").ToString(),
                .FirstName = row("FirstName").ToString(),
                .MiddleName = row("MiddleName").ToString(),
                .LastName = row("LastName").ToString(),
                .Address = row("Address").ToString()
                }
        End If

        Return _data

    End Function

End Class
