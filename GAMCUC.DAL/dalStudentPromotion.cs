using GAMCUC.DataModel;
using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.DAL
{
    public class dalStudentPromotion : IStudentPromotion
    {
         private GaMCucDbEntities _context;

         public dalStudentPromotion()
        {
            _context = new GaMCucDbEntities();
        }

         public void Promotion(List<StudentPromotionViewModel> model, int PSemesterId)
         {
             foreach (var item in model)
             {
                 var currStd = _context.StudentSemesterMappings
                     .Where(x => x.StudentId == item.StudentId
                         && x.SemesterId == item.SemesterId 
                         && x.IsActive == item.IsActive).FirstOrDefault();


                 if(item.IsPromotion==true && currStd!=null)
                 {
                     currStd.IsActive = false;
                     _context.Entry(currStd).State = EntityState.Modified;
                     _context.SaveChanges();

                     if(currStd.SemesterId != PSemesterId)
                     {
                         var proStd = new StudentSemesterMapping();
                         proStd.StudentId = item.StudentId;
                         proStd.SemesterId = PSemesterId;
                         proStd.IsActive = true;
                         _context.StudentSemesterMappings.Add(proStd);
                         _context.SaveChanges();
                     }
                 }

             }
         }
    }
}
