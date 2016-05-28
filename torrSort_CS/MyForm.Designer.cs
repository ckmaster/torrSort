namespace torrSort_CS
{
    partial class MyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.popFiles = new System.Windows.Forms.Button();
            this.moveFiles = new System.Windows.Forms.Button();
            this.editXML = new System.Windows.Forms.Button();
            this.FancyLogo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grabFiles = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.AutoSize = false;
            this.mainMenu.BackColor = System.Drawing.Color.SlateGray;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(284, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "File Menu";
            // 
            // FileMenu
            // 
            this.FileMenu.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileMenu.ForeColor = System.Drawing.Color.Silver;
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(44, 20);
            this.FileMenu.Text = "Help";
            this.FileMenu.Click += new System.EventHandler(this.FileMenu_Click);
            // 
            // popFiles
            // 
            this.popFiles.BackColor = System.Drawing.Color.Silver;
            this.popFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.popFiles.Location = new System.Drawing.Point(90, 75);
            this.popFiles.Name = "popFiles";
            this.popFiles.Size = new System.Drawing.Size(115, 30);
            this.popFiles.TabIndex = 2;
            this.popFiles.Text = "Show Source Files";
            this.popFiles.UseVisualStyleBackColor = false;
            this.popFiles.Click += new System.EventHandler(this.popFiles_Click);
            // 
            // moveFiles
            // 
            this.moveFiles.BackColor = System.Drawing.Color.Silver;
            this.moveFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveFiles.Location = new System.Drawing.Point(90, 145);
            this.moveFiles.Name = "moveFiles";
            this.moveFiles.Size = new System.Drawing.Size(115, 30);
            this.moveFiles.TabIndex = 3;
            this.moveFiles.Text = "Run Rule List";
            this.moveFiles.UseVisualStyleBackColor = false;
            this.moveFiles.Click += new System.EventHandler(this.moveFiles_Click);
            // 
            // editXML
            // 
            this.editXML.BackColor = System.Drawing.Color.Silver;
            this.editXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editXML.Location = new System.Drawing.Point(90, 185);
            this.editXML.Name = "editXML";
            this.editXML.Size = new System.Drawing.Size(115, 30);
            this.editXML.TabIndex = 4;
            this.editXML.Text = "Show Rule XML";
            this.editXML.UseVisualStyleBackColor = false;
            this.editXML.Click += new System.EventHandler(this.editXML_Click);
            // 
            // FancyLogo
            // 
            this.FancyLogo.AutoSize = true;
            this.FancyLogo.BackColor = System.Drawing.Color.SlateGray;
            this.FancyLogo.CausesValidation = false;
            this.FancyLogo.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FancyLogo.ForeColor = System.Drawing.Color.Silver;
            this.FancyLogo.Location = new System.Drawing.Point(172, 219);
            this.FancyLogo.Name = "FancyLogo";
            this.FancyLogo.Size = new System.Drawing.Size(100, 33);
            this.FancyLogo.TabIndex = 5;
            this.FancyLogo.Text = "torrSort";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "_____________________________";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            //this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // grabFiles
            // 
            this.grabFiles.BackColor = System.Drawing.Color.Silver;
            this.grabFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grabFiles.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grabFiles.Location = new System.Drawing.Point(90, 35);
            this.grabFiles.Name = "grabFiles";
            this.grabFiles.Size = new System.Drawing.Size(115, 30);
            this.grabFiles.TabIndex = 0;
            this.grabFiles.Text = "Update Source Files";
            this.grabFiles.UseVisualStyleBackColor = false;
            this.grabFiles.Click += new System.EventHandler(this.grabFiles_Click);
            // 
            // MyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FancyLogo);
            this.Controls.Add(this.editXML);
            this.Controls.Add(this.moveFiles);
            this.Controls.Add(this.popFiles);
            this.Controls.Add(this.grabFiles);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyForm";
            this.Text = "torrSort";
            this.Load += new System.EventHandler(this.MyForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.Button popFiles;
        private System.Windows.Forms.Button moveFiles;
        private System.Windows.Forms.Button editXML;
        private System.Windows.Forms.Label FancyLogo;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button grabFiles;
    }
}

