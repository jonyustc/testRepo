using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.IDAL
{
    public interface IStudent
    {


        IList<ExamViewModel> GetExamList();
        IList<GroupViewModel> GetGroupList();
        IList<BoardViewModel> GetBoardList();
        IList<CourseViewModel> GetCourseList();
        IList<SemesterViewModel> GetSemesterList();
        IList<AcSessionViewModel> GetAcSessionList();
        IList<PaymentScheduleViewModel> GetPaymentScheduleList();
        IList<OriginalDocumentViewModel> GetEndorsementList();
        int Add(StudentProfileViewModel model,
            List<AcademicRecordsViewModel> records,
            GuardianViewModel gardian,
            InitialPaymentDetailViewModel officeFrom, int[] OriginalDocumentId);

        List<StudentAdmissionViewModel> All();

        StudentDetailsViewModel StudentDetails(Guid id);
        List<StudentDetailsViewModel> StudentDetailsPrint();
        List<AcademicRecordsViewModel> AcademicRecordsPrint();
        List<GuardianViewModel> GuardianInfoPrint();
        List<InitialPaymentDetailViewModel> InitialPaymentPrint();
        List<StudentOriginalDocumentVM> OrgDocPrint();
        List<StudentListForPayment> SearchStudent(string stdID, int? courseId, int? semesterId, string roll);
        StudentProfileViewModel getStudentInfo(Guid id);
        List<AcademicRecordsViewModel> getAcademicRecord(Guid id);
        void UpdateStudentProfile(StudentProfileViewModel model);

        void Inactive(Guid id);
        void Active(Guid id);
    }
}
