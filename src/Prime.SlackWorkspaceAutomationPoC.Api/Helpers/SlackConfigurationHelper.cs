using Microsoft.Extensions.Configuration;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Helpers;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Helpers
{
    public class SlackConfigurationHelper : ISlackConfigurationHelper
    {
        private readonly string slackAccessToken = "SlackApi:AccessToken";
        private readonly string slackInviteLink = "SlackApi:InviteLink";
        public string AccessToken { get; init; }
        public string InviteLink { get; init; }

        public SlackConfigurationHelper(IConfiguration configuration)
        {
            AccessToken = configuration[slackAccessToken];
            InviteLink = configuration[slackInviteLink];
        }
    }
}
