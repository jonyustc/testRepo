using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace GAMCUC.ViewModel
{

    public class StudentProfileViewModel
    {
        [Required(ErrorMessage="Course Name Required")]
        public int? CourseId { get; set; }

        [Required(ErrorMessage = "Session Name Required")]
        public Guid? AcademicSessionId { get; set; }
        public string FormNo { get; set; }
        public System.Guid Id { get; set; }
        public string StdNameBangla { get; set; }

        [Required(ErrorMessage = "Student Name Required")]
        public string StdNameEnglish { get; set; }
        public string StdFatherNameBangla { get; set; }

        [Required(ErrorMessage = "Father Name Required")]
        public string StdFatherNameEnglish { get; set; }
        public string StdMotherNameBangla { get; set; }

        public string StdMotherNameEnglish { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string Picture { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }

        public string RegistrationNo { get; set; }

        public string ExamRollNo { get; set; }

        public bool? IsSuspended { get; set; }
        public string SuspendedDesc { get; set; }
        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "Semester Name Required")]
        public int SemesterId { get; set; }

        [NotMapped]
        public HttpPostedFileBase image { get; set; }
    }

    public class StudentAdmissionViewModel
    {
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
        public Guid? AcademicSessionId { get; set; }
        public string FormNo { get; set; }
        public System.Guid Id { get; set; }
        public string StdNameBangla { get; set; }
        public string StdNameEnglish { get; set; }
        public string StdFatherNameBangla { get; set; }
        public string StdFatherNameEnglish { get; set; }
        public string StdMotherNameBangla { get; set; }
        public string StdMotherNameEnglish { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string Picture { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public int StdId { get; set; }


        public bool? IsSuspended { get; set; }
        public string SuspendedDesc { get; set; }
        public bool? IsActive { get; set; }

        public List<AcademicRecordsViewModel> AcademicRecords { get; set; }

        public GuardianViewModel Guardian { get; set; }

        public InitialPaymentDetailViewModel InitialPayment { get; set; }

        public List<int> OriginalDocumentId { get; set; }

    }

    public class StudentDetailsViewModel
    {
        public string CourseName { get; set; }
        public string AcademicSession { get; set; }
        public Guid Id { get; set; }
        public string StdNameBangla { get; set; }
        public string StdNameEnglish { get; set; }
        public string StdFatherNameBangla { get; set; }
        public string StdFatherNameEnglish { get; set; }
        public string StdMotherNameBangla { get; set; }
        public string StdMotherNameEnglish { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public string Picture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? EntryDate { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }

        public int FormNo { get; set; }

        public bool IsSuspended { get; set; }
        public string SuspendedDesc { get; set; }
        public bool IsActive { get; set; }
        public string ExamRollNo { get; set; }
        public string RegistrationNo { get; set; }


        public int StdID { get; set; }
        public string SemesterName { get; set; }
        public List<AcademicRecordsViewModel> AcademicRecords { get; set; }

        public GuardianViewModel Guardian { get; set; }

        public InitialPaymentDetailViewModel InitialPayment { get; set; }

        public List<int> OriginalDocumentId { get; set; }

    }

    public class AcademicRecordsViewModel
    {
        public int Id { get; set; }

        public int ExamId { get; set; }
        public string ExamName { get; set; }

        public int GroupId { get; set; }

        public Guid  StudentId { get; set; }

        public string GroupName { get; set; }

        public int BoardId { get; set; }
        public string BoardName { get; set; }

        public int YearOfPassing { get; set; }
        public string RollOfExam { get; set; }
        public string Grade { get; set; }

    }

    public class GuardianViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Relation { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class InitialPaymentDetailViewModel
    {
        public int Id { get; set; }
        public decimal InitialPayment { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal CourseFee { get; set; }
        public int PaymentScheduleId { get; set; }
        public string PaymentSchedule { get; set; }

        public int StdID { get; set; }
        public string RegistrationNo { get; set; }
        public string ExamRollNo { get; set; }
        public Guid StudentId { get; set; }

        //[Required(ErrorMessage = "Endorsement Required")]
        //public int OrgId { get; set; }

        //[Required(ErrorMessage = "Endorsement Required")]
        public int OriginalDocumentId { get; set; }
        public string DocumentName { get; set; }
    }

    public class OriginalDocumentViewModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
    }

    public class PaymentScheduleViewModel
    {
        public int Id { get; set; }
        public string Schedule { get; set; }
    }

    public class SemesterViewModel
    {
        public int Id { get; set; }
        public string SemesterName { get; set; }
    }

    public class AcademicSessionViewModel
    {
        public Guid Id { get; set; }
        public string Session { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ExamViewModel
    {
        public int Id { get; set; }
        public string ExamName { get; set; }
        public int ExamId { get; set; }

    }

    public class GroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
    }

    public class BoardViewModel
    {
        public int Id { get; set; }
        public string BoardName { get; set; }
    }

    public class CourseViewModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
    }

    public class AcSessionViewModel
    {
        public Guid Id { get; set; }
        public string Session { get; set; }
        public bool? IsActive { get; set; }
    }

    public class StudentOriginalDocumentVM
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }
        public int OriginalDocumentId { get; set; }
        public string DocumentName { get; set; }
    }
}
