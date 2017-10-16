//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAMCUC.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class AcademicRecord
    {
        public int Id { get; set; }
        public Nullable<int> ExamId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> BoardId { get; set; }
        public Nullable<int> YearOfPassing { get; set; }
        public string RollOfExam { get; set; }
        public string Grade { get; set; }
        public Nullable<System.Guid> StudentId { get; set; }
    
        public virtual AcrBoard AcrBoard { get; set; }
        public virtual AcrExam AcrExam { get; set; }
        public virtual AcrGroup AcrGroup { get; set; }
        public virtual Student Student { get; set; }
    }
}