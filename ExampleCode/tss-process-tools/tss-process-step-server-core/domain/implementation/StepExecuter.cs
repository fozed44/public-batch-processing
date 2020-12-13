using System;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.StepServer.Core.Interface;

namespace Tss.Process.Step.Server.Core {

    /// <summary>
    /// StepExecuter
    ///
    /// Executer a step given a IStepDefinition describing that step.
    /// </summery>
    public class StepExecuter : IStepExecuter {

        #region Fields

        private readonly ILog _log;

        #endregion Fields

        #region ctor

        public StepExecuter(ILog log) {
            _log = log;
        }

        #endregion ctor

        #region IStepExecuter

        public string Execute(IStepDefinition stepDefinition, string input) {
            throw new NotImplementedException();
        }

        #endregion IStepExecuter
        
        #region Private

        #endregion Private
    }    
}