﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.ChannelDtos
{
    public class ChannelRequestDto
    {
        public string ChannelId { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; } = true;
    }
}
