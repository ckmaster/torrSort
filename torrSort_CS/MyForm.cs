﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic;

namespace torrSort_CS
{
    public partial class MyForm : Form
    {
        private const string filesLocated = ".\\filesLocated.txt";
        private const string torrXML = ".\\torrSort_XML.xml";
        private const string sourceDirFile = ".\\sourceDir.txt";
        private string sourceDir = "";

        public MyForm ()
        {
            InitializeComponent();

            //Structure Check
            if (!File.Exists(filesLocated))
            {
                File.Create(filesLocated);
                MessageBox.Show("Blank file created: filesLocated.txt");
            }

            if (!File.Exists(torrXML))
            {
                MessageBox.Show("No XML file detected.");
            }

            //Should add in some error handling here to check if the file is formatted correctly
            if (!File.Exists(sourceDirFile))
            {
                File.Create(sourceDirFile);
                MessageBox.Show("No source directory set. Close program and input source directory to sourceDir.txt");
            } else
            {
                using (StreamReader sr = new StreamReader(sourceDirFile))
                {
                    sourceDir = sr.ReadLine();
                }
            }
            //End Structure Check
        }

        private void MyForm_Load (object sender, EventArgs e)
        {

        }

        private void FileMenu_Click (object sender, EventArgs e)
        {
            Form frm1 = new Form();
            frm1.Size = new Size(850, 300);
            TextBox textBox1 = new TextBox();

            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(90, 75);
            textBox1.Size = frm1.Size;
            textBox1.Multiline = true;
            textBox1.ReadOnly = true;

            string toDisplay = "SETUP:\r\n\r\n" +
                               "All files below need to be in the same directory from which you launch the app.\r\n\r\n" +
                               "sourceDir.txt -- this file needs to contain only the source directory, no spaces anywhere\r\n\r\n" +
                               "torrSort_XML.xml -- this file should contain your rules in the following format (note: you need the asterisk after your searchPattern AND the backslash after your destFolder\r\n" +
                               "\t<Rules>\r\n" +
                               "\t\t<searchPattern>The.Show.S01E01</searchPattern>\r\n" +
                               "\t\t<destFolder>C:\\TheDestination\\The Show Season 1\\</destFolder>\r\n" +
                               "\t</Rules>\r\n\r\n\r\n" +
                               "USE:\r\n\r\n" +
                               "'Update Source Files' - Updates your source files list\r\n" +
                               "'Show Source Files' - Shows your available files in your source directory (.avi, .mkv, .mp4)\r\n" +
                               "'Run Rule List' - Runs the XML rules\r\n" +
                               "'Show Rule XML' - Shows the XML rule list";

            textBox1.Text = toDisplay;
            Controls.Add(textBox1);
            frm1.Show();
            frm1.Controls.Add(textBox1);
        }
        //end FileMenu_Click

        public static void GetFilesRecursive (List<string> initial)
        {
            List<string> result = new List<string>();
            Stack<string> stack = new Stack<string>();

            foreach (string s in initial)
            {
                stack.Push(s);
            }

            while (stack.Count > 0)
            {
                string tempDir = stack.Pop();
                try
                {
                    result.AddRange(Directory.GetFiles(tempDir, "*.mp4"));
                    result.AddRange(Directory.GetFiles(tempDir, "*.avi"));
                    result.AddRange(Directory.GetFiles(tempDir, "*.mkv"));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString() + "\r\n\r\nCould not find or add files.");
                }
            }
        }
        //end GetFilesRecursive

        public void Grabber ()
        {
            try
            {
                string[] files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);

                if (File.Exists(filesLocated))
                {
                    File.Delete(filesLocated);
                }

                if (files.Count() == 0)
                {
                    MessageBox.Show("No files in source directory: " + sourceDir);
                }

                foreach (string s in files)
                {
                    File.AppendAllText((filesLocated), "\r\n" + s);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString() + "\r\n\r\nCould not find source directory.");
            }
        }
        //end Grabber

        public void RuleRunner ()
        {
            backgroundWorker1.RunWorkerAsync(null);
        }
        //end RuleRunner

        private void moveFiles_Click (object sender, EventArgs e)
        {
            RuleRunner();
        }
        //end moveFiles_Click

        private void grabFiles_Click (object sender, EventArgs e)
        {
            Grabber();
        }
        //end grabFiles_Click

        private void popFiles_Click (object sender, EventArgs e)
        {
            if (!File.Exists(filesLocated))
            {
                File.Create(filesLocated);
                MessageBox.Show("No files in source directory: " + sourceDir);
            }
            else
            {
                Process.Start(filesLocated);
            }
        }
        //end popFiles_Click

        private void editXML_Click (object sender, EventArgs e)
        {
            Process.Start(torrXML);
        }
        //end editXML_Click

        private void backgroundWorker1_DoWork (object sender, DoWorkEventArgs e)
        {
            List<Rule> rules = GetRules();
            foreach (Rule r in rules)
            {
                string[] filesToMove = Directory.GetFiles(sourceDir, r.searchPattern, SearchOption.AllDirectories);
                foreach(string s in filesToMove)
                {
                    string fileName = Path.GetFileName(s);
                    if (File.Exists(r.destFolder + fileName))
                    {
                        MessageBox.Show("File already exists in destination:    " + fileName + "\r\nRemove the file from your source directory\r\nClick OK to continue running rules . . .");
                    }
                    else
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(s, r.destFolder + fileName, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
                    }
                }
            }
        }
        //end backgroundWorker1_DoWork

        public static List<Rule> GetRules ()
        {
            List<Rule> toReturn = new List<Rule>();
            XElement rulesXML = XElement.Load(torrXML);
            IEnumerable<XElement> rules = from x in rulesXML.Elements() select x;

            foreach(XElement x in rules)
            {
                string destFolder = x.Element("destFolder").Value;
                string searchPattern = x.Element("searchPattern").Value;
                toReturn.Add(new Rule(destFolder, searchPattern));
            }
            return toReturn;
        }
        //end GetRules

    }
    //end class MyForm
}
//end namespace torrSort_CS