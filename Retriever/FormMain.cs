﻿using System;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Amazon;
using Amazon.Runtime;
using AWSRetriver.Controls;
using CloudOps;
using NickAc.ModernUIDoneRight;
using NickAc.ModernUIDoneRight.Forms;
using NickAc.ModernUIDoneRight.Objects;
using NickAc.ModernUIDoneRight.Objects.MenuItems;
using Retriever.Model;
using Retriever.Properties;

namespace Retriever
{
    public partial class FormMain : ModernForm
    {
        private AWSCredentials creds;
        private Scanner scanner;
        private Profile profile;
        private CloudObjects cloudObjects = new CloudObjects();
        private ProgressMessages progressMessages = new ProgressMessages();
        private AppAction stopAction;
        private SidebarTextItem scanAction;
        private enum ScannerState
        {
            Working,
            Idle
        }

        private ScannerState scannerState = ScannerState.Idle;

        public FormMain()
        {
            InitializeComponent();

            PopulateActions();

            InitializeBackgroundWorker();

            InitializeScanner();
        }

        private void InitializeScanner()
        {
            scanner = new Scanner
            {
                MaxTasks = Settings.Default.ConcurrentConnecitons,
                TimeOut = Settings.Default.Timeout // 15 minutes default
            };
            scanner.Progress.ProgressChanged += Scanner_ProgressChanged;
        }

        private void PopulateActions()
        {
            AppBarMenuTextItem aboutAction = new AppBarMenuTextItem("About");
            aboutAction.Click += AboutAction_Click;
            appBar.MenuItems.Add(aboutAction);


            this.stopAction = new AppAction();
            stopAction.Image = Resources.Private50;
            stopAction.Click += StopAction_Click;
            appBar.Actions.Add(stopAction);

            AppAction loadProfileAction = new AppAction();
            loadProfileAction.Image = Resources.Form50;
            loadProfileAction.Click += LoadProfileAction_Click;
            appBar.Actions.Add(loadProfileAction);

            this.scanAction = new SidebarTextItem("Scan...");
            this.scanAction.Click += ScanAction_Click;
            this.sidebarControl.Items.Add(this.scanAction);

            SidebarTextItem runAction = new SidebarTextItem("Run Single Operation...");
            runAction.Click += RunAction_Click;
            this.sidebarControl.Items.Add(runAction);

            SidebarTextItem editProfileAction = new SidebarTextItem("Edit Profile...");
            editProfileAction.Click += EditProfileAction_Click;
            this.sidebarControl.Items.Add(editProfileAction);

            SidebarTextItem editCredentialsAction = new SidebarTextItem("Edit Credentials...");
            editCredentialsAction.Click += EditCredentialsAction_Click;
            this.sidebarControl.Items.Add(editCredentialsAction);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormAction("Loading profile...",
                delegate
                {
                    LoadProfile(Settings.Default.Profile, Profile.AllServices());
                }, false);
            FormAction("Loading objects..", LoadObjectsFromFile, false);
            FormAction("Loading messages..", LoadMessagesFromFile, false);
        }

