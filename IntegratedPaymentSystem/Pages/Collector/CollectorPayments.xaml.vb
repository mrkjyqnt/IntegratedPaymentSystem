Imports System.ComponentModel

Public Class CollectorPayments
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

    Public Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim _collectorView As CollectorView = ViewControl.CurrentView

        If sender Is ButtonCreatePayment Then

            _collectorView.NavigationChange("Payments")
            ChangeView(mainView, New CollectorSearch)

        End If
        

    End Sub

End Class
