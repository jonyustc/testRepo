using GAMCUC.DataModel;
using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIT.DataLogicLayer;

namespace GAMCUC.DAL
{
    public class dalAdministrator : IAdministrator
    {
        private GaMCucDbEntities _context;

        public dalAdministrator()
        {
            _context = new GaMCucDbEntities();
        }

        public void Create(AdministratorVM model)
        {
            var adminProfile = new Administrator {
                Id=Guid.NewGuid(),
                FirstName=model.FirstName,
                LastName=model.LastName,
                ApplicationUserId=model.ApplicationUserId
            };

            _context.Administrators.Add(adminProfile);
            _context.SaveChanges();
        }
        #region Backup Database
      

        public string BackupDB()
        {
            string archiveName = String.Format("archive-{0}", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));

            try
            {
                SqlConnection con;
                SqlCommand cmd;
                string FileName = archiveName + ".bak";
                string filepath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) +
                                  ConfigurationSettings.AppSettings.Get("DatabaseBackup");
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                con = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
                cmd = new SqlCommand(@"backup database " + ConfigurationSettings.AppSettings.Get("DestinationDatabaseName") + " to disk='" + filepath + FileName + "'", con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                archiveName = archiveName + ".zip";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(String.Format("{0}{1}", filepath, FileName), "");
                    zip.Save(String.Format("{0}{1}", filepath, archiveName));
                }

               // File.Delete(String.Format("{0}{1}", filepath, FileName));

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //log.LogErrorWithException("Error",ex);
            }
            return archiveName;

        }
        public DataTable GetBackupHistory()
        {
            ArrayList altParams = new ArrayList();
            return DatabaseManager.GetInstance().ExecuteStoredProcedureDataTable("USP_BackupHistory_Get", altParams);
        }

        public int InsertBackupHistory(string BackupBy, string FileName)
        {
            ArrayList altParams = new ArrayList();
            altParams.Add(new SqlParameter("@BackupBy", BackupBy));
            altParams.Add(new SqlParameter("@FileName", FileName));

            return DatabaseManager.GetInstance().ExecuteNonQueryStoredProcedure("USP_BackupHistory_Insert", altParams);
        }

        public int DeleteBackupHistory(string filename)
        {
            ArrayList altParams = new ArrayList();
            altParams.Add(new SqlParameter("@FileName", filename));

            return DatabaseManager.GetInstance().ExecuteNonQueryStoredProcedure("USP_BackupHistory_Delete", altParams);
        }

        public DataTable GetLogHistory(string FromDate, string ToDate)
        {
            ArrayList altParams = new ArrayList();
            altParams.Add(new SqlParameter("@FromDate", FromDate));
            altParams.Add(new SqlParameter("@ToDate", ToDate));

            return DatabaseManager.GetInstance().ExecuteStoredProcedureDataTable("USP_LogHistory_GetAll", altParams);

        }






        #endregion
    }
}
