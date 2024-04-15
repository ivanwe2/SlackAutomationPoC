using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.RegisteredUserDtos
{
    public class RegisteredUserRequestDto
    {
        public string SlackId { get; set; }
        public string Email { get; set; }
    }
}
