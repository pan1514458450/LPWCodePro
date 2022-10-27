using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Model.Migrations
{
    public partial class i1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shoopCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoopId = table.Column<int>(type: "int", nullable: false),
                    CardNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoopCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shoopCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoopCardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shoops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoopName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "商品名称"),
                    Introduce = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "商品介绍"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "价钱"),
                    ShoopTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "商品标签"),
                    NumBerWarn = table.Column<int>(type: "int", nullable: false, comment: "库存预警"),
                    IsWarn = table.Column<int>(type: "int", nullable: false, comment: "预警开关"),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "商户id"),
                    Url1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shoopsType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoopsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shoopUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShoopId = table.Column<int>(type: "int", nullable: false),
                    PriceProportion = table.Column<double>(type: "float", nullable: false, comment: "价钱比例"),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shoopUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysAdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "用户账号"),
                    PassWord = table.Column<string>(type: "varchar(64)", nullable: false, comment: "用户密码"),
                    ParantId = table.Column<int>(type: "int", nullable: false, comment: "父id"),
                    RoleId = table.Column<int>(type: "int", nullable: false, comment: "权限Id"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "用户名"),
                    Wallet = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "钱包余额"),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysAdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sysMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParantId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sysMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRoleMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoleMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRoles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shoopCards");

            migrationBuilder.DropTable(
                name: "shoopCardTypes");

            migrationBuilder.DropTable(
                name: "shoops");

            migrationBuilder.DropTable(
                name: "shoopsType");

            migrationBuilder.DropTable(
                name: "shoopUsers");

            migrationBuilder.DropTable(
                name: "SysAdminUsers");

            migrationBuilder.DropTable(
                name: "sysMenus");

            migrationBuilder.DropTable(
                name: "SysRoleMenus");

            migrationBuilder.DropTable(
                name: "SysRoles");
        }
    }
}
