using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.IDAL
{
    public interface IPayment
    {
        List<PaymentTypeViewModel> getAllPaymentType();
        IList<PaymentMethodViewModel> GetPaymentMethodList();
        IList<BankVM> GetBankList();
        Guid AddPayment(PaymentTypeName model, List<PaymentTypeViewModel> ptvm, int[] noMonth);
        PaymentViewModel invoiceInsertedData(Guid id);
        List<PaymentViewModel> PaymentList(string id);
        DataTable GetPaymentReport(Guid ID);
        DataTable GetStdPaymentList(int courseId, int semesterId);
        IList<SemesterMonthVM> GetOddMonths();
        IList<SemesterMonthVM> GetEvenMonths();
        List<PaymentViewModel> PaymentPrint();
        List<PaymentDetailsViewModel> PaymentListPrint();
        void UpdateFeeSetup(List<PaymentTypeViewModel> ptvm);
        void AddBank(BankVM model);
    }
}
