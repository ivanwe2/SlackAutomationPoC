using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos.PendingInviteDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Repositories
{
    public class PendingInviteRepository : BaseRepository<PendingInvite>, IPendingInviteRepository
    {
        public PendingInviteRepository(SlackWorkspaceAutomationPoCDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<PendingInviteResponseDto> GetInvitationByUserEmailAsync(string email)
        {
            return await Mapper
                .ProjectTo<PendingInviteResponseDto>(Items.AsNoTracking().Where(p => p.UserEmail == email))
                .FirstOrDefaultAsync(i => i.UserEmail == email);
        }
    }
}
