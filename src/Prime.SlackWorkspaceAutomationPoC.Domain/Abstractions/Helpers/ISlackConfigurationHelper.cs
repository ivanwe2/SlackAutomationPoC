using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Helpers
{
    public interface ISlackConfigurationHelper
    {
        public string AccessToken { get; init; }
        public string InviteLink { get; init; }
    }
}
