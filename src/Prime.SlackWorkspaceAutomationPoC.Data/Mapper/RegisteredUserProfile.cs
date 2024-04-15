using AutoMapper;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.RegisteredUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Mapper
{
    public class RegisteredUserProfile : Profile
    {
        public RegisteredUserProfile()
        {
            CreateMap<RegisteredUserRequestDto, RegisteredUser>().ReverseMap();
            CreateMap<RegisteredUser, RegisteredUserResponseDto>().ReverseMap();
        }
    }
}
