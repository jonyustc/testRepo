using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.IDAL
{
    public interface IUser
    {
        List<User> getUsers();
    }
}
