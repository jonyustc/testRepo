using GAMCUC.IDAL;
using GAMCUC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMCUC.BLL
{
    public class bllStudentPromotion : IStudentPromotion
    {
        IDAL.IStudentPromotion _iPromotion = null;

        public bllStudentPromotion()
        {
            _iPromotion = new DAL.dalStudentPromotion();
        }

        public void Promotion(List<StudentPromotionViewModel> model, int PSemesterId)
        {
            _iPromotion.Promotion(model, PSemesterId);
        }
    }
}
