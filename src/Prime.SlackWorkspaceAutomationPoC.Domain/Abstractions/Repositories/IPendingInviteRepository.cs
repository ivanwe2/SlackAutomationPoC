using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories
{
    public interface IPendingInviteRepository : IBaseRepository
    {
        Task<PendingInviteResponseDto> GetInvitationByUserEmailAsync(string email);
    }
}
