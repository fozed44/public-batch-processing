using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tss.Process.Core.Domain.Implementations;
using Tss.Process.StepServer.Contracts.Interface;
using Tss.Process.StepServer.Domain.Implementation;

namespace Tss.Process.StepServer.Domain.Configuration {
    public static class StepServerConfigurationExtensions {

        public static IServiceCollection AddStepServer (
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

        public static IServiceCollection AddStepServer (
            this IServiceCollection serviceCollection
        ) {
            var helpers = new StepHelpers();
            return serviceCollection.AddTransient<IStepServiceLoader>(s =>
                new StepServiceLoader (
                    helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:name"),
                    helpers.GetRequiredConfiguration(s.GetService<IConfiguration>(), "step-server:description")
                )
            );
        }
    }
}