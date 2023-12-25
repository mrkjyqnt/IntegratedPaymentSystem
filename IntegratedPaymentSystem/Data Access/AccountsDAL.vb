Imports System.Data
Imports System.Windows.Markup

Public Class AccountsDAL

    Public Function GetAll() As List(Of AccountsModel)
        Dim list As New List(Of AccountsModel)

        Query("SELECT * FROM Accounts")

        If DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New AccountsModel(row))
            Next

        Else
            MessageBox.Show("No record found")
        End If

        Return list
    End Function

    Public Function GetUserByUsername(username As String) As AccountsModel

        Dim query As String = "SELECT * FROM Accounts WHERE Username = @Username"

        Prepare(query)
        AddParam("@Username", username)
        Execute()

        If Not HASERROR Then
            Dim _data As DataSet = DATA

            If HASRECORD Then
                Return New AccountsModel(_data.Tables(0).Rows(0))
            End If

        Else
            MessageBox.Show($"Error: {ERRORMESSAGE}")
        End If

        Return Nothing

    End Function

    Public Function Update(account As AccountsModel) As AccountsModel
        Dim _data As AccountsModel = Nothing 

        Dim _query As String = "UPDATE Accounts
                                SET Password = @Password
                                OUTPUT INSERTED.*
                                WHERE Username = @Username;"

        Prepare(_query)
        AddParam("@Password", account.Password)
        AddParam("@Username", account.Username)
        Execute()

        If DATA IsNot Nothing AndAlso DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then
            Dim row = DATA.Tables(0).Rows(0)
            _data = New AccountsModel() With {
                .ID = Convert.ToInt64(row("ID")),
                .Username = row("Username").ToString(),
                .Password = row("Password").ToString(),
                .Role = row("Role").ToString()
                }
        End If

        Return _data
    End Function

End Class
