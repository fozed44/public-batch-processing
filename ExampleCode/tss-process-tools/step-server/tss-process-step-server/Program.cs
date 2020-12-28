using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tss.Process.StepServer.Domain.Implementation;

namespace tss_process_step_server
{
    public class Program
    {
        public static void Main(string[] args) {
            log4net.LogManager.GetLogger(typeof(Program)).Info("Loading step server.");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(s => s.AddHostedService<ProcessControllerNotificationService>() );
    }
}
