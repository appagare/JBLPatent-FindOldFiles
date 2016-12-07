namespace FindOldFiles
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
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
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SourcePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OutputFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TotalCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TotalSize = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MoveToFolder = new System.Windows.Forms.TextBox();
            this.chkConfirm = new System.Windows.Forms.CheckBox();
            this.KeepCount = new System.Windows.Forms.TextBox();
            this.MoveSize = new System.Windows.Forms.TextBox();
            this.MoveCount = new System.Windows.Forms.TextBox();
            this.KeepSize = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkUpdateStatus = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.FileMask = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(505, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(505, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SourcePath
            // 
            this.SourcePath.Location = new System.Drawing.Point(87, 9);
            this.SourcePath.Name = "SourcePath";
            this.SourcePath.Size = new System.Drawing.Size(122, 20);
            this.SourcePath.TabIndex = 0;
            this.SourcePath.Text = "E:\\attdir";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log File:";
            // 
            // OutputFile
            // 
            this.OutputFile.Location = new System.Drawing.Point(465, 104);
            this.OutputFile.Name = "OutputFile";
            this.OutputFile.Size = new System.Drawing.Size(100, 20);
            this.OutputFile.TabIndex = 6;
            this.OutputFile.Text = "c:\\t1\\compare.log";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Status:";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(84, 70);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(33, 13);
            this.Status.TabIndex = 7;
            this.Status.Text = "Idle...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Count:";
            // 
            // TotalCount
            // 
            this.TotalCount.Location = new System.Drawing.Point(87, 106);
            this.TotalCount.Name = "TotalCount";
            this.TotalCount.ReadOnly = true;
            this.TotalCount.Size = new System.Drawing.Size(100, 20);
            this.TotalCount.TabIndex = 9;
            this.TotalCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Size:";
            // 
            // TotalSize
            // 
            this.TotalSize.Location = new System.Drawing.Point(87, 130);
            this.TotalSize.Name = "TotalSize";
            this.TotalSize.ReadOnly = true;
            this.TotalSize.Size = new System.Drawing.Size(100, 20);
            this.TotalSize.TabIndex = 11;
            this.TotalSize.Text = "0";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(351, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(122, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(282, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Older Than:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Archive Folder:";
            // 
            // MoveToFolder
            // 
            this.MoveToFolder.Location = new System.Drawing.Point(87, 37);
            this.MoveToFolder.Name = "MoveToFolder";
            this.MoveToFolder.Size = new System.Drawing.Size(122, 20);
            this.MoveToFolder.TabIndex = 3;
            this.MoveToFolder.Text = "E:\\archive";
            // 
            // chkConfirm
            // 
            this.chkConfirm.AutoSize = true;
            this.chkConfirm.Location = new System.Drawing.Point(215, 41);
            this.chkConfirm.Name = "chkConfirm";
            this.chkConfirm.Size = new System.Drawing.Size(279, 17);
            this.chkConfirm.TabIndex = 4;
            this.chkConfirm.Text = "Move files to archive (leave unchecked to only scan )";
            this.chkConfirm.UseVisualStyleBackColor = true;
            this.chkConfirm.CheckedChanged += new System.EventHandler(this.chkConfirm_CheckedChanged);
            // 
            // KeepCount
            // 
            this.KeepCount.Location = new System.Drawing.Point(193, 104);
            this.KeepCount.Name = "KeepCount";
            this.KeepCount.ReadOnly = true;
            this.KeepCount.Size = new System.Drawing.Size(100, 20);
            this.KeepCount.TabIndex = 17;
            this.KeepCount.Text = "0";
            // 
            // MoveSize
            // 
            this.MoveSize.Location = new System.Drawing.Point(299, 130);
            this.MoveSize.Name = "MoveSize";
            this.MoveSize.ReadOnly = true;
            this.MoveSize.Size = new System.Drawing.Size(100, 20);
            this.MoveSize.TabIndex = 18;
            this.MoveSize.Text = "0";
            // 
            // MoveCount
            // 
            this.MoveCount.Location = new System.Drawing.Point(299, 104);
            this.MoveCount.Name = "MoveCount";
            this.MoveCount.ReadOnly = true;
            this.MoveCount.Size = new System.Drawing.Size(100, 20);
            this.MoveCount.TabIndex = 19;
            this.MoveCount.Text = "0";
            // 
            // KeepSize
            // 
            this.KeepSize.Location = new System.Drawing.Point(193, 130);
            this.KeepSize.Name = "KeepSize";
            this.KeepSize.ReadOnly = true;
            this.KeepSize.Size = new System.Drawing.Size(100, 20);
            this.KeepSize.TabIndex = 20;
            this.KeepSize.Text = "0";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(505, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "test button";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkUpdateStatus
            // 
            this.checkUpdateStatus.AutoSize = true;
            this.checkUpdateStatus.Location = new System.Drawing.Point(414, 85);
            this.checkUpdateStatus.Name = "checkUpdateStatus";
            this.checkUpdateStatus.Size = new System.Drawing.Size(151, 17);
            this.checkUpdateStatus.TabIndex = 5;
            this.checkUpdateStatus.Text = "Show details while running";
            this.checkUpdateStatus.UseVisualStyleBackColor = true;
            this.checkUpdateStatus.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Total";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(296, 86);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Archive on Fileserver";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(190, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Push To Cloud";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(212, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Mask:";
            // 
            // FileMask
            // 
            this.FileMask.Location = new System.Drawing.Point(244, 9);
            this.FileMask.Name = "FileMask";
            this.FileMask.Size = new System.Drawing.Size(32, 20);
            this.FileMask.TabIndex = 1;
            this.FileMask.Text = "*.*";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(414, 130);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(155, 17);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Create AzCopy Placeholder";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 158);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.FileMask);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkUpdateStatus);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.KeepSize);
            this.Controls.Add(this.MoveCount);
            this.Controls.Add(this.MoveSize);
            this.Controls.Add(this.KeepCount);
            this.Controls.Add(this.chkConfirm);
            this.Controls.Add(this.MoveToFolder);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.TotalSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TotalCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OutputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SourcePath);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "File Compare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox SourcePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OutputFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TotalCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TotalSize;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MoveToFolder;
        private System.Windows.Forms.CheckBox chkConfirm;
        private System.Windows.Forms.TextBox KeepCount;
        private System.Windows.Forms.TextBox MoveSize;
        private System.Windows.Forms.TextBox MoveCount;
        private System.Windows.Forms.TextBox KeepSize;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkUpdateStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox FileMask;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

