<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtbox_username = New System.Windows.Forms.TextBox()
        Me.txtbox_password = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.goButton = New System.Windows.Forms.Button()
        Me.lbl_loggedin = New System.Windows.Forms.Label()
        Me.lbl_viewdata = New System.Windows.Forms.Label()
        Me.lbl_menudata = New System.Windows.Forms.Label()
        Me.btn_exit = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 70)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'txtbox_username
        '
        Me.txtbox_username.Location = New System.Drawing.Point(146, 43)
        Me.txtbox_username.Name = "txtbox_username"
        Me.txtbox_username.Size = New System.Drawing.Size(100, 20)
        Me.txtbox_username.TabIndex = 1
        '
        'txtbox_password
        '
        Me.txtbox_password.Location = New System.Drawing.Point(146, 106)
        Me.txtbox_password.Name = "txtbox_password"
        Me.txtbox_password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtbox_password.Size = New System.Drawing.Size(100, 20)
        Me.txtbox_password.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "username"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "password"
        '
        'goButton
        '
        Me.goButton.Location = New System.Drawing.Point(146, 269)
        Me.goButton.Name = "goButton"
        Me.goButton.Size = New System.Drawing.Size(100, 23)
        Me.goButton.TabIndex = 5
        Me.goButton.Text = "Go"
        Me.goButton.UseVisualStyleBackColor = True
        '
        'lbl_loggedin
        '
        Me.lbl_loggedin.AutoSize = True
        Me.lbl_loggedin.Location = New System.Drawing.Point(13, 164)
        Me.lbl_loggedin.Name = "lbl_loggedin"
        Me.lbl_loggedin.Size = New System.Drawing.Size(60, 13)
        Me.lbl_loggedin.TabIndex = 6
        Me.lbl_loggedin.Text = "login status"
        '
        'lbl_viewdata
        '
        Me.lbl_viewdata.AutoSize = True
        Me.lbl_viewdata.Location = New System.Drawing.Point(16, 198)
        Me.lbl_viewdata.Name = "lbl_viewdata"
        Me.lbl_viewdata.Size = New System.Drawing.Size(53, 13)
        Me.lbl_viewdata.TabIndex = 7
        Me.lbl_viewdata.Text = "view data"
        '
        'lbl_menudata
        '
        Me.lbl_menudata.AutoSize = True
        Me.lbl_menudata.Location = New System.Drawing.Point(16, 234)
        Me.lbl_menudata.Name = "lbl_menudata"
        Me.lbl_menudata.Size = New System.Drawing.Size(57, 13)
        Me.lbl_menudata.TabIndex = 8
        Me.lbl_menudata.Text = "menu data"
        '
        'btn_exit
        '
        Me.btn_exit.Location = New System.Drawing.Point(146, 313)
        Me.btn_exit.Name = "btn_exit"
        Me.btn_exit.Size = New System.Drawing.Size(100, 23)
        Me.btn_exit.TabIndex = 9
        Me.btn_exit.Text = "Exit"
        Me.btn_exit.UseVisualStyleBackColor = True
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 359)
        Me.Controls.Add(Me.lbl_menudata)
        Me.Controls.Add(Me.btn_exit)
        Me.Controls.Add(Me.lbl_viewdata)
        Me.Controls.Add(Me.lbl_loggedin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.goButton)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtbox_username)
        Me.Controls.Add(Me.txtbox_password)
        Me.Name = "LoginForm"
        Me.Text = "LoginForm"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents txtbox_username As System.Windows.Forms.TextBox
    Friend WithEvents txtbox_password As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents goButton As System.Windows.Forms.Button
    Friend WithEvents lbl_loggedin As System.Windows.Forms.Label
    Friend WithEvents lbl_viewdata As System.Windows.Forms.Label
    Friend WithEvents lbl_menudata As System.Windows.Forms.Label
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btn_exit As System.Windows.Forms.Button
End Class
