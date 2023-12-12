using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CrystalReportDeploy.Services
{
    static class Unities
    {
        public static void SetAppSetting(string key, string value)
        {
            Configuration configuration =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, false);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        public static void ShowDarkTheme(Form form, ref Color dataGridViewRowBackColor, CheckBox headerCheckBox = null)
        {
            ShowDarkTheme(form, headerCheckBox);
            dataGridViewRowBackColor = Color.FromArgb(60, 60, 60);
        }

        public static void ShowDarkTheme(Form form, CheckBox headerCheckBox = null)
        {
            form.BackColor = Color.FromArgb(37, 37, 38);
            form.ForeColor = Color.FromArgb(159, 159, 159);

            var labelControls = form.Controls.OfType<Label>().ToList();
            foreach (var item in labelControls)
            {
                item.BackColor = Color.FromArgb(37, 37, 38);
            }

            var buttonControls = form.Controls.OfType<Button>().ToList();
            foreach (var item in buttonControls)
            {
                item.BackColor = Color.FromArgb(60, 60, 60);
                item.FlatStyle = FlatStyle.Flat;
                item.FlatAppearance.BorderSize = 0;
            }

            var textBoxControls = form.Controls.OfType<TextBox>().ToList();
            foreach (var item in textBoxControls)
            {
                item.BackColor = Color.FromArgb(60, 60, 60);
                item.ForeColor = Color.FromArgb(159, 159, 159);
                item.Multiline = true;
                item.BorderStyle = BorderStyle.None;
                item.Height = 22;
            }

            var richTextBoxControls = form.Controls.OfType<RichTextBox>().ToList();
            foreach (var item in richTextBoxControls)
            {
                item.BackColor = Color.FromArgb(60, 60, 60);
                item.ForeColor = Color.FromArgb(159, 159, 159);
                item.BorderStyle = BorderStyle.None;
            }

            var dataGridViewControls = form.Controls.OfType<DataGridView>().ToList();
            foreach (var item in dataGridViewControls)
            {
                item.BackgroundColor = Color.FromArgb(37, 37, 38);
                item.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
                item.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(159, 159, 159);
                item.EnableHeadersVisualStyles = false;

                //item.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
                //item.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(159, 159, 159);

                item.RowsDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
                item.RowsDefaultCellStyle.ForeColor = Color.FromArgb(159, 159, 159);

                item.GridColor = Color.FromArgb(159, 159, 159);
            }

            var checkBoxControls = form.Controls.OfType<CheckBox>().ToList();
            foreach (var item in checkBoxControls)
            {
                item.BackColor = Color.FromArgb(60, 60, 60);
            }

            if (headerCheckBox != null)
            {
                headerCheckBox.BackColor = Color.FromArgb(60, 60, 60);
            }
        }
    }
}
