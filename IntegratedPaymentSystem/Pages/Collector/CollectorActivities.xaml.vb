Imports System.ComponentModel
Imports System.Transactions

Public Class CollectorActivities
    Private ReadOnly _activities = Models.UserActivities

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
        Dim customerView As CustomerView = TryCast(Me.Parent, CustomerView)

        If customerView IsNot Nothing Then
            Me.DataContext = customerView
        End If

        If mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(mainWindow)
        End If
    End Sub
    Private Sub OnSearchChanged(sender As Object, e As TextChangedEventArgs)
        Dim searchText As String = TextBoxSearch.Text.Trim.ToLower()
        Dim view As ICollectionView = CollectionViewSource.GetDefaultView(_activities)

        If String.IsNullOrEmpty(searchText) Then
            ' If the search text is empty, clear the filter and display all items
            view.Filter = Nothing
        Else
            ' If the search text is not empty, filter items based on various properties
            view.Filter = Function(item)
                              If TypeOf item Is UserTransactionsAbstract Then
                                  Dim activities = DirectCast(item, UserActivitiesModel)
                                  Return Not (activities Is Nothing) AndAlso
                                         Not (activities.ActivityDate = Date.MinValue) AndAlso activities.ActivityDate.ToString().ToLower().Contains(searchText) OrElse
                                         Not (activities.Description Is Nothing) AndAlso activities.Description.ToLower().Contains(searchText) OrElse
                                         Not (activities.Type Is Nothing) AndAlso activities.Type.ToLower().Contains(searchText)
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
            Dim view As ICollectionView = CollectionViewSource.GetDefaultView(_activities)
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

            Dim sortDescription As New SortDescription("ActivityDate", direction)
            view.SortDescriptions.Add(sortDescription)
        End If
    End Sub
End Class
