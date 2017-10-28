using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAMCUC.Web.Areas.Students.Controllers
{
    public class StudentPromotionController : Controller
    {
        IDAL.IStudentPromotion _iPromotion = null;
        IDAL.IStudent _iStudent = null;

        public StudentPromotionController()
        {
            _iPromotion = new BLL.bllStudentPromotion();
            _iStudent = new BLL.bllStudent();
        }



        //
        // GET: /Students/StudentPromotion/
        public ActionResult StdPromotion()
        {
            ViewBag.CourseList = new SelectList(_iStudent.GetCourseList(), "Id", "CourseName");
            ViewBag.Semester = new SelectList(_iStudent.GetSemesterList(), "Id", "SemesterName");
            ViewBag.Session = new SelectList(_iStudent.GetAcSessionList(), "Id", "Session");
            return View();
        }

        [HttpPost]
        public ActionResult StdListForPromotion(int courseId, int semesterId,Guid session)
        {
            ViewBag.Semester = new SelectList(_iStudent.GetSemesterList(), "Id", "SemesterName");
            var list = _iStudent.StdListSearchForPromotion(courseId, semesterId,session);
            ViewBag.SemesterId = semesterId;
            return View(list);
        }

        [HttpPost]
        public ActionResult Promotion(List<StudentPromotionViewModel> model, int PSemesterId)
        {
            _iPromotion.Promotion(model, PSemesterId);
            return RedirectToAction("StdPromotion");
        }


	}
}