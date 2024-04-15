using AutoMapper;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.ChannelDtos;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Mapper
{
    public class ChannelProfile : Profile
    {
        public ChannelProfile() 
        {
            CreateMap<ChannelRequestDto, Channel>().ReverseMap();
            CreateMap<Channel, ChannelResponseDto>().ReverseMap();
        }
    }
}
