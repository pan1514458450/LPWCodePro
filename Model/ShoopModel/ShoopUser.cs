using Microsoft.EntityFrameworkCore;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ShoopModel
{
    public class ShoopUser: SoureModel
    {
        
        public int UserId { get;set; }
        public int ShoopId { get;set; }
        [Comment("价钱比例")]
        public double PriceProportion { get;set; }
    }
}
