Imports MailKit.Search
Imports Microsoft.Win32

Public Class AdminExternalTransationsImport

    Public Property Path As String
    Public Property List As List(Of ExternalTransactionsModel)
    Public Property Text As String

    Private _openFileDialog As New OpenFileDialog()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ApplyBlurEffect()
        ' Add any initialization after the InitializeComponent() call.

        Dim billingList As List(Of String) = Models.Billings.Where(Function(data) data.IsAdmin).Select(Function(data) data.Number).ToList()

        For Each prop As String In billingList
            ComboBillingList.Items.Add(prop)
        Next

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim clickedButton As Button = TryCast(sender, Button)
        Dim accountNumber As String = ComboBillingList.SelectedItem.ToString()

        If clickedButton Is ButtonEnter Then

            If ReadPdfTextWithPassword(Path, accountNumber) IsNot Nothing Then

                List = ExtractTransactions(Path, accountNumber)

                If List IsNot Nothing AndAlso List.Count > 0 Then

                    DialogResult = True

                Else

                    Message.Show("Failed", "No data extracted from the PDF.")
                    List = Nothing
                    DialogResult = False

                End If

            Else

                Message.Show("Failed", "Wrong account selected.")
                DialogResult = False
                Return

            End If

        ElseIf clickedButton Is ButtonCancel Then

            Message.Show("Failed", "User canceled.")
            RemoveBlurEffect()

        End If

        RemoveBlurEffect()
        Close()

    End Sub

    Private Sub Border_Loaded(sender As Object, e As RoutedEventArgs)
        _openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf"
        _openFileDialog.Title = "Select a PDF File"

        If _openFileDialog.ShowDialog() Then

            Path = _openFileDialog.FileName

        Else 

            Message.Show("Failed","Invalid file selection")
            Me.DialogResult = False

        End If

    End Sub
End Class
