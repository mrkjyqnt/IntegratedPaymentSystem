Imports System.Data

Public Class DefaultModelFunction
    Public Sub Apply(row As DataRow, obj As Object, columnMappings As Dictionary(Of String, String))
        For Each mapping In columnMappings

            Dim columnName = mapping.Key
            Dim propertyName = mapping.Value

            If row.Table.Columns.Contains(columnName) Then

                Dim value = row(columnName)

                If value IsNot DBNull.Value Then

                    Dim propInfo = obj.GetType().GetProperty(propertyName)

                    If propInfo IsNot Nothing Then

                        propInfo.SetValue(obj, Convert.ChangeType(value, propInfo.PropertyType))

                    End If

                End If

            End If
        Next
    End Sub
End Class
