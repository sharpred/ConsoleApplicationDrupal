Imports Microsoft.Http

Public Class DrupalForm
    Public Function create(ByVal type As String, ByVal formatoutput As Boolean) As HttpMultipartMimeForm
        Dim form As New HttpMultipartMimeForm
        Try
            Select Case type
                Case "node"
                Case "menu"
                Case Else
                    'gets treated as a view if not specified
                    form.Add("method", My.Settings.service)
                    form.Add(My.Settings.serviceParameter, My.Settings.serviceValue)
            End Select
            If formatoutput = True Then
                form.Add("format_output", "true")
            End If

        Catch ex As Exception

        End Try
        Return form
    End Function

End Class
