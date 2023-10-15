using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.GroupUser;
using System.Linq;

namespace EPS.Service.Profiles
{
    public class GroupUserDtoToEntity : Profile
    {
        public GroupUserDtoToEntity()
        {
            CreateMap<GroupUserCreateDto, GroupUser>();
            CreateMap<GroupUserUpdateDto, GroupUser>();
        }
    }

    public class GroupUserEntityToDto : Profile
    {
        public GroupUserEntityToDto()
        {
            CreateMap<GroupUser, GroupUserCreateDto>();
            CreateMap<GroupUser, GroupUserDetailDto>(); ;
            CreateMap<GroupUser, GroupUserGridDto>(); ;

        }
    }
}
