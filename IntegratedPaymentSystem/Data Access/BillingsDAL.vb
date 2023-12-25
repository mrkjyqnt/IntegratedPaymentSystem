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

End Class
