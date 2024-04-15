using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prime.SlackWorkspaceAutomationPoC.Api.Extenions;
using Prime.SlackWorkspaceAutomationPoC.Api.Helpers;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.EmailSender;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Helpers;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Services;
using Prime.SlackWorkspaceAutomationPoC.Domain.EmailSender;
using Prime.SlackWorkspaceAutomationPoC.Services;

namespace Prime.SlackWorkspaceAutomationPoC.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSlackWorkspaceAutomationPoCDbContext(Configuration.GetConnectionString("DefaultConnection"));
            services.AddSlackWorkspaceAutomationPoCAutomapper();
            services.AddSlackWorkspaceAutomationPoCSwagger();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddTransient<ISlackService,SlackService>();
            services.AddTransient<IEmailSender,EmailSender>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UpdateDatabase();
        }
    }
}
