using LPWService.StaticFile;
using System.ComponentModel.DataAnnotations;

namespace LPWBussion.DTO.SysDTO
{
    public class SysRoleDTO : SgParantDTO
    {
        private string? _RoleQuery;
        public string RoleQuery
        {
            get
            {
                return _RoleQuery is null ? string.Empty : _RoleQuery.Sha256Encrypto();
            }
            set { _RoleQuery = value; }
        }
        [Required(ErrorMessage = "不能为空")]
        public string RoleName { get; set; }
        public string? Mark { get; set; }
    }
}
