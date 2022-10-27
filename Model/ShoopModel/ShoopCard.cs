using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ShoopModel
{
    public class ShoopCard:SoureModel
    {
        public int ShoopId { get; set; }
        public string CardNO { get; set; }
        public int CardTypeId { get; set; }
    }
}
