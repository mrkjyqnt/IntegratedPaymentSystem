Public Class CollectorDashboard
    Inherits UserControl

    ' <summary>
    '
    ' Main function of the CustomerDashboard
    '
    ' <summary>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Dim _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)

        ' Add any initialization after the InitializeComponent() call.
        ' Use always to prevent mainView and dashView collision

        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If

        If Models.UserTransactions IsNot Nothing Then
            TextTransactionToday.Text = Models.UserTransactions.Where(Function(data) data.TransactionDate.Date = Date.Now.Date).Count.ToString()

            TextTransactionWeek.Text = Models.UserTransactions.Where(Function(data) data.TransactionDate.Date >= Date.Today.AddDays(-CInt(Date.Today.DayOfWeek)).Date AndAlso data.TransactionDate.Date <= Date.Today.AddDays(6 - CInt(Date.Today.DayOfWeek)).Date).Count.ToString()

            TextTransactionMonth.Text = Models.UserTransactions.Where(Function(data) data.TransactionDate.Month = Date.Now.Month).Count.ToString()
        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim clickedButton As Button = TryCast(sender, Button)
        Dim _collectorView As CollectorView = ViewControl.CurrentView

        If clickedButton Is ButtonTransactions Then

            _collectorView.NavigationChange("Transactions")
            ChangeView(mainView, New CollectorTransaction)

        ElseIf clickedButton Is ButtonPayment Then

            _collectorView.NavigationChange("Payments")
            ChangeView(mainView, New CollectorPayments)

        ElseIf clickedButton Is ButtonSupport Then

            NavigateWebURL("https://www.facebook.com/profile.php")

        End If

    End Sub
End Class
