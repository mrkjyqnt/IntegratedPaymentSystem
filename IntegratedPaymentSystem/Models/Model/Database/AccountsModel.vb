Imports System.ComponentModel
Imports System.Data

Public Class AccountsModel
    Inherits AccountsAbstract
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()
    End Sub

    Public Sub New(row As DataRow)
        Dim user = New AccountsAbstract()
        Dim columnMappings = New Dictionary(Of String, String) From {
                {"ID", "ID"},
                {"Username", "Username"},
                {"Password", "Password"},
                {"Role", "Role"}
                }

        Try
            Apply(row, Me, columnMappings)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Username = If(String.IsNullOrWhiteSpace(Username), Username, Username.Trim())
    End Sub

    Private Sub Apply(row As DataRow, instance As Object, columnMappings As Dictionary(Of String, String))
        For Each mapping In columnMappings
            Dim columnName = mapping.Key
            Dim propertyName = mapping.Value

            If row.Table.Columns.Contains(columnName) Then
                Dim value = row(columnName)
                Dim prop = instance.GetType().GetProperty(propertyName)

                If prop IsNot Nothing Then
                    prop.SetValue(instance, value)
                    RaisePropertyChanged(propertyName)
                End If
            End If
        Next
    End Sub

    Private Sub RaisePropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class