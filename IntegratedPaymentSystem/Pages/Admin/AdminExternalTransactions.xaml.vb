Imports System.ComponentModel
Imports System.Data
Imports Microsoft.Win32

Public Class AdminExternalTransactions
    Inherits UserControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
        Dim customerView As CollectorView = TryCast(Me.Parent, CollectorView)

        If customerView IsNot Nothing Then
            Me.DataContext = customerView
        End If

        If mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(mainWindow)
        End If
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog As New OpenFileDialog()
        openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf"
        openFileDialog.Title = "Select a PDF File"

        If sender Is ButtonImport Then
            If openFileDialog.ShowDialog() Then
                Dim filePath As String = openFileDialog.FileName

                ' Get the password from the PasswordBox

                If Message.Confirm("PDF Password", "Please enter a valid password</newline></newline>Usual password consists of Billing Account's Last Name and Last 4 of Contact Number</newline>Ex: delacruz1234", "Empty") Then

                    If IsPasswordCorrect(filePath, CustomConfirmationBox._confirmedText) Then

                        ' Extract reference numbers and debits
                        Dim extractedData As List(Of Tuple(Of String, String)) = ExtractReferenceAndDebit(filePath)

                        If extractedData IsNot Nothing AndAlso extractedData.Count > 0 Then
                            ' Assuming your DataGrid is named 'TransactionData'
                            TransactionData.ItemsSource = Nothing
                            TransactionData.ItemsSource = extractedData
                        Else
                            Message.Show("Failed", "No data extracted from the PDF.")
                        End If
                    Else
                        Message.Show("Failed", "Invalid password.")
                    End If

                End If
            End If
        End If
    End Sub


End Class
