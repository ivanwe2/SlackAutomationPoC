using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Entities
{
    public class RegisteredUser : BaseEntity
    {
        public string SlackId { get; set; }
        public string Email { get; set; }
    }
}
