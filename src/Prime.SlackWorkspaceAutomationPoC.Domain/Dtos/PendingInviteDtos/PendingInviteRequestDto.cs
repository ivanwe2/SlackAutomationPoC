﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos
{
    public class PendingInviteRequestDto
    {
        public string UserEmail { get; set; }
        public string ChannelIds { get; set; }
    }
}
