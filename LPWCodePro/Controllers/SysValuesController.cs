using LPWBussion.DTO;
using LPWBussion.DTO.SysDTO;
using LPWBussion.MailkitEmail;
using LPWBussion.SysBussion;
using LPWService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LPWCodePro.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SysValuesController : ControllerBase
    {
        private readonly ISysBussionService _sysBussionService;
        private readonly ISendEmail _sendemail;
        public SysValuesController(ISysBussionService sysBussionService, ISendEmail sendemail)
        {
            _sysBussionService = sysBussionService;
            _sendemail = sendemail;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<object> GetUs()
        {
          return await  _sysBussionService.GetUser("1514458450@qq.com", "");
        } 
        [HttpGet]
        [Authorize]
        public async Task SendEmial()
        {
            await _sendemail.MySendEmail(GetClaims());

        }
        
        [HttpPost]
        public async Task<ResultCode> UpdateUser(UpdateUserDTO updateUser)
        {
            return ResponeResultCode(await _sysBussionService.UpdateUser(updateUser,GetClaims()));
        }
        [HttpPost]
        public async Task<ResultCode> CreateUser(CreateUserDTO updateUser)
        {
            return ResponeResultCode(await _sysBussionService.CreateUser(updateUser, GetClaims()));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ResultCode> login(SysLoginDTO sysLoginDTO)
        {
            return await _sysBussionService.Login(sysLoginDTO);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<FileContentResult> GetCatpImg(string key)
        {
            var result = await _sysBussionService.GenerateCaptchaImageAsync(key);
            return File(result.CaptchaMemoryStream.ToArray(), "image/png");
        }
        [Authorize]
        [HttpPost]
        public async Task<ResultCode> GetInfo()
        {
            return new ResultCode() { data = await _sysBussionService.GetMyMenu(GetClaims()) };
        }

        [Authorize]
        [HttpPost]
        public async Task<ResultCode> CreateRole(SysRoleDTO sysRole)
        {
            return ResponeResultCode(await _sysBussionService.CreateRole(sysRole));

        }

        [Authorize]
        [HttpPost]
        public async Task<ResultCode> UpdateRole(UpdateSysRoleDTO roleDTO)
        {
            return ResponeResultCode(await _sysBussionService.UpdateRole(roleDTO));
        }
        [Authorize]
        [HttpGet]
        public async Task<ResultCode> GetRole()
        {
            return new ResultCode() { data = await _sysBussionService.GetRole(GetClaims()) };
        }
        [Authorize]
        [HttpGet]
        public async Task<ResultCode> DeleteRole(string id)
        {

            return ResponeResultCode(await _sysBussionService.DeleteRole(id));

        }
        [Authorize]
        [HttpPost]
        public async Task<ResultCode> CreateMenu(SysMenuDTO sysMenu)
        {
            return ResponeResultCode(await _sysBussionService.CreateMenu(sysMenu));
        }
        [Authorize]
        [HttpPost]
        public async Task<ResultCode> UpdateMenu(SysMenuDTO sysMenu)
        {
            return ResponeResultCode(await _sysBussionService.UpdateMenu(sysMenu));
        }
        [Authorize]
        [HttpPost]
        public async Task<ResultCode> DeleteMenu(int id)
        {
            return ResponeResultCode(await _sysBussionService.DeleteMenu(id));
        }

        private ResultCode ResponeResultCode(bool status)
        {
            var result = new ResultCode() { Status = status };
            result.Message = result.Status ? ConstCode.Success : ConstCode.Error;
            return result;
        }
        private string GetClaims()
        {
            return HttpContext.User.Claims.First().Value;

        }

    }
}
