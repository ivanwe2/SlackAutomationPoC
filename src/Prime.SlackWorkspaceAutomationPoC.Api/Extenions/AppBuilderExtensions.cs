using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prime.SlackWorkspaceAutomationPoC.Data;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Extenions
{
    public static class AppBuilderExtensions
    {
        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<SlackWorkspaceAutomationPoCDbContext>();
            context.Database.Migrate();
        }
    }
}
