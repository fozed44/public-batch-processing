using System;
using log4net;

namespace TssProcess {

    /// <summary>
    /// StepRunner
    /// 
    /// Manages the running and process controller steps 
    /// </summary>
    public class StepRunner {

        #region Fields

        private readonly IProcessControllerApiClient _processControllerClient;
        private readonly ILog                        _log;

        #endregion Fields

        #region ctor

        public StepRunner (
            IProcessControllerApiClient client,
            ILog                        log
        ) {
            _processControllerClient = client;
            _log                     = log;
        }
        #region Public

        /// <summary>
        /// Run a step on behalf of the process controller.
        /// The process controller will have already created a new step exectuion db object.
        /// </summary>
        public string RunStep(long stepExecutionId, string input) {

            _log?.Info($"Begin execution of step {stepMetaDataId}");

            var stepDefinition = GetStepDefinition(stepMetaDataId);

            // Note:
            try {

                _processControllerClient.NotifiyBeginStepExecution(stepExecution);

                var result = ExecuteStep(stepExecution)

                _processControllerClient.NotifyStepExecutionComplete(stepExecution, result);
            } catch(Exception ex) {
                _processControllerClient.NotifyStepExecutionFail(ex);
            }
        }
        #endregion Public
    }
}