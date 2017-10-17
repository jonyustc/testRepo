using CrystalDecisions.CrystalReports.Engine;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAMCUC.Web.Areas.Students.Controllers
{
     [Authorize(Roles = "SuperAdmin,Admin")]
    public class StudentController : Controller
    {
        IDAL.IStudent _iStudent = null;

        public StudentController()
        {
            _iStudent = new BLL.bllStudent();
        }
        //
        // GET: /Students/Student/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StudentList()
        {
            
            var list = _iStudent.All();
            TempData["isActive"] = list.Select(x => x.IsActive).FirstOrDefault();
            return View(list);
        }

        public ActionResult StudentDetails(Guid id)
        {
            var student = _iStudent.StudentDetails(id);
            return View(student);
        }

        public ActionResult StudentFullProfilePrint(Guid id)
        {

            var student = from s in _iStudent.StudentDetailsPrint().ToList()
                          where s.Id==id
                          select new {
                                  Id=s.Id,
                                  StdNameBangla = s.StdNameBangla,
                                  StdNameEnglish = s.StdNameEnglish,
                                  StdFatherNameEnglish=s.StdFatherNameEnglish,
                                  StdFatherNameBangla=s.StdFatherNameBangla,
                                  StdMotherNameBangla=s.StdMotherNameBangla,
                                  StdMotherNameEnglish=s.StdMotherNameEnglish,
                                  PermanentAddress = s.PermanentAddress,
                                  PresentAddress = s.PresentAddress,
                                  Picture = s.Picture,
                                  DateOfBirth = s.DateOfBirth,
                                  Phone = s.Phone,
                                  Email = s.Email,
                                  Religion = s.Religion,
                                  Nationality = s.Nationality,
                                  FormNo = s.FormNo,
                                  IsSuspended = s.IsSuspended,
                                  SuspendedDesc = s.SuspendedDesc,
                                  CourseName = s.CourseName,
                                  AcademicSession = s.AcademicSession
                          };

            var academicRecords = from s in _iStudent.AcademicRecordsPrint().ToList()
                          where s.StudentId == id
                          select new
                          {
                              ExamName = s.ExamName,
                              GroupName = s.GroupName,
                              BoardName = s.BoardName,
                              YearOfPassing = s.YearOfPassing,
                              RollOfExam = s.RollOfExam,
                              Grade = s.Grade
                          };

            var guardianInfo = from ac in _iStudent.GuardianInfoPrint().ToList()
                               where ac.StudentId == id
                                  select new
                                  {
                                      Name = ac.Name,
                                      Occupation = ac.Occupation,
                                      Relation = ac.Relation,
                                      Address = ac.Address,
                                      Phone = ac.Phone
                                  };

            var initialPayment = (from ac in _iStudent.InitialPaymentPrint().ToList()
                                  where ac.StudentId == id
                                  select new 
                                  {
                                      CourseName = ac.CourseName,
                                      CourseFee = ac.CourseFee,
                                      StdID = ac.StdID,
                                      RegistrationNo = ac.RegistrationNo,
                                      ExamRollNo = ac.ExamRollNo
                                  }).ToList();

            var orgDoc = (from ac in _iStudent.OrgDocPrint().ToList()
                                  where ac.StudentId == id
                                  select new
                                  {
                                      Id=ac.Id,
                                      DocumentName = ac.DocumentName
                                  }).ToList();

            string stdimage = _iStudent.StudentDetailsPrint().Where(x => x.Id.Equals(id)).Select(x => x.Picture).SingleOrDefault();
            var stdimageurl = Server.MapPath("~/uploads/" + stdimage);

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "AdmissionForm.rpt"));
            rd.SetDataSource(student);
            rd.Subreports[0].SetDataSource(academicRecords);
            rd.Subreports[1].SetDataSource(guardianInfo);
            rd.Subreports[2].SetDataSource(initialPayment);
            rd.Subreports[3].SetDataSource(orgDoc);
            rd.SetParameterValue("stdimageurl", stdimageurl.ToString());
            rd.SummaryInfo.ReportTitle = "Student Profile";
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

        public ActionResult StudentIdCardPrint(Guid id)
        {
            var student = from s in _iStudent.StudentDetailsPrint().ToList()
                          where s.Id == id
                          select new
                          {
                              StdNameEnglish = s.StdNameEnglish,
                              DateOfBirth = s.DateOfBirth.ToShortDateString(),
                              Phone = s.Phone,
                              RegistrationNo=s.RegistrationNo,
                              StdID=s.StdID
                          };


            string stdimage = _iStudent.StudentDetailsPrint().Where(x => x.Id.Equals(id)).Select(x => x.Picture).SingleOrDefault();
            var stdimageurl = Server.MapPath("~/uploads/" + stdimage);

            ReportDocument rd = new ReportDocument();

            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "rptStudentIdCard.rpt"));
            rd.SetDataSource(student);
            rd.SetParameterValue("stdimageurl", stdimageurl.ToString());
            rd.SummaryInfo.ReportTitle = "Student ID Card";
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

        public ActionResult Personalinfo()
        {
            var s = User.Identity.Name;
            ViewBag.CourseList = new SelectList(_iStudent.GetCourseList().ToList(), "Id", "CourseName");
            ViewBag.SemesterList = new SelectList(_iStudent.GetSemesterList().ToList(), "Id", "SemesterName");
            ViewBag.AcademicSessionList = new SelectList(_iStudent.GetAcSessionList().ToList(), "Id", "Session");
            return View();
        }


        [HttpPost]
        public ActionResult Personalinfo(StudentProfileViewModel model)
       {
            string filename = "";

            if (model.image != null && model.image.ContentLength > 0)
            {
                filename = Path.GetFileName(Guid.NewGuid() + "." + model.image.FileName.Split('.')[1]);
                string targetPath = Server.MapPath("~/uploads/" + filename);
                Stream strm = model.image.InputStream;
                var targetFile = targetPath;

                GenerateThumbnails(0.5, strm, targetFile);
            }
            model.Picture = filename;
            Session["Personalinfo"] = model;
            return RedirectToAction("AcademicRecords");
            
        }

        public ActionResult AcademicRecords()
        {
            if (Session["Personalinfo"] == null)
            {
                return RedirectToAction("Personalinfo");
            }
            var data = from s in _iStudent.GetExamList().ToList()
                       select new AcademicRecordsViewModel
                       { 
                          
                        Id=0,
                        ExamId=s.Id,
                        ExamName=s.ExamName,
                        BoardId=0,
                        GroupId=0,
                        RollOfExam="",
                        YearOfPassing =0,
                        Grade=""
                          
                       };

            ViewBag.ExamList = new SelectList(_iStudent.GetExamList().ToList(), "Id", "ExamName");
            ViewBag.GroupList = new SelectList(_iStudent.GetGroupList().ToList(), "Id", "GroupName");
            ViewBag.BoardList = new SelectList(_iStudent.GetBoardList().ToList(), "Id", "BoardName");

            return View(data.ToList());
        }

        [HttpPost]
        public ActionResult AcademicRecords(List<AcademicRecordsViewModel> records)
        {
            Session["AcademicRecords"] = records;

            return RedirectToAction("GuardianInfo");
        }

        public ActionResult GuardianInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuardianInfo(GuardianViewModel gardian)
        {
            Session["GuardianInfo"] = gardian;

            return RedirectToAction("OfficeUseFrom");
        }

        public ActionResult OfficeUseFrom()
        {
            ViewBag.PaymentSystemList = new SelectList(_iStudent.GetPaymentScheduleList().ToList(), "Id", "Schedule");
            ViewBag.EndorsementList = new SelectList(_iStudent.GetEndorsementList().ToList(), "Id", "DocumentName");
            return View();
        }

        [HttpPost]
        public ActionResult OfficeUseFrom(InitialPaymentDetailViewModel officeFrom, int[] OriginalDocumentId)
        {
            var ps = (StudentProfileViewModel)Session["Personalinfo"];
            var p = (List<AcademicRecordsViewModel>)Session["AcademicRecords"];
            var bs = (GuardianViewModel)Session["GuardianInfo"];

            var result=_iStudent.Add(ps, p, bs, officeFrom, OriginalDocumentId);

            Session["OfficeUseFrom"] = officeFrom;

            return RedirectToAction("StudentList");
        }

        public ActionResult PersonalinfoEdit(Guid id)
        {
            ViewBag.CourseList = new SelectList(_iStudent.GetCourseList().ToList(), "Id", "CourseName");
            ViewBag.SemesterList = new SelectList(_iStudent.GetSemesterList().ToList(), "Id", "SemesterName");
            ViewBag.AcademicSessionList = new SelectList(_iStudent.GetAcSessionList().ToList(), "Id", "Session");

            var result = _iStudent.getStudentInfo(id);

            return View(result);
        }

        [HttpPost]
        public ActionResult PersonalinfoEdit(StudentProfileViewModel model)
        {
            if (model.image != null && model.image.ContentLength > 0)
            {
                string filename = "";
                filename = Path.GetFileName(Guid.NewGuid() + "." + model.image.FileName.Split('.')[1]);
                model.Picture = filename;
                string targetPath = Server.MapPath("~/uploads//" + filename);
                Stream strm = model.image.InputStream;
                var targetFile = targetPath;

                GenerateThumbnails(0.5, strm, targetFile);
            }

            _iStudent.UpdateStudentProfile(model);

            return RedirectToAction("StudentDetails", "Student", new { area="Students", id=model.Id });

            //std.Picture = model.Picture;
        }


        public ActionResult AcademicRecordEdit(Guid id)
        {
            //var data = _iStudent.getAcademicRecord(id);
            var data = (from s in _iStudent.getAcademicRecord(id).ToList()
                       select new AcademicRecordsViewModel
                       {

                           ExamId = s.ExamId,
                           ExamName = s.ExamName,
                           BoardId = s.BoardId,
                           GroupId = s.GroupId,
                           RollOfExam = s.RollOfExam,
                           YearOfPassing = s.YearOfPassing,
                           Grade = s.Grade

                       }).ToList();

            var Edata = from s in _iStudent.GetExamList().ToList()
                       
                       select new ExamViewModel
                       {
                           ExamId = s.Id,
                           ExamName = s.ExamName,

                       };


            ViewBag.ExamList = new SelectList(_iStudent.GetExamList(), "Id", "ExamName", data.Select(x => x.ExamName));
            ViewBag.GroupList = new SelectList(_iStudent.GetGroupList().ToList(), "Id", "GroupName");
            ViewBag.BoardList = new SelectList(_iStudent.GetBoardList().ToList(), "Id", "BoardName");

            return View(data.ToList());
        }

        public ActionResult TestAdmission()
        {
            return View();
        }

        [HttpPost]
        public JsonResult TestAdmisson(StudentAdmissionViewModel model)
        {
            bool status = false;
            //var result = Guid.Empty;
            if (ModelState.IsValid)
            {

                status = true;
            }
            else
                status = false;


            return new JsonResult { Data = new { status = status } };
        }

        public JsonResult GetAllAcSession()
        {
            var acSession = _iStudent.GetAcSessionList().ToList();

            return Json(acSession, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCourse()
        {
            var course = _iStudent.GetCourseList().ToList();

            return Json(course, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGroup()
        {
            var group = _iStudent.GetGroupList().ToList();
            
            return Json(group, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllBoard()
        {
            var board = _iStudent.GetBoardList().ToList();

            return Json(board, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult StudentInactive(Guid id)
        {
            var result = _iStudent.getStudentInfo(id);

            _iStudent.Inactive(id);
            return RedirectToAction("StudentList");
        }

       
        public ActionResult StudentActive(Guid id)
        {
            var result = _iStudent.getStudentInfo(id);

            _iStudent.Active(id);
            return RedirectToAction("StudentList");
        }


        //Image Helper
        private void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = System.Drawing.Image.FromStream(sourcePath))
            {
                int newWidth;
                int newHeight;

                newWidth = 512;
                newHeight = 384;

                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                if (System.IO.File.Exists(targetPath))
                {
                    System.IO.File.Delete(targetPath);
                }

                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }


        
       
	}
}