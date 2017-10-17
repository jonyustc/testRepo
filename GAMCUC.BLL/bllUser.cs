using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.BLL
{
    public class bllUser : IUser
    {
        IDAL.IUser _iUser = null;

        public bllUser()
        {
            _iUser = new DAL.dalUser();
        }

        public List<User> getUsers()
        {
            return _iUser.getUsers();
        }
    }
}
