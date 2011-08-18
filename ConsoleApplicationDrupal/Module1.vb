Imports Microsoft.Http
Imports Microsoft.Http.HttpClient
Imports Microsoft.Http.HttpMethodExtensions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq

Module Module1
    Dim connectoutput As String
    Dim loginoutput As String
    Dim content As String
    Dim menus As String
    Dim response As HttpResponseMessage = Nothing
    Dim sessID As String = Nothing
    Dim job As JObject = Nothing
    Dim jtok As JToken = Nothing
    Dim sess As String = Nothing
    Sub Main()
        Try
            Dim myURL As String = My.Settings.protocol & My.Settings.domain
            Dim http As New HttpClient(myURL)
            Dim drupalform As New DrupalForm
            Dim conn As HttpMultipartMimeForm = drupalform.create("system.connect", False, "")
            response = http.Post(My.Settings.service, conn.CreateHttpContent)
            connectoutput = response.Content.ReadAsString
            'parse the output and get the session ID
            job = JObject.Parse(connectoutput)
            'the first part of the json file is the header confirming if the file is an error, the last part
            'contains the data.  The first part of the data contains the session id (sessid)
            jtok = job.Last.First
            sess = jtok.SelectToken("sessid")

            Console.WriteLine(connectoutput)

            'log in
            Dim login As HttpMultipartMimeForm = drupalform.create("user.login", True, sess)
            response = http.Post(My.Settings.service, login.CreateHttpContent)
            response.EnsureStatusIsSuccessful()
            loginoutput = response.Content.ReadAsString
            'need to get the session id from this connection and use it from hereon
            job = JObject.Parse(loginoutput)
            jtok = job.Last.First
            sess = jtok.SelectToken("sessid")

            Console.WriteLine(loginoutput)
            'do something
            Dim viewform As HttpMultipartMimeForm = drupalform.create("view", True, sess)
            response = http.Post(My.Settings.service, viewform.CreateHttpContent)
            response.EnsureStatusIsSuccessful()
            content = response.Content.ReadAsString
            Console.WriteLine(content)

            Dim menuform As HttpMultipartMimeForm = drupalform.create("menu", True, sess)
            response = http.Post(My.Settings.service, menuform.CreateHttpContent)
            response.EnsureStatusIsSuccessful()
            menus = response.Content.ReadAsString
            Console.WriteLine(menus)

            'log out 
            Dim logout As HttpMultipartMimeForm = drupalform.create("user.logout", True, sess)
            response = http.Post(My.Settings.service, logout.CreateHttpContent)
            response.EnsureStatusIsSuccessful()
            Console.WriteLine(response.Content.ReadAsString)

            Dim sw As New StreamWriter("nodeout.json")

            'uncomment these to lose the crappiness at the start of the json output
            'REMEMBER to write out newoutput to file instead of output
            'Dim replacementstring As String = "#error" & """: false, """ & "#data"
            'Dim newoutput As String = output.Replace(replacementstring, "content")
            sw.Write(content)
            sw.Flush()
            sw.Close()

            Dim mw As New StreamWriter("menus.json")
            mw.Write(menus)
            mw.Flush()
            mw.Close()



        Catch ex As Exception

            Console.WriteLine(ex.Message & " " & connectoutput)
        End Try

    End Sub


End Module
