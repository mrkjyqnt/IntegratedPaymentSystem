Imports iText.Kernel.Pdf
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Globalization
Imports iText.Kernel.Exceptions
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Pdf.Canvas.Parser.Listener
Imports System

Module GCashControl

    Private Property passwordBytes As Byte()
    Private Property _password As String

     Public Function ReadPdfTextWithPassword(filePath As String, accountNumber As String) As String
        Dim pdfText As New StringBuilder()

        Dim billings As BillingsModel = Models.Billings.FirstOrDefault(function(data) data.Number = accountNumber)
        _password = billings.Name.ToLower().Split(" "c).Last() + billings.Number.Substring(billings.Number.Length - 4)
        passwordBytes = Encoding.UTF8.GetBytes(_password)

        Try
            Using pdfReader As New PdfReader(filePath, New ReaderProperties().SetPassword(passwordBytes))
                Using pdfDoc As New PdfDocument(pdfReader)
                    For pageNum As Integer = 1 To pdfDoc.GetNumberOfPages()
                        Dim strategy As ITextExtractionStrategy = New SimpleTextExtractionStrategy()
                        Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(pageNum), strategy)
                        pdfText.Append(currentText)
                    Next
                End Using
            End Using
         Catch ex As BadPasswordException

            Return Nothing

        Catch ex As Exception

            Message.Show("Failed", $"Error reading PDF: {ex.Message}")
            Return Nothing

        End Try

        Return pdfText.ToString()
    End Function
    
    Public Function ExtractTransactions(filePath As String, accountNumber As String) As List(Of ExternalTransactionsModel)
        Dim transactions As New List(Of ExternalTransactionsModel)()
        Dim billings As BillingsModel = Models.Billings.FirstOrDefault(function(data) data.Number = accountNumber)
        _password = billings.Name.ToLower().Split(" "c).Last() + billings.Number.Substring(billings.Number.Length - 4)
        passwordBytes = Encoding.UTF8.GetBytes(_password)
        
        Try
            Dim extractedText As String = ReadPdfTextWithPassword(filePath, accountNumber)

            Dim pattern As String = "(\d{4}-\d{2}-\d{2} \d{2}:\d{2} [AP]M)\s+(Transfer from \d+ to " & accountNumber & ")\s+(\d+)\s+([\d.]+)"

            Dim matches As MatchCollection = Regex.Matches(extractedText, pattern, RegexOptions.Multiline)

            For Each match As Match In matches
                
                Dim dateTime As String = match.Groups(1).Value.Trim()
                Dim description As String = match.Groups(2).Value.Trim()
                Dim referenceNo As String = match.Groups(3).Value.Trim()
                Dim debit As String = match.Groups(4).Value.Trim()

                Dim externalTransaction As New ExternalTransactionsModel() With {
                        .TransactionDate = dateTime,
                        .Description = description,
                        .ReferenceNumber = referenceNo,
                        .Amount = debit
                        }

                transactions.Add(externalTransaction)
            Next
        Catch ex As Exception
            MsgBox($"Error extracting transactions: {ex.Message}")
        End Try

        Return transactions
    End Function

End Module
