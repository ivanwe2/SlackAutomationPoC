using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Autofac;

namespace Prime.SlackWorkspaceAutomationPoC.Api.Autofac.Modules
{
    public class RepositoriesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(BaseEntity).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
        }
    }

}
