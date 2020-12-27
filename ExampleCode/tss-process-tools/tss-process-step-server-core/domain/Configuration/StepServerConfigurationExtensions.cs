using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tss.Process.Contracts.Interface;
using Tss.Process.Core.Domain.Implementations;
using Tss.Process.StepServer.Core.Contracts.Interface;
using Tss.Process.StepServer.Domain.Implementation;

namespace Tss.Process.StepServer.Domain.Configuration {
    public static class StepServerConfigurationExtensions {

        public static IServiceCollection AddStepServer (
            this IServiceCollection serviceCollection,
            string                  serviceName,
            string                  serviceDescription,
            string                  assemblyDirectory,
            IProcessServiceClient   processServiceClient
        ) {
            return serviceCollection.AddSingleton<IStepService>(_ => {
                var result = new StepServiceLoader(
                        serviceName,
                        serviceDescription,
                        processServiceClient
                    ).LoadService(assemblyDirectory);
                return result;
            });
        }

        public static IServiceCollection AddStepServer (
            this IServiceCollection serviceCollection
        ) {
            return serviceCollection.AddSingleton<IStepService>(s => {

                var result = new StepServiceLoader (
                        Helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:name"),
                        Helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:description"),
                        s.GetRequiredService<IProcessServiceClient>()
                    ).LoadService(Helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:assembly-directory"));

                return result;

            });
        }
    }
}