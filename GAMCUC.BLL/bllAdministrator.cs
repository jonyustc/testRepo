using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
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
    }
}
