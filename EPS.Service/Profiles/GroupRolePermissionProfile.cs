using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.GroupRolePermission;
using System.Linq;

namespace EPS.Service.Profiles
{
    public class GroupRolePermissionDtoToEntity : Profile
    {
        public GroupRolePermissionDtoToEntity()
        {
            CreateMap<GroupRolePermissionCreateDto, GroupRolePermission>();
            CreateMap<GroupRolePermissionUpdateDto, GroupRolePermission>();
        }
    }

    public class GroupRolePermissionEntityToDto : Profile
    {
        public GroupRolePermissionEntityToDto()
        {
            CreateMap<GroupRolePermission, GroupRolePermissionDetailDto>(); ;
            CreateMap<GroupRolePermission, GroupRolePermissionGridDto>()
                .ForMember(dest => dest.PermissionCode, mo => mo.MapFrom(src => src.Permission.Code))
                ;

        }
    }
}
