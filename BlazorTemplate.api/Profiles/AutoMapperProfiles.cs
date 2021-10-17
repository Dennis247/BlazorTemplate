using AutoMapper;
using BlazorTemplate.api.Context;
using Entities.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTemplate.api.Profiles
{
    public class AutoMapperProfiles : Profile
    {
      public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }

       
    }
}
