using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Channel = Prime.SlackWorkspaceAutomationPoC.Data.Entities.Channel;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Repositories
{
    public class ChannelRepository : BaseRepository<Channel>, IChannelRepository
    {
        public ChannelRepository(SlackWorkspaceAutomationPoCDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<string> GetChannelIdByNameAsync(string channelName)
        {
            var entity = await Items.FirstOrDefaultAsync(c => c.Name == channelName);

            if(entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return entity.ChannelId;
        }
    }
}
