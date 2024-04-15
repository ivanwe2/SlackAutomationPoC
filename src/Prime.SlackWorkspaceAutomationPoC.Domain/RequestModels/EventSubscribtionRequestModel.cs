using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.RequestModels
{
    public class EventSubscribtionRequestModel
    {
        public string Challenge { get; set; }
        public Event Event { get; set; }
    }

    public class Event
    {
        public string User { get; set; }

        public string Email { get; set; }
    }
}
