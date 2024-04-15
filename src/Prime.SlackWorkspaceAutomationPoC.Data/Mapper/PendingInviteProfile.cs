using AutoMapper;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.TechnologyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Mapper
{
    public class PendingInviteProfile : Profile
    {
        public PendingInviteProfile()
        {
            CreateMap<PendingInviteRequestDto, PendingInvite>().ReverseMap();
            CreateMap<PendingInvite, PendingInviteResponseDto>().ReverseMap();
        }
    }
}
