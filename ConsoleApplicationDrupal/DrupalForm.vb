Imports Microsoft.Http
Imports System.Text

Public Class DrupalForm
    Public Function create(ByVal type As String, ByVal formatoutput As Boolean, ByVal authenticate As Boolean) As HttpMultipartMimeForm
        Dim form As New HttpMultipartMimeForm
        Dim helper As New DrupalHelper
        Dim method As String = Nothing
        Dim methodstring As String = Nothing
        Try
            Select Case type
                Case "node"
                    ' to be added
                Case "menu"
                    'to be added
                Case "view"
                    'method needs to be in quotes
                    'trying it without quotes for hash
                    methodstring = "views.get"
                    method = """views.get"""
                    form.Add("method", method)
                    form.Add("view_name", My.Settings.viewname)
                    If formatoutput = True Then
                        'default is false, so not added
                        form.Add("format_output", "TRUE")
                    End If
                Case Else
                    Throw New Exception("invalid method specified: must be node, menu or view")
            End Select
            

            If authenticate = True Then
                'create an SHA-256 hash of the timestamp, domain, nonce, and method name delimited by semicolons
                'and using the remote API key as the shared key

                Dim domain As String = My.Settings.domain
                Dim apikey As String = My.Settings.API_key
                Dim timestamp As String = helper.getUnixTimestamp()
                Dim nonce As String = helper.getNonce(10)
                Dim sessionID As String = helper.getNonce(20)
                Dim sb As New StringBuilder()

                If timestamp IsNot Nothing And nonce IsNot Nothing And sessionID IsNot Nothing And domain IsNot Nothing Then
                    sb.Append(timestamp)
                    sb.Append(";")
                    sb.Append(domain)
                    sb.Append(";")
                    sb.Append(nonce)
                    sb.Append(";")
                    sb.Append(methodstring)

                    Console.WriteLine("prehash string: " & sb.ToString())

                    Dim hash As String = helper.getHMAC(sb.ToString(), apikey)

                    If hash IsNot Nothing Then
                        form.Add("hash", hash)
                        form.Add("domain_name", My.Settings.domain)
                        form.Add("domain_time_stamp", timestamp)
                        form.Add("nonce", nonce)
                        form.Add("sessid", sessionID)
                        Console.WriteLine("API: " & My.Settings.API_key)
                        Console.WriteLine("method: " & method)
                        Console.WriteLine("view name: " & My.Settings.viewname)
                        Console.WriteLine("hash: " & hash)
                        Console.WriteLine("domain name: " & My.Settings.domain)
                        Console.WriteLine("time :" & timestamp)
                        Console.WriteLine("nonce: " & nonce)
                        Console.WriteLine("sessid: " & sessionID)
                    Else
                        Throw New Exception("unable to create hash")
                    End If
                Else
                    Throw New Exception("unable to create drupal helper variables")
                End If
            Else
                'not authenticating.  No extra code here
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return form
    End Function

End Class
