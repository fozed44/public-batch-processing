using Tss.Process.StepServer.Core.Contracts.Interface;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using log4net;
using System;
using System.Threading;

namespace Tss.Process.StepServer.Domain.Implementation {

    internal class ProcessControllerNotificationInitiator : IProcessControllerNotificationInitiator {

       #region Fields

        private          Task                  _initiatorTask;
        private readonly IProcessServiceClient _processServiceClient;
        private readonly StepServicePackageDto _stepServicePackageDto;
        private readonly ILog                  _log;

       #endregion

       #region ctor

        public ProcessControllerNotificationInitiator(
            IProcessServiceClient processServiceClient,
            StepServicePackageDto stepServicePackageDto,
            ILog                  log
        ) {
            _processServiceClient  = processServiceClient;
            _stepServicePackageDto = stepServicePackageDto;
            _log                   = log;
        }

        public ProcessControllerNotificationInitiator(
            IProcessServiceClient processServiceClient,
            StepServicePackageDto stepServicePackageDto
        ) : this(
            processServiceClient,
            stepServicePackageDto,
            LogManager.GetLogger(typeof(ProcessControllerNotificationInitiator))
        ){}

       #endregion

       #region IProcessControllerNotificationInitiator  

        public void InitiateProcessControllerNotification() {

            // We are not allowed to start a second task. The one
            // and only task that we create should run until it
            // succeeds.
            if(_initiatorTask != null)
                throw new ValidationException("Can't restart initiator.");

            _initiatorTask = Task.Run(NotifyStepService);
        }

       #endregion

       #region Private

        private void NotifyStepService() {
            while(true) {
                _log.Info("Notifying process controller...");

                try {
                    _processServiceClient.PublishStepServerPackage(_stepServicePackageDto);
                    return;
                } catch (Exception e) {
                    // Currently we will just keep looping untill the
                    // notification succeeds.
                    _log.Info("Failed to notify process controller.", e);
                    Thread.Sleep(20_000);
                }
            }
        }

       #endregion

    }
}