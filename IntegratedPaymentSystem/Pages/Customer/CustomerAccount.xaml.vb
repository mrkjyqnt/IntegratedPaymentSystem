Imports System.Data
Imports System.Security.Principal
Imports iText.StyledXmlParser.Jsoup.Select

Public Class CustomerAccount
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
        Dim _customerView As CustomerView = ViewControl.CurrentView

        If sender Is ButtonTransactions Then

            _customerView.NavigationChange("Transactions")
            ChangeView(mainView, new CustomerTransaction())

        ElseIf sender Is ButtonInformation Then

            Dim showInformation As New CustomerInformation()
            showInformation.ShowDialog()
            ChangeView(mainView, new CustomerAccount())

        ElseIf sender Is ButtonActivities Then

            _customerView.NavigationChange("Activities")
            ChangeView(mainView, new CustomerActivities)

        End If

    End Sub
End Class
