Public Class ViewControl
    Private Shared _instance As ViewControl
    Private _mainWindow As MainWindow
    Private Shared _currentView As UserControl ' Temporary storage for the current view

    Private Sub New()
    End Sub

    Public Shared ReadOnly Property Instance() As ViewControl
        Get
            If _instance Is Nothing Then
                _instance = New ViewControl()
            End If
            Return _instance
        End Get
    End Property

    Public Sub Initialize(mainWindow As MainWindow)
        _mainWindow = mainWindow
    End Sub

    Public Sub ChangeView(view As UserControl)
        _mainWindow.ChangeView(view)
    End Sub

    ' Setter to update the current view
    Public Shared WriteOnly Property SetCurrentView As UserControl
        Set(value As UserControl)
            _currentView = value
        End Set
    End Property

    ' Getter to retrieve the stored current view
    Public Shared ReadOnly Property CurrentView As UserControl
        Get
            Return _currentView
        End Get
    End Property
End Class