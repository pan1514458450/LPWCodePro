using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.UserModel
{
    public class SysAdminUsers : SoureModel
    {
        
        [Comment("用户账号")]
        public string Email { get; set; }
        [Column(TypeName = ("varchar(64)"))]
        [Comment("用户密码")]
        public string PassWord { get; set; }
        [Comment("父id")]
        public int ParantId { get; set; }
        [Comment("权限Id")]
        public int RoleId { get; set; }
        [Comment("用户名")]
        public string UserName { get; set; }
        [Comment("钱包余额")]
        public decimal Wallet { get; set; }
    }
}
