using AutoMapper;
using TempaWeb.api.Context;
using Entities.DTO.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TempaWeb.api.Profiles
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
