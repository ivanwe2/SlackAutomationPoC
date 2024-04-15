using Autofac;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Autofac.Modules
{
    public class HelpersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(Program).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Helper"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
        }
    }
}
