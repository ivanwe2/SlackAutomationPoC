using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Prime.SlackWorkspaceAutomationPoC.Data;
using Prime.SlackWorkspaceAutomationPoC.Data.Mapper;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Extenions
{
    public static class ServiceColletionExtensions
    {
        public static IServiceCollection AddSlackWorkspaceAutomationPoCDbContext(
            this IServiceCollection services, string connString) 
            => services.AddDbContext<SlackWorkspaceAutomationPoCDbContext>(
                options => options.UseSqlServer(connString));

        public static IServiceCollection AddSlackWorkspaceAutomationPoCAutomapper(this IServiceCollection services) 
            => services.AddAutoMapper(mc =>
                {
                    mc.AddProfile(new ChannelProfile());
                    mc.AddProfile(new TechnologyProfile());
                    mc.AddProfile(new RegisteredUserProfile());
                    mc.AddProfile(new PendingInviteProfile());
                });

        public static IServiceCollection AddSlackWorkspaceAutomationPoCSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SlackWorkspaceAutomationPoC API", Version = "v1" });
            });
    }
}
