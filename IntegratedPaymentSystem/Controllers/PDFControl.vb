Imports System.IO
Imports iText.Kernel.Pdf
Imports iText.Kernel.Pdf.Canvas.Parser
Imports iText.Kernel.Pdf.Canvas.Parser.Listener
Imports System.Text
Imports iText.Layout.Element
Imports iText.Kernel.Exceptions

Module PDFControl

    Public memoryPDF As New MemoryStream

    Public Sub GeneratePdf(transactions As List(Of UserTransactionsModel), filePath As String)
        Try
            Dim pdfWriter As iText.Kernel.Pdf.PdfWriter = New iText.Kernel.Pdf.PdfWriter(filePath)
            Dim pdfDoc As iText.Kernel.Pdf.PdfDocument = New iText.Kernel.Pdf.PdfDocument(pdfWriter)
            Dim document As iText.Layout.Document = New iText.Layout.Document(pdfDoc)

            ' Create a table with columns matching your data structure
            Dim table As Table = New Table(6)

            ' Add headers to the table
            Dim headers() As String = {"Reference No.", "Status", "Type", "Date", "Amount", "Collector"}
            For Each header As String In headers
                table.AddHeaderCell(header)
            Next

            ' Body of the transaction
            For Each transaction As UserTransactionsModel In transactions
                table.AddCell(transaction.ID.ToString())
                table.AddCell(transaction.Status)
                table.AddCell(transaction.Type)
                table.AddCell(transaction.TransactionDate.ToString("MM/dd/yyyy"))
                table.AddCell(transaction.Amount.ToString())
                table.AddCell(transaction.Collector.ToString())
            Next

            ' Add the table to the document
            document.Add(table)

            ' Close the document
            document.Close()

            ' Open the saved PDF file using the default PDF viewer
            System.Diagnostics.Process.Start(filePath)
        Catch ex As Exception
            ' Handle exceptions
            Console.WriteLine("Error creating PDF: " & ex.Message)
        End Try
    End Sub

End Module
