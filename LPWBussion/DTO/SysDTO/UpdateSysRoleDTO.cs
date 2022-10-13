using LPWService.StaticFile;
using System.ComponentModel.DataAnnotations;

namespace LPWBussion.DTO.SysDTO
{
    public class UpdateSysRoleDTO
    {
        private string? _RoleQuery;
        [Required(ErrorMessage = "ID不能为空")]
        public string RoleQuery
        {
            get
            {
                return _RoleQuery.Sha256Decrypto();
            }
            set { _RoleQuery = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        public string RoleName { get; set; }
        public string? Mark { get; set; }
    }
}
