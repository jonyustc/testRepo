using GAMCUC.DataModel;
using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIT.DataLogicLayer;

namespace GAMCUC.DAL
{
    public class dalPayment : IPayment
    {
        private GaMCucDbEntities _context;

        public dalPayment()
        {
            _context = new GaMCucDbEntities();
        }

        public DataTable GetPaymentReport(Guid ID)
        {

            ArrayList altParams = new ArrayList();
            altParams.Add(new SqlParameter("@StudentId", ID));
            return DatabaseManager.GetInstance().ExecuteStoredProcedureDataTable("GetPaymentDetails", altParams);
        }

        public DataTable GetStdPaymentList(int courseId, int semesterId)
        {
            ArrayList altParams = new ArrayList();
            altParams.Add(new SqlParameter("@courseId", courseId));
            altParams.Add(new SqlParameter("@semesterId", semesterId));
            return DatabaseManager.GetInstance().ExecuteStoredProcedureDataTable("GetStudentPaymentList", altParams);
        }

        public List<PaymentViewModel> PaymentList(string id)
        {
            var salesList = from p in _context.Payments.ToList()
                            join v in _context.Students.ToList() on p.StudentId equals v.Id
                            where p.StudentId.Equals(new Guid(id))
                            select new PaymentViewModel
                            {
                                Id = p.Id,
                                AutoId = p.AutoId,
                                StudentId = v.Id,
                                StudentName = v.StdNameEnglish,
                                InvoiceNo = p.InvoiceNo,
                                PaymentDate = p.PaymentDate ?? DateTime.Now,
                                GrandTotal = p.GrandTotal ?? 0,
                                PaidAmount = p.PaidAmount ?? 0,
                                Discount = p.Discount ?? 0,
                                Due = p.Due ?? 0
                            };

            return salesList.ToList();
        }

        public Guid AddPayment(PaymentTypeName model, List<PaymentTypeViewModel> ptvm, int[] noMonth)
        {
            Payment payment = new Payment();
            payment.Id = Guid.NewGuid();
            payment.InvoiceNo = RandomString(5);
            payment.StudentId = model.StudentId;
            payment.GrandTotal = model.TotalSemesterFees;
            payment.Discount = model.Discount;
            payment.PaidAmount = model.PaidAmount;
            payment.Due = model.Due;
            payment.PaymentDate = model.PaymentDate;
            payment.PaymentMethodId = model.PaymentMethodId;
            if (model.PaymentMethodId == 2)
            {
                payment.BankId = model.BankId;
                payment.CheckNo = model.CheckNo;
                payment.CheckDate = model.CheckDate;
            }
            else
            {
                payment.BankId = 0;
                payment.CheckNo = null;
                payment.CheckDate = null;
            }

            _context.Payments.Add(payment);

            var stdSemester = from std in _context.Students.ToList()
                              join smt in _context.Semesters.ToList() on std.SemesterId equals smt.Id
                              where std.Id == model.StudentId
                              select new { SemesterId = std.SemesterId };

            foreach (var item in ptvm)
            {
                if ((item.Amount > 0 && item.IsChecked == true))
                {
                    if (item.Id == 3)
                    {
                        foreach (var pMonth in noMonth)
                        {
                            PaymentDetail pd = new PaymentDetail();
                            pd.PaymentTypeID = item.Id;
                            pd.PayAmount = item.Amount;
                            pd.PaymentsId = payment.Id;
                            pd.PayMonth = pMonth;
                            _context.PaymentDetails.Add(pd);
                        }
                    }
                    else
                    {
                        PaymentDetail pd = new PaymentDetail();
                        pd.PaymentTypeID = item.Id;
                        pd.PayAmount = item.Amount;
                        pd.PaymentsId = payment.Id;
                        _context.PaymentDetails.Add(pd);
                    }

                }

            }

            _context.SaveChanges();

            return payment.Id;


        }

        public void UpdateFeeSetup(List<PaymentTypeViewModel> ptvm)
        {
            foreach (var item in ptvm)
            {
                if ((item.Amount > 0 && item.IsChecked == true))
                {

                    var pd = _context.PaymentTypes.Where(x => x.Id == item.Id).FirstOrDefault();
                    pd.Amount = item.Amount;
                    _context.Entry(pd).Property(x => x.Amount).IsModified = true;


                }

            }

            _context.SaveChanges();

        }

