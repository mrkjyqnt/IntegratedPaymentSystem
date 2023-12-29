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

    Public Function ExtractReferenceAndDebit(filePath As String) As List(Of Tuple(Of String, String))
        Dim refAndDebit As New List(Of Tuple(Of String, String))()

        Try
            Dim text As New StringBuilder()

            Using pdfReader As New PdfReader(filePath, New ReaderProperties().SetPassword(passwordBytes))
                Using pdfDoc As New PdfDocument(pdfReader)
                    For pageNum As Integer = 1 To pdfDoc.GetNumberOfPages()
                        Dim strategy As ITextExtractionStrategy = New LocationTextExtractionStrategy()
                        Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(pageNum), strategy)
                        text.Append(currentText)
                    Next
                End Using
            End Using

            Dim extractedText As String = text.ToString()

            ' Define a regex pattern to match the table structure
            Dim pattern As String = "Date and Time\s+Description\s+Reference No\s+Debit\s+Credit\s+Balance(.*?)Total Credit"
            Dim tableRegex As New Regex(pattern, RegexOptions.Singleline)
            Dim match As Match = tableRegex.Match(extractedText)

            If match.Success Then
                Dim tableText As String = match.Groups(1).Value.Trim()

                ' Split the table text into rows
                Dim rows() As String = tableText.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                ' Find the header index to identify columns
                Dim headerIndex As Integer = -1
                For i As Integer = 0 To rows.Length - 1
                    If rows(i).Contains("Reference No.") Then
                        headerIndex = i
                        Exit For
                    End If
                Next

                ' Start processing the rows after the header
                If headerIndex >= 0 Then
                    For i As Integer = headerIndex + 1 To rows.Length - 1
                        Dim columns() As String = rows(i).Split({" "c}, StringSplitOptions.RemoveEmptyEntries)
                        If columns.Length >= 4 Then ' Assuming Reference No. and Debit are at least 4 columns apart
                            Dim referenceNo As String = columns(2).Trim()
                            Dim debit As String = columns(3).Trim()
                            refAndDebit.Add(New Tuple(Of String, String)(referenceNo, debit))
                        End If
                    Next
                Else
                    Console.WriteLine("Header not found")
                End If
            Else
                Console.WriteLine("Table extraction failed")
            End If
        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
        End Try

        Return refAndDebit
    End Function

    Public Function IsPasswordCorrect(filePath As String, password As String) As Boolean
        Try
            passwordBytes = Encoding.UTF8.GetBytes(password)

            Using pdfReader As New PdfReader(filePath, New ReaderProperties().SetPassword(passwordBytes))
                Return True
            End Using

        Catch ex As BadPasswordException
            Return False

        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Failed")
            Return False
        End Try
    End Function

End Module
