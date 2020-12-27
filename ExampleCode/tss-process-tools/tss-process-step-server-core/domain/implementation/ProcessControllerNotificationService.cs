using Tss.Process.StepServer.Core.Contracts.Interface;
using System.Threading.Tasks;
using Tss.Process.Contracts.Interface;
using log4net;
using System;
using System.Threading;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Tss.Process.StepServer.Domain.Implementation {

     public class ProcessControllerNotificationService : IHostedService {

       #region Fields

        private readonly IProcessServiceClient _processServiceClient;
        private readonly IStepService          _stepService;
        private readonly ILog                  _log;
        private readonly IConfiguration        _config;

       #endregion

       #region ctor

        public ProcessControllerNotificationService (
            IProcessServiceClient processServiceClient,
            IStepService          stepService,
            IConfiguration        config,
            ILog                  log
        ) {
            _processServiceClient  = processServiceClient;
            _stepService           = stepService;
            _log                   = log;
            _config                = config;
        }

        public ProcessControllerNotificationService (
            IProcessServiceClient processServiceClient,
            IStepService          stepService,
            IConfiguration        config
        ) : this(
            processServiceClient,
            stepService,
            config,
            LogManager.GetLogger(typeof(ProcessControllerNotificationService))
        ) {}

       #endregion

       #region IHostedService

        public async Task StartAsync(CancellationToken cancellationToken) {
            _log?.Info("## (Process controller notification) ## Starting process controller notification service.");

            if(_config?["step-server:process-controller-notification-enabled"]?.ToLower() == "false") {
                _log?.Info("## (Process controller notification) ## process controller is disabled by connfiguration.");
                return;
            }

            await NotifyStepServiceAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }

       #endregion

       #region Private

        private async Task NotifyStepServiceAsync() {
            while(true) {
                _log?.Info("## (Process controller notification) ## Notifying process controller...");

                _log?.Info(JsonSerializer.Serialize(_stepService.StepServicePackageDto));

                try {
                    var result = await _processServiceClient.PublishStepServerPackageAsync(_stepService.StepServicePackageDto);

                    _log?.Info($"## (Process controller notification) ## Server response: {result}");
                    return;
                } catch (Exception e) {

                    // Currently we will just keep looping untill the
                    // notification succeeds.
                    _log?.Info("## (Process controller notification) ## Failed to notify process controller.", e);
                    Thread.Sleep(20_000);
                }
            }
        }
 
       #endregion

    }
}