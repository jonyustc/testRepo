using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.BLL
{
    public class bllStudent : IStudent
    {
        IDAL.IStudent _iStudent = null;

        public bllStudent()
        {
            _iStudent = new DAL.dalStudent();
        }

        public IList<ExamViewModel> GetExamList()
        {
            return _iStudent.GetExamList();
        }

        public IList<GroupViewModel> GetGroupList()
        {
            return _iStudent.GetGroupList();
        }

        public IList<BoardViewModel> GetBoardList()
        {
            return _iStudent.GetBoardList();
        }

        public IList<CourseViewModel> GetCourseList()
        {
            return _iStudent.GetCourseList();
        }

        public IList<SemesterViewModel> GetSemesterList()
        {
            return _iStudent.GetSemesterList();
        }

        public IList<AcSessionViewModel> GetAcSessionList()
        {
            return _iStudent.GetAcSessionList();
        }

        public IList<PaymentScheduleViewModel> GetPaymentScheduleList()
        {
            return _iStudent.GetPaymentScheduleList();
        }

        public IList<OriginalDocumentViewModel> GetEndorsementList()
        {
            return _iStudent.GetEndorsementList();
        }

        public int Add(StudentProfileViewModel model,
            List<AcademicRecordsViewModel> records,
            GuardianViewModel gardian,
            InitialPaymentDetailViewModel officeFrom, int[] OriginalDocumentId)
        {
            return _iStudent.Add(model, records, gardian, officeFrom, OriginalDocumentId);
        }

        public List<StudentAdmissionViewModel> All()
        {
            return _iStudent.All();
        }

        public StudentDetailsViewModel StudentDetails(Guid id)
        {
            return _iStudent.StudentDetails(id);
        }

        public List<StudentDetailsViewModel> StudentDetailsPrint()
        {
          return  _iStudent.StudentDetailsPrint();
        }

        public List<AcademicRecordsViewModel> AcademicRecordsPrint()
        {
            return _iStudent.AcademicRecordsPrint();
        }

        public List<GuardianViewModel> GuardianInfoPrint()
        {
            return _iStudent.GuardianInfoPrint();
        }

        public List<InitialPaymentDetailViewModel> InitialPaymentPrint()
        {
            return _iStudent.InitialPaymentPrint();
        }

        public List<StudentOriginalDocumentVM> OrgDocPrint()
        {
            return _iStudent.OrgDocPrint();
        }

        public StudentProfileViewModel getStudentInfo(Guid id)
        {
            return _iStudent.getStudentInfo(id);
        }

        public List<AcademicRecordsViewModel> getAcademicRecord(Guid id)
        {
            return _iStudent.getAcademicRecord(id);
        }

        public void UpdateStudentProfile(StudentProfileViewModel model)
        {
            _iStudent.UpdateStudentProfile(model);
        }

        public List<StudentListForPayment> SearchStudent(string stdID, int? courseId, int? semesterId, string roll)
        {
            return _iStudent.SearchStudent(stdID, courseId, semesterId, roll);
        }

        public void Inactive(Guid id)
        {
            _iStudent.Inactive(id);
        }

        public void Active(Guid id)
        {
            _iStudent.Active(id);
        }
    }
}
