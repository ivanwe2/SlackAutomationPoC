using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Helpers
{
    public interface IHeaderHelper
    {
        public string UserEmail { get; init; }
        public string UserTechnology { get; init; }
    }
}
