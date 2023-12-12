
namespace CrystalReportDeploy
{
    partial class HandleForm
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
            this.DeployButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.PublishFolderPathLabel = new System.Windows.Forms.Label();
            this.PublishProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeployProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackupDeployFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridView = new System.Windows.Forms.DataGridView();
            this.ProcessingLabel = new System.Windows.Forms.Label();
            this.DeployFolderD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BackupDeployFolderD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DeployButton
            // 
            this.DeployButton.Location = new System.Drawing.Point(446, 48);
            this.DeployButton.Name = "DeployButton";
            this.DeployButton.Size = new System.Drawing.Size(75, 23);
            this.DeployButton.TabIndex = 4;
            this.DeployButton.Text = "Deploy";
            this.DeployButton.UseVisualStyleBackColor = true;
            this.DeployButton.Click += new System.EventHandler(this.DeployButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Path of publish source folder:";
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(527, 48);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "Cancel";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // PublishFolderPathLabel
            // 
            this.PublishFolderPathLabel.AutoSize = true;
            this.PublishFolderPathLabel.BackColor = System.Drawing.SystemColors.Control;
            this.PublishFolderPathLabel.Location = new System.Drawing.Point(173, 48);
            this.PublishFolderPathLabel.MaximumSize = new System.Drawing.Size(250, 0);
            this.PublishFolderPathLabel.Name = "PublishFolderPathLabel";
            this.PublishFolderPathLabel.Size = new System.Drawing.Size(24, 12);
            this.PublishFolderPathLabel.TabIndex = 9;
            this.PublishFolderPathLabel.Text = "data";
            // 
            // PublishProject
            // 
            this.PublishProject.DataPropertyName = "PublishProject";
            this.PublishProject.HeaderText = "Publish Project";
            this.PublishProject.Name = "PublishProject";
            this.PublishProject.ReadOnly = true;
            this.PublishProject.Width = 200;
            // 
            // DeployProject
            // 
            this.DeployProject.DataPropertyName = "DeployProject";
            this.DeployProject.HeaderText = "Deploy Project";
            this.DeployProject.Name = "DeployProject";
            // 
            // BackupDeployFolder
            // 
            this.BackupDeployFolder.DataPropertyName = "BackupDeployFolder";
            this.BackupDeployFolder.HeaderText = "Backup Deploy Folder";
            this.BackupDeployFolder.Name = "BackupDeployFolder";
            this.BackupDeployFolder.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BackupDeployFolder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DeployFolder";
            this.dataGridViewTextBoxColumn1.HeaderText = "Deploy Folder";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PublishProject";
            this.dataGridViewTextBoxColumn2.HeaderText = "Publish Project";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DeployProject";
            this.dataGridViewTextBoxColumn3.HeaderText = "Deploy Project";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "v";
            this.dataGridViewTextBoxColumn4.HeaderText = "Backup Deploy Folder";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // DataGridView
            // 
            this.DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeployFolderD,
            this.BackupDeployFolderD});
            this.DataGridView.EnableHeadersVisualStyles = false;
            this.DataGridView.Location = new System.Drawing.Point(22, 113);
            this.DataGridView.Name = "DataGridView";
            this.DataGridView.RowHeadersVisible = false;
            this.DataGridView.RowTemplate.Height = 25;
            this.DataGridView.Size = new System.Drawing.Size(579, 388);
            this.DataGridView.TabIndex = 13;
            this.DataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            this.DataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.DataGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.DataGridView_Paint);
            // 
            // ProcessingLabel
            // 
            this.ProcessingLabel.AutoSize = true;
            this.ProcessingLabel.ForeColor = System.Drawing.Color.Red;
            this.ProcessingLabel.Location = new System.Drawing.Point(22, 89);
            this.ProcessingLabel.Name = "ProcessingLabel";
            this.ProcessingLabel.Size = new System.Drawing.Size(184, 12);
            this.ProcessingLabel.TabIndex = 14;
            this.ProcessingLabel.Text = "Processing, Please wait for process end";
            this.ProcessingLabel.Visible = false;
            // 
            // DeployFolderD
            // 
            this.DeployFolderD.DataPropertyName = "DeployFolder";
            this.DeployFolderD.HeaderText = "Deployment Folder";
            this.DeployFolderD.Name = "DeployFolderD";
            this.DeployFolderD.Width = 200;
            // 
            // BackupDeployFolderD
            // 
            this.BackupDeployFolderD.DataPropertyName = "BackupDeployFolder";
            this.BackupDeployFolderD.HeaderText = "Backup Deployment Folder";
            this.BackupDeployFolderD.Name = "BackupDeployFolderD";
            this.BackupDeployFolderD.Width = 200;
            // 
            // HandleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(626, 570);
            this.Controls.Add(this.ProcessingLabel);
            this.Controls.Add(this.DataGridView);
            this.Controls.Add(this.PublishFolderPathLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeployButton);
            this.Font = new System.Drawing.Font("PMingLiU", 9F);
            this.Name = "HandleForm";
            this.Text = "HandleForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
            this.Load += new System.EventHandler(this.HandleForm_Load);
            this.Shown += new System.EventHandler(this.HandleForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button DeployButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label PublishFolderPathLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublishProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeployProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn BackupDeployFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView DataGridView;
        private System.Windows.Forms.Label ProcessingLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeployFolderD;
        private System.Windows.Forms.DataGridViewTextBoxColumn BackupDeployFolderD;
    }
}

