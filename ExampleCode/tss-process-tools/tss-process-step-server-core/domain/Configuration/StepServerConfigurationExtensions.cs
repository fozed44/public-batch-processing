using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tss.Process.Core.Domain.Implementations;
using Tss.Process.StepServer.Core.Contracts.Interface;
using Tss.Process.StepServer.Domain.Implementation;

namespace Tss.Process.StepServer.Domain.Configuration {
    public static class StepServerConfigurationExtensions {

        public static IServiceCollection AddStepServer (
            this IServiceCollection serviceCollection,
            string                  serviceName,
            string                  serviceDescription,
            string                  assemblyDirectory

        ) {
            return serviceCollection.AddTransient<IStepService>(_ =>
                new StepServiceLoader(
                    serviceName,
                    serviceDescription
                ).LoadService(assemblyDirectory)
            );
        }

        public static IServiceCollection AddStepServer (
            this IServiceCollection serviceCollection
        ) {
            var helpers = new StepHelpers();
            return serviceCollection.AddTransient<IStepService>(s =>
                new StepServiceLoader (
                    helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:name"),
                    helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:description")
                ).LoadService(helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:assebmly-directory"))
            );
        }
    }
}