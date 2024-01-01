Imports System.ComponentModel
Imports System.Data
Imports Microsoft.Win32

Public Class AdminExternalTransactions
    Inherits UserControl

    Private Property externalTransaction As New List(Of ExternalTransactionsModel)

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        UpdateButtonVisibility()

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
        Dim clickedButton As Button = TryCast(sender, Button)
        Dim imported As New AdminExternalTransationsImport

        If clickedButton Is ButtonImport Then

            Dim result = imported.ShowDialog()
            
            If result Then

                externalTransaction = imported.List
                TransactionData.ItemsSource = externalTransaction
                UpdateButtonVisibility()

            End If

        End If

        If clickedButton Is ButtonConfirm Then

            Message.Show("Warning", "Transactions will be processed after clicking the button, please do not close the program to prevent further system errors")

            AddExternalTransactions(externalTransaction)
            ConfirmPayments()

            If confirmedCount > 0 Then

                 Message.Show("Success", $"Import Result:</newline></newline>Imported: {importedCount}</newline>Void: {nonimportedCount}</newline></newline>Payments Verification Result: </newline></newline>Payments Confirmed: {confirmedCount}</newline>Payments Declined: {nonconfirmedCount}</newline>")

                ChangeView(mainView, New AdminPayments())

            Else

                 Message.Show("Failed", "Transaction is already recorded on the database recently")

            End If
    
           

        ElseIf clickedButton Is ButtonReset Then

            TransactionData.ItemsSource = Nothing
            RemoveBlurEffect()
            UpdateButtonVisibility()

        ElseIf clickedButton Is ButtonCancel Then

            ChangeView(mainView, New AdminPayments)

        End If  

    End Sub

    Private Sub UpdateButtonVisibility()
        If TransactionData.ItemsSource Is Nothing OrElse TransactionData.Items.Count = 0 Then
            ButtonConfirm.Visibility = Visibility.Collapsed
            ButtonReset.Visibility = Visibility.Collapsed
        Else
            ButtonConfirm.Visibility = Visibility.Visible
            ButtonReset.Visibility = Visibility.Visible
        End If
    End Sub
End Class
