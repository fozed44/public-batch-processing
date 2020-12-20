using Microsoft.Extensions.DependencyInjection;
using Tss.Process.StepServer.Contracts.Interface;
using Tss.Process.StepServer.Domain.Implementation;

namespace Tss.Process.StepServer.Core.Domain.Configuration {
    public static class StepServerConfigurationExtensions {

        public static IServiceCollection UseStepServer(
            this IServiceCollection serviceCollection,
            string                  serviceName,
            string                  serviceDescription
        ) {
            return serviceCollection.AddTransient<IStepServiceLoader>(_ =>
                new StepServiceLoader(
                    serviceName,
                    serviceDescription
                )
            );
        }
        
    }
}