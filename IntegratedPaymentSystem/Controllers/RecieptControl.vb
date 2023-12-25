Imports System.IO
Imports System.Reflection
Imports System.Transactions

Module RecieptControl
    Public Function GenerateCustomerReceipt(transaction As UserTransactionsModel, connection As UserConnectionModel) As String
        Try

            Dim currentDirectory As String = Directory.GetCurrentDirectory()
            Dim projectRoot As String = currentDirectory

            For i As Integer = 1 To 5 
                projectRoot = Directory.GetParent(projectRoot).FullName
                If Directory.Exists(Path.Combine(projectRoot, "IntegratedPaymentSystem")) Then 
                    Exit For
                End If
            Next

            ' Check if the assembly location is available
            If Not String.IsNullOrEmpty(currentDirectory) Then
                ' Combine the executing assembly directory with the relative path
                Dim relativePath As String = "IntegratedPaymentSystem\Pages\Receipts\CustomerReceipt.txt"
                Dim fullPath As String = Path.Combine(projectRoot, relativePath)

                If File.Exists(fullPath) Then
                    Dim htmlContent As String = File.ReadAllText(fullPath)
                    ' Replace placeholders with actual data
                    htmlContent = htmlContent.Replace("[Collector's Name]", transaction.Collector)
                    htmlContent = htmlContent.Replace("[Transaction Amount]", transaction.Amount.ToString("0.00"))
                    htmlContent = htmlContent.Replace("[Date of Transaction]", transaction.TransactionDate.ToString("MM/dd/yyyy"))
                    htmlContent = htmlContent.Replace("[Type of Plan]", connection.PlanName)
                    htmlContent = htmlContent.Replace("[Status of Payment]", transaction.Status)

                    Return htmlContent
                Else
                    ' Handle case where the file doesn't exist at the expected location
                    MessageBox.Show("File not found.")
                    Return Nothing
                End If
            Else
                ' Handle case where the executing assembly location is null or empty
                MessageBox.Show("Executing assembly location not available.")
                Return Nothing
            End If
        Catch ex As Exception
            ' Handle exceptions here, such as logging the error or displaying a message box
            MessageBox.Show("Error generating receipt: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GenerateConfirmationReceipt(transaction As UserTransactionsModel, connection As UserConnectionModel) As String
        Try

            Dim currentDirectory As String = Directory.GetCurrentDirectory()
            Dim projectRoot As String = currentDirectory

            For i As Integer = 1 To 5 
                projectRoot = Directory.GetParent(projectRoot).FullName
                If Directory.Exists(Path.Combine(projectRoot, "IntegratedPaymentSystem")) Then 
                    Exit For
                End If
            Next

            ' Check if the assembly location is available
            If Not String.IsNullOrEmpty(currentDirectory) Then
                ' Combine the executing assembly directory with the relative path
                Dim relativePath As String = "IntegratedPaymentSystem\Pages\Receipts\ReceiveReceipt.txt"
                Dim fullPath As String = Path.Combine(projectRoot, relativePath)

                If File.Exists(fullPath) Then
                    Dim htmlContent As String = File.ReadAllText(fullPath)
                    ' Replace placeholders with actual data
                    htmlContent = htmlContent.Replace("[Collector's Name]", transaction.Collector)
                    htmlContent = htmlContent.Replace("[Transaction Amount]", transaction.Amount.ToString("0.00"))
                    htmlContent = htmlContent.Replace("[Date of Transaction]", transaction.TransactionDate.ToString("MM/dd/yyyy"))
                    htmlContent = htmlContent.Replace("[Type of Plan]", connection.PlanName)
                    htmlContent = htmlContent.Replace("[Status of Payment]", transaction.Status)

                    Return htmlContent
                Else
                    ' Handle case where the file doesn't exist at the expected location
                    MessageBox.Show("File not found.")
                    Return Nothing
                End If
            Else
                ' Handle case where the executing assembly location is null or empty
                MessageBox.Show("Executing assembly location not available.")
                Return Nothing
            End If
        Catch ex As Exception
            ' Handle exceptions here, such as logging the error or displaying a message box
            MessageBox.Show("Error generating receipt: " & ex.Message)
            Return Nothing
        End Try
End Function

     Public Function GenerateConfirmationCode(code As String) As String
        Try

            Dim currentDirectory As String = Directory.GetCurrentDirectory()
            Dim projectRoot As String = currentDirectory

            For i As Integer = 1 To 5 
                projectRoot = Directory.GetParent(projectRoot).FullName
                If Directory.Exists(Path.Combine(projectRoot, "IntegratedPaymentSystem")) Then 
                    Exit For
                End If
            Next
            
            ' Check if the assembly location is available
            If Not String.IsNullOrEmpty(currentDirectory) Then
                ' Combine the executing assembly directory with the relative path
                Dim relativePath As String = "IntegratedPaymentSystem\Pages\Receipts\ConfirmationCode.txt"
                Dim fullPath As String = Path.Combine(projectRoot, relativePath)

                If File.Exists(fullPath) Then
                    Dim htmlContent As String = File.ReadAllText(fullPath)
                    ' Replace placeholders with actual data
                    htmlContent = htmlContent.Replace("[Confirmation Code]", code)

                    Return htmlContent
                Else
                    ' Handle case where the file doesn't exist at the expected location
                    MessageBox.Show("File not found.")
                    Return Nothing
                End If
            Else
                ' Handle case where the executing assembly location is null or empty
                MessageBox.Show("Executing assembly location not available.")
                Return Nothing
            End If
        Catch ex As Exception
            ' Handle exceptions here, such as logging the error or displaying a message box
            MessageBox.Show("Error generating receipt: " & ex.Message)
            Return Nothing
        End Try
    End Function
End Module
