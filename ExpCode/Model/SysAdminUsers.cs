using System.ComponentModel.DataAnnotations.Schema;

namespace Model.UserModel
{
    public class SysAdminUsers 
    {
        
        public string Email { get; set; }
        public string PassWord { get; set; }
        public int ParantId { get; set; }
        public int RoleId { get; set; }
    }
}
