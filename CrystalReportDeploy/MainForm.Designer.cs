namespace CrystalReportDeploy
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.PublishFolderTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DeployFolderTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path of publish source folder";
            // 
            // PublishFolderTextBox
            // 
            this.PublishFolderTextBox.Location = new System.Drawing.Point(23, 61);
            this.PublishFolderTextBox.Name = "PublishFolderTextBox";
            this.PublishFolderTextBox.Size = new System.Drawing.Size(468, 22);
            this.PublishFolderTextBox.TabIndex = 1;
            this.PublishFolderTextBox.TextChanged += new System.EventHandler(this.PublishFolderTextBox_TextChanged);
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(522, 61);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(441, 241);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(527, 241);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Deployment folder of the Crystal Service";
            // 
            // DeployFolderTextBox
            // 
            this.DeployFolderTextBox.Location = new System.Drawing.Point(21, 111);
            this.DeployFolderTextBox.Name = "DeployFolderTextBox";
            this.DeployFolderTextBox.Size = new System.Drawing.Size(470, 94);
            this.DeployFolderTextBox.TabIndex = 9;
            this.DeployFolderTextBox.Text = "";
            this.DeployFolderTextBox.TextChanged += new System.EventHandler(this.DeployFolderTextBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 324);
            this.Controls.Add(this.DeployFolderTextBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.PublishFolderTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("PMingLiU", 9F);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox PublishFolderTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox DeployFolderTextBox;
    }
}



