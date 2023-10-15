using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EPS.Data.Entities;
using EPS.Service.Dtos.Log;
using System.Linq;
using System.ComponentModel;

namespace EPS.Service.Profiles
{
    public class LogDtoToEntity : Profile
    {
        public LogDtoToEntity()
        {
            CreateMap<LogCreateDto, Log>();
            CreateMap<LogUpdateDto, Log>();           
        }
    }

    public class LogEntityToDto : Profile
    {
        public LogEntityToDto()
        {
            CreateMap<Log, LogGridDto>();
            CreateMap<Log, LogCreateDto>();
            CreateMap<Log, LogDetailDto>();
        }

        public static string GetDescriptionEnumValue<T>(int Val)
        {
            try
            {
                string stringKey = Enum.GetName(typeof(T), Val);
                var enumType = typeof(T);
                var memberInfos = enumType.GetMember(stringKey);
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
                var valueAttributes =
                      enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)valueAttributes[0]).Description;
                return description;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string GetDescriptionEnumName<T>(string KeyName)
        {
            try
            {
                var enumType = typeof(T);
                var memberInfos = enumType.GetMember(KeyName);
                var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
                var valueAttributes =
                      enumValueMemberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = ((DescriptionAttribute)valueAttributes[0]).Description;
                return description;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
