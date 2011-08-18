Imports Microsoft.Http
Imports System.Text

Public Class DrupalForm
    
    Public Function create(ByVal type As String, ByVal authenticate As Boolean, ByVal sessID As String) As HttpMultipartMimeForm
        Dim form As New HttpMultipartMimeForm
        Dim helper As New DrupalHelper
        Dim method As String = Nothing
        Dim methodstring As String = Nothing
        Dim username As String = "subscriber"
        Dim password As String = "qwerty123"
        Dim methodsb As New StringBuilder()

        Try
            Select Case type
                Case "system.connect"
                    method = "system.connect"

                Case "user.login"

                    method = "user.login"

                    Dim usernamesb As New StringBuilder()
                    usernamesb.Append("""")
                    usernamesb.Append(username)
                    usernamesb.Append("""")

                    Dim passwordsb As New StringBuilder()
                    passwordsb.Append("""")
                    passwordsb.Append(password)
                    passwordsb.Append("""")
                    form.Add("username", usernamesb.ToString())
                    form.Add("password", passwordsb.ToString())

                Case "user.logout"

                    method = "user.logout"

                    Dim usernamesb As New StringBuilder()
                    usernamesb.Append("""")
                    usernamesb.Append(username)
                    usernamesb.Append("""")

                    Dim passwordsb As New StringBuilder()
                    passwordsb.Append("""")
                    passwordsb.Append(password)
                    passwordsb.Append("""")

                Case "node"
                    ' to be added
                Case "menu"
                    'to be added
                Case "view"
                    'method needs to be in quotes
                    'trying it without quotes for hash

                    method = "views.get"

                    form.Add("view_name", My.Settings.viewname)
                    form.Add("format_output", "TRUE")
                    Console.WriteLine("view name: " & My.Settings.viewname)
                Case Else
                    Throw New Exception("invalid method specified: must be node, menu or view")
            End Select

            methodsb.Append("""")
            methodsb.Append(method)
            methodsb.Append("""")
            form.Add("method", methodsb.ToString())

            If authenticate = True Then
                'create an SHA-256 hash of the timestamp, domain, nonce, and method name delimited by semicolons
                'and using the remote API key as the shared key

                Dim domain As String = My.Settings.domain
                Dim apikey As String = My.Settings.API_key
                Dim timestamp As String = helper.getUnixTimestamp()
                Dim nonce As String = helper.getNonce(10)
                'not needed as getting session from system.connect call
                'If sessID = "" Then
                'sessionID = helper.getNonce(20)
                'End If
                Dim sb As New StringBuilder()

                If timestamp IsNot Nothing And nonce IsNot Nothing And sessID IsNot Nothing And domain IsNot Nothing Then
                    sb.Append(timestamp)
                    sb.Append(";")
                    sb.Append(domain)
                    sb.Append(";")
                    sb.Append(nonce)
                    sb.Append(";")
                    sb.Append(method)

                    Console.WriteLine("prehash string: " & sb.ToString())

                    Dim hash As String = helper.getHMAC(sb.ToString(), apikey)

                    'problems with adding quotes to my fields, so doing it the long way!!
                    Dim hashsb As New StringBuilder()
                    hashsb.Append("""")
                    hashsb.Append(hash)
                    hashsb.Append("""")

                    Dim noncesb As New StringBuilder()
                    noncesb.Append("""")
                    noncesb.Append(nonce)
                    noncesb.Append("""")

                    Dim sessIDsb As New StringBuilder()
                    sessIDsb.Append("""")
                    sessIDsb.Append(sessID)
                    sessIDsb.Append("""")

                    Dim domainsb As New StringBuilder()
                    domainsb.Append("""")
                    domainsb.Append(domain)
                    domainsb.Append("""")

                    If hash IsNot Nothing Then
                        form.Add("hash", hashsb.ToString())
                        form.Add("domain_name", domainsb.ToString())
                        form.Add("domain_time_stamp", timestamp)
                        form.Add("nonce", noncesb.ToString())
                        form.Add("sessid", sessIDsb.ToString())
                        'form.Add("display_id", "wibble")
                        'form.Add("args", "wibbleagain")
                        'arbitrary offset and limit to see what ends up in array
                        'form.Add("offset", "5")
                        'form.Add("limit", "10")
                        'Console.WriteLine("API: " & My.Settings.API_key)
                        'Console.WriteLine("method: " & method)
                        'Console.WriteLine("hash: " & hash)
                        'Console.WriteLine("domain name: " & My.Settings.domain)
                        'Console.WriteLine("time :" & timestamp)
                        'Console.WriteLine("nonce: " & nonce)
                        'Console.WriteLine("sessid: " & sessionID)
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
