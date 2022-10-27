using Microsoft.EntityFrameworkCore;
using Model.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ShoopModel
{
    public class Shoop : SoureModel
    {
        [Comment("商品名称")]
        public string ShoopName { get; set; }
        [Comment("商品介绍")]
        public string? Introduce { get; set; }
        //[Comment("库存")]
        //public int Number { get; set; }
        [Comment("价钱")]
        public decimal Price { get; set; }
        [Comment("商品标签")]
        public string ShoopTypeId { get; set; }
        [Comment("库存预警")]
        public int NumBerWarn { get; set; }
        [Comment("预警开关")]
        public int IsWarn { get; set; }
        //[Comment("已售数量")]
        //public int SellQuantity { get; set; }
        [Comment("商户id")]
        public int UserId { get; set; }
        public string Url1 { get; set; }
        public string? Url2 { get; set; }
        public string? Url3 { get; set; }
    }
}
