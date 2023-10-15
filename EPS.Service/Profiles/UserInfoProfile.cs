using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.UserInfo;
using System.Linq;

namespace EPS.Service.Profiles
{
    public class UserInfoDtoToEntity : Profile
    {
        public UserInfoDtoToEntity()
        {
            CreateMap<UserInfoCreateDto, UserDetail>();
            CreateMap<UserInfoUpdateDto, UserDetail>();
        }
    }

    public class UserInfoEntityToDto : Profile
    {
        public UserInfoEntityToDto()
        {
            CreateMap<UserDetail, UserInfoDetailDto>(); ;

        }
    }
}
