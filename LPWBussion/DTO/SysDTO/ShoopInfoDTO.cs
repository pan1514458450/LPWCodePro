using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.DTO.SysDTO
{
    public class ShoopInfoDTO
    {
        public List<IFormFile> files { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ShoopName { get; set; }
        /// <summary>
        /// 商品介绍
        /// </summary>
        public string? Introduce { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 商品标签
        /// </summary>
        public string ShoopTypeId { get; set; }
        /// <summary>
        /// 库存预警
        /// </summary>
        public int NumBerWarn { get; set; }
        /// <summary>
        /// 预警开关
        /// </summary>
        public int IsWarn { get; set; }
        public int Id { get; set; }
    }
}
