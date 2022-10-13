using LPWService.StaticFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.DTO.SysDTO
{
    public class CreateUserDTO:UpdateUserDTO
    {
        private string? _RoleQuery;
        [Required(ErrorMessage ="不能为空")]
        public string RoleQuery
        {
            get;
            set;
        }
        [EmailAddress(ErrorMessage = "邮箱格式错误")]
        public string Email { get; set; }
    }
}
