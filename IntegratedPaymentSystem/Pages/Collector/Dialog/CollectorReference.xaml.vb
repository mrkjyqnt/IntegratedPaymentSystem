Imports Microsoft.Win32

Public Class CollectorReference


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If customerPayment.ReferenceNumber IsNot "" Then
            TextNumber.Text = customerPayment.ReferenceNumber
        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        If TextNumber.Text IsNot "" Then

            customerPayment.ReferenceNumber = TextNumber.Text.Replace(" ", String.Empty)
        Else 

            Message.Show("Error", "Please fill the following")
            ApplyBlurEffect()
            Return

        End If

        RemoveBlurEffect()
        Close()
    End Sub

    Private Sub NumericOnlyTextBox(sender As Object, e As TextCompositionEventArgs)
        If Not IsNumeric(e.Text) Then
            e.Handled = True
        End If
    End Sub
End Class
