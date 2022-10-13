using LPWService.ModelValidation;
using System.ComponentModel.DataAnnotations;

namespace LPWBussion.DTO.SysDTO
{
    public class SysLoginDTO : SgParantDTO
    {
        [EmailAddress(ErrorMessage = "邮箱地址错误")]
        public string Email { get; set; }
        [StringLength(maximumLength: 32, MinimumLength = 32, ErrorMessage = "密码格式错误")]
        public string PassWord { get; set; }
        /// <summary>
        /// 验证码格式位 你的值 +,+验证码
        /// </summary>
        [RedisKeyValiddation(ErrorMessage = "验证码错误")]
        public string CapCode { get; set; }

    }
}
