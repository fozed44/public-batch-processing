using Microsoft.Extensions.DependencyInjection;
using Tss.Process.Api.Client.Domain.Implementations;
using Tss.Process.Contracts.Interface;

namespace Tss.Process.Api.Client.Domain.Configuration {

     public static class ProcessClientServiceExtensions {
        
        static public IServiceCollection AddProcessServiceClient(this IServiceCollection serviceCollection, string baseUrl, string hostVersion) {
            return serviceCollection.AddTransient<IProcessServiceClient>(_ => new ProcessServiceClient(baseUrl, hostVersion));
        }
    }    
}