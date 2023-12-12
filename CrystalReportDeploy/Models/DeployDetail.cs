using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalReportDeploy.Models
{
    class DeployDetail
    {
        public string DeployFolder { get; set; }
        public string BackupDeployFolder { get; set; }
        public bool IsChecked { get; set; }
    }
}
