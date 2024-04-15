using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Repositories
{
    public class RegisteredUserRepository : BaseRepository<RegisteredUser>, IRegisteredUserRepository
    {
        public RegisteredUserRepository(SlackWorkspaceAutomationPoCDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool> HasAnyByEmailAsync(string email)
            => await Items.AnyAsync(r => r.Email == email); 
        
    }
}
