using AutoMapper;
using LPWBussion.DTO.SysDTO;
using LPWBussion.DTO.SysShoopDTO;
using Model.ShoopModel;
using Model.UserModel;

namespace LPWBussion.AutoMapperFile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SysAdminUsers, SysLoginDTO>().ReverseMap();
            CreateMap<SysMenus, SysMenuDTO>().ReverseMap();
            CreateMap<SysRoles, SysRoleDTO>().ForMember(d => d.RoleQuery, c => c.MapFrom(s => s.Id));
            CreateMap<UpdateSysRoleDTO, SysRoles>().ForMember(d => d.Id, c => c.MapFrom(s => s.RoleQuery));
            CreateMap<CreateUserDTO, SysAdminUsers>();
            CreateMap<ShoopType, ShoopTypeDTO>();
            CreateMap<Shoop, ShoopInfoDTO>().ReverseMap();

        }
    }
}
