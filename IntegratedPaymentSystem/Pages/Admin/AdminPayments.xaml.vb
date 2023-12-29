Imports System.ComponentModel

Public Class AdminPayments
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

        If Models.InternalTransactions IsNot Nothing Then

            TextMonth.Text = Date.Today.ToString("MMMM")

            TextPending.Text = Models.InternalTransactions.Where(Function(data) data.Status = "Pending").Count.ToString()
            TextConfirmed.Text = Models.InternalTransactions.Where(Function(data) data.Status = "Confirmed").Count.ToString()
            TextDeclined.Text = Models.InternalTransactions.Where(Function(data) data.Status = "Declined").Count.ToString()

        End If
    End Sub

    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If sender Is ButtonConfirmPayments Then

            ChangeView(mainView, New AdminExternalTransactions())

        End If

    End Sub

End Class
