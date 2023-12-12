using CrystalDecisions.Enterprise;
using CrystalDecisions.Enterprise.Desktop;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace CrystalReportDeploy.Services
{
    public class CrystalReportService
    {
        public SessionMgr sessionMgr = new SessionMgr();
        public InfoStore infoStore;
        public EnterpriseSession enterpriseSession;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public CrystalReportService()
        {
            enterpriseSession = sessionMgr.Logon(ConfigData.CMSUserId, ConfigData.CMSPassword, ConfigData.CMSServer, "secEnterprise");
            EnterpriseService enterpriseService = enterpriseSession.GetService("InfoStore");
            infoStore = new InfoStore(enterpriseService);
        }

        ~CrystalReportService()
        {
            enterpriseSession.Logoff();
        }

        //Get Folder ID from Crystal Report Server
        public int GetFolderID(InfoStore infoStore, string folderName)
        {

            string Query = @"Select SI_ID FROM CI_INFOOBJECTS WHERE
            SI_KIND ='Folder' and SI_Name = '" + folderName + "'";

            InfoObjects infoObjects = infoStore.Query(Query);

            if (infoObjects.Count > 0)
            {
                InfoObject infoObject = infoObjects[1];
                int folderId = infoObject.ID;
                return folderId;
            }
            else
            {
                return -1;
            }

        }

        //Get Folder ID from Crystal Report Server under a Parent  folder
        public int GetFolderID(InfoStore infoStore, string folderName, int ParentFolderID)
        {
            string Query = @"Select SI_ID FROM CI_INFOOBJECTS WHERE
            SI_KIND = 'Folder' and SI_Name = '" + folderName + "' AND SI_PARENTID=" + ParentFolderID;

            InfoObjects infoObjects = infoStore.Query(Query);

            if (infoObjects.Count > 0)
            {

                InfoObject infoObject = infoObjects[1];

                int folderId = infoObject.ID;

                return folderId;

            }
            else
            {
                return -1;
            }
        }

        //Add a Repoort to the CMS–Crystal Report Server
        public void AddReport(InfoStore infostore, List<string> reportPaths, int folderId)
        {
            foreach (var reportPath in reportPaths)
            {
                InfoObjects infoObjects = infostore.NewInfoObjectCollection();
                var pluginManager = infostore.PluginManager;
                var pluginInfo = pluginManager.GetPluginInfo("CrystalEnterprise.Report");
                InfoObject infoObject = infoObjects.Add(pluginInfo);
                FileInfo fileinfo = new FileInfo(reportPath);
                var fileName = Path.GetFileNameWithoutExtension(fileinfo.Name);
                var report = new Report(infoObject.PluginInterface);
                report.Files.Add(reportPath);//error
                report.Properties.Add("SI_PARENTID", folderId);
                report.EnableThumbnail = false;
                report.NeedsLogon = true;
                report.KeepSavedData = true;
                report.Title = fileName;

                infostore.Commit(infoObjects);
            }
        }

        public void UpdateReportLogonInformation(InfoStore infostore, int folderId, string folderName, List<string> fileNames)
        {
            if (fileNames.Count > 0)
            {  
                InfoObjects infoObjects = GetCrystalReports(folderId, fileNames);
                if (infoObjects != null)
                {
                    foreach (InfoObject item in infoObjects)
                    {
                        var report1 = new Report(item.PluginInterface);
                        foreach (ReportLogon reportLogonsItem in report1.ReportLogons)
                        {
                            reportLogonsItem.CustomServerName = ConfigurationManager.AppSettings[folderName + "DBServer"];
                            reportLogonsItem.CustomDatabaseName = ConfigurationManager.AppSettings[folderName + "DBName"];
                            reportLogonsItem.CustomUserName = ConfigurationManager.AppSettings[folderName + "DBUserName"];
                            reportLogonsItem.CustomPassword = ConfigurationManager.AppSettings[folderName + "DBPassword"];
                            reportLogonsItem.UseOriginalDataSource = false;
                            reportLogonsItem.PromptOnDemandViewing = false;
                            reportLogonsItem.ReportLogonMode = CeReportLogonMode.ceLogonModeUseSpecifiedLogon;
                        }
                    }

                    if (infoObjects.Count > 0)
                    {
                        infostore.Commit(infoObjects);
                    }
                }
            }
        }

        public InfoObjects GetCrystalReports(int folderId, List<string> fileNames)
        {
            InfoObjects infoObjects = null;
            if (fileNames.Count > 0)
            {
                var fileNameWhere = "";
                foreach (var item in fileNames)
                {
                    if (fileNameWhere == "")
                    {
                        fileNameWhere += "'" + item + "'";
                    }
                    else
                    {
                        fileNameWhere += ",'" + item + "'";
                    }
                }
                string queryString = "select * from CI_INFOOBJECTS "
                                        + " where SI_PARENTID = " + folderId + " and (SI_KIND='CrystalReport' or SI_KIND = 'Webi') "
                                        + " and SI_NAME in(" + fileNameWhere + ") "
                                        + " order by SI_ID asc";
                infoObjects = infoStore.Query(queryString);
            }

            return infoObjects;
        }

        //Add a Folder to the Crystal Report Server using .net sdk
        public int AddFolder(InfoStore infoStore, int parentFolderID, string FolderName, string FolderDescription)

        {

            int folderID = -1;

            try

            {

                InfoObjects infoObjects = infoStore.NewInfoObjectCollection();

                PluginManager pluginManager = infoStore.PluginManager;

                PluginInfo pluginInfo = pluginManager.GetPluginInfo("CrystalEnterprise.Folder");

                InfoObject infoObject = infoObjects.Add(pluginInfo);

                Folder folder = new Folder(infoObject.PluginInterface);

                folder.Title = FolderName;

                folder.Description = FolderDescription;

                folder.Properties.Add("SI_PARENTID", parentFolderID);

                folderID = folder.ID;

                infoStore.Commit(infoObjects);

                return folderID;

            }

            catch (Exception ex)

            {

                if (ex.Message.ToString().Contains("Duplicate"))

                {

                    folderID = GetFolderID(infoStore, FolderName,

              parentFolderID);

                    return folderID;

                }

                else
                {

                    //Log.WriteFile("Error adding folder to Crystal ReportServer.Message:" + ex.Message, Log.LogFileType.ERROR);

                }

            }

            return folderID;

        }

        //Delete Folder in the Crystal Report Server using .net SDK
        public void DeleteFolder(InfoStore infoStore, int parentFolderID, string FolderName)
        {

            try

            {

                string Query = "Select SI_ID FROM CI_INFOOBJECTS WHERE SI_KIND ='Folder' and SI_Name = '" + FolderName + "' AND SI_PARENTID =" + parentFolderID;

                InfoObjects infoObjects = infoStore.Query(Query);

                if (infoObjects.Count > 0)

                {
                    InfoObject infoObject = infoObjects[1];

                    int folderId = infoObject.ID;

                    infoObject.DeleteNow();

                }

            }

            catch (Exception ex)

            {

                throw new ApplicationException("Error Deleting Product Folder:" + FolderName + " from Crystal Report Server.Message:" + ex.Message);

            }

        }

        //Move a Object from one folder to another folder in Crystal Report Server using .net SDK
        public void MoveObjects(InfoStore infoStore, int ObjectFolderID, List<string> ObjectFileNames, int MoveFolderID)
        {
            try
            {
                var crystalReports = GetCrystalReports(ObjectFolderID, ObjectFileNames);
                if (crystalReports != null)
                {
                    foreach (InfoObject item in crystalReports)
                    {
                        item.ParentID = MoveFolderID;
                    }

                    if (crystalReports.Count > 0)
                    {
                        infoStore.Commit(crystalReports);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error moving Folder in Crystal Report Server.Message:" + ex.Message);

            }
        }
    }
}

