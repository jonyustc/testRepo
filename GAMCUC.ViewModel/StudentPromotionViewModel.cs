using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.ViewModel
{
    public class StudentPromotionViewModel
    {
        public int Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public int StdID { get; set; }
        public int SemesterId { get; set; }
        public int PSemesterId { get; set; }
        public bool? IsActive { get; set; }

        public bool IsPromotion { get; set; }
    }
}
