Imports System.Data

Public Class InternetPlansDAL

    Public Function GetAll() As List(Of InternetPlansModel)
        Dim list As New List(Of InternetPlansModel)

        Query("SELECT * FROM InternetPlans")

        If DATA.Tables.Count > 0 AndAlso DATA.Tables(0).Rows.Count > 0 Then

            For Each row As DataRow In DATA.Tables(0).Rows
                list.Add(New InternetPlansModel(row))
            Next

        End If

        Return list
    End Function

End Class
