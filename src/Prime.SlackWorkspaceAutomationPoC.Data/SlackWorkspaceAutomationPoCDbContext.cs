using Microsoft.EntityFrameworkCore;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;

namespace Prime.SlackWorkspaceAutomationPoC.Data
{
    public class SlackWorkspaceAutomationPoCDbContext : DbContext
    {
        public SlackWorkspaceAutomationPoCDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<PendingInvite> PendingInvites { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
    }
}
