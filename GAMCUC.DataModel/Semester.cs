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
    
    public partial class Semester
    {
        public Semester()
        {
            this.Payments = new HashSet<Payment>();
            this.Students = new HashSet<Student>();
            this.StudentSemesterMappings = new HashSet<StudentSemesterMapping>();
        }
    
        public int Id { get; set; }
        public string SemesterName { get; set; }
    
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<StudentSemesterMapping> StudentSemesterMappings { get; set; }
    }
}
