using AutoMapper;
using LPWBussion.DTO;
using LPWBussion.DTO.SysDTO;
using LPWService;
using LPWService.BaseRepostiory;
using LPWService.StaticFile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Model.UserModel;
using SqlSugar;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace LPWBussion.SysBussion
{
    public sealed class SysBussionService : ISysBussionService
    {
        private readonly IUnitWorkRepository _unitAdmin;
        private readonly IMapper mapper;
        private readonly ICsredisHelp csredis;
        private readonly ISqlSugarRepository sugarRepository;
        private readonly IAdoSql _dapper;
        public SysBussionService(IUnitWorkRepository unitAdmin, IMapper mapper,
            ICsredisHelp csredis, ISqlSugarRepository sugarRepository, IAdoSql dapper)
        {
         
            _unitAdmin = unitAdmin;
            this.mapper = mapper;
            this.csredis = csredis;
            this.sugarRepository = sugarRepository;
            _dapper = dapper;
        }
        public async Task<ResultCode> Login(SysLoginDTO sysLogin)
        {
            ResultCode Coderesult = new ResultCode();
            var result = await _unitAdmin.GetFirstAsync<SysAdminUsers>(x =>
            x.Email == sysLogin.Email && x.IsDelete == 0 && x.PassWord == sysLogin.PassWord);
            if (result == null)
            {
                Coderesult.Message = "账号或密码错误";
                Coderesult.Status = false;
            }
            else
            {
                Coderesult.Message = Token(result.Email,result.Id);
            }
            return Coderesult;
        }


        #region 验证码
        private const string Letters = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";

        private string CatpCode(int leg, string[] array, StringBuilder sb, Random random1)
        {
            for (int i = 0; i < leg; i++)
            {
                sb.Append(array[random1.Next(array.Length - 1)]);
            }
            if (csredis.CheckKey(sb.ToString()).Result)
            {
                sb.Clear();
                return CatpCode(leg, array, sb, random1);
            }
            else
            {
                return sb.ToString();
            }

        }
        public Task<CaptchaResult> GenerateCaptchaImageAsync(string key, int leg = ConstCode.VerificationCode, int width = 0, int height = 30)
        {
            var array = Letters.Split(new[] { ',' });
            StringBuilder sb = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < leg; i++)
            {
                sb.Append(array[random.Next(array.Length - 1)]);
            }
            csredis.SetRedis(key, sb.ToString(), 300);
            var captchaCode = sb.ToString();
            //验证码颜色集合
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };

            //定义图像的大小，生成图像的实例
            var image = new Bitmap(width == 0 ? captchaCode.Length * 25 : width, height);

            var g = Graphics.FromImage(image);

            //背景设为白色
            g.Clear(Color.White);

            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(image.Width);
                var y = random.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //验证码绘制在g中
            for (var i = 0; i < captchaCode.Length; i++)
            {
                //随机颜色索引值
                var cindex = random.Next(c.Length);

                //随机字体索引值
                var findex = random.Next(fonts.Length);

                //字体
                var f = new Font(fonts[findex], 15, FontStyle.Bold);

                //颜色  
                Brush b = new SolidBrush(c[cindex]);

                var ii = 4;
                if ((i + 1) % 2 == 0)
                    ii = 2;

                //绘制一个验证字符  
                g.DrawString(captchaCode.Substring(i, 1), f, b, 17 + i * 17, ii);
            }

            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);

            g.Dispose();
            image.Dispose();

            return Task.FromResult(new CaptchaResult
            {
                CaptchaCode = captchaCode,
                CaptchaMemoryStream = ms,
                Timestamp = DateTime.Now
            });
        }

        #endregion

        #region 私有方法

        private string Token(string email,int id)
        {
            var claims = new[] { new Claim("Email", email),new Claim("Id",id.ToString().Sha256Encrypto()) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstCode.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                issuer: ConstCode.ValidIssuer,
                audience: ConstCode.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(ConstCode.Expires),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        private void Tree(List<SysMenuDTO> list, int parantId, ref SysMenuDTO sysMenu)
        {
            var result = list.Where(x => x.ParantId == parantId);
            foreach (var item in result)
            {
                Tree(list, item.Id, ref sysMenu);
                sysMenu.Childe = item;

            }
        }

        #endregion
        public async Task<List<SysMenuDTO>> GetMyMenu(string email)
        {
            var dbresult =await _unitAdmin          
                .JoinTableAsync<SysMenus, SysRoleMenus, SysAdminUsers>
                ((x, c) => x.Id == c.Id&&c.IsDelete==0, (x, c, d) => c.RoleId == d.RoleId&&d.IsDelete==0)
                .Where(x=> x.IsDelete==0).ToListAsync();
            var result = mapper.Map<List<SysMenuDTO>>(dbresult);
            var sysmenudto = new SysMenuDTO();
            var sysmenudtoList = new List<SysMenuDTO>();
            result.Where(d => d.ParantId == 0).ToList().ForEach(x =>
            {
                sysmenudto.UserName = email;
                Tree(result, 0, ref sysmenudto);
                sysmenudtoList.Add(sysmenudto);
                sysmenudto = new SysMenuDTO();
            });
            return sysmenudtoList;
        }

        public async Task<bool> CreateRole(SysRoleDTO sysRole)
        {
            var dbsysrole = mapper.Map<SysRoles>(sysRole);
            return await _unitAdmin.AddAsync(dbsysrole) > 0 ? true : false;
        }

        public async Task<bool> UpdateRole(UpdateSysRoleDTO sysRole)
        {
            var dbsysrole = mapper.Map<SysRoles>(sysRole);

            return await sugarRepository.UpdateAsync(dbsysrole, x => new { x.UpdateTime, x.RoleName, x.Mark }) > 0 ? true : false;
        }

        public async Task<List<SysRoleDTO>> GetRole(string email)
        {
            var dbresult = await (from i in _unitAdmin.Get<SysRoles>()
                                  where _unitAdmin.Get<SysAdminUsers>().Where(d => d.Email == email).
                                  Select(d => d.RoleId).First() <= i.Id && i.IsDelete == 0
                                  select i).ToListAsync();

            return mapper.Map<List<SysRoleDTO>>(dbresult);
        }

        public async Task<bool> DeleteRole(string id)
        {
            var intid = int.Parse(id.Sha256Decrypto());
            return await sugarRepository.DeleteAsync<SysRoles>(intid) > 0 ? true : false;
        }

        public async Task<bool> CreateMenu(SysMenuDTO sysMenu)
        {

            var dbmenu = mapper.Map<SysMenus>(sysMenu);
            return await _unitAdmin.AddAsync(dbmenu) > 0 ? true : false;
        }

        public async Task<bool> UpdateMenu(SysMenuDTO sysMenu)
        {
            var dbmenu = mapper.Map<SysMenus>(sysMenu);
            return await sugarRepository.UpdateAsync(dbmenu, x =>
            new { x.UpdateTime, x.MenuName, x.MenuUrl, x.Icon, x.ParantId }) > 0 ? true : false;
        }
        public async Task<bool> DeleteMenu(int Id)
        {
            return await sugarRepository.DeleteAsync<SysMenus>(Id) > 0 ? true : false;
        }

        public async Task<bool> UpdateUser(UpdateUserDTO userDTO,string email)
        {
            var codeemail=await csredis.GetDeleteRedis<string>(email);

            if (codeemail != userDTO.EmailCode) return false;
            var dicemail = nameof(SysAdminUsers.Email);
            var dic = new Dictionary<string, object>();
            dic.Add(nameof(SysAdminUsers.PassWord),userDTO.Password);
            dic.Add(dicemail,email);
            dic.Add(nameof(SoureModel.UpdateTime), DateTime.Now);
          return await  sugarRepository.UpdateAsync<SysAdminUsers>(dic, dicemail) > 0 ? true : false;
        }
        public async Task<bool> CreateUser(CreateUserDTO userDTO, string email)
        {
            var codeemail = await csredis.GetDeleteRedis<string>(email);

            if (codeemail != userDTO.EmailCode) return false;
            var IsEmail=await _unitAdmin.GetFirstAsync<SysAdminUsers>(x => x.Email == userDTO.Email);
            if (IsEmail is null) return false;
            var myuser=await _unitAdmin.GetFirstAsync<SysAdminUsers>(d => d.Email == email);
            var dbadmin = mapper.Map<SysAdminUsers>(userDTO);
            dbadmin.ParantId = myuser.Id;
            dbadmin.RoleId =int.Parse( userDTO.RoleQuery.Sha256Decrypto());
            return await _unitAdmin.AddAsync(dbadmin)>0?true:false;
        }

        public Task<bool> DeleteUser(string ToEmail, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SysAdminUsers>> GetUser(string ToEmail, string email)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" with sysadmin as  (  select  * from SysAdminUsers ");
            sb.Append(" where Email=@email union all ");
            sb.Append("  select  G.* from SysAdminUsers inner join SysAdminUsers as G on SysAdminUsers.Id=G.ParantId where IsDelete=0) ");
            sb.Append($"  select  * from sysadmin {(email.IsNull()? " where Email=@Toemail":"")}   w order by  CreateDateTime desc ");
            List<SqlParameter> para=new List<SqlParameter>();
            para.Add(new SqlParameter("@email", ToEmail));

            if (email.IsNull())
            {
                para.Add(new SqlParameter("@Toemail", email));
            }
            return await _dapper.GetAllAsync<SysAdminUsers>(sb.ToString(), para);
        }
    }
}
