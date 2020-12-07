using System;
using System.Collections.Generic;
using System.Linq;
using TssProcess.Data;

namespace TssProcess {
    public class ProcessRunner {

        #region Fields

        private readonly Func<ProcessContext> _processContextGenerator;

        #endregion Fields

        #region ctor

        public ProcessRunner(Func<ProcessContext> processContextGenerator) {
            _processContextGenerator = processContextGenerator;
        }

        #endregion ctor

        #region Public
    
        /// <summary>
        /// Continures an process execution.
        /// </summary>
        public void ContinueExecution(long processExecutionId) {
        }

        public void ContinueExecution(ProcessExecution processExecution) {
            StepMetadata nextStep;
            while((nextStep = GetNextStepMetaData(processExecution)) != null)
                ExecuteStep(nextStep);
        }
        private void ExecuteStep(StepMetadata nextStep) {
            throw new NotImplementedException();
        }

        private StepMetadata GetNextStepMetaData(ProcessExecution processExecution) {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Starts a new process execution.
        /// <\summary>
        public void Execute(string processName, long cycleId) {
          
            // Get or create the process definition.
            var processDefinition = GetProcessDefinition(processName);       
    
            // Determine if there is already a ProcessExecution for this
            // ProcessDefinition/cycle
            var currentExecution = GetCurrentExecutions(processDefinition, cycleId);
    
            // If there is already an execution of this process in progress
            // make sure that we are allowed to start a new execution. Most
            // of the time we wont be.
            ValidateConcurrentExecution(processDefinition);
    
            // Create a new process
            var processExecution = InitializeProcessExecution(processDefinition, cycleId);
    
            // Run the new process until it is complete.
            ContinueExecution(processExecution);
        }

        #endregion Public

        #region Private

        private IProcessDefinition GetProcessDefinition(string processName) {
            using(var context = _processContextGenerator()) {
                var processMetadata = context.ProcessMetadata.Where(x => x.Name == processName).Single();
                return GetProcessDefinition(processMetadata);
            }
        }

        private IProcessDefinition GetProcessDefinition(ProcessMetadata metadata) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// return a list of all process execution objects for the processDefinition
        /// that are currently running. 
        /// </summary>
        private List<ProcessExecution> GetCurrentExecutions(IProcessDefinition processDefinition, long cycleId) {
            using(var context = _processContextGenerator()) {
                return context.ProcessExecution.Where(x =>
                    x.ProcessMetadataId == processDefinition.ProcessMetadata.ProcessMetadataId
                 && x.End != null
                ).ToList();
            }
        }
    
        private void ValidateConcurrentExecution(IProcessDefinition processDefinition) {
            throw new NotImplementedException();
        }
    
        private ProcessExecution InitializeProcessExecution(IProcessDefinition processDefinition, long cycleId) {
            throw new NotImplementedException();
        }

        #endregion Private
    }
}