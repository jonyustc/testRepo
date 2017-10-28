using GAMCUC.DataModel;
using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.DAL
{
    public class dalStudent : IStudent
    {
        private GaMCucDbEntities _context;

        public dalStudent()
        {
            _context = new GaMCucDbEntities();
        }

        public List<StudentAdmissionViewModel> All(int courseId, int semesterId, bool status, Guid session)
        {
            if ((courseId != 0) && (semesterId != 0))
            {
                List<StudentAdmissionViewModel> list = new List<StudentAdmissionViewModel>();
                var students = from pr in _context.Students.ToList()
                               join c in _context.Courses.ToList() on pr.CourseId equals c.Id
                               join s in _context.Semesters.ToList() on pr.SemesterId equals s.Id
                               join ss in _context.AcademicSessions.ToList() on pr.AcademicSessionId equals ss.Id
                               join stdMap in _context.StudentSemesterMappings.ToList() on pr.Id equals stdMap.StudentId
                               where ((pr.CourseId.Equals(courseId)
                               && stdMap.SemesterId.Equals(semesterId)
                               && stdMap.IsActive == true) && (pr.IsActive.Equals(status)) && ss.Id.Equals(session))
                               select new StudentAdmissionViewModel
                               {
                                   Id = pr.Id,
                                   StdId = pr.StdID,
                                   CourseName = pr.Course.CourseName,
                                   StdNameEnglish = pr.StdNameEnglish,
                                   StdFatherNameEnglish = pr.StdFatherNameEnglish,
                                   StdMotherNameEnglish = pr.StdMotherNameEnglish,
                                   IsActive = pr.IsActive,
                                   Email = pr.Email
                               };
                list = students.ToList();
                return students.ToList();
            }

            return null;

        }

        public List<StudentPromotionViewModel> StdListSearchForPromotion(int courseId, int semesterId, Guid session)
        {
            if ((courseId != 0) && (semesterId != 0))
            {
                List<StudentPromotionViewModel> list = new List<StudentPromotionViewModel>();
                var students = from pr in _context.Students.ToList()
                               join c in _context.Courses.ToList() on pr.CourseId equals c.Id
                               join s in _context.Semesters.ToList() on pr.SemesterId equals s.Id
                               join ss in _context.AcademicSessions.ToList() on pr.AcademicSessionId equals ss.Id
                               join stdMap in _context.StudentSemesterMappings.ToList() on pr.Id equals stdMap.StudentId
                               where (pr.CourseId.Equals(courseId) && stdMap.SemesterId.Equals(semesterId) 
                               && stdMap.IsActive == true && ss.Id.Equals(session))
                               select new StudentPromotionViewModel
                               {
                                   StudentId = pr.Id,
                                   StdID = pr.StdID,
                                   StudentName = pr.StdNameEnglish,
                                   FatherName = pr.StdFatherNameEnglish,
                                   IsActive = pr.IsActive,
                                   SemesterId = s.Id
                               };
                list = students.ToList();
                return students.ToList();
            }

            return null;

        }

        public StudentDetailsViewModel StudentDetails(Guid id)
        {
            List<StudentDetailsViewModel> list = new List<StudentDetailsViewModel>();
            var query = from pr in _context.Students.ToList()
                        join c in _context.Courses.ToList() on pr.CourseId equals c.Id
                        join s in _context.Semesters.ToList() on pr.SemesterId equals s.Id
                        where (pr.Id.Equals(id))
                        select new StudentDetailsViewModel
                        {
                            Id = pr.Id,
                            CourseName = pr.Course.CourseName,
                            AcademicSession = pr.AcademicSession.Session,
                            StdNameEnglish = pr.StdNameEnglish,
                            StdFatherNameEnglish = pr.StdFatherNameEnglish,
                            StdMotherNameEnglish = pr.StdMotherNameEnglish,
                            StdID = pr.StdID,
                            SemesterName = pr.Semester.SemesterName,
                            Email = pr.Email,
                            Phone = pr.Phone,
                            Gender = pr.Gender,
                            DateOfBirth = pr.DateOfBirth ?? DateTime.Now,
                            PresentAddress = pr.PresentAddress,
                            PermanentAddress = pr.PermanentAddress,
                            Picture = pr.Picture,
                            RegistrationNo = pr.RegistrationNo,
                            ExamRollNo = pr.ExamRollNo,
                            AcademicRecords = (from ac in _context.AcademicRecords.ToList()
                                               join Exm in _context.AcrExams.ToList() on ac.ExamId equals Exm.Id
                                               join grp in _context.AcrGroups.ToList() on ac.GroupId equals grp.Id
                                               join brd in _context.AcrBoards.ToList() on ac.BoardId equals brd.Id
                                               where ac.StudentId == id
                                               select new AcademicRecordsViewModel
                                               {
                                                   ExamName = Exm.ExamName,
                                                   GroupName = grp.GroupName,
                                                   BoardName = brd.BoardName,
                                                   YearOfPassing = ac.YearOfPassing ?? 0,
                                                   RollOfExam = ac.RollOfExam,
                                                   Grade = ac.Grade
                                               }).ToList()
                        };

            list = query.ToList();
            return query.FirstOrDefault();
        }

        public List<StudentDetailsViewModel> StudentDetailsPrint()
        {
            List<StudentDetailsViewModel> list = new List<StudentDetailsViewModel>();
            var query = from pr in _context.Students.ToList()
                        join c in _context.Courses.ToList() on pr.CourseId equals c.Id
                        join s in _context.Semesters.ToList() on pr.SemesterId equals s.Id
                        // where (pr.Id.Equals(id))
                        select new StudentDetailsViewModel
                        {
                            Id = pr.Id,
                            CourseName = pr.Course.CourseName,
                            FormNo = pr.FormNo,
                            AcademicSession = pr.AcademicSession.Session,
                            StdNameBangla = pr.StdNameBangla,
                            StdNameEnglish = pr.StdNameEnglish,
                            StdFatherNameBangla = pr.StdFatherNameBangla,
                            StdFatherNameEnglish = pr.StdFatherNameEnglish,
                            StdMotherNameBangla = pr.StdMotherNameBangla,
                            StdMotherNameEnglish = pr.StdMotherNameEnglish,
                            StdID = pr.StdID,
                            SemesterName = pr.Semester.SemesterName,
                            Email = pr.Email,
                            Religion = pr.Religion,
                            Nationality = pr.Nationality,
                            Phone = pr.Phone,
                            Gender = pr.Gender,
                            DateOfBirth = pr.DateOfBirth ?? DateTime.Now,
                            PresentAddress = pr.PresentAddress,
                            PermanentAddress = pr.PermanentAddress,
                            Picture = pr.Picture,
                            RegistrationNo = pr.RegistrationNo,
                            ExamRollNo = pr.ExamRollNo

                        };


            return query.ToList();
        }

        public List<AcademicRecordsViewModel> AcademicRecordsPrint()
        {
            var AcademicRecords = (from ac in _context.AcademicRecords.ToList()
                                   join Exm in _context.AcrExams.ToList() on ac.ExamId equals Exm.Id
                                   join grp in _context.AcrGroups.ToList() on ac.GroupId equals grp.Id
                                   join brd in _context.AcrBoards.ToList() on ac.BoardId equals brd.Id
                                   select new AcademicRecordsViewModel
                                   {
                                       StudentId = ac.StudentId ?? Guid.Empty,
                                       ExamName = Exm.ExamName,
                                       GroupName = grp.GroupName,
                                       BoardName = brd.BoardName,
                                       YearOfPassing = ac.YearOfPassing ?? 0,
                                       RollOfExam = ac.RollOfExam,
                                       Grade = ac.Grade
                                   }).ToList();


            return AcademicRecords.ToList();
        }

        public List<GuardianViewModel> GuardianInfoPrint()
        {
            var guardianInfo = (from ac in _context.Guardians.ToList()
                                select new GuardianViewModel
                                   {
                                       StudentId = ac.StudentId ?? Guid.Empty,
                                       Name = ac.Name,
                                       Occupation = ac.Occupation,
                                       Relation = ac.Relation,
                                       Address = ac.Address,
                                       Phone = ac.Phone
                                   }).ToList();


            return guardianInfo.ToList();
        }

        public List<InitialPaymentDetailViewModel> InitialPaymentPrint()
        {
            var initialPayment = (from ac in _context.InitialPaymentDetails.ToList()
                                  join c in _context.Courses.ToList() on ac.CourseId equals c.Id
                                  join std in _context.Students.ToList() on ac.StudentId equals std.Id
                                  select new InitialPaymentDetailViewModel
                                {
                                    StudentId = ac.StudentId ?? Guid.Empty,
                                    CourseName = c.CourseName,
                                    CourseFee = ac.CourseFee ?? 0,
                                    StdID = std.StdID,
                                    RegistrationNo = std.RegistrationNo,
                                    ExamRollNo = std.ExamRollNo

                                }).ToList();


            return initialPayment.ToList();
        }

        public List<StudentOriginalDocumentVM> OrgDocPrint()
        {
            var orgDoc = (from std in _context.Students.ToList()
                          join stdoc in _context.StudentOriginalDocuments.ToList() on std.Id equals stdoc.StudentId
                          join doc in _context.OriginalDocuments.ToList() on stdoc.OriginalDocumentId equals doc.Id
                          select new StudentOriginalDocumentVM
                                  {
                                      StudentId = std.Id,
                                      Id = doc.Id,
                                      DocumentName = doc.DocumentName
                                  }).ToList();


            return orgDoc.ToList();
        }

        public StudentProfileViewModel getStudentInfo(Guid id)
        {
            StudentProfileViewModel list = new StudentProfileViewModel();
            var result = from std in _context.Students.ToList()
                         where std.Id == id
                         select new StudentProfileViewModel
                             {
                                 Id = std.Id,
                                 StdNameEnglish = std.StdNameEnglish,
                                 StdNameBangla = std.StdNameBangla,
                                 StdFatherNameEnglish = std.StdFatherNameEnglish,
                                 StdFatherNameBangla = std.StdFatherNameBangla,
                                 StdMotherNameEnglish = std.StdMotherNameEnglish,
                                 StdMotherNameBangla = std.StdMotherNameBangla,
                                 Gender = std.Gender,
                                 DateOfBirth = std.DateOfBirth,
                                 Email = std.Email,
                                 Phone = std.Phone,
                                 Picture = std.Picture,
                                 PresentAddress = std.PresentAddress,
                                 PermanentAddress = std.PermanentAddress,
                                 RegistrationNo = std.RegistrationNo,
                                 ExamRollNo = std.ExamRollNo

                             };

            list = result.FirstOrDefault();
            return list;

        }

        public List<AcademicRecordsViewModel> getAcademicRecord(Guid id)
        {
            var list = new List<AcademicRecordsViewModel>();
            var result = (from ac in _context.AcademicRecords.ToList()
                          join Exm in _context.AcrExams.ToList() on ac.ExamId equals Exm.Id into gE
                          join grp in _context.AcrGroups.ToList() on ac.GroupId equals grp.Id into gR
                          join brd in _context.AcrBoards.ToList() on ac.BoardId equals brd.Id into gB
                          where ac.StudentId == id
                          from Exm in gE.DefaultIfEmpty()
                          from grp in gR.DefaultIfEmpty()
                          from brd in gB.DefaultIfEmpty()
                          select new AcademicRecordsViewModel
                          {
                              ExamId = Exm.Id,
                              GroupId = grp.Id,
                              BoardId = brd.Id,
                              YearOfPassing = ac.YearOfPassing ?? 0,
                              RollOfExam = ac.RollOfExam,
                              Grade = ac.Grade
                          }).ToList();

            list = result.ToList();
            return list;
        }

        public void UpdateStudentProfile(StudentProfileViewModel model)
        {
            var std = _context.Students.Where(x => x.Id == model.Id).FirstOrDefault();
            std.StdNameEnglish = model.StdNameEnglish;
            std.StdNameBangla = model.StdNameBangla;
            std.StdFatherNameEnglish = model.StdFatherNameEnglish;
            std.StdFatherNameBangla = model.StdFatherNameBangla;
            std.StdMotherNameBangla = model.StdMotherNameBangla;
            std.StdMotherNameEnglish = model.StdMotherNameEnglish;
            std.DateOfBirth = model.DateOfBirth;
            std.Phone = model.Phone;
            std.Picture = model.Picture;
            std.PermanentAddress = model.PermanentAddress;
            std.PresentAddress = model.PresentAddress;
            std.RegistrationNo = model.RegistrationNo;
            std.ExamRollNo = model.ExamRollNo;

            _context.Entry(std).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Inactive(Guid id)
        {
            var std = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            std.IsActive = false;

            //_context.Entry(std).State = EntityState.Modified;
            _context.Entry(std).Property(x => x.IsActive).IsModified = true;
            _context.SaveChanges();
        }

        public void Active(Guid id)
        {
            var std = _context.Students.Where(x => x.Id == id).FirstOrDefault();
            std.IsActive = true;

            //_context.Entry(std).State = EntityState.Modified;
            _context.Entry(std).Property(x => x.IsActive).IsModified = true;
            _context.SaveChanges();
        }

        public List<StudentListForPayment> SearchStudent(string stdID, int? courseId, int? semesterId, string roll,Guid session)
        {
            if ((courseId != 0 && courseId != null) && (semesterId != 0 && semesterId != null))
            {
                List<StudentListForPayment> list = new List<StudentListForPayment>();
                var students = from pr in _context.Students.ToList()
                               join c in _context.Courses.ToList() on pr.CourseId equals c.Id
                               join s in _context.Semesters.ToList() on pr.SemesterId equals s.Id
                               join ss in _context.AcademicSessions.ToList() on pr.AcademicSessionId equals ss.Id
                               join stdMap in _context.StudentSemesterMappings.ToList() on pr.Id equals stdMap.StudentId
                               where (pr.CourseId.Equals(courseId) 
                               && stdMap.SemesterId.Equals(semesterId)
                               && stdMap.IsActive == true
                               && pr.IsActive==true
                               && ss.Id.Equals(session))
                               select new StudentListForPayment
                               {
                                   Id = pr.Id,
                                   StdID = pr.StdID,
                                   CourseName = pr.Course.CourseName,
                                   SemesterName = pr.Semester.SemesterName,
                                   StdNameEnglish = pr.StdNameEnglish,
                                   StdFatherNameEnglish = pr.StdFatherNameEnglish,
                                   StdMotherNameEnglish = pr.StdMotherNameEnglish,
                                   ExamRollNo = pr.ExamRollNo,
                                   Email = pr.Email
                               };
                list = students.ToList();
                return students.ToList();
            }
            else if (stdID != null)
            {
                List<StudentListForPayment> list = new List<StudentListForPayment>();
                var semId = _context.StudentSemesterMappings.Where(x => x.StudentId.Equals(new Guid(stdID))
                    && x.IsActive == true).Select(x => x.SemesterId).FirstOrDefault();
                var students = from pr in _context.Students.ToList()
                               join c in _context.Courses.ToList() on pr.CourseId equals c.Id
                               join ac in _context.AcademicRecords.ToList() on pr.Id equals ac.StudentId
                               join Exm in _context.AcrExams.ToList() on ac.ExamId equals Exm.Id
                               join grp in _context.AcrGroups.ToList() on ac.GroupId equals grp.Id
                               join brd in _context.AcrBoards.ToList() on ac.BoardId equals brd.Id
                               join stdMap in _context.StudentSemesterMappings.ToList() on pr.Id equals stdMap.StudentId
                               where (stdMap.StudentId.Equals((new Guid(stdID))) && stdMap.SemesterId.Equals(semId) && stdMap.IsActive==true)
                               select new StudentListForPayment
                               {
                                   Id = pr.Id,
                                   StdID = pr.StdID,
                                   CourseName = pr.Course.CourseName,
                                   SemesterId = stdMap.SemesterId,
                                   SemesterName = pr.Semester.SemesterName,
                                   StdNameEnglish = pr.StdNameEnglish,
                                   StdFatherNameEnglish = pr.StdFatherNameEnglish,
                                   StdMotherNameEnglish = pr.StdMotherNameEnglish,
                                   ExamRollNo = pr.ExamRollNo,
                                   Email = pr.Email,
                                   Phone = pr.Phone,
                                   Gender = pr.Gender,
                                   DateOfBirth = pr.DateOfBirth,
                                   PresentAddress = pr.PresentAddress,
                                   PermanentAddress = pr.PermanentAddress,
                                   Picture = pr.Picture

                               };
                list = students.ToList();
                return students.ToList();
            }
            else
                return null;



        }

        public int Add(StudentProfileViewModel model,
            List<AcademicRecordsViewModel> records,
            GuardianViewModel gardian,
            InitialPaymentDetailViewModel officeFrom, int[] OriginalDocumentId)
        {

            Student std = new Student();
            std.Id = Guid.NewGuid();
            std.StdNameBangla = model.StdNameBangla;
            std.StdNameEnglish = model.StdNameEnglish;
            std.StdFatherNameBangla = model.StdFatherNameBangla;
            std.StdFatherNameEnglish = model.StdFatherNameEnglish;
            std.StdMotherNameBangla = model.StdMotherNameBangla;
            std.StdMotherNameEnglish = model.StdMotherNameEnglish;
            std.AdmissionDate = DateTime.Now;
            std.DateOfBirth = model.DateOfBirth;
            std.EntryDate = DateTime.Now;
            std.FormNo = _context.Students.Select(s => s.FormNo).Max() + 1;
            std.Gender = model.Gender;
            std.IsActive = true;
            std.IsSuspended = model.IsSuspended;
            std.Nationality = model.Nationality;
            std.PermanentAddress = model.PermanentAddress;
            std.Phone = model.Phone;
            std.Email = model.Email;
            std.Picture = model.Picture;
            std.PresentAddress = model.PresentAddress;
            std.Religion = model.Religion;
            std.SuspendedDesc = model.SuspendedDesc;

            std.RegistrationNo = officeFrom.RegistrationNo;
            std.ExamRollNo = officeFrom.ExamRollNo;
            std.CourseId = model.CourseId;
            std.AcademicSessionId = model.AcademicSessionId;
            std.SemesterId = model.SemesterId;
            _context.Students.Add(std);

            foreach (var item in records)
            {
                if (item.ExamId > 0)
                {
                    AcademicRecord r = new AcademicRecord();
                    r.ExamId = item.ExamId;
                    r.GroupId = item.GroupId;
                    r.BoardId = item.BoardId;
                    r.YearOfPassing = item.YearOfPassing;
                    r.RollOfExam = item.RollOfExam;
                    r.Grade = item.Grade;
                    r.StudentId = std.Id;
                    _context.AcademicRecords.Add(r);
                }
            }

            Guardian gar = new Guardian();
            gar.Id = Guid.NewGuid();
            gar.Name = gardian.Name;
            gar.Occupation = gardian.Occupation;
            gar.Relation = gardian.Relation;
            gar.Phone = gardian.Phone;
            gar.Address = gardian.Address;
            gar.StudentId = std.Id;
            _context.Guardians.Add(gar);

            InitialPaymentDetail ip = new InitialPaymentDetail();
            ip.CourseFee = officeFrom.CourseFee;
            //ip.InitialPayment = officeFrom.InitialPayment;
            ip.CourseId = model.CourseId;
            //ip.PaymentScheduleId = officeFrom.PaymentScheduleId;
            ip.StudentId = std.Id;
            _context.InitialPaymentDetails.Add(ip);


            foreach (var d in OriginalDocumentId)
            {
                StudentOriginalDocument stdDOc = new StudentOriginalDocument();
                stdDOc.StudentId = std.Id;
                stdDOc.OriginalDocumentId = d;
                _context.StudentOriginalDocuments.Add(stdDOc);

            }

            StudentSemesterMapping sm = new StudentSemesterMapping();
            sm.StudentId = std.Id;
            sm.SemesterId = model.SemesterId;
            sm.IsActive = true;
            _context.StudentSemesterMappings.Add(sm);

            try
            {

                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }

        public IList<CourseViewModel> GetCourseList()
        {

            List<CourseViewModel> List = new List<CourseViewModel>();
            var crsList = _context.Courses.ToList();
            foreach (var item in crsList)
            {

                List.Add(new CourseViewModel()
                {
                    Id = item.Id,
                    CourseName = item.CourseName

                });
            }
            return List;
        }

        public IList<SemesterViewModel> GetSemesterList()
        {

            List<SemesterViewModel> List = new List<SemesterViewModel>();
            var sList = _context.Semesters.ToList();
            foreach (var item in sList)
            {

                List.Add(new SemesterViewModel()
                {
                    Id = item.Id,
                    SemesterName = item.SemesterName

                });
            }
            return List;
        }

        public IList<PaymentScheduleViewModel> GetPaymentScheduleList()
        {

            List<PaymentScheduleViewModel> List = new List<PaymentScheduleViewModel>();
            var PaymentScheuleList = _context.PaymentSchedules.ToList();
            foreach (var item in PaymentScheuleList)
            {

                List.Add(new PaymentScheduleViewModel()
                {
                    Id = item.Id,
                    Schedule = item.Schedule
                });
            }
            return List;
        }

        public IList<OriginalDocumentViewModel> GetEndorsementList()
        {

            List<OriginalDocumentViewModel> List = new List<OriginalDocumentViewModel>();
            var endList = _context.OriginalDocuments.ToList();
            foreach (var item in endList)
            {

                List.Add(new OriginalDocumentViewModel()
                {
                    Id = item.Id,
                    DocumentName = item.DocumentName
                });
            }
            return List;
        }

        public IList<AcSessionViewModel> GetAcSessionList()
        {

            List<AcSessionViewModel> List = new List<AcSessionViewModel>();
            var acSessionList = _context.AcademicSessions.ToList();
            foreach (var item in acSessionList)
            {

                List.Add(new AcSessionViewModel()
                {
                    Id = item.Id,
                    Session = item.Session,
                    IsActive = item.IsActive
                });
            }
            return List;
        }

        public IList<ExamViewModel> GetExamList()
        {

            List<ExamViewModel> List = new List<ExamViewModel>();
            var exmList = _context.AcrExams.ToList();
            foreach (var item in exmList)
            {

                List.Add(new ExamViewModel()
                {
                    Id = item.Id,
                    ExamId = item.Id,
                    ExamName = item.ExamName

                });
            }
            return List;
        }

        public IList<GroupViewModel> GetGroupList()
        {

            List<GroupViewModel> List = new List<GroupViewModel>();
            var grpList = _context.AcrGroups.ToList();
            foreach (var item in grpList)
            {

                List.Add(new GroupViewModel()
                {
                    Id = item.Id,
                    GroupName = item.GroupName

                });
            }
            return List;
        }

        public IList<BoardViewModel> GetBoardList()
        {

            List<BoardViewModel> List = new List<BoardViewModel>();
            var brdList = _context.AcrBoards.ToList();
            foreach (var item in brdList)
            {

                List.Add(new BoardViewModel()
                {
                    Id = item.Id,
                    BoardName = item.BoardName

                });
            }
            return List;
        }


    }
}
