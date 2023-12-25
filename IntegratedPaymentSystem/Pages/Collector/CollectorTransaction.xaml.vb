Imports System.ComponentModel

Public Class CollectorTransaction
    Private ReadOnly _transactions = Models.UserTransactions

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

        If Models.UserTransactions IsNot Nothing Then
            TextTransactionToday.Text = Models.UserTransactions.Where(Function(data) data.TransactionDate.Date = Date.Now.Date).Count.ToString()

            TextTransactionWeek.Text = Models.UserTransactions.Where(Function(data) data.TransactionDate.Date >= Date.Today.AddDays(-CInt(Date.Today.DayOfWeek)).Date AndAlso data.TransactionDate.Date <= Date.Today.AddDays(6 - CInt(Date.Today.DayOfWeek)).Date).Count.ToString()

            TextTransactionMonth.Text = Models.UserTransactions.Where(Function(data) data.TransactionDate.Month = Date.Now.Month).Count.ToString()
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

End Class
