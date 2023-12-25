Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        ViewControl.Instance.Initialize(Me)
        ViewControl.Instance.ChangeView(New Login())
    End Sub

    ' Changes the view of the grid
    Public Sub ChangeView(view As UserControl)
        displayView.Children.Clear()
        displayView.Children.Add(view)
    End Sub

    Private Sub MainWindow_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        ' Perform any cleanup or additional actions before closing the application
        ' ...

        ' Terminate the application
        Application.Current.Shutdown()
    End Sub
End Class
