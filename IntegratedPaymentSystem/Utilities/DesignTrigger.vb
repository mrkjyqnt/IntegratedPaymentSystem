Imports System.Windows

Public Class DesignTrigger
    Public Shared ReadOnly RuntimeStyleProperty As DependencyProperty =
                               DependencyProperty.RegisterAttached("RuntimeStyle", GetType(Boolean), GetType(DesignTrigger),
                                                                   New PropertyMetadata(True, AddressOf OnRuntimeStyleChanged))

    Public Shared Sub SetRuntimeStyle(obj As DependencyObject, value As Boolean)
        obj.SetValue(RuntimeStyleProperty, value)
    End Sub

    Private Shared Sub OnRuntimeStyleChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim dataGrid = TryCast(d, DataGrid)
        dataGrid.Style = TryCast(Application.Current.Resources("RuntimeTable"), Style)
    End Sub
End Class