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
    
    public partial class PaymentType
    {
        public PaymentType()
        {
            this.PaymentDetails = new HashSet<PaymentDetail>();
        }
    
        public int Id { get; set; }
        public string PaymentType1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ICollection<PaymentDetail> PaymentDetails { get; set; }
    }
}
