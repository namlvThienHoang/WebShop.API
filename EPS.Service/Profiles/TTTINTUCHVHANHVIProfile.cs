using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.TTTINTUC;
using System.Linq;
using EPS.Service.Dtos.TTTINTUCHVHANHVI;

namespace EPS.Service.Profiles
{
    public class TTTINTUCHVHANHVIDtoToEntity : Profile
    {
        public TTTINTUCHVHANHVIDtoToEntity()
        {
            CreateMap<TTTINTUCHVHANHVICreateDto, TTTINTUCHVHANHVI>();
            CreateMap<TTTINTUCHVHANHVIUpdateDto, TTTINTUCHVHANHVI>();
        }
    }

    public class TTTINTUCHVHANHVIEntityToDto : Profile
    {
        public TTTINTUCHVHANHVIEntityToDto()
        {   
            CreateMap<TTTINTUCHVHANHVI, TTTINTUCHVHANHVIDetailDto>();
            CreateMap<TTTINTUCHVHANHVI, TTTINTUCHVHANHVIGridDto>();            
            CreateMap<TTTINTUCHVHANHVI, TTTINTUCHVHANHVIGridDto>()
                .ForMember(dest => dest.ID_TACNHAN, mo => mo.MapFrom(src => src.TTTINTUCHVHANHVI_ID_TACNHAN.TITLE));
            //Người dung ẩn danh
            CreateMap<TTTINTUCHVHANHVI, TTTINTUCHVHANHVIGridDto>()
                .ForMember(dest => dest.ID_TACNHAN, mo => mo.MapFrom(src => src.TTTINTUCHVHANHVI_ID_TACNHAN.TITLE))
                ;
        }
    }
}
