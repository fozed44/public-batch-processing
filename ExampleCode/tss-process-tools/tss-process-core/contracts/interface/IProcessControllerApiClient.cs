using System;
using TssProcessCore.Contracts.Types;

namespace TssProcessCore.Contracts.Interface {
    public interface IProcessControllerApiClient {
        void NotifyStepExecutionInitialize(StepExecution stepExecution);
        void NotifyStepExecutionStart(StepExecution stepExecution);
        void NotifyStepExecutionComplete(StepExecution stepExecution, string resultJson);
        void NotifyStepExecutionFail(StepExecution stepExecution, string errorJson);
        void NotifyStepExecutionFail(StepExecution stepExecution, Exception ex);
    }
}