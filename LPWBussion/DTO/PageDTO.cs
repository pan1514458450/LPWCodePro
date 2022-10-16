using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.DTO
{
    public class PageDTO:SgParantDTO
    {
        /// <summary>
        /// 每页数
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageIndex { get; set; }
    }
}
