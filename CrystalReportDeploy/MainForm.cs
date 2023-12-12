using CrystalReportDeploy.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrystalReportDeploy
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = ConfigData.FormFullTitle;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            PublishFolderTextBox.Text = DynamicData.GetDynamicData("PublishFolderPath");
            DeployFolderTextBox.Text = DynamicData.GetDynamicData("DeployFolder"); ;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            using (diag)
            {
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    PublishFolderTextBox.Text = diag.SelectedPath;
                }
            }
        }

        private void OutputBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            using (diag)
            {
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    DeployFolderTextBox.Text = diag.SelectedPath;
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(PublishFolderTextBox.Text))
                {
                    MessageBox.Show("The publish folder does not exist.", "Error", MessageBoxButtons.OK);
                    return;
                }

                if(String.IsNullOrEmpty(DeployFolderTextBox.Text))
                {
                    MessageBox.Show("Please input the deployment folder of the crystal service.", "Error", MessageBoxButtons.OK);
                    return;
                }

                var handleForm = new HandleForm(PublishFolderTextBox.Text, DeployFolderTextBox.Text);
                this.Hide();
                handleForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                return;
            }
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

        private void PublishFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            DynamicData.SetDynamicData("PublishFolderPath", PublishFolderTextBox.Text);
        }

        private void DeployFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            DynamicData.SetDynamicData("DeployFolder", DeployFolderTextBox.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (ConfigData.ShowDarkTheme)
            {
                Unities.ShowDarkTheme(this);
            }
        }
    }
}