        private void LoadProfileAction_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Profile.FileFilter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.LoadProfile(openFileDialog.FileName, this.profile);
            }
        }

        private void AboutAction_Click(object sender, EventArgs e)
        {
            FormAbout form = new FormAbout();
            form.ShowDialog(this);
        }

        private void EditCredentialsAction_Click(object sender, MouseEventArgs e)
        {
            FormAction("Ready", ShowCredentialsDialog);
        }

        private void StopAction_Click(object sender, EventArgs e)
        {
            if (this.scannerState==ScannerState.Idle)
            {
                ModernMessageBox.ShowError(new ApplicationException("Not scanning"));
            }
            FormAction("Stopping...", delegate ()
            {
                if (this.scanner != null)
                {

                    this.scanner.Cancel();
                }
            });
        }

        private void EditProfileAction_Click(object sender, MouseEventArgs e)
        {
            FormAction("Ready", ShowProfileDialog);
        }

        private void RunAction_Click(object sender, MouseEventArgs e)
        {
            FormAction("Ready", ShowRunDialog);
        }

        private void ShowRunDialog()
        {
            FormRun form = new FormRun();
            form.Profile = this.profile;
            DialogResult dr = form.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (form.Operation == null)
                {
                    SetStatus(new ApplicationException("No opeation selected"));
                    return;
                }
                if (this.creds == null)
                {
                    ShowCredentialsDialog();
                }
                foreach (var region in form.SelectedRegions)
                {
                    Operation op = form.Operation;
                    op.Proxy = this.Proxy;
                    scanner.Invokations.Enqueue(new OperationInvokation(op, region, this.creds, Settings.Default.PageSize));
                }
                if (!backgroundWorker.IsBusy)
                {                    
                    backgroundWorker.RunWorkerAsync();
                }
                UpdateCursor();
            }
        }

        private void ScanAction_Click(object sender, MouseEventArgs e)
        {
            if (this.scannerState==ScannerState.Working)
            {
                ModernMessageBox.ShowError(new ApplicationException("Scan in progress. Stop it first."));
                return;
            }
            FormAction("Scanning...", delegate
             {
                 ValidateCredentials();
                 statusLabel.Text = String.Empty;                 
                 ClearObjects();
                 ClearProgressMessages();
                 QueueOperations();
                 if (!backgroundWorker.IsBusy)
                 {
                     backgroundWorker.RunWorkerAsync();
                 }
             });
        }

        private void ValidateCredentials()
        {
            if (this.creds == null)
            {
                ShowCredentialsDialog();
            }
            if (this.creds != null)
            {
                if (this.creds.GetCredentials() != null)
                {
                    return;
                }
            }
            throw new ApplicationException("Invalid credentials");
        }
      
        private void UpdateCursor()
        {            
            if (this.scannerState==ScannerState.Working)
            {
                this.Cursor = Cursors.AppStarting;
            }
            else
            {
                this.Cursor = Cursors.Default;             
            }            
        }

        #region background worker

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            if (creds == null)
            {
                throw new ApplicationException("No Credentials are provided");
            }
            try
            {
                this.scannerState = ScannerState.Working;
                scanner.Scan();
                // TODO: make sure all background events are processed (don't sure where i am missing a mutex...)                                
                Thread.Sleep(1000);                
            }
            finally
            {
                backgroundWorker.ReportProgress(0, "Done");
                this.scannerState = ScannerState.Idle;
            }
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is InvokationResult ir)
            {
                this.cloudObjects.Update(ir);
                this.listViewFound.VirtualListSize = this.cloudObjects.Count;
                this.progressMessages.Add(new ProgressMessage(ir));
                this.listViewMessages.VirtualListSize = this.progressMessages.Count;
                listViewMessages.EnsureVisible(this.progressMessages.Count - 1);
                UpdateStatusBar(ir);
            }
            else
            {
                //?!
                Console.WriteLine(e);
            }
        }


        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SetStatus(e.Error);
                ModernMessageBox.ShowError(e.Error);
            }
            else if (e.Cancelled)
            {
                SetStatus("Canceled");
                this.progressBar.Value = 0;
            }
            else
            {
                SetStatus("Done");
            }

            UpdateCursor();
            FormAction("Saving objects...", cloudObjects.Save);
        }

        #endregion

        private void UpdateStatusBar(InvokationResult ir)
        {
            this.progressBar.Value = ir.Progress;
            this.statusLabel.Text = ir.ResultText();
        }


        private void SetStatus(Exception e)
        {
            SetStatus("Error: " + e.Message);
        }

        private void SetStatus(string message)
        {
            this.statusLabel.Text = message;

        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Scanner_ProgressChanged(object sender, InvokationResult e)
        {
            if (backgroundWorker.CancellationPending)
            {
                return;
            }
            if (backgroundWorker.IsBusy) // only when it is working...
            {
                backgroundWorker.ReportProgress(e.Progress, e);
            }
        }

        private void QueueOperations()
        {
            SetStatus("Queuing work items...");
            foreach (ProfileRecord p in this.profile)
            {
                if (p.Enabled)
                {
                    Operation op = Profile.FindOpeartion(p);
                    if (op != null)
                    {
                        op.Proxy = this.Proxy;
                        foreach (RegionEndpoint region in RegionsString.ParseSystemNames(p.Regions).Items)
                        {
                            scanner.Invokations.Enqueue(new OperationInvokation(op, region, creds, p.PageSize));
                        }
                    }
                }
            }
        }

        private void ShowCredentialsDialog()
        {
            FormCredentials formCredentials = new FormCredentials();
            DialogResult dr = formCredentials.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                this.creds = formCredentials.Credentials;
            }
        }

        private void ViewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModernMessageBox.ShowError(new NotImplementedException());
        }

        private void ListViewFound_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listViewFound.SelectedIndices.Count > 0)
            {
                CloudObject cobo = this.cloudObjects[this.listViewFound.SelectedIndices[0]];
                this.propertyGridObject.SelectedObject = cobo;
                this.richTextBoxCobo.Text = cobo.Source;
            }
        }


        private void ShowProfileDialog()
        {
            FormProfiles formProfiles = new FormProfiles();
            formProfiles.Profile = this.profile;
            formProfiles.ShowDialog();
            FormAction("Saving profile", profile.Save);
        }

        private void LoadMessagesFromFile()
        {
            try
            {
                this.progressMessages = ProgressMessages.Load();
                this.listViewMessages.VirtualListSize = this.progressMessages.Count;
            }
            catch (Exception)
            {
                this.progressMessages = new ProgressMessages();
                throw;
            }
        }

        private void LoadObjectsFromFile()
        {
            try
            {
                this.cloudObjects = CloudObjects.Load();
                this.listViewFound.VirtualListSize = this.cloudObjects.Count;
            }
            catch (Exception)
            {
                this.cloudObjects = new CloudObjects();
                throw;
            }

        }

        private void FormAction(string status, Action action, bool showMessageBox = true)
        {
            this.sidebarControl.HideSidebar();
            Cursor.Current = Cursors.WaitCursor;
            SetStatus(status);
            try
            {
                action.Invoke();
                SetStatus("Ready");
            }
            catch (Exception ex)
            {
                if (showMessageBox)
                {
                    ModernMessageBox.ShowError(ex);
                }
                else
                {
                    SetStatus(ex.Message);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void LoadProfile(string fileName, Profile defaultProfile)
        {
            try
            {
                this.profile = Profile.Load(fileName);
                Settings.Default.Profile = fileName;
            }
            catch (Exception ex)
            {
                ModernMessageBox.ShowError(
                    new ApplicationException(String.Format("Failed to load profile '{0}': {1}, creating new.", fileName, ex.Message)));
                this.profile = defaultProfile;
            }
            finally
            {
                RefreshProfileName();
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public WebProxy Proxy
        {
            get
            {
                if (string.IsNullOrEmpty(Settings.Default.ProxyHost))
                {
                    return null;
                }
                return new WebProxy(Settings.Default.ProxyHost, Settings.Default.ProxyPort)
                {
                    Credentials = new NetworkCredential(Settings.Default.ProxyUser, Settings.Default.ProxyPassword)
                };
            }
        }

        private void RefreshProfileName()
        {
            this.appBar.Text = String.Format("{0} - (Active Profile: '{1}')", AssemblyProduct, this.profile.Name);
        }

        private void ListViewFound_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            cloudObjects.RetrieveVirtualItem(e);
        }

        private void ListViewMessages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            progressMessages.RetrieveVirtualItem(e);
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearObjects();
        }

        private void ClearObjects()
        {
            cloudObjects.Clear();
            listViewFound.VirtualListSize = 0;
        }

        private void ClearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearProgressMessages();
        }

        private void ClearProgressMessages()
        {
            progressMessages.Clear();
            listViewMessages.VirtualListSize = 0;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.progressMessages.Save();
        }

    }
}