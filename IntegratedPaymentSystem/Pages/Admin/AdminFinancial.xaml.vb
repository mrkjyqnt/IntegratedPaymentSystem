Imports System.ComponentModel

Public Class AdminFinancial
    Inherits UserControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        CreateFinanceData()

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

    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)
        
        If sender Is ButtonBilling Then

            ChangeView(mainView, New AdminBillingSearch())
            
        ElseIf sender Is ButtonPrint Then

            MsgBox("No")

        End If
        

    End Sub

    Private Sub TransactionData_SelectionChanged()

    End Sub
End Class
