Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports System.Threading

Module Tools
    Public Function EmptyStr(str As String) As Boolean
        Return Len(str) <= 0
    End Function

    Public Function EmptyOr(str1 As String, str2 As String) As String
        If EmptyStr(str1) Then
            Return str2
        End If

        Return str1
    End Function

    Public Function CreateType(abstractName As String) As Object
        Dim assemblies = AppDomain.CurrentDomain.GetAssemblies()

        Dim type = (From assembly In assemblies From t In assembly.GetTypes() Where t.Name.Equals(abstractName) Select t).FirstOrDefault()

        If type.Equals(Nothing) Then
            Throw New InvalidOperationException("Invalid Type")
        End If

        Return Activator.CreateInstance(type)
    End Function

    Public Function GetPasswordFromPasswordBox(passwordBox As PasswordBox) As String
        Dim password As String = Nothing

        Dim securePassword As System.Security.SecureString = passwordBox.SecurePassword
        Dim bstr As IntPtr = IntPtr.Zero

        Try
            bstr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(securePassword)
            password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr)
        Finally
            If bstr <> IntPtr.Zero Then
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstr)
            End If
        End Try

        Return password.ToString()
    End Function

    Public Function HashPassword(Password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password))
            Dim builder As New StringBuilder()

            For i As Integer = 0 To hashedBytes.Length - 1
                builder.Append(hashedBytes(i).ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Function GenerateRandomCode() As String
        Dim _random As New Random()

        Dim codeBuilder As New StringBuilder()
        For i As Integer = 0 To 5
            codeBuilder.Append(_random.Next(0, 10)) 
            Thread.Sleep(10)
        Next
        Return codeBuilder.ToString()
    End Function

    Public Sub ChangeView(_grid As Grid, _view As UserControl)
        _grid.Children.Clear()
        _grid.Children.Add(_view)
    End Sub

End Module