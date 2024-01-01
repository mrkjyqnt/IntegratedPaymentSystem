Imports System.Text.RegularExpressions

Module Message

    ' <SUMMARY>
    ' 
    ' Custom Message Box
    '
    ' <SUMMARY>

    Public Sub Show(MessageTitle As String, MessageText As String)
        Dim messageBox As New CustomMessageBox(MessageTitle, MessageText, MeWindow())
        messageBox.ShowDialog()
    End Sub

    Public Function Confirm(MessageTitle As String, MessageText As String, Optional KeyConfirmation As String = Nothing) As Boolean
        Dim confirmationBox As New CustomConfirmationBox(MessageTitle, MessageText, KeyConfirmation, MeWindow())
        Dim result As Nullable(Of Boolean) = confirmationBox.ShowDialog()
        Return result
    End Function

    Public Sub  Loading(MessageTitle As String, MessageText As String)
        Dim loadingBox As New CustomLoadingBox(MessageTitle, MessageText, MeWindow())
        loadingBox.Show()
    End Sub

    Public Function MeWindow() As Window
        Return Application.Current.MainWindow
    End Function

    
    Public Sub FormatTextblock(textBlock As TextBlock, text As String)
        Dim boldPattern As String = "<bold>(.*?)</bold>"
        Dim newlinePattern As String = "</newline>"

        ' Combine both patterns to find bold and newline tags
        Dim combinedPattern As String = $"{boldPattern}|{newlinePattern}"
        Dim matches As MatchCollection = Regex.Matches(text, combinedPattern)

        textBlock.Inlines.Clear()

        Dim currentPosition As Integer = 0

        For Each match As Match In matches
            If match.Index > currentPosition Then
                Dim nonBoldText As New Run(text.Substring(currentPosition, match.Index - currentPosition))
                textBlock.Inlines.Add(nonBoldText)
            End If

            If match.Value.StartsWith("<bold>") Then
                Dim boldText As New Run(match.Groups(1).Value)
                boldText.FontWeight = FontWeights.Bold
                textBlock.Inlines.Add(boldText)
            ElseIf match.Value = "</newline>" Then
                textBlock.Inlines.Add(New LineBreak()) ' Add a LineBreak for </newline>
            End If

            currentPosition = match.Index + match.Length
        Next

        If currentPosition < text.Length Then
            Dim remainingText As New Run(text.Substring(currentPosition))
            textBlock.Inlines.Add(remainingText)
        End If
    End Sub

End Module
