using LPWBussion.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.AutoMapperFile
{
    public sealed class UserClaimsMethods : IUserClaimsMethods
    {
        private readonly IHttpContextAccessor httpContext;
        public UserClaimsMethods(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }
        #region cailms
        public ClaimDTO GetClaims()
        {
            var cliaimDTO = new ClaimDTO();
            var claimdic= httpContext.HttpContext.User.Claims.ToDictionary(d=>d.Type,d=>d.Value);
            foreach (var item in claimdic)
            {
               var type=  cliaimDTO.GetType().GetProperty(item.Key);
                if (type != null)
                {
                    type.SetValue(cliaimDTO, item.Value, null);
                }
            }
            return cliaimDTO;
        }
        #endregion
    }
}
