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
    
    public partial class OriginalDocument
    {
        public OriginalDocument()
        {
            this.StudentOriginalDocuments = new HashSet<StudentOriginalDocument>();
        }
    
        public int Id { get; set; }
        public string DocumentName { get; set; }
    
        public virtual ICollection<StudentOriginalDocument> StudentOriginalDocuments { get; set; }
    }
}
