Option Explicit On

Imports System
Imports System.IO
Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Resources
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Object
Imports System.Xml

' 6/8/15 - Now with technicolor. Also added some error handling. Still seeing a bug when you spam-click the grabber/popper due to filesLocated being locked.
' 6/9/15 - Added a sweet divider line. Added structure check to app initialization to reduce the need for error checking later on. Added help.

' parse filenames, smarter engine
' check xml for blank
' check xml for ending * or \, add if missing

Module Module1

    Public Class MyForm
        Inherits Form
        Private grabFiles As Button
        Private popFiles As Button
        Private moveFiles As Button
        Private editXML As Button
        Private titleBox As New PictureBox()
        Private mainMenu As New MenuStrip()
        Private textBox1 As New TextBox()

        Public Shared appPath As String = My.Application.Info.DirectoryPath

        Public Shared filesLocated As String = (appPath + "\filesLocated.txt")
        Public Shared torrXML As String = (appPath + "\torrSort_XML.xml")
        Public Shared sourceDirFile As String = (appPath + "\sourceDir.txt")

        Public Shared sourceDir As String = File.ReadAllText(sourceDirFile)


        Public Sub New()
            InitializeComponent()
        End Sub

        Protected Overloads Overrides Sub Dispose(disposing As Boolean)
            MyBase.Dispose(disposing)
        End Sub

        Public Sub InitializeComponent()

            ' Structure Check

            If Not File.Exists(filesLocated) Then
                File.Create(filesLocated)
                MsgBox("Blank file created: filesLocated.txt")
            End If

            If Not File.Exists(torrXML) Then
                MsgBox("No XML file detected.")
            End If

            If Not File.Exists(sourceDirFile) Then
                File.Create(sourceDirFile)
                MsgBox("No source directory set. Close program and input source directory to sourceDir.txt")
            End If

            ' Form Build

            FormBorderStyle = FormBorderStyle.Fixed3D
            MinimizeBox = False
            MaximizeBox = False
            Text = "torrSort"

            Dim FileMenu As New ToolStripMenuItem("File", Nothing, New EventHandler(AddressOf menu_Click))
            FileMenu.BackColor = Color.SlateGray
            FileMenu.ForeColor = Color.Silver
            FileMenu.Text = "Help"
            FileMenu.Font = New Font("Calibri", 10)

            mainMenu.BackColor = Color.SlateGray
            mainMenu.ForeColor = Color.SlateGray
            mainMenu.Text = "File Menu"
            mainMenu.Dock = DockStyle.Top
            mainMenu.AutoSize = False
            mainMenu.Items.Add(FileMenu)
            Controls.Add(mainMenu)

            grabFiles = New Button()
            grabFiles.Text = "Update Source Files"
            grabFiles.Location = New Point(90, 35)
            grabFiles.Size = New Size(115, 30)
            grabFiles.BackColor = Color.Silver
            AddHandler grabFiles.Click, AddressOf button_Click
            Controls.Add(grabFiles)

            popFiles = New Button()
            popFiles.Text = "Show Source Files"
            popFiles.Location = New Point(90, 75)
            popFiles.Size = New Size(115, 30)
            popFiles.BackColor = Color.Silver
            AddHandler popFiles.Click, AddressOf button_Click
            Controls.Add(popFiles)

            moveFiles = New Button()
            moveFiles.Text = "Run Rule List"
            moveFiles.Location = New Point(90, 145)
            moveFiles.Size = New Size(115, 30)
            moveFiles.BackColor = Color.Silver
            AddHandler moveFiles.Click, AddressOf button_Click
            Controls.Add(moveFiles)

            editXML = New Button()
            editXML.Text = "Show Rule XML"
            editXML.Location = New Point(90, 185)
            editXML.Size = New Size(115, 30)
            editXML.BackColor = Color.Silver
            AddHandler editXML.Click, AddressOf button_Click
            Controls.Add(editXML)

            titleBox.Dock = DockStyle.Fill
            titleBox.BackColor = Color.SlateGray
            AddHandler titleBox.Paint, AddressOf Me.titleBox_Paint
            Controls.Add(titleBox)

        End Sub

        Public Sub HelpMenu()

            Dim frm1 As New Form()
            Dim textBox1 As New TextBox()

            textBox1 = New TextBox()
            textBox1.Dock = DockStyle.Fill
            textBox1.Location = New Point(90, 75)
            textBox1.Size = frm1.Size()
            textBox1.Multiline = True
            textBox1.Text = "SETUP:" & vbCrLf &
        "" & vbCrLf &
           "All files below need to be in the same directory from which you launch the app." & vbCrLf &
        "" & vbCrLf &
           "sourceDir.txt -- this file needs to contain only the source directory, no spaces anywhere" & vbCrLf &
           "" & vbCrLf &
           "torrSort_XML.xml -- this file should contain your rules in the following format (note: you need the asterisk after your searchPattern AND the backslash after your destFolder)" & vbCrLf &
           "    <Rules>" & vbCrLf &
               "        <searchPattern>The.Show.S01E01</searchPattern>" & vbCrLf &
            "        <destFolder>C:\TheDestination\The Show Season 1\</destFolder>" & vbCrLf &
            "    </Rules>" & vbCrLf &
            "" & vbCrLf &
            "" & vbCrLf &
            "USE:" & vbCrLf &
            "" & vbCrLf &
                "'Update Source Files' - Updates your source files list" & vbCrLf &
                "'Show Source Files' - Shows your available files in your source directory (.avi, .mkv, .mp4)" & vbCrLf &
                "'Run Rule List' - Runs the XML rules" & vbCrLf &
                "'Show Rule XML' - Shows the XML rule list"



            Controls.Add(textBox1)

            frm1.Show()
            frm1.Controls.Add(textBox1)

        End Sub

        Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)

            Dim result As New List(Of String)
            Dim stack As New Stack(Of String)

            stack.Push(initial)

            Do While (stack.Count > 0)
                ' Get top directory string
                Dim dir As String = stack.Pop
                Try

                    result.AddRange(Directory.GetFiles(dir, "*.mp4")) 'This is where we will customize filetypes
                    result.AddRange(Directory.GetFiles(dir, "*.avi"))
                    result.AddRange(Directory.GetFiles(dir, "*.mkv"))

                    Dim directoryName As String
                    For Each directoryName In Directory.GetDirectories(dir)
                        stack.Push(directoryName)
                    Next

                Catch ex As Exception
                End Try

            Loop

            Return result

        End Function

        Public Shared Sub grabber()

            Dim list As List(Of String) = GetFilesRecursive(sourceDir)

            If File.Exists(filesLocated) Then
                File.Delete(filesLocated)
            End If

            If list.Count = 0 Then
                MsgBox("No files in source directory: " + sourceDir)
            End If

            For Each x In list
                File.AppendAllText((filesLocated), Environment.NewLine + x)
            Next

        End Sub

        Public Shared Function titleBox_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
            Dim g As Graphics = e.Graphics
            Dim theText As String = "torrSort"
            Dim blackPen As New Pen(Color.DarkSlateGray, 0.5)
            Dim point1 As New Point(60, 125)
            Dim point2 As New Point(232, 125)
            g.DrawString(theText, New Font("Calibri", 20), Brushes.Silver, New Point(190, 230))
            g.DrawLine(blackPen, point1, point2)
        End Function

        Public Shared Function ruleRunner()

            Dim rulesXml = System.Xml.Linq.XDocument.Load((My.Application.Info.DirectoryPath + "\torrSort_XML.XML"))

            Dim ruleList As New NameValueCollection
            Dim values() As String

            Dim rulesDoc = System.Xml.Linq.XDocument.Parse(rulesXml.ToString())

            Dim rules = From x In rulesDoc...<Rules>
                        Select x

            For Each x In rules
                If Not Directory.Exists(x.<destFolder>.Value) Then
                    Directory.CreateDirectory(x.<destFolder>.Value)
                End If
            Next

            For Each x In rules
                ruleList.Add((x.<searchPattern>.Value), (x.<destFolder>.Value))
            Next

            For Each key In ruleList.Keys
                values = ruleList.GetValues(key)
                For Each value As String In values
                    Dim destDir As String = value
                    Dim Rule = Directory.EnumerateFiles(sourceDir, key, SearchOption.AllDirectories)
                    For Each x As String In Rule
                        Dim fileName = Path.GetFileName(x)
                        If File.Exists(destDir + fileName) Then
                            MsgBox("File already exists in destination:    " + fileName + Environment.NewLine + "Remove the file from your source directory." + Environment.NewLine + "Click OK to continue running rules . . .")
                            Continue For
                        Else
                            FileIO.FileSystem.MoveFile(x, destDir + fileName, FileIO.UIOption.AllDialogs )
                        End If
                    Next
                Next value
            Next key

        End Function

        Private Sub menu_Click(ByVal sender As Object, ByVal e As EventArgs)

            HelpMenu()

        End Sub

        Private Sub button_Click(sender As Object, e As EventArgs)

            If sender Is grabFiles Then
                grabber()
            End If

            If sender Is popFiles Then
                If Not File.Exists(filesLocated) Then
                    File.Create(filesLocated)
                    MsgBox("No files in source directory: " + sourceDir)
                Else
                    Process.Start((filesLocated))
                End If
            End If

            If sender Is moveFiles Then
                ruleRunner()
            End If

            If sender Is editXML Then
                Process.Start((torrXML)) ' To specify -- possibly a later option - Process.Start("notepad.exe", (torrXML))
            End If

        End Sub
    End Class

    <STAThread()>
    Public Sub Main()
        Try
            Dim form As New MyForm()
            Application.Run(form)
        Catch e As Exception
            MsgBox(e.ToString())
        End Try
    End Sub

End Module
