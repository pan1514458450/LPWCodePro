using LPWBussion.DTO.SysDTO;
using LPWBussion.DTO.SysShoopDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.ShoopBussion
{
    public interface IShoopInter
    {
        #region MyRegion
        Task<bool> CreatShoopType(string TypeName);
        Task<bool> UpdateShoopType(int Id, string TypeName);
        Task<bool> DeleteShoopType(int Id);
        Task<List<ShoopTypeDTO>> ReadShoopType(string TypeName, int Size, int Index);

      
        #endregion
        #region shoop
        /// <summary>
        /// 这里吧IFormFile清口 
        /// </summary>
        /// <param name="shoopInfo"></param>
        /// <param name="arr">文件路劲</param>
        /// <returns></returns>
        Task<bool> CreateShoop(ShoopInfoDTO shoopInfo, string[] arr);
        /// <summary>
        /// 这里吧IFormFile清口 
        /// </summary>
        /// <param name="shoopInfo"></param>
        /// <param name="arr">文件路劲</param>
        /// <returns></returns>
        Task<bool> UpdateShoop(ShoopInfoDTO shoopInfo, string[] arr);
        Task<bool> DeleteShoop(int Id);
        Task<List<ShoopUserListDTO>> GetShoopList(string ShoopName, int ShoopTypeid,int index, int page);
        Task<bool> SetOrUpdateUserShoop(List<SetOrUpdateUserShoopDTO> setOrUpdateUsers);
        Task<bool> DeleteUserShoop(int id);
        #endregion
    }
}
