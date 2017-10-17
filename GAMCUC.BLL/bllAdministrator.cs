using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.BLL
{
    public class bllAdministrator : IAdministrator
    {
        IDAL.IAdministrator iAdmin = null;

        public bllAdministrator()
        {
            iAdmin = new DAL.dalAdministrator();
        }

        public void Create(AdministratorVM model)
        {
            iAdmin.Create(model);
        }
        #region Backup Database

        public DataTable GetBackupHistory()
        {
            return iAdmin.GetBackupHistory();
        }

        public int InsertBackupHistory(string BackupBy, string FileName)
        {
            return iAdmin.InsertBackupHistory(BackupBy, FileName);
        }

        public int DeleteBackupHistory(string filename)
        {
            return iAdmin.DeleteBackupHistory(filename);
        }

        public DataTable GetLogHistory(string FromDate, string ToDate)
        {
            return iAdmin.GetLogHistory(FromDate, ToDate);
        }

        public string BackupDB()
        { return iAdmin.BackupDB(); }
        #endregion
    }
}
