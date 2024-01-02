Imports iText.Layout.Properties

Public Class AdminDashboard
    Inherits UserControl

    ' <summary>
    '
    ' Main function of the CustomerDashboard
    '
    ' <summary>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        CreateFinanceData()
        Dim _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)

        ' Add any initialization after the InitializeComponent() call.
        ' Use always to prevent mainView and dashView collision

        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If

        If Models.InternalTransactions IsNot Nothing Then

            TextMonth.Text = Date.Today.ToString("MMMM")

            TextPending.Text = Models.InternalTransactions.Where(Function(data) data.Status = "Pending" AndAlso data.TransactionDate.Month = Date.Today.Month).Count.ToString()
            TextConfirmed.Text = Models.InternalTransactions.Where(Function(data) data.Status = "Confirmed" AndAlso data.TransactionDate.Month = Date.Today.Month).Count.ToString()
            TextDeclined.Text = Models.InternalTransactions.Where(Function(data) data.Status = "Declined" AndAlso data.TransactionDate.Month = Date.Today.Month).Count.ToString()

            TextWeekSales.Text = "₱" & ThisWeekSales.ToString("#,##0")
            TextMonthSales.Text = "₱" & ThisMonthSales.ToString("#,##0")

            TextWeekSalesPercent.Text = WeeksPercentage.ToString("0.00") & "%"
            TextMonthSalesPercent.Text = MonthsPercentage.ToString("0.00") & "%"

            If WeekResult Then
                TextWeekSalesPercent.Foreground = TryCast(Application.Current.Resources("PositiveBrush"), Brush)
            Else 
                TextWeekSalesPercent.Foreground = TryCast(Application.Current.Resources("NegativeBrush"), Brush)
            End If

            If MonthResult Then
                TextMonthSalesPercent.Foreground = TryCast(Application.Current.Resources("PositiveBrush"), Brush)
            Else 
                TextMonthSalesPercent.Foreground = TryCast(Application.Current.Resources("NegativeBrush"), Brush)
            End If

        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim clickedButton As Button = TryCast(sender, Button)
        Dim _adminView  As AdminView = ViewControl.CurrentView

        If clickedButton Is ButtonFinancial Then

            _adminView.NavigationChange("Financial")
            ChangeView(mainView, New AdminFinancial)

        ElseIf clickedButton Is ButtonPayments Then

            _adminView.NavigationChange("Payments")
            ChangeView(mainView, New AdminPayments)

        ElseIf clickedButton Is ButtonUserManagement Then

            Message.Show("Failed", "Currently not available")

        ElseIf clickedButton Is ButtonDataBackup Then

            Message.Show("Failed", "Currently not available")


        End If

    End Sub
End Class
