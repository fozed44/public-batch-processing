using Microsoft.Extensions.DependencyInjection;
using Tss.Process.Api.Client.Domain.Implementations;
using Tss.Process.Contracts.Interface;

namespace Tss.Process.Api.Client.Domain.Configuration {

     public static class ProcessClientServiceExtensions {
        
        static public IServiceCollection AddProcessClient(this IServiceCollection serviceCollection, string baseUrl, string hostVersion) {
            return serviceCollection.AddTransient<IProcessClient>(_ => new ProcessClient(baseUrl, hostVersion));
        }
    }    
}