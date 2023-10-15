using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Group;
using System.Linq;

namespace EPS.Service.Profiles
{
    public class GroupDtoToEntity : Profile
    {
        public GroupDtoToEntity()
        {
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();           
        }
    }

    public class GroupEntityToDto : Profile
    {
        public GroupEntityToDto()
        {
            CreateMap<Group, GroupGridDto>();
            CreateMap<Group, GroupDetailDto>();
        }
    }
}
