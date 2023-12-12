using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalReportDeploy.Services
{
    static class ConfigData
    {
        public static string FormTitle = ConfigurationManager.AppSettings["formTitle"];
        public static string VersionDate = ConfigurationManager.AppSettings["versionDate"];
        public static int MaxSelectRecords = int.Parse(ConfigurationManager.AppSettings["maxSelectRecords"]);
        public static bool ShowDarkTheme = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowDarkTheme"]);
        public static string CMSServer = ConfigurationManager.AppSettings["CMSServer"];
        public static string CMSUserId = ConfigurationManager.AppSettings["CMSUserId"];
        public static string CMSPassword = ConfigurationManager.AppSettings["CMSPassword"];

        public static string FormFullTitle = FormTitle + " (Ver. Date " + VersionDate + ")";
    }
}
