Imports System.ComponentModel
Imports System.Data
Imports System.Security.Principal
Imports System.Windows.Interop
Imports iText.StyledXmlParser.Jsoup.Select

Public Class CollectorPaymentOverview
    Inherits UserControl

    Implements INotifyPropertyChanged
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private ReadOnly _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
    Private customerData As New CollectorView

    Public Shared Property ToPay As Double

    Public Sub New(ID As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DataContext = customerData

        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If

        customerData.CustomerData = UserInformationModel.GetAllByID(ID)
        customerData.CustomerConnectionData = UserConnectionModel.GetAllByID(ID)
        customerData.CustomerTransactionsData = UserTransactionsModel.GetAllByCustomerID(ID)
        Models.CustomerInformation = customerData.CustomerData
        Models.CustomerConnection = customerData.CustomerConnectionData
        Models.CustomerTransactions = customerData.CustomerTransactionsData

        Dim latestTransaction As UserTransactionsModel = customerData.CustomerTransactionsData _
            .Where(Function(data) data.TransactionDate > Date.MinValue) _
            .OrderByDescending(Function(data) data.TransactionDate) _
            .FirstOrDefault()


        If latestTransaction IsNot Nothing Then

            CreatePaymentDate(latestTransaction.TransactionDate)

            If Month(latestTransaction.TransactionDate) = Date.Today.Month Then

                TextAmount.Text = latestTransaction.Amount
                TextButtonText.Text = latestTransaction.Status
                ButtonPay.IsEnabled = False
                ButtonPay.Background = TryCast(Application.Current.Resources("PositiveBrush"), Brush)

            Else 

                If latestTransaction.Status = "Pending" Then

                    TextAmount.Text = latestTransaction.Amount
                    TextButtonText.Text = latestTransaction.Status
                    ButtonPay.IsEnabled = False
                    ButtonPay.Background = TryCast(Application.Current.Resources("NeutralBrush"), Brush)

                ElseIf latestTransaction.Status = "Declined" Then

                    TextAmount.Text = latestTransaction.Amount
                    TextButtonText.Text = latestTransaction.Status
                    ButtonPay.IsEnabled = False
                    ButtonPay.Background = TryCast(Application.Current.Resources("NegativeBrush"), Brush)

                Else

                    ToPay = CalculatePayment(customerData.CustomerConnectionData.PlanAmount)
                    TextAmount.Text = ToPay.ToString()

                End If

            End If

        Else 

            ToPay = customerData.CustomerConnectionData.PlanAmount
            TextAmount.Text = ToPay.ToString()

        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If sender Is ButtonPay Then

            ChangeView(mainView, New CollectorPaymentConfirmation())

        End If


    End Sub
End Class
