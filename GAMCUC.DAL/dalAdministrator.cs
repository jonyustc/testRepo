using GAMCUC.DataModel;
using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
