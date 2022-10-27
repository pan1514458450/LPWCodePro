using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.DTO.SysShoopDTO
{
    public class ShoopUserListDTO
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ShoopName { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 商品描叙
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NumBerWarn { get; set; }

        public int IsWarn { get; set; }
        public string Url1 { get; set; }
        public string Url2 { get; set; }
        public string Url3 { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int inventoryCard { get; set; }
        public int disableCard { get; set; }
        public int sellCard { get; set; }
    }
}
