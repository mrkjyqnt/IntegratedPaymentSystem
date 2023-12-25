Imports System.IO
Imports MimeKit
Imports MailKit.Net.Smtp
Imports System.Net.Mail

Module EmailSender

    Private message As New MimeMessage()
    Private builder As New BodyBuilder()

    Public Sub PrepareMail(_fromName As String, _fromMail As String, _toName As String, _toMail As String, _subject As String)
        
        message.From.Add(New MailboxAddress(_fromName, _fromMail))
        message.To.Add(New MailboxAddress("Recipient Name", _toMail))
        message.Subject = _subject
        
    End Sub

    Public Sub AddBody(_textBody As String)

        builder.HtmlBody = _textBody

        message.Body = builder.ToMessageBody()

    End Sub

    Public Sub AddAttachment(_title As String, Optional _filePath As String = Nothing)

        builder.Attachments.Add(_title, File.OpenRead(_filePath))

    End Sub

     Public Sub SendMail()

        Try
            Using client As New MailKit.Net.Smtp.SmtpClient()
                client.Connect("smtp.gmail.com", 587, False)
                client.Authenticate("system.geekxfiber@gmail.com", "ynzmgafwkndqrvni") ' Use your Gmail credentials
                client.Send(message)
                client.Disconnect(True)
            End Using
        Catch ex As Exception
            ' Handle exceptions here, such as logging the error or displaying a message box
            MsgBox("Failed to send email: " & ex.Message)
        End Try

    End Sub
End Module