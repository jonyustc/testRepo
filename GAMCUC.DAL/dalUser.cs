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
    public class dalUser : IUser
    {
        private GaMCucDbEntities _context;

        public dalUser()
        {
            _context = new GaMCucDbEntities();
        }

        public List<User> getUsers()
        {
            var userlist = from s in _context.Administrators.ToList()
                           join u in _context.AspNetUsers.ToList() on s.ApplicationUserId equals u.Id
                           join r in _context.AspNetUserRoles.ToList() on u.Id equals r.UserId
                           join ru in _context.AspNetRoles.ToList() on r.RoleId equals ru.Id
                           where ru.Name != "SuperAdmin"
                           select new User { Name = s.FirstName + " " +  s.LastName, username = u.UserName,role=ru.Name };
            return userlist.ToList();
        }
    }
}
