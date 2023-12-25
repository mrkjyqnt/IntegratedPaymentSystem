Imports System.Windows.Interop

Public Class CustomerDashboard
    Inherits UserControl

    ' <summary>
    '
    ' Main function of the CustomerDashboard
    '
    ' </summary>
    

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        

        ' Add any initialization after the InitializeComponent() call.
        ' Use always to prevent mainView and dashView collision
        Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
        Dim latestTransaction As InternalTransactionsModel = Models.InternalTransactions _
                .OrderByDescending(Function(data) data.TransactionDate) _
                .FirstOrDefault(Function(data) data.CustomerID = Models.User.ID)



        If mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(mainWindow)
        End If

        if latestTransaction IsNot Nothing Then
            CreatePaymentDate(latestTransaction.TransactionDate)

            If Month(latestTransaction.TransactionDate) = Date.Today.Month Then

                TextPaymentSubtitle.Text = latestTransaction.Amount.ToString("0.00")
                ButtonPay.IsEnabled = False
                ButtonPay.Background = TryCast(Application.Current.Resources("PositiveBrush"), Brush)
                ButtonPay.Content = "Paid"
                TextDuePaymentTitle.Text = "Next Payment"
                TextDuePaymentDate.Text = $"{ThisMonth:MMMM dd, yyyy} - {ThisMonthSpan:MMMM dd, yyyy}"

            Else 

                If latestTransaction.Status = "Pending" Then

                    TextPaymentSubtitle.Text = latestTransaction.Amount
                    ButtonPay.IsEnabled = False
                    ButtonPay.Background = TryCast(Application.Current.Resources("NeutralBrush"), Brush)
                    ButtonPay.Content = "Pending"
                    TextDuePaymentTitle.Text = "Paid On"
                    TextDuePaymentDate.Text = latestTransaction.TransactionDate.ToString("MMMM dd, yyyy")

                Else 

                    If IsEligible() Then

                        TextDuePaymentDate.Text = ThisMonth.ToString("MMMM dd, yyyy") & " - " & ThisMonthSpan.ToString("MMMM dd, yyyy")
                        TextDuePaymentDate.Foreground = TryCast(Application.Current.Resources("NeutralBrush"), Brush)
                        TextPaymentTitle.Text = "Amount"
                        TextPaymentSubtitle.Text = Models.UserConnection.PlanAmount

                    Else 

                        TextDuePaymentDate.Foreground = TryCast(Application.Current.Resources("NegativeBrush"), Brush)
                        TextDuePaymentDate.Text = ThisMonth.ToString("MMMM dd, yyyy") & " - " & ThisMonthSpan.ToString("MMMM dd, yyyy")
                        TextPaymentTitle.Text = "Due Exceeded"
                        TextPaymentSubtitle.Text = "Please go to the nearest store"
                        ButtonPay.Background = TryCast(Application.Current.Resources("NegativeBrush"), Brush)
                        ButtonPay.IsEnabled = False
                        ButtonPay.Content = "Unpaid"


                    End If

                End If

            End If

        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim clickedButton As Button = TryCast(sender, Button)
        Dim _customerView As CustomerView = ViewControl.CurrentView


        If clickedButton Is ButtonAccount Then
           
            _customerView.NavigationChange("Account")
            ChangeView(mainView, New CustomerAccount)

        ElseIf clickedButton Is ButtonTransaction Then
            
            _customerView.NavigationChange("Transactions")
            ChangeView(mainView, New CustomerTransaction)

        ElseIf clickedButton Is ButtonPay Then
            
            _customerView.NavigationChange("Dashboard")
            ChangeView(mainView, New CustomerPayment)

        End If

    End Sub
End Class
