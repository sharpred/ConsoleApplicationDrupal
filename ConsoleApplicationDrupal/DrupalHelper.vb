Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class DrupalHelper

    'most of the ideas here came from an article at http://drupal.org/node/308629

    Public Function getUnixTimestamp() As String
        Dim ts As New TimeSpan
        Dim timestring As String
        Try
            ts = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0))
        Catch ex As Exception
            Console.WriteLine("unable to create UNIX timestamp")
        End Try
        timestring = Convert.ToString(Convert.ToUInt64(ts.TotalSeconds))
        Return timestring
    End Function

    Public Function getNonce(ByVal length As Integer) As String
        'creates a nonce value for session
        Dim allowedCharacters As String = "abcdefghijklmnopqrstuvwxyzABCDEFGFHIJKLMNOPQRSTUVWXYZ1234567890"
        Dim password As New StringBuilder()
        Dim passwordstring As String = Nothing
        Dim rand As New Random()
        Try
            Dim i As Integer = 0

            For i = 0 To (length - 1)
                Dim alphanumer As String = rand.Next(0, allowedCharacters.Length)
                password.Append(allowedCharacters(alphanumer))
            Next
            passwordstring = password.ToString()
        Catch ex As Exception
            Console.WriteLine("unable to create nonce value")
        End Try
        Return passwordstring
    End Function

    Public Function getHMAC(ByVal message As String, ByVal key As String) As String
        Dim sbinary As String = String.Empty
        Dim encoding As New ASCIIEncoding()
        Try
            Dim i As Integer = 0
            Dim keybyte As Byte() = encoding.GetBytes(key)
            Dim messagebyte As Byte() = encoding.GetBytes(message)
            Dim hmac = New HMACSHA256(keybyte)
            Dim hashMessageByte As Byte() = hmac.ComputeHash(messagebyte)
            For i = 0 To (hashMessageByte.Length - 1)
                sbinary += hashMessageByte(i).ToString("x2")
            Next
        Catch ex As Exception
            Console.WriteLine("Unable to create HMAC")
        End Try
        Return sbinary
    End Function
End Class
