using Microsoft.EntityFrameworkCore;
using Model.ShoopModel;
using Model.UserModel;

namespace Model
{
    public sealed class DbContextModule : DbContext
    {
        public DbSet<SysAdminUsers> SysAdminUsers { get; set; }
        public DbSet<SysRoles> SysRoles { get; set; }
        public DbSet<SysRoleMenus> SysRoleMenus { get; set; }
        public DbSet<SysMenus> sysMenus { get; set; }
        public DbSet<Shoop> shoops { get; set; }
        public DbSet<ShoopType> shoopsType { get; set; }
        public DbSet<ShoopUser> shoopUsers { get; set; }
        public DbSet<ShoopCard>  shoopCards { get; set; }
        public DbSet<ShoopCardType>  shoopCardTypes { get; set; }
        public DbContextModule(DbContextOptions<DbContextModule> options) : base(options)
        {
        }


    }
}
