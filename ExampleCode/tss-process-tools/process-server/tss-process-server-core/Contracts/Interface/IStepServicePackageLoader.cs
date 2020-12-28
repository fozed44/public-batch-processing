using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.ProcessServer.Core.Contracts.Interface {

    /// <summary>
    /// Load a StepServicePackageDto into the process-server. This interface
    /// will be used by the process-server to handle calls to the api method
    /// that is called by a step-server with the step-server starts up in order
    /// to inform the process-server of its existance and capabilities.
    /// </summary>
    public interface IStepServicePackageLoader {
        string LoadStepServicePackage(StepServicePackageDto stepServicePackageDto);
    }
}