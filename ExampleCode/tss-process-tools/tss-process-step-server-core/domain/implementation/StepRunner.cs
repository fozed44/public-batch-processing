using log4net;
using Tss.Process.StepServer.Contracts.Interface;
using Tss.Process.StepServer.Domain.Interface;

namespace Tss.Process.SteServer.Domain.Implementation {

    internal class StepRunner : IStepRunner {

       #region Fields

        private readonly ILog                 _log;
        private readonly IStepExecuterCache   _cache;

       #endregion

       #region ctor
       
        public StepRunner(
            IStepExecuterCache   cache,
            ILog                 log
        ) {
            _log             = log;
            _cache           = cache;
        }

        public StepRunner(IStepExecuterCache cache)
            : this(
                cache,
                LogManager.GetLogger(typeof(StepRunner))
        ) {}
       
       #endregion

       #region IStepRunner 

        public string Run(string stepName, string input) {
            var executer = _cache.GetStepExecuter(stepName);
            return executer.Execute(input);
        }

       #endregion
    }
}