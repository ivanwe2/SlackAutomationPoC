using AutoMapper;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.RegisteredUserDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.TechnologyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Mapper
{
    public class TechnologyProfile : Profile
    {
        public TechnologyProfile()
        {
            CreateMap<TechnologyRequestDto, Technology>().ReverseMap();
            CreateMap<Technology, TechnologyResponseDto>().ReverseMap();
        }
    }
}