        public PaymentViewModel invoiceInsertedData(Guid id)
        {
            List<PaymentViewModel> list = new List<PaymentViewModel>();
            var query = from s in _context.Payments.ToList()
                        join sd in _context.PaymentDetails.ToList() on s.Id equals sd.PaymentsId
                        join stdInfo in _context.Students.ToList() on s.StudentId equals stdInfo.Id
                        join crs in _context.Courses.ToList() on stdInfo.CourseId equals crs.Id
                        join smtr in _context.Semesters.ToList() on stdInfo.SemesterId equals smtr.Id
                        join sn in _context.AcademicSessions.ToList() on stdInfo.AcademicSessionId equals sn.Id
                        where s.Id == id
                        select new PaymentViewModel
                        {
                            InvoiceNo = s.InvoiceNo,
                            StudentName = s.Student.StdNameEnglish,
                            PaymentDate = s.PaymentDate ?? DateTime.Now,
                            Id = s.Id,
                            StdID = stdInfo.StdID,
                            CourseName = crs.CourseName,
                            SemesterName = smtr.SemesterName,
                            Session = sn.Session,
                            GrandTotal = s.GrandTotal ?? 0,
                            PaidAmount = s.PaidAmount ?? 0,
                            Due = s.Due ?? 0,
                            PaymentMethod = s.PaymentMethod.MethodName,
                            PaymentDetailsViewModel = (from ss in _context.PaymentDetails
                                                       join p in _context.PaymentTypes on ss.PaymentTypeID equals p.Id
                                                       join pM in _context.Months on ss.PayMonth equals pM.Id into gMp
                                                       from pM in gMp.DefaultIfEmpty()
                                                       where ss.PaymentsId == id
                                                       select new PaymentDetailsViewModel()
                                                       {
                                                           PaymentTypeName = p.PaymentType1,
                                                           Id = ss.Id,
                                                           PayAmount = ss.PayAmount ?? 0,
                                                           PayMonth = p.Id.Equals(3) ? pM.MonthName : ""
                                                       }).ToList()
                        };

            list = query.ToList();
            return query.FirstOrDefault();
        }

        public List<PaymentViewModel> PaymentPrint()
        {
            var payList = (from s in _context.Payments.ToList()
                           join sd in _context.PaymentDetails.ToList() on s.Id equals sd.PaymentsId
                           select new PaymentViewModel
                           {
                               InvoiceNo = s.InvoiceNo,
                               StudentName = s.Student.StdNameEnglish,
                               PaymentDate = s.PaymentDate ?? DateTime.Now,
                               Id = s.Id,
                               GrandTotal = s.GrandTotal ?? 0,
                               PaidAmount = s.PaidAmount ?? 0,
                               Due = s.Due ?? 0,
                               PaymentMethod = s.PaymentMethod.MethodName
                           }).ToList();


            return payList.ToList();
        }

        public List<PaymentDetailsViewModel> PaymentListPrint()
        {
            var pList = (from ss in _context.PaymentDetails
                         join p in _context.PaymentTypes on ss.PaymentTypeID equals p.Id
                         join pM in _context.Months on ss.PayMonth equals pM.Id into gMp
                         from pM in gMp.DefaultIfEmpty()
                         select new PaymentDetailsViewModel()
                         {
                             PaymentTypeName = p.PaymentType1,
                             Id = ss.Id,
                             studentId = ss.PaymentsId ?? Guid.Empty,
                             PayAmount = ss.PayAmount ?? 0,
                             PayMonth = p.Id.Equals(3) ? pM.MonthName : ""
                         }).ToList();


            return pList.ToList();
        }

        public List<PaymentTypeViewModel> getAllPaymentType()
        {
            var list = from p in _context.PaymentTypes.ToList()
                       select new PaymentTypeViewModel
                       {
                           Id = p.Id,
                           PaymentType = p.PaymentType1,
                           Amount = p.Amount ?? 0
                       };

            return list.ToList();
        }

        public IList<PaymentMethodViewModel> GetPaymentMethodList()
        {

            List<PaymentMethodViewModel> List = new List<PaymentMethodViewModel>();
            var pMList = _context.PaymentMethods.ToList();
            foreach (var item in pMList)
            {

                List.Add(new PaymentMethodViewModel()
                {
                    Id = item.Id,
                    MethodName = item.MethodName

                });
            }
            return List;
        }

        public IList<SemesterMonthVM> GetOddMonths()
        {
            List<SemesterMonthVM> List = new List<SemesterMonthVM>();
            var oMList = _context.Months.ToList().Where(x => x.Id <= 6);
            foreach (var item in oMList)
            {

                List.Add(new SemesterMonthVM()
                {
                    Id = item.Id,
                    MonthName = item.MonthName

                });
            }
            return List;
        }

        public IList<SemesterMonthVM> GetEvenMonths()
        {
            List<SemesterMonthVM> List = new List<SemesterMonthVM>();
            var eMList = _context.Months.ToList().Where(x => x.Id >= 7);
            foreach (var item in eMList)
            {

                List.Add(new SemesterMonthVM()
                {
                    Id = item.Id,
                    MonthName = item.MonthName

                });
            }
            return List;
        }

        public IList<BankVM> GetBankList()
        {

            List<BankVM> List = new List<BankVM>();
            var BnkList = _context.Banks.ToList();
            foreach (var item in BnkList)
            {

                List.Add(new BankVM()
                {
                    Id = item.Id,
                    BankName = item.BankName

                });
            }
            return List;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789MMTRADING";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
