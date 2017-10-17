using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAMCUC.Web.Areas.Students.Controllers
{
    public class SettingController : Controller
    {
        IDAL.IPayment _iPayment = null;
        IDAL.IStudent _iStudent = null;

        public SettingController()
        {
            _iPayment = new BLL.bllPayment();
            _iStudent = new BLL.bllStudent();
        }

        //
        // GET: /Students/Setting/
       
        public ActionResult FeeSetup()
        {
            List<PaymentTypeViewModel> pTypeList = new List<PaymentTypeViewModel>();

            pTypeList = _iPayment.getAllPaymentType().ToList();

            return View(pTypeList);
        }

        [HttpPost]
        public ActionResult FeeSetup(List<PaymentTypeViewModel> pType)
        {
            _iPayment.UpdateFeeSetup(pType);

            List<PaymentTypeViewModel> pTypeList = new List<PaymentTypeViewModel>();

            pTypeList = _iPayment.getAllPaymentType().ToList();

            return View("FeeSetup", pTypeList);
            
        }
	}
}