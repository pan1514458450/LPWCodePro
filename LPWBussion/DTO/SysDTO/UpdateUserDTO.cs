using LPWService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.DTO.SysDTO
{
    public class UpdateUserDTO:SgParantDTO
    {
        [StringLength(maximumLength:32,MinimumLength =32,ErrorMessage ="密码错误")]
        public string Password { get; set; }
        [MaxLength(ConstCode.VerificationCode,ErrorMessage ="验证码错误")]
        public string EmailCode { get; set; }
    }
}
