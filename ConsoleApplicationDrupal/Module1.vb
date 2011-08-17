Imports Microsoft.Http
Imports Microsoft.Http.HttpClient
Imports Microsoft.Http.HttpMethodExtensions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Text
Module Module1
    Dim output As String
    Sub Main()
        Try
            Dim http As New HttpClient(My.Settings.myURL)
            Dim drupalform As New DrupalForm
            Dim form As HttpMultipartMimeForm = drupalform.create("view", False, True)
            Dim response As HttpResponseMessage = http.Post("/services/json", form.CreateHttpContent)
            response.EnsureStatusIsSuccessful()
            output = response.Content.ReadAsString
            Dim sw As New StreamWriter("node_out.json")

            'uncomment these to lose the crappiness at the start of the json output
            'REMEMBER to write out newoutput to file instead of output
            'Dim replacementstring As String = "#error" & """: false, """ & "#data"
            'Dim newoutput As String = output.Replace(replacementstring, "content")
            Console.WriteLine(output)
            sw.Write(output)
            sw.Flush()
            sw.Close()

        Catch ex As Exception

            Console.WriteLine(ex.Message & " " & output)
        End Try
 
    End Sub


End Module
