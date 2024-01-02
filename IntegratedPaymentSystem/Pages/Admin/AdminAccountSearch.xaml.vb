Imports System.ComponentModel

Public Class AdminAccountSearch
    Inherits UserControl

    Private ReadOnly _mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
    Private ReadOnly _adminView As AdminView = TryCast(Me.Parent, AdminView)
    Private ReadOnly _usersInformations = Models.UsersInformation

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _adminView IsNot Nothing Then 
            Me.DataContext = _adminView
        End If

        If _mainWindow IsNot Nothing Then
            ViewControl.Instance.Initialize(_mainWindow)
        End If
    End Sub

    Private Sub OnSearchChanged(sender As Object, e As TextChangedEventArgs)
        Dim searchText As String = TextBoxSearch.Text.Trim.ToLower()
        Dim view As ICollectionView = CollectionViewSource.GetDefaultView(_usersInformations)

        If String.IsNullOrEmpty(searchText) Then
            ' If the search text is empty, clear the filter and display all items
            view.Filter = Nothing
        Else
            ' If the search text is not empty, filter items based on various properties
            view.Filter = Function(item)
                If TypeOf item Is AccountInformationsModel Then
                    Dim information = DirectCast(item, AccountInformationsModel)
                    Return Not (information Is Nothing) AndAlso
                           Not (information.AccountID = 0) AndAlso information.AccountID.ToString().ToLower().Contains(searchText) OrElse
                           Not (information.ID = 0) AndAlso information.ID.ToString().ToLower().Contains(searchText) OrElse
                           Not (information.FirstName Is Nothing) AndAlso information.FirstName.ToLower().Contains(searchText) OrElse
                           Not (information.MiddleName Is Nothing) AndAlso information.MiddleName.ToLower().Contains(searchText) OrElse
                           Not (information.LastName Is Nothing) AndAlso information.LastName.ToLower().Contains(searchText) OrElse
                           Not (information.Address Is Nothing) AndAlso information.Address.ToLower().Contains(searchText) OrElse
                           Not (information.ContactNumber Is Nothing) AndAlso information.ContactNumber.ToLower().Contains(searchText) OrElse
                           Not (information.Email Is Nothing) AndAlso information.Email.ToLower().Contains(searchText)
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
            Dim view As ICollectionView = CollectionViewSource.GetDefaultView(_usersInformations)
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
        Dim rowData As UserInformationModel = TryCast(button.DataContext, UserInformationModel)

        If button Is ButtonAdd Then


        Else

            If rowData IsNot Nothing Then

                ChangeView(mainView, New AdminAccountsView(rowData))

            End If

        End If
        
    End Sub

End Class
