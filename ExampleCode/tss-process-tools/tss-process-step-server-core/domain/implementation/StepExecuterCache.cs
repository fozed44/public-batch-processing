using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.StepServer.Core.Domain.Implementation;
using Tss.Process.StepServer.Domain.Interface;

namespace Tss.Process.StepServer.Domain.Implementation {

    /// <summary>
    /// The StepExecuterCache stores a map between stepnames and StepExecuter 
    /// interfaces.
    /// 
    /// IStepExecuterCache is only concerned with the reading of the cache. This
    /// StepExecuterCache implementation provides the AddStpeDefinition method to
    /// populate the cache via the injected IStepExecuterFactory implementation.
    /// </summary>
    public class StepExecuterCache : IStepExecuterCache {

       #region Fields

        private readonly Dictionary<string, IStepExecuter> _cache;
        private readonly IStepExecuterFactory              _stepExecuterFactory;
        private readonly ILog                              _log;

       #endregion

       #region ctor

        public StepExecuterCache(
            IStepExecuterFactory stepExecuterFactory,
            ILog                 log
        ) {
            _stepExecuterFactory = stepExecuterFactory;
            _log                 = log;
            _cache               = new Dictionary<string, IStepExecuter>();
        }

        public StepExecuterCache()
            : this(
                new StepExecuterFactory(),
                LogManager.GetLogger(typeof(StepExecuterCache))
        ) {}

       #endregion
       
       #region IStepCache
       
        public IStepExecuter GetStepExecuter(string stepName)
            => _cache[stepName];

       #endregion

       #region Public

        public void CacheStepDefinitions(IEnumerable<IStepDefinition> stepDefinitions) {
            foreach(var stepDefinition in stepDefinitions)
                CacheStepDefinition(stepDefinition);
        }

        public void CacheStepDefinition(IStepDefinition stepDefinition) {
            if(stepDefinition == null)
                throw new ArgumentNullException(nameof(stepDefinition));

            if(string.IsNullOrEmpty(stepDefinition.StepInfo.Name))
                throw new ArgumentException("stepDefinition.StepInfo.Name name property is empty.");
            
            _log?.Info($"Caching executer for {stepDefinition.StepInfo.Name}");

            // By design, the CacheStepDefinition should only be called onece
            // per step definition, we will validate this to help with
            // debugging.    
            if(_cache.ContainsKey(stepDefinition.StepInfo.Name))
                throw new ValidationException(
                    $"stepDefinition {stepDefinition.StepInfo.Name} already " + 
                    $"exists in the step cache!!"
                );

            _cache[stepDefinition.StepInfo.Name] = 
                _stepExecuterFactory.CreateExecuter(stepDefinition);
        }

       #endregion 

    }
}