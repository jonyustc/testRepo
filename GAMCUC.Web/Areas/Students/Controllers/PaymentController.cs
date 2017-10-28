using CrystalDecisions.CrystalReports.Engine;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAMCUC.Web.Areas.Students.Controllers
{
     [Authorize(Roles = "SuperAdmin,Admin,SubAdmin")]
    public class PaymentController : Controller
    {
        IDAL.IStudent _iStudent = null;
        IDAL.IPayment _iPayment = null;

        public PaymentController()
        {
            _iStudent = new BLL.bllStudent();
            _iPayment = new BLL.bllPayment();
        }
        //
        // GET: /Students/Payment/
        public ActionResult StudentPayment()
        {
            ViewBag.CourseList = new SelectList(_iStudent.GetCourseList(), "Id", "CourseName");
            ViewBag.Semester = new SelectList(_iStudent.GetSemesterList(), "Id", "SemesterName");
            ViewBag.Session = new SelectList(_iStudent.GetAcSessionList(), "Id", "Session");
            return View();
        }

        [HttpPost]
        public ActionResult SearchStudentPaymentList(string stdID, int? courseId, int? semesterId, string roll,Guid session)
        {
            var list = _iStudent.SearchStudent(stdID, courseId, semesterId, roll,session);
            return View(list);
        }


        public ActionResult StudentPaymentReport()
        {
            ViewBag.CourseList = new SelectList(_iStudent.GetCourseList(), "Id", "CourseName");
            ViewBag.Semester = new SelectList(_iStudent.GetSemesterList(), "Id", "SemesterName");
            ViewBag.Session = new SelectList(_iStudent.GetAcSessionList(), "Id", "Session");
            return View();
        }

        public ActionResult StudentPaymentReportList(int courseId, int semesterId,Guid session)
        {
            DataTable list = _iPayment.GetStdPaymentList(courseId, semesterId,session);
            ViewBag.cId = courseId;
            ViewBag.sId = semesterId;
            ViewBag.session = session;
            return View(list);
        }

        public ActionResult StudentDuePrint(int courseId, int semesterId,Guid session)
        {
            DataTable dueRecords = _iPayment.GetStdPaymentList(courseId, semesterId,session);

            string stdCourse = _iStudent.GetCourseList().Where(x => x.Id.Equals(courseId)).Select(x => x.CourseName).SingleOrDefault();
            string stdSemester = _iStudent.GetSemesterList().Where(x => x.Id.Equals(semesterId)).Select(x => x.SemesterName).SingleOrDefault();
            string stdSession = _iStudent.GetAcSessionList().Where(x => x.Id.Equals(session)).Select(x => x.Session).SingleOrDefault();
            int stdSemesterId = _iStudent.GetSemesterList().Where(x => x.Id.Equals(semesterId)).Select(x => x.Id).SingleOrDefault();

            string stdSessionYear = "";
            string stdSessionMonth = "";
            if(stdSemesterId % 2 == 0)
            {
                stdSessionYear = "July to December"+DateTime.Now.Year;
                stdSessionMonth = "July to Dec";
            }
            else
            {
                stdSessionYear = "January to June"+DateTime.Now.Year;
                stdSessionMonth = "Jan to June";
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Rpt_DueReports.rpt"));
            rd.SetDataSource(dueRecords);
            rd.SetParameterValue("stdCourse", stdCourse.ToString());
            rd.SetParameterValue("stdSemester", stdSemester.ToString());
            rd.SetParameterValue("stdSession", stdSession.ToString());
            rd.SetParameterValue("stdSessionYear", stdSessionYear.ToString());
            rd.SetParameterValue("stdSessionMonth", stdSessionMonth.ToString());
            rd.SummaryInfo.ReportTitle = "Student Due Reports";
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult StudentPaymentCollection(string id = null)
        {
            List<PaymentTypeViewModel> pTypeList = new List<PaymentTypeViewModel>();

            pTypeList = _iPayment.getAllPaymentType().ToList();

            var s = new PaymentTypeName();

            s.TotalSemesterFees = pTypeList.Where(x => x.Id == 9).Select(x => x.Amount).FirstOrDefault();
           

            ViewBag.StudentId = id;
            ViewBag.PaymentMethodList = new SelectList(_iPayment.GetPaymentMethodList(), "Id", "MethodName");
            ViewBag.BankList = new SelectList(_iPayment.GetBankList(), "Id", "BankName");

            var res = _iPayment.PaymentList(id);
            //var due = res.Select(x => x.Due).Max();
            var due = _iPayment.PaymentList(id)
                             .OrderByDescending(x => x.AutoId)
                             .Take(1)
                             .Select(x => x.Due)
                             .ToList()
                             .FirstOrDefault();


            DataView dv = _iPayment.GetPaymentReport(new Guid(id)).DefaultView;

            if(dv.RowFilter!="")
            {
                dv.RowFilter = "Months='Total'";
                
            }
            else
            {
                dv.RowFilter = "";
            }

            DataTable data = dv.ToTable();
            foreach (System.Data.DataRow row in data.Rows)
            {
                ViewBag.TotalSemesterFees = row[10];
            }


            #region due



            var stdSemsterInfo = from std in _iStudent.SearchStudent(id, null, null, null,Guid.Empty)
                                 select new { SemesterId = std.SemesterId };

            foreach (var item in stdSemsterInfo)
            {
                

                if (item.SemesterId % 2 == 0)
                {
                    ViewBag.SemesterId = item.SemesterId;
                    ViewBag.SemesterList = new SelectList(_iPayment.GetEvenMonths().ToList(), "Id", "MonthName");

                }
                else
                {
                    ViewBag.SemesterId = item.SemesterId;
                    ViewBag.SemesterList = new SelectList(_iPayment.GetOddMonths().ToList(), "Id", "MonthName");

                }
            }
            #endregion due

            return View(pTypeList);
        }

        [HttpPost]
        public ActionResult StudentPaymentCollection(PaymentTypeName model,int[] noMonth, List<PaymentTypeViewModel> pType)
        {
            
            var result = _iPayment.AddPayment(model, pType, noMonth);

            return RedirectToAction("InvoiceView", new { id = result });

        }

        public ActionResult InvoiceView(Guid id)
        {
            ViewBag.User = User.Identity.Name;
            var data = _iPayment.invoiceInsertedData(id);
            ViewBag.words = NumberToWords(Convert.ToInt32(data.PaidAmount));
            return View(data);
        }

        public ActionResult StdPaymentDetailsShow(string id = null)
        {
            
            TempData["std_id"] = id;
            TempData["stdid"] = id;
            return View();
        }

        public ActionResult StudentDetailsForPayment(Guid StdId)
        {
            var stdIno = _iStudent.StudentDetails(StdId);

            return View(stdIno);
        }

        public ActionResult StdViewPayment(string stdId)
        {
            ViewBag.stdId = stdId;
            TempData["stdid"] = stdId;
            var pay = _iPayment.PaymentList(stdId);
            return View(pay);
        }


        public ActionResult PaymentReportsShow(Guid id)
        {
            ViewBag.User = User.Identity.Name;
            TempData["stdid"] = id;
            return View();
        }

        public ActionResult PaymentReports(Guid id)
        {
            DataTable data = _iPayment.GetPaymentReport(id);
         
            ViewBag.userdetails = data; 
            return View(data);
        }

        public ActionResult InvoiceTotal(Guid id)
        {
            DataView dv = _iPayment.GetPaymentReport(id).DefaultView;
            dv.RowFilter = "Months='Total'"; 
            DataTable data = dv.ToTable();
          //  data.Rows.Cast<System.Data.DataRow>().Take(1);
            ViewBag.userdetails = data;
            return View(data);
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}