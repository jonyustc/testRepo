using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAMCUC.Web.Areas.Administration.Controllers
{
    public class UserController : Controller
    {
        IDAL.IUser _iUser = null;

        public UserController()
        {
            _iUser = new BLL.bllUser();
        }

        //
        // GET: /Administration/User/
        public ActionResult UserList()
        {
            var users = _iUser.getUsers().ToList();
            return View(users);
        }
	}
}