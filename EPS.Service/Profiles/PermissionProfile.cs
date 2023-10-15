using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Permission;
using System.Linq;

namespace EPS.Service.Profiles
{
    public class PermissionDtoToEntity : Profile
    {
        public PermissionDtoToEntity()
        {
            CreateMap<PermissionCreateDto, Permission>();
            CreateMap<PermissionUpdateDto, Permission>();           
        }
    }

    public class PermissionEntityToDto : Profile
    {
        public PermissionEntityToDto()
        {
            CreateMap<Permission, PermissionGridDto>();
            CreateMap<Permission, PermissionDetailDto>();
        }
    }
}
