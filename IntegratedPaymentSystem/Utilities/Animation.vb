Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects

Module Animation
    ' <SUMMARY>
    ' 
    ' Animation for the forms
    '
    ' <SUMMARY>

    Public Async Sub Animation(_grid As Grid)
        ' Create a new Grid
        _grid.Visibility = Windows.Visibility.Collapsed

        ' Create and add RenderTransform
        Dim translateTransform As New TranslateTransform()
        translateTransform.Y = 200
        _grid.RenderTransform = translateTransform

        Dim slideAnimation As New DoubleAnimation(200, 0, New Duration(TimeSpan.FromSeconds(0.3)))
        Dim fadeAnimation As New DoubleAnimation(0, 1, New Duration(TimeSpan.FromSeconds(0.3)))

        translateTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation)
        _grid.BeginAnimation(Grid.OpacityProperty, fadeAnimation)

        _grid.Visibility = Visibility.Visible
    End Sub


    Public Sub ApplyBlurEffect()
        Dim mainWindow As Window = Application.Current.MainWindow

        If mainWindow IsNot Nothing Then
            Dim blurEffect As New BlurEffect()
            blurEffect.Radius = 5

            mainWindow.Effect = blurEffect
        End If
    End Sub

    Public Sub RemoveBlurEffect()
        Dim mainWindow As Window = Application.Current.MainWindow

        If mainWindow IsNot Nothing Then

            If TypeOf mainWindow.Effect Is BlurEffect Then
                mainWindow.Effect = Nothing
            End If

        End If
    End Sub
End Module