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
    
    public partial class AcrBoard
    {
        public AcrBoard()
        {
            this.AcademicRecords = new HashSet<AcademicRecord>();
        }
    
        public int Id { get; set; }
        public string BoardName { get; set; }
    
        public virtual ICollection<AcademicRecord> AcademicRecords { get; set; }
    }
}
