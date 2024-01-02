Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects

Public Class CustomConfirmationBox
    Private _confirmation as String
    Public Shared Property _confirmedText As String

    Public Sub New(title As String, message As String, Optional confirmation As String = Nothing, Optional ownerWindow As Window = Nothing)

        InitializeComponent()
        ApplyBlurEffect()

        Owner = ownerWindow 
        TextTitle.Text = title
        _confirmation = If(confirmation, Nothing)

        If _confirmation Is Nothing Then

            TextConfirmation.Visibility = Visibility.Collapsed
            FormatTextblock(TextError, message)

        ElseIf _confirmation Is "Empty" Then

            FormatTextblock(TextError, message)

        Else

            FormatTextblock(TextError, message)

        End If

        ' Register the Loaded event handler
        AddHandler Me.Loaded, AddressOf Animate

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        
        If sender Is ButtonConfirm Then
            
            If _confirmation Is Nothing Then

                DialogResult = True
                
            Else 

                If _confirmation Is "Empty" Then

                    _confirmedText = TextConfirmation.Text.ToString()
                    MsgBox(_confirmedText)
                    DialogResult = True

                Else 

                    If TextConfirmation.Text = _confirmation Then

                        Message.Show("Success", $"Action has been executed")
                        DialogResult = True

                    Else 

                        Message.Show("Error", $"Please enter the confirmaation")
                        ApplyBlurEffect()
                        Return

                    End If

                End If

                

            End If

        ElseIf sender Is ButtonCancel Then

            DialogResult = False

        End If

        RemoveBlurEffect()
        Close()

    End Sub

    Private Sub Animate(sender As Object, e As RoutedEventArgs)
        ' Create a fade-in animation
        Dim fadeInAnimation As New DoubleAnimation(0, 1, New Duration(TimeSpan.FromSeconds(0.25)))

        ' Apply the animation to the window
        Me.BeginAnimation(Window.OpacityProperty, fadeInAnimation)

    End Sub

    Public Property EmptyArgs As RoutedEventArgs

    Private Sub Pressed_Enter(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Key = Key.Enter Then
            Button_Click(ButtonConfirm, EmptyArgs)
            e.Handled = True
        End If
    End Sub
End Class
