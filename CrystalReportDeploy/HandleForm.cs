using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalReportDeploy.Services;
using CrystalReportDeploy.Models;
using Newtonsoft.Json;

namespace CrystalReportDeploy
{
    public partial class HandleForm : Form
    {
        private CheckBox headerCheckBox = new CheckBox();
        private BindingSource bindingSource = new BindingSource();
        private int maxSelectRecords = ConfigData.MaxSelectRecords;
        private string deployFolder = "";
        private string publishFolderPath = "";
        private Color dataGridViewRowBackColor = Color.White;

        public HandleForm(string publishFolderPath, string deployFolder)
        {
            InitializeComponent();

            this.Text = ConfigData.FormFullTitle;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            this.PublishFolderPathLabel.Text = publishFolderPath;
            this.publishFolderPath = publishFolderPath;
            this.deployFolder = deployFolder;

            DataGridViewInit();
            DataGridViewBind();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.OpenForms["MainForm"].Show();
            this.Close();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            var hasVisibleForm = false;
            foreach (Form formItem in Application.OpenForms)
            {
                if (formItem.Visible)
                {
                    hasVisibleForm = true;
                    break;
                }
            }

            if (!hasVisibleForm)
            {
                Application.ExitThread();
                Application.Exit();
            }
        }

        private void DataGridViewInit()
        {
            DataGridView.AutoGenerateColumns = false;

            //Add a CheckBox Column to the dataTable Header Cell.

            //Find the Location of Header Cell.
            Point headerCellLocation = this.DataGridView.GetCellDisplayRectangle(0, -1, true).Location;

            //Place the Header CheckBox in the Location of the Header Cell.
            //headerCheckBox.Location = new Point(headerCellLocation.X + 10, headerCellLocation.Y + 10);
            headerCheckBox.BackColor = Color.White;
            headerCheckBox.Size = new Size(18, 18);
            headerCheckBox.Checked = true;

            //Assign Click event to the Header CheckBox.
            headerCheckBox.Click += new EventHandler(HeaderCheckBox_Clicked);
            DataGridView.Controls.Add(headerCheckBox);

            //Add a CheckBox Column to the dataTable at the first position.
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "";
            checkBoxColumn.Width = 30;
            checkBoxColumn.Name = "IsChecked";
            checkBoxColumn.DataPropertyName = "IsChecked";
            DataGridView.Columns.Insert(0, checkBoxColumn);

            //Assign Click event to the dataTable Cell.
            DataGridView.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellClick);
            DataGridView.ColumnHeadersHeight = 26;
            DataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
        }

        private void DataGridViewBind()
        {
            try
            {
                var deployDetails = new List<DeployDetail>();
                var deployFolders = this.deployFolder.Split(new string[] { "\n" }, StringSplitOptions.None);
                foreach(var deployFolder in deployFolders)
                {
                    var backupDeployFolder = "";
                    var backupDeployFolderData = DynamicData.GetDynamicData("BackupDeployFolder");
                    if (!string.IsNullOrEmpty(backupDeployFolderData))
                    {
                        foreach (var backupDeployFolderDataItem in backupDeployFolderData.Split(new string[] { "##" }, StringSplitOptions.None))
                        {
                            if (!string.IsNullOrEmpty(backupDeployFolderDataItem))
                            {
                                var itemArray = backupDeployFolderDataItem.Split(new string[] { "@@" }, StringSplitOptions.None);
                                var deployFolderName = itemArray[0];
                                var backUpFolderName = itemArray[1];
                                if (deployFolderName.ToLower() == deployFolder.ToLower())
                                {
                                    backupDeployFolder = backUpFolderName;
                                }
                            }
                        }
                    }

                    var deployDetail = new DeployDetail();
                    deployDetail.IsChecked = true;
                    deployDetail.DeployFolder = deployFolder;
                    deployDetail.BackupDeployFolder = backupDeployFolder;

                    deployDetails.Add(deployDetail);
                }

                deployDetails = deployDetails.OrderBy(p => p.DeployFolder).ToList();
                bindingSource.DataSource = deployDetails;
                DataGridView.DataSource = bindingSource;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                return;
            }
        }

