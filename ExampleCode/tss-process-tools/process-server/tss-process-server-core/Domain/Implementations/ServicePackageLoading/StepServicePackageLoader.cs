using Tss.Process.Contracts.Types.Dto;
using Tss.Process.ProcessServer.Core.Contracts.Interface;

namespace Tss.Process.ProcessServer.Core.Domain.Implementations {

    /// <summary>
    /// Load a StepServicePackageDto into the process-server. This implementation
    /// will be used by the process-server to handle calls to the api method
    /// that is called by a step-server with the step-server starts up in order
    /// to inform the process-server of its existance and capabilities.
    /// 
    /// StepServicePackage loading steps:
    ///     1. Validate the StepServicePackageDto object.
    ///         - Let an implementation of IStepServicePackageDtoValidator handle
    ///           this.
    ///     
    ///     2. Determine it the process already exists.
    ///         - Should we overwrite ? How to know if it is correct or overwrite
    ///           or throw an exception.
    ///   
    ///     3. Insert the package into the database.
    /// </summary>
    internal class StepServicePackageLoader : IStepServicePackageLoader {
       public string LoadStepServicePackage(StepServicePackageDto stepServicePackageDto) {
            throw new System.NotImplementedException();
        }
    }
}