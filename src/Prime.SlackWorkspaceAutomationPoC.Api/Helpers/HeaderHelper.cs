using Microsoft.AspNetCore.Http;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Helpers;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Helpers
{
    public class HeaderHelper : IHeaderHelper
    {
        private const string EMAIL_HEADER_NAME = "user-email";
        private const string TECHNOLOGY_HEADER_NAME = "user-technology";

        public string UserEmail { get; init; }
        public string UserTechnology { get; init; }

        public HeaderHelper(IHttpContextAccessor httpContextAccessor)
        {
            UserEmail = httpContextAccessor.HttpContext.Request.Headers[EMAIL_HEADER_NAME].ToString();
            UserTechnology = httpContextAccessor.HttpContext.Request.Headers[TECHNOLOGY_HEADER_NAME].ToString();
        }

    }
}
