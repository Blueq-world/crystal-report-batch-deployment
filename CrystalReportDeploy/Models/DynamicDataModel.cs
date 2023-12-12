namespace CrystalReportDeploy.Models
{
    public class DynamicDataModel
    {
        public string PublishFolderPath { get; set; }
        public string DeployFolder { get; set; }
        public string BackupDeployFolder { get; set; }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
