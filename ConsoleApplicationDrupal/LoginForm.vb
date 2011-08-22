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
Imports Common.Logging
Imports System.Windows.Forms

Public Class LoginForm
    Private log As ILog = LogManager.GetCurrentClassLogger()
    Dim connstring As String
    Dim loginoutput As String
    Dim content As String
    Dim menus As String
    Dim response As HttpResponseMessage = Nothing
    Dim sessID As String = Nothing
    Dim job As JObject = Nothing
    Dim jtok As JToken = Nothing
    Dim anonymous_session As String = Nothing
    Dim loggedin_session As String = Nothing
    Dim drupalform As New DrupalForm
    Dim helper As New DrupalHelper
    Private exitprog As Boolean = False

    Private Sub runDrupal()
        Try
            Dim myURL As String = My.Settings.protocol & My.Settings.domain
            Dim http As New HttpClient(myURL)
            Dim conn As HttpMultipartMimeForm = drupalform.create("system.connect", False, "")
            Dim sessionstatus As Boolean = False
            response = http.Post(My.Settings.service, conn.CreateHttpContent)
            connstring = response.Content.ReadAsString
            sessionstatus = helper.getSessionStatus(connstring)
            If sessionstatus = True Then
                'reset sessionstatus to false as will reuse in next call
                sessionstatus = False
                anonymous_session = helper.getSessID(connstring)
                log.Debug(connstring)
                'session for anonymous user
                If anonymous_session IsNot Nothing Then
                    log.Debug("using session: " & anonymous_session)
                    'log in
                    Dim login As HttpMultipartMimeForm = drupalform.create("user.login", True, anonymous_session)
                    response = http.Post(My.Settings.service, login.CreateHttpContent)
                    response.EnsureStatusIsSuccessful()
                    loginoutput = response.Content.ReadAsString
                    log.Debug(loginoutput)
                    sessionstatus = helper.getSessionStatus(loginoutput)
                    If sessionstatus = True Then
                        'reset sessionstatus to false as will reuse in next call
                        sessionstatus = False
                        'session for logged in user
                        loggedin_session = helper.getSessID(loginoutput)

                        If loggedin_session IsNot Nothing Then
                            log.Debug("using session: " & loggedin_session)
                            lbl_loggedin.Text = "Logged in OK"
                            'do something
                            Dim viewform As HttpMultipartMimeForm = drupalform.create("views.get", True, loggedin_session)
                            response = http.Post(My.Settings.service, viewform.CreateHttpContent)
                            response.EnsureStatusIsSuccessful()
                            content = helper.getSessionData(response.Content.ReadAsString)
                            'write the files
                            lbl_viewdata.Text = "data returned"
                            Dim sw As New StreamWriter("viewdata.json")
                            sw.Write(content)
                            sw.Flush()
                            sw.Close()
                            log.Debug(content)
                            Dim menuform As HttpMultipartMimeForm = drupalform.create("menu.get", True, loggedin_session)
                            response = http.Post(My.Settings.service, menuform.CreateHttpContent)
                            response.EnsureStatusIsSuccessful()
                            menus = helper.getSessionData(response.Content.ReadAsString)
                            lbl_menudata.Text = "menus written"
                            Dim mw As New StreamWriter("menus.json")
                            mw.Write(menus)
                            mw.Flush()
                            mw.Close()
                            log.Debug(menus)
                            'log out 
                            Dim logout As HttpMultipartMimeForm = drupalform.create("user.logout", True, loggedin_session)
                            response = http.Post(My.Settings.service, logout.CreateHttpContent)
                            response.EnsureStatusIsSuccessful()
                            sessionstatus = helper.getSessionStatus(response.Content.ReadAsString())
                            If sessionstatus = True Then
                                log.Debug("Logged out successfully")
                            Else
                                log.Debug("Log out failed" & response.Content.ReadAsString)
                            End If
                        Else
                            log.Error("no session returned by logged in user login")
                        End If
                    Else
                        log.Error("No session returned for anonymous login")
                    End If
                End If

            End If

        Catch ex As Exception
            log.Error(ex.Message & " " & connstring)
        End Try
    End Sub

    Private Sub goButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles goButton.Click
        runDrupal()
    End Sub

    Private Sub txtbox_username_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbox_username.TextChanged
        drupalform._username = txtbox_username.Text
    End Sub

    Private Sub txtbox_password_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbox_password.TextChanged
        drupalform._password = txtbox_password.Text
    End Sub

    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            With Me
                .Opacity = 0
                .ShowInTaskbar = False
                .WindowState = FormWindowState.Minimized
            End With
        Catch ex As Exception
            log.Error("form_load Sub failed: " & ex.Message)
        End Try

    End Sub

    Private Sub LoginForm_Close(ByVal sender As System.Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            With Me
                .Opacity = 0
                .ShowInTaskbar = False
                .WindowState = FormWindowState.Minimized
            End With
            'only shutdown the app if closedown has been initiated from exit context menu
            If exitprog = False Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If
        Catch ex As Exception
            log.Error("form_close Sub failed: " & ex.Message)
        End Try
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Try
            With Me
                .Opacity = 1
                .ShowInTaskbar = True
                .WindowState = FormWindowState.Normal
            End With
        Catch ex As Exception
            log.Error("OpenToolStripMenuItem_Click Sub failed: " & ex.Message)
        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Try
            'allow the formclosing event to actually shut the app down
            exitprog = True
            Me.Close()
        Catch ex As Exception
            log.Error("ExitToolStripMenuItem_Click Sub failed: " & ex.Message)
        End Try

    End Sub


    Private Sub btn_exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_exit.Click
        Try
            'allow the formclosing event to actually shut the app down
            exitprog = True
            Me.Close()
        Catch ex As Exception
            log.Error("ExitToolStripMenuItem_Click Sub failed: " & ex.Message)
        End Try

    End Sub
End Class