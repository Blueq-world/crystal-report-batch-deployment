using CrystalReportDeploy.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace CrystalReportDeploy.Services
{
    static class DynamicData
    {
        public static void SetDynamicData(string key, string value)
        {
            var dynamicDataPath = Path.Combine(Application.StartupPath, "DynamicData.json");
            var jsonText = File.ReadAllText(dynamicDataPath);
            var dynamicDataModel = JsonConvert.DeserializeObject<DynamicDataModel>(jsonText);

            if(dynamicDataModel == null)
            {
                dynamicDataModel = new DynamicDataModel();
            }

            dynamicDataModel[key] = value;

            var UpdatejsonText = JsonConvert.SerializeObject(dynamicDataModel);
            File.WriteAllText(dynamicDataPath, UpdatejsonText);
        }

        public static string GetDynamicData(string key)
        {
            var result = "";
            var dynamicDataPath = Path.Combine(Application.StartupPath, "DynamicData.json");
            var jsonText = File.ReadAllText(dynamicDataPath);
            var dynamicDataModel = JsonConvert.DeserializeObject<DynamicDataModel>(jsonText);

            if(dynamicDataModel != null)
            {
                result = (string)dynamicDataModel[key];
            }

            return result;
        }
    }
}
