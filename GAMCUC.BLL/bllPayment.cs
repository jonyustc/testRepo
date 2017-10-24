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
    public class bllPayment : IPayment
    {
        IDAL.IPayment _iPayment = null;

        public bllPayment()
        {
            _iPayment = new DAL.dalPayment();
        }

        public List<PaymentTypeViewModel> getAllPaymentType()
        {
            return _iPayment.getAllPaymentType();
        }
        public IList<PaymentMethodViewModel> GetPaymentMethodList()
        {
            return _iPayment.GetPaymentMethodList();
        }

        public IList<BankVM> GetBankList()
        {
            return _iPayment.GetBankList();
        }

        public Guid AddPayment(PaymentTypeName model, List<PaymentTypeViewModel> ptvm, int[] noMonth)
        {
            return _iPayment.AddPayment(model, ptvm,noMonth);
        }

        public PaymentViewModel invoiceInsertedData(Guid id)
        {
            return _iPayment.invoiceInsertedData(id);
        }

        public List<PaymentViewModel> PaymentList(string id)
        {
            return _iPayment.PaymentList(id);
        }

        public DataTable GetPaymentReport(Guid ID)
        {
            return _iPayment.GetPaymentReport(ID);
        }

        public DataTable GetStdPaymentList(int courseId,int semesterId)
        {
            return _iPayment.GetStdPaymentList(courseId, semesterId);
        }

        public DataTable GetStdPaymentDueList(int courseId, int semesterId)
        {
            return _iPayment.GetStdPaymentDueList(courseId, semesterId);
        }

        public IList<SemesterMonthVM> GetOddMonths()
        {
           return _iPayment.GetOddMonths();
        }

        public IList<SemesterMonthVM> GetEvenMonths()
        {
            return _iPayment.GetEvenMonths();
        }

        public List<PaymentViewModel> PaymentPrint()
        {
            return _iPayment.PaymentPrint();
        }
        public List<PaymentDetailsViewModel> PaymentListPrint()
        {
            return _iPayment.PaymentListPrint();
        }

        public void UpdateFeeSetup(List<PaymentTypeViewModel> ptvm)
        {
            _iPayment.UpdateFeeSetup(ptvm);
        }

        public void AddBank(BankVM model)
        {
            _iPayment.AddBank(model);
        }
    }
}