        private void HeaderCheckBox_Clicked(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            DataGridView.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in DataGridView.Rows)

            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["IsChecked"] as DataGridViewCheckBoxCell);
                checkBox.Value = headerCheckBox.Checked;
            }

            HighlightsCheckedRow();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check to ensure that the row CheckBox is clicked.
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataGridView.EndEdit();
            }
        }
        private void SetHeaderCheckBoxValue()
        {
            bool isChecked = true;
            foreach (DataGridViewRow row in DataGridView.Rows)

            {
                if (Convert.ToBoolean(row.Cells["IsChecked"].EditedFormattedValue) == false)

                {
                    isChecked = false;
                    break;
                }

            }
            headerCheckBox.Checked = isChecked;
        }


        private async void DeployButton_Click(object sender, EventArgs e)
        {
            ProcessingStart();
            try
            {   
                if (DeployButtonValidate())
                {
                    var list = DataGridView.Rows
                        .Cast<DataGridViewRow>()
                        .Where(row => (row.Cells["IsChecked"].Value != null && Convert.ToBoolean(row.Cells["IsChecked"].Value) == true))
                        .ToList();
                    var logs = new List<string>();
                    var startTime = DateTime.Now;
                    CrystalReportService crystalReportService = new CrystalReportService();
                    foreach (var row in list)
                    {
                        var startItemTime = DateTime.Now;

                        var deployFolder = (row.Cells["DeployFolderD"] as DataGridViewTextBoxCell).Value.ToString();
                        var backupDeployFolder = (row.Cells["BackupDeployFolderD"] as DataGridViewTextBoxCell).Value.ToString();

                        var crystalReportDeployFolderID = crystalReportService.GetFolderID(crystalReportService.infoStore, deployFolder);
                        var fileNames = Directory.GetFiles(this.publishFolderPath).Select(p => Path.GetFileNameWithoutExtension(p)).ToList();

                        #region backup deploy folder
                        logs.Add("backup " + deployFolder + " start:" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                        if(fileNames.Count > 0)
                        {
                            var crystalReportRootFolderFolderID = crystalReportService.GetFolderID(crystalReportService.infoStore, "Root Folder");
                            var crystalReportBackupDeployFolderID = crystalReportService.AddFolder(crystalReportService.infoStore, crystalReportRootFolderFolderID, backupDeployFolder, "");
                            crystalReportService.MoveObjects(crystalReportService.infoStore, crystalReportDeployFolderID, fileNames, crystalReportBackupDeployFolderID);
                        }
                        logs.Add("backup " + deployFolder + " end:" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                        #endregion

                        #region copy to deploy folder
                        logs.Add("copy to " + deployFolder + " start:" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                        var filePaths = Directory.GetFiles(this.publishFolderPath).ToList();
                        crystalReportService.AddReport(crystalReportService.infoStore, filePaths, crystalReportDeployFolderID);
                        crystalReportService.UpdateReportLogonInformation(crystalReportService.infoStore, crystalReportDeployFolderID, deployFolder.ToUpper(), fileNames);
                        logs.Add("copy to " + deployFolder + " end:" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                        #endregion

                        var endItemTime = DateTime.Now;
                        logs.Add("total file count: " + fileNames.Count);
                        logs.Add("time spent(seconds): " + (endItemTime-startItemTime).TotalSeconds.ToString("F2"));
                        logs.Add("");
                    }

                    var endTime = DateTime.Now;
                    logs.Add("total time spent(seconds): " + (endTime - startTime).TotalSeconds.ToString("F2"));

                    MessageBox.Show(string.Join("\n", logs), "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            ProcessingEnd();
        }

        private bool DeployButtonValidate()
        {
            var result = true;

            var list = DataGridView.Rows
                        .Cast<DataGridViewRow>()
                        .Where(row => (row.Cells["IsChecked"].Value != null && Convert.ToBoolean(row.Cells["IsChecked"].Value) == true))
                        .ToList();

            var selectCount = list.Count();

            if (selectCount == 0)
            {
                MessageBox.Show("Please select at least one record.", "Error");
                return false;
            }
            else if (selectCount >= maxSelectRecords)
            {
                MessageBox.Show("Maximum records is " + maxSelectRecords + ". Please select less than " + maxSelectRecords + " records.", "Error");
                return false;
            }

            foreach(var row in list)
            {
                var deployFolder = (row.Cells["DeployFolderD"] as DataGridViewTextBoxCell).Value?.ToString();
                var backupDeployFolder = (row.Cells["BackupDeployFolderD"] as DataGridViewTextBoxCell).Value?.ToString();

                if (string.IsNullOrEmpty(deployFolder) || string.IsNullOrEmpty(backupDeployFolder))
                {
                    MessageBox.Show("Please input the value of Deployment Folder, Backup Deployment Folder.", "Error");
                    return false;
                }
            }

            return result;
        }

        private void ProcessingStart()
        {
            ProcessingLabel.Show();
            DataGridView.Enabled = false;
            DeployButton.Enabled = false;
            ExitButton.Enabled = false;
            this.Update();
        }

        private void ProcessingEnd()
        {
            ProcessingLabel.Hide();
            DataGridView.Enabled = true;
            DeployButton.Enabled = true;
            ExitButton.Enabled = true;

            this.ActiveControl = null;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == DataGridView.Columns["IsChecked"].Index)
            {
                DataGridView.EndEdit();
            }
        }

        public void CopyToDeployFolder(string sourceDir, string destinationDir, string[] ignoreFolders, string[] ignoreConfigs)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                var ignoreConfigCount = ignoreConfigs.Where(p => file.Name.ToLower() == p.ToLower()).Count();
                if (ignoreConfigCount > 0)
                {
                    continue;
                }

                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            // If recursive and copying subdirectories, recursively call this method
            foreach (DirectoryInfo subDir in dirs)
            {
                var ignoreFolderCount = ignoreFolders.Where(p => subDir.FullName.ToLower().Contains(p.ToLower())).Count();

                if(ignoreFolderCount > 0)
                {
                    continue;
                }

                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyToDeployFolder(subDir.FullName, newDestinationDir, ignoreFolders, ignoreConfigs);
            }
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {   
            if (e.ColumnIndex == 2)
            {
                var backupDeployFolderData = DynamicData.GetDynamicData("BackupDeployFolder");

                var row = DataGridView.Rows[e.RowIndex];
                var deployFolder = (row.Cells["DeployFolderD"] as DataGridViewTextBoxCell).Value?.ToString();
                var backupDeployFolder = (row.Cells["BackupDeployFolderD"] as DataGridViewTextBoxCell).Value?.ToString();

                if (string.IsNullOrEmpty(deployFolder))
                {
                    return;
                }

                var backupDeployFolderNew = "";

                if (!backupDeployFolderData.ToLower().Contains(deployFolder.ToLower() + "@@"))
                {
                    if(backupDeployFolderData == "")
                    {
                        backupDeployFolderNew = deployFolder + "@@" + backupDeployFolder + "##";
                    }
                    else
                    {
                        backupDeployFolderNew = backupDeployFolderData + (backupDeployFolderData.EndsWith("##") ? "" : "##") + deployFolder + "@@" + backupDeployFolder;
                    }
                }
                else
                {
                    var backupDeployFolderDataArray = backupDeployFolderData.Split(new string[] { "##" }, StringSplitOptions.None);
                    
                    foreach (var item in backupDeployFolderDataArray)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            if (item.ToLower().StartsWith(deployFolder.ToLower() + "@@"))
                            {
                                backupDeployFolderNew += deployFolder + "@@" + backupDeployFolder + "##";
                            }
                            else
                            {
                                backupDeployFolderNew += item + "##";
                            }
                        }
                    }
                }

                DynamicData.SetDynamicData("BackupDeployFolder", backupDeployFolderNew);
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == DataGridView.Columns["IsChecked"].Index)
            {
                SetHeaderCheckBoxValue();
                HighlightsCheckedRow();
            }
        }

        private void ManageApplicationPoolsButton_Click(object sender, EventArgs e)
        {
            
            
        }

        private void HandleForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void HighlightsCheckedRow()
        {
            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                if (Convert.ToBoolean(row.Cells["IsChecked"].EditedFormattedValue))
                {
                    row.DefaultCellStyle.BackColor = Color.DarkGreen;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dataGridViewRowBackColor;
                }

            }
        }

        private void HandleForm_Load(object sender, EventArgs e)
        {
            if (ConfigData.ShowDarkTheme)
            {
                Unities.ShowDarkTheme(this, ref dataGridViewRowBackColor, headerCheckBox);
            }
            
            HighlightsCheckedRow();
        }

        private void DataGridView_Paint(object sender, PaintEventArgs e)
        {
            //Find the second column Location of Header Cell.
            Point headerCellLocation = this.DataGridView.GetColumnDisplayRectangle(1, true).Location;
            headerCheckBox.Location = new Point(headerCellLocation.X - 21, headerCellLocation.Y + 5);
        }
    }
}
