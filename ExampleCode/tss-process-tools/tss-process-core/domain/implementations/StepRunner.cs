using System;
using log4net;
using Tss.Process.Contracts.Interface;

namespace TssProcess {

    /// <summary>
    /// StepRunner
    /// 
    /// Manages the running and process controller steps 
    /// </summary>
    public class StepRunner {

        #region Fields

        private readonly IProcessControllerApi _processControllerApi;
        private readonly ILog                  _log;

        #endregion Fields

        #region ctor

        public StepRunner (
            IProcessControllerApi client,
            ILog                  log
        ) {
            _processControllerApi = client;
            _log                  = log;
        }

        #endregion ctor 

        #region Public


        /// <summary>
        /// Run a step on behalf of the process controller.
        /// The process controller will have already created a new step exectuion db object.
        /// </summary>
        public string RunStep(long stepExecutionId, string input) {
            throw new NotImplementedException();
        }

        #endregion Public
    }
}