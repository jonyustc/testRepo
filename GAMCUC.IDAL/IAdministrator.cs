using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.IDAL
{
    public interface IAdministrator
    {
        void Create(AdministratorVM model);
        DataTable GetBackupHistory();
        int InsertBackupHistory(string BackupBy, string FileName);
        int DeleteBackupHistory(string filename);
        DataTable GetLogHistory(string FromDate, string ToDate);
        string BackupDB();
    }
}
