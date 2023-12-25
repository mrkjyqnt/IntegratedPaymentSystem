Imports Microsoft.Win32

Public Class CustomerPrint

    Public Sub New

        ' This call is required by the designer.
        InitializeComponent()

        ApplyBlurEffect()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If sender Is ButtonPrint Then

            Dim fromDateValue As Date = FromDate.SelectedDate.GetValueOrDefault()
            Dim toDateValue As Date = ToDate.SelectedDate.GetValueOrDefault()

            Dim list As List(Of UserTransactionsModel) = UserTransactionsModel.GetAllByDate(Models.User.ID, fromDateValue, toDateValue)

            Dim saveDialog As New SaveFileDialog()

            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.FileName = "CustomerTransaction_" & DateTime.Now.ToString("yyyyMMdd") & ".pdf"

            Dim result As Boolean? = saveDialog.ShowDialog()

            If result = True Then
                Dim filePath As String = saveDialog.FileName
                ' Generate PDF using the provided transactions list
                PDFControl.GeneratePdf(Models.UserTransactions, filePath)
            End If

        End If

        RemoveBlurEffect()
        Close()

    End Sub
End Class
