using LPWBussion.DTO.SysShoopDTO;
using LPWBussion.ShoopBussion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LPWCodePro.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoopController : ControllerBase
    {
        private readonly IShoopInter _shoop;
        public ShoopController(IShoopInter shoop)
        {
            _shoop = shoop;
        }
        
        [HttpGet]
        public async Task<List<ShoopUserListDTO>> GetShoopList(string? ShoopName,int ShoopTypeId=0,int page=0, int index = 10)
        {
            return await _shoop.GetShoopList(ShoopName, ShoopTypeId,index,page);
        }
    }
}
