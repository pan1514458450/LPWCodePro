using Microsoft.EntityFrameworkCore;
using Model.UserModel;

namespace Model
{
    public sealed class DbContextModule : DbContext
    {
        public DbSet<SysAdminUsers> SysAdminUsers { get; set; }
        public DbSet<SysRoles> SysRoles { get; set; }
        public DbSet<SysRoleMenus> SysRoleMenus { get; set; }
        public DbSet<SysMenus> sysMenus { get; set; }
        public DbContextModule(DbContextOptions<DbContextModule> options) : base(options)
        {
        }


    }
}
