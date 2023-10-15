using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Role;
using System.Linq;

namespace EPS.Service.Profiles
{
    public class RoleDtoToEntity : Profile
    {
        public RoleDtoToEntity()
        {
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
        }
    }

    public class RoleEntityToDto : Profile
    {
        public RoleEntityToDto()
        {
            CreateMap<Role, RoleGridDto>();
            CreateMap<Role, RoleDetailDto>();
        }
    }
}
