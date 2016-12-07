using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FindOldFiles
{
       
    public partial class Form1 : Form
    {
        private const int DRIVE = 0;
        private const int ROOT = 1;
        private const int CLIENT = 2;
        private const int JOB = 3;
        private const int FILENAME = 4;

        private bool checkDate = false;
        private DateTime testDate = DateTime.Now;

        private delegate void UpdateStatusCallback(OperatingParams p); 

        BackgroundWorker workerThread;
        public StreamWriter OutputStream { get; set; }

        private int _TotalCount = 0;
        private long _TotalSize = 0;
        private int _KeepCount = 0;
        private long _KeepSize = 0;
        private int _MoveCount = 0;
        private long _MoveSize = 0;
        
        
        public Form1()
        {
            InitializeComponent();

            //initialize background process
            workerThread = new BackgroundWorker();
            //set it's properties
            workerThread.WorkerSupportsCancellation = true;
            workerThread.WorkerReportsProgress = true;  
            // assign the event handlers supported
            workerThread.DoWork += new DoWorkEventHandler(workerThread_DoWork);
            workerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerThread_RunWorkerCompleted);
            
        }

        private void updateStatus(OperatingParams p)
        {

            this.KeepCount.Text = p.KeepCount.ToString();
            this.KeepSize.Text = fileSizeFormat(p.KeepSize);

            this.MoveCount.Text = p.MoveCount.ToString();
            this.MoveSize.Text = fileSizeFormat(p.MoveSize);

            this.TotalCount.Text = p.TotalCount.ToString();
            this.TotalSize.Text = fileSizeFormat(p.TotalSize);

            if (p.Msg != "")
            {
                WriteMsg(p.Msg); 
            }
        }

        private string fileSizeFormat(long originalSize)
        {
            if (originalSize < 1000000) {
                //less than 1Mb, display as KB 
                return ((double)originalSize / 1000).ToString("N2") + " KB";
 
            }
            else if (originalSize < 1000000000)
            {
                //less than 1Gb, display as MB 
                return ((double)originalSize / 1000000).ToString("N2") + " MB";
            }
            else
            {
                //display as GB
                return ((double)originalSize / 1000000000).ToString("N2") + " GB";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* 
             * tasks
             * get comparison date
             * recursive loop through source path, getting each file, increment total count
             * if file is older than compare date, increment compare count and 
             * 
             */
            string msg = "";

            if (SourcePath.Text.Length == 0)
            {
                msg = "You must specify a valid source folder to scan.\n";
            }
            else if (Directory.Exists(SourcePath.Text) == false)
            {
                msg = SourcePath.Text + " is not a valid source folder.\n";
            }

            if (CheckDate(dateTimePicker1.Value.ToString()) == false)
            {
                // CheckDate shouldn't be necessary w/ datepicker but it's handy code;
                msg += "Invalid compare date.\n";
            }
            else if ((dateTimePicker1.Value.Year < 1990) || (dateTimePicker1.Value.Year > DateTime.Now.Year ))
            {
                msg += "Invalid  year in date.\n";
            }

            if (chkConfirm.Checked == true)
            {

                if (MoveToFolder.Text.Length == 0)
                {
                    msg += "You must specify a valid target folder to archive old files.";
                }
                else if (Directory.Exists(MoveToFolder.Text) == false)
                {
                    msg += MoveToFolder.Text + " is not a valid target folder.";
                }
            }
            // make sure archive folder is not within source folder or it could recursively fill up the disk
            // only bother with this if we have valid folders already specified
            if (msg =="") 
            {
                string[] a;
                a = SourcePath.Text.Split('\\');
                if (MoveToFolder.Text.IndexOf(a[DRIVE] + "\\" + a[ROOT]) > -1)
                {
                    msg = "Archive folder cannot exist beneath source folder.";
                }
            }
            if (msg != "")
            {
                MessageBox.Show(msg, "Invalid Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // provide a warning if not actually moving files but an invalid target was specified 
            if ((chkConfirm.Checked == false) && MoveToFolder.Text.Length > 0 && (Directory.Exists(MoveToFolder.Text) == false))
            {
                // allow option to proceed or bail
                DialogResult warn = MessageBox.Show("Archive folder specified does not exist.\n\n OK to continue with scan only. Otherwise, click Cancel.", "Warning Only", MessageBoxButtons.OKCancel, MessageBoxIcon.Information  );

                if (warn == DialogResult.Cancel) 
                {
                    return;
                }
            }
            
         //ok to proceed
        // initialize counters
        _TotalCount = 0;
        _TotalSize = 0;
        _KeepCount = 0;
        _KeepSize = 0;
        _MoveCount = 0;
        _MoveSize = 0;
        

        if (workerThread.IsBusy != true)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            // checkBox2.Enabled = false;
            FileMask.Enabled = false;
            chkConfirm.Enabled = false;
            dateTimePicker1.Enabled = false;
            MoveToFolder.Enabled = false;
            processfiles(); //does initial prep and then launches background thread
        }

            
                                    
        }
        protected bool CheckDate(String date)
        {
            DateTime Temp;
            if (DateTime.TryParse(date, out Temp) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void processfiles()
        {
            Status.Text = "Starting...";

            //close output file if it is open
            CloseFile();

            //delete output file if it exists
            if ((OutputFile.Text.Length > 0) && (File.Exists(OutputFile.Text) == true))
            {
                Status.Text = "Removing old output file...";
                File.Delete(OutputFile.Text);
            }

            // open a filestream if we have a file specified
            if (OutputFile.Text.Length > 0)
            {
                Status.Text = "Creating new output file...";
                OutputStream = new StreamWriter(OutputFile.Text);
            }
                    
            // capture operating parameters
            OperatingParams p = new OperatingParams();
            p.KeepCount = 0;
            p.KeepSize = 0;
            p.MoveCount = 0;
            p.MoveSize = 0;
            p.TotalCount = 0;
            p.TotalSize = 0;
            p.Msg = "";
            p.root = new DirectoryInfo(SourcePath.Text); // start at root
            p.CompareDate = dateTimePicker1.Value;
            p.ArchiveFolder = MoveToFolder.Text.Trim();
            p.Move = chkConfirm.Checked;
            p.ArchiveFolder = MoveToFolder.Text;

            if ((p.Move == true) && (p.ArchiveFolder.Substring(p.ArchiveFolder.Length - 1) != "\\"))
            {
                p.ArchiveFolder += "\\";
            }
            
            
            if (checkUpdateStatus.Checked == true)
            {
                // display a generic message if not showing details
                Status.Text = "Scanning...";
            }
            
            // launch background thread
            workerThread.RunWorkerAsync(p);
            
        }

        /// <summary>
        /// sub that kicks off long running process
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            // method that kicks off long running process;
            // per docs., don't access the BackgroundWorker reference directly.
            // Use the reference provided by the sender parameter.
            BackgroundWorker worker = sender as BackgroundWorker;

            OperatingParams p = (OperatingParams)e.Argument;
            
            // actual work is in ProcessDirectory
            ProcessDirectory(p.root, p.CompareDate, p.MoveCount, p.MoveSize, p.KeepCount, p.KeepSize, p.TotalCount, p.TotalSize, p.Move, p.ArchiveFolder, ref worker);

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

        }
        /// <summary>
        /// event that occurs when long running process terminates
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                WriteMsg("User cancelled.");
            }

            else if (!(e.Error == null))
            {
                WriteMsg("Process error, aborted.");
            }

            else
            {
                WriteMsg("Done.");
            }

            OperatingParams p = new OperatingParams();

            p.TotalCount = _TotalCount;
            p.TotalSize = _TotalSize;
            p.KeepCount = _KeepCount;
            p.KeepSize = _KeepSize;
            p.MoveCount = _MoveCount;
            p.MoveSize = _MoveSize;
            p.Msg = "Async. pass complete";
            updateStatus(p);
            

            CloseFile();
            // no matter what, re-enable buttons
            button1.Enabled = true;
            button2.Enabled = false;
            // checkBox2.Enabled = true;
            FileMask.Enabled = true;
            chkConfirm.Enabled = true;
            dateTimePicker1.Enabled = true;
            MoveToFolder.Enabled = true; 
        }

        
        private bool MoveFile(string FilePathToMove, string ArchiveRoot)
        {
            // note = ArchiveRoot has trailing \

            if (ArchiveRoot.Substring(ArchiveRoot.Length -1) != "\\")
            {
                ArchiveRoot += "\\";
            }

            

            string[] a;
            a = FilePathToMove.Split('\\');

            if (a.Length == (FILENAME +1))  // this only moves files in E:\attdir\{client}\{job} directory structure
            {
                try
                {
                    string targetPath = ArchiveRoot + a[CLIENT] + "\\" + a[JOB];
                    //check if root exists; if not, create it
                    if (Directory.Exists(targetPath) == false)
                    {
                        Directory.CreateDirectory(targetPath);  
                    }

                    // shouldn't happen but delete old one first so move will be successful
                    if (File.Exists(targetPath + "\\" + a[FILENAME]) == true)
                    {
                        File.Delete(targetPath + "\\" + a[FILENAME]); 
                    }
                    
                    // do the move
                    File.Move(FilePathToMove, targetPath + "\\" + a[FILENAME]);
                    return true;
                }
                catch
                {
                    // failed create or move
                    return false;
                }
            }
            else
            {
                // not moved; wrong format
                return false;
            }
        }

        private void ProcessDirectory(DirectoryInfo root, DateTime CompareDate, 
             int MoveCount, 
             long MoveSize,
             int KeepCount, 
             long KeepSize,
             int TotalCount, 
             long TotalSize,
             bool Move,
             string ArchiveFolder,
             ref BackgroundWorker wp)

        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            Delegate upd = new UpdateStatusCallback(updateStatus);

            // First, process all the files directly under this folder
            try
            {
                //files = root.GetFiles("*.*");
                if (FileMask.Text.Length < 1)
                {
                    files = root.GetFiles("*.*");
                }
                else
                {
                    files = root.GetFiles(FileMask.Text);
                }
                
            }
            
            catch (System.IO.DirectoryNotFoundException e)
            {
                //Console.WriteLine(e.Message);
            }
            

            OperatingParams p = new OperatingParams();

            p.CompareDate = dateTimePicker1.Value;
            CompareDate = dateTimePicker1.Value;
            p.Move = chkConfirm.Checked; 
            Move = chkConfirm.Checked;
            p.ArchiveFolder = MoveToFolder.Text ;
            ArchiveFolder = MoveToFolder.Text; 
 
            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    //here is where the file work is done

                    _TotalCount++;
                    _TotalSize += fi.Length;

                    if (fi.LastWriteTime < CompareDate)
                    {
                        // move to archive
                        _MoveCount++;
                        _MoveSize += fi.Length;
                        
                        if (Move == true)
                        {
                            if (MoveFile(fi.FullName, ArchiveFolder) == true) // MoveFile function will create client/job folders if necessary
                            {
                                p.Msg = string.Format("{0}\t{1}\t{2}", "Archived", fi.LastWriteTime.ToString(), fi.FullName);

                                // here is where we'd create a new empty file to insure azcopy builds complete structure
                                if (checkBox1.Checked == true)
                                {
                                    if (File.Exists(root.FullName + "\\azcopy.txt") == false)
                                    {
                                        StreamWriter sw = File.CreateText(root.FullName + "\\azcopy.txt");
                                        sw.WriteLine(fi.FullName);
                                        sw.Dispose();
                                    }
                                }
                            }
                            else
                            {
                                p.Msg = string.Format("{0}\t{1}\t{2}", "Error", fi.LastWriteTime.ToString(), fi.FullName);
                            }
                        }
                        else
                        {
                            p.Msg = string.Format("{0}\t{1}\t{2}", "Archive", fi.LastWriteTime.ToString(), fi.FullName);
                        }
                    }
                    else
                    {
                        //Keep for cloud
                        _KeepCount++;
                        _KeepSize += fi.Length;
                        // p.Msg = fi.FullName + " will be kept (" + fi.LastWriteTime + "). TC:[" + p.TotalCount.ToString() + "] MC:[" + p.MoveCount.ToString() + "] KC:" + p.KeepCount.ToString() + "] TS:[" + p.TotalSize.ToString() + "] KS:" + p.KeepSize.ToString() + "]";
                        p.Msg = string.Format("{0}\t{1}\t{2}", "Cloud", fi.LastWriteTime.ToString(), fi.FullName);
                    }

                    //update status here
                    if (checkUpdateStatus.Checked == true)
                    {
                        p.CompareDate = CompareDate;
                        p.MoveCount = _MoveCount;
                        p.MoveSize = _MoveSize;
                        p.KeepCount = _KeepCount;
                        p.KeepSize = _KeepSize;
                        p.TotalCount = _TotalCount;
                        p.TotalSize = _TotalSize;
                        p.ArchiveFolder = ArchiveFolder;
                        p.Move = Move;
                        this.Invoke(upd, p);
                    }

                    // check for cancel request
                    if (wp.CancellationPending == true)
                    {
                        return;
                    }
                    
                }

                // find all the subdirectories under this directory.
                subDirs = root.GetDirectories();
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Recursive call for each subdirectory.
                    if (wp.CancellationPending == true)
                    {
                        return;
                    }
                    ProcessDirectory(dirInfo, p.CompareDate,  p.MoveCount,  p.MoveSize,  p.KeepCount,  p.KeepSize,  p.TotalCount,  p.TotalSize, p.Move, p.ArchiveFolder, ref wp);
                }
            }
        }
        
        
        private void CloseFile()
        {
            if (OutputStream != null)
            {
                try
                {
                    OutputStream.Dispose();
                    OutputStream = null;
                }
                catch
                {

                }
            }
        }

        private void WriteMsg(string msg)
        {
            Status.Text = msg;
            if (OutputStream != null)
            {
                OutputStream.WriteLine("{0}\t{1}", DateTime.Now, msg); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // tell long running process to stop
            if (workerThread.WorkerSupportsCancellation == true)
            {
                workerThread.CancelAsync();
            }
            
        }

        private void chkConfirm_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConfirm.Checked == true)
            {
                string msg = "";
                if (MoveToFolder.Text.Length == 0)
                {
                    msg = "You must first specify a valid target folder to archive old files.";
                }
                else if (Directory.Exists(MoveToFolder.Text) == false)
                {
                    msg = MoveToFolder.Text + " is not a valid target folder.";
                }
                // don't all flag to be set unless target folder exists first
                if (msg != "")
                {
                    MessageBox.Show(msg, "Invalid Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkConfirm.Checked = false; 
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (workerThread.IsBusy == true)
            {
                if (checkUpdateStatus.Checked == false) 
                {
                    // set a default if already running but changing to don't show status
                    Status.Text = "Scanning...";
                }
            }
        }

        private void ProcessDirectory2(DirectoryInfo root)

        {
            DirectoryInfo[] subDirs = null;

            if (File.Exists(root.FullName + "\\azcopy.txt") == false)
            {
                StreamWriter sw = File.CreateText(root.FullName + "\\azcopy.txt");
                sw.WriteLine("azcopy.txt");
                sw.Dispose();
             }
            subDirs = root.GetDirectories();
           foreach (System.IO.DirectoryInfo dirInfo in subDirs)
           {
                ProcessDirectory2(dirInfo);
            }
        }
        
        // hidden test code
        private void button3_Click(object sender, EventArgs e)
        {


            int test;
            if (int.TryParse(SourcePath.Text, out test))
            {
                MessageBox.Show(SourcePath.Text + "is the number: " + test.ToString() , "Yes");
            }
            else
            {
                MessageBox.Show(SourcePath.Text + "is not a number: " + test.ToString() , "No");
            }
            //DateTime nowDate = Convert.ToDateTime(SourcePath.Text);
            //int seedHr = Convert.ToInt16(FileMask.Text);
            //if ((nowDate >= testDate) && (nowDate.Hour == seedHr))
            //{
            //    // if the current time is >= to the next time to run,
            //    // and this hour is our seedhr,
            //    // increase the next time to run by 24 hrs and then do stuff
            //    testDate = nowDate.AddDays(1);
            //    // MoveToFolder.Text = testDate.ToString();

            //    // add safety to only do this during seed hour 

            //    MessageBox.Show("Would run AzCopy. New test date is: " + testDate.ToString(), "Yes");
            //}
            //else
            //{
            //    MessageBox.Show("Would not run AzCopy. Test date is: " + testDate.ToString() + " and NowDate is " + nowDate.ToString() + " and SeedHour is " + seedHr.ToString()    , "No");
            //}

            //// azcopy everything in source
            //ProcessDirectory2(new DirectoryInfo(SourcePath.Text));

            //MessageBox.Show("Done", "Done");  


        //    string[] a;
        //    string s = "";
        //    //a = SourcePath.Text.Split('\\');

        //    //const int DRIVE = 0;
        //    //const int ROOT = 1;
        //    //const int CLIENT = 2;
        //    //const int JOB = 3;

        //    //s = string.Format("DRIVE:{0} ROOT:{1} CLIENT:{2} JOB:{3}", a[DRIVE], a[ROOT], a[CLIENT], a[JOB]);

        //    //MessageBox.Show(s);
        //    string line = "";
        //    int counter = 0;
        //    System.IO.StreamReader file = new System.IO.StreamReader(@"C:\appagare\consult\JBL Patents\compare.log");
        //    while((line = file.ReadLine()) != null)
        //    {
        //        a = line.Split('\t');
        //        if (a.Length == 4)
        //        {
        //            s = "insert into JBL select '" + a[2] + "','" + a[1] + "','" + a[3] + "'";
        //        }
        //        counter++;
        //        ASi.DataAccess.SqlHelper.ExecuteNonQuery(@"data source=SQLMachine\SQLInstance;initial catalog=DBName;Integrated Security=true;persist security info=True;packet size=4096", CommandType.Text, s); 
        //    }
        //    file.Close();


       }
    }

    class OperatingParams
    {
        public int TotalCount { get; set; }
        public long TotalSize { get; set; }
        public int KeepCount { get; set; }
        public long KeepSize { get; set; }
        public int MoveCount { get; set; }
        public long MoveSize { get; set; }
        public string Msg { get; set; }
        public DirectoryInfo root { get; set; }
        public DateTime CompareDate { get; set; }
        public string ArchiveFolder { get; set; }
        public bool Move { get; set; }
    }


}
