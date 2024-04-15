using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories
{
    public interface IRegisteredUserRepository : IBaseRepository
    {
        Task<bool> HasAnyByEmailAsync(string email);
    }
}
