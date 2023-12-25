Imports System.ComponentModel

Public Class CollectorSearch
    Inherits UserControl

    Private ReadOnly _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
    Private ReadOnly _customerView As CollectorView = TryCast(Me.Parent, CollectorView)
    Private ReadOnly _transactions = Models.UserTransactions

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _customerView IsNot Nothing Then
            Me.DataContext = _customerView
        End If

        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If
    End Sub

    Private Sub OnSearchChanged(sender As Object, e As TextChangedEventArgs)
        Dim searchText As String = TextBoxSearch.Text.Trim.ToLower()
        Dim view As ICollectionView = CollectionViewSource.GetDefaultView(_transactions)

        If String.IsNullOrEmpty(searchText) Then
            ' If the search text is empty, clear the filter and display all items
            view.Filter = Nothing
        Else
            ' If the search text is not empty, filter items based on various properties
            view.Filter = Function(item)
                If TypeOf item Is UserTransactionsAbstract Then
                    Dim transaction = DirectCast(item, UserTransactionsModel)
                    Return Not (transaction Is Nothing) AndAlso
                           Not (transaction.Status Is Nothing) AndAlso transaction.Status.ToLower().Contains(searchText) OrElse
                           Not (transaction.ID = 0) AndAlso transaction.ID.ToString().ToLower().Contains(searchText) OrElse
                           Not (transaction.TransactionDate = Date.MinValue) AndAlso transaction.TransactionDate.ToString().ToLower().Contains(searchText) OrElse
                           Not (transaction.Amount = 0) AndAlso transaction.Amount.ToString().ToLower().Contains(searchText) OrElse
                           Not (transaction.Type Is Nothing) AndAlso transaction.Type.ToLower().Contains(searchText)
                Else
                    Return False
                End If
            End Function
        End If
    End Sub

    Private Sub OnChangeSelection(sender As Object, e As SelectionChangedEventArgs)
        If SearchItem.SelectedItem IsNot Nothing Then
            Dim selectedItem As ComboBoxItem = TryCast(SearchItem.SelectedItem, ComboBoxItem)
            Dim search As String = selectedItem.Content.ToString()
            Dim view As ICollectionView = CollectionViewSource.GetDefaultView(_transactions)
            Dim direction As ListSortDirection

            Select Case search
                Case "Ascending"
                    direction = ListSortDirection.Ascending
                Case "Descending"
                    direction = ListSortDirection.Descending
                Case Else
                    direction = ListSortDirection.Ascending
            End Select

            view.SortDescriptions.Clear()

            Dim sortDescription As New SortDescription("ID", direction)
            view.SortDescriptions.Add(sortDescription)
        End If
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = TryCast(sender, Button)

        ' Access the data item (your model) from the button's DataContext (the row's data item)
        Dim rowData As AccountInformationsModel = TryCast(button.DataContext, AccountInformationsModel)

        If rowData IsNot Nothing Then

            ChangeView(mainView, New CollectorPaymentOverview(rowData.AccountID))
            
        End If
    End Sub

End Class
