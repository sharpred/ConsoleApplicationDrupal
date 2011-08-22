Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Imports Newtonsoft.Json.Linq
Imports Common.Logging

Public Class DrupalHelper
    Private log As ILog = LogManager.GetCurrentClassLogger()

    Public Function getSessionStatus(ByVal jsonstring) As Boolean
        Dim job As JObject = Nothing
        Dim jtok As JToken = Nothing
        Dim status As String = Nothing
        Try
            job = JObject.Parse(jsonstring)
            'the first part of the json file is the header confirming if the file is an error, the last part
            'contains the data.  The first part of the data contains the session id (sessid)
            status = job.First.First.ToString
            If status = "false" Then
                Return True
            Else
                Throw New Exception("drupal error: " & job.Last.ToString())
                Return False
            End If
        Catch ex As Exception
            log.Error(ex.Message)
            Return False
        End Try
    End Function

    Public Function getSessionData(ByVal jsonstring As String) As String
        Dim job As JObject = Nothing
        Dim jtok As JToken = Nothing
        Dim sessiondata As String = Nothing
        Try
            job = JObject.Parse(jsonstring)
            'the first part of the json file is the header confirming if the file is an error, the last part
            'contains the data.  
            jtok = job.Last
            sessiondata = jtok.ToString()
        Catch ex As Exception
            log.Error(ex.Message)
        End Try
        Return sessiondata

    End Function

    Public Function getSessID(ByVal jsonstring As String) As String
        Dim job As JObject = Nothing
        Dim jtok As JToken = Nothing
        Dim sess As String = Nothing
        Try
            job = JObject.Parse(jsonstring)
            'the first part of the json file is the header confirming if the file is an error, the last part
            'contains the data.  The first part of the data contains the session id (sessid)
            jtok = job.Last.First
            sess = jtok.SelectToken("sessid")
        Catch ex As Exception
            log.Error(ex.Message)
        End Try
        Return sess
    End Function

    'most of the ideas below came from an article at http://drupal.org/node/308629


    Public Function getUnixTimestamp() As String
        Dim ts As New TimeSpan
        Dim timestring As String = Nothing
        Try
            ts = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0))
            timestring = Convert.ToString(Convert.ToUInt64(ts.TotalSeconds))
        Catch ex As Exception
            log.Error("unable to create UNIX timestamp")
        End Try
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
            log.Error("unable to create nonce value")
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
            log.Error("Unable to create HMAC")
        End Try
        Return sbinary
    End Function
End Class
