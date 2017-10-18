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

        public ActionResult BankList()
        {
            var list = _iPayment.GetBankList();
            return View(list);
        }

        public ActionResult AddNewBank()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewBank(BankVM model)
        {
            _iPayment.AddBank(model);
            return RedirectToAction("BankList");
        }

        public ActionResult BankEdit(int id)
        {
            var bank = from s in _iPayment.GetBankList().ToList()
                       where s.Id == id
                       select new BankVM 
                       {
                         BankName=s.BankName,
                         Branch=s.Branch
                       };
            return View(bank.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult BankEdit(BankVM model)
        {
            _iPayment.AddBank(model);
            return RedirectToAction("BankList");
        }
	}
}