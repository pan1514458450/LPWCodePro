using LPWBussion.DTO;
using LPWBussion.DTO.SysDTO;
using Model.UserModel;

namespace LPWBussion.SysBussion
{
    public interface ISysBussionService
    {
        Task<List<SysAdminUsers>> GetUser(string ToEmail, string email);
        Task<bool> UpdateUser(UpdateUserDTO userDTO, string email);
        Task<bool> DeleteUser(string ToEmail, string email);
        //Task<bool> GetUser(string ToEmail, string email);
        Task<bool> CreateUser(CreateUserDTO userDTO, string email);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sysLogin"></param>
        /// <returns></returns>
        Task<ResultCode> Login(SysLoginDTO sysLogin);
        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="leg"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Task<CaptchaResult> GenerateCaptchaImageAsync(string key, int leg = 4, int width = 0, int height = 30);
        /// <summary>
        /// 根据token获取权限树
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<List<SysMenuDTO>> GetMyMenu(string email);

        Task<bool> CreateRole(SysRoleDTO sysRole);
        Task<bool> UpdateRole(UpdateSysRoleDTO sysRole);
        Task<List<SysRoleDTO>> GetRole(string email);
        Task<bool> DeleteRole(string id);

        Task<bool> CreateMenu(SysMenuDTO sysMenu);
        Task<bool> UpdateMenu(SysMenuDTO sysMenu);
        Task<bool> DeleteMenu(int Id);

    }
}
