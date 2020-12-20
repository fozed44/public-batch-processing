using Tss.Process.Contracts.Types.Dto;
using Tss.Process.StepServer.Contracts.Interface;

namespace Tss.Process.StepServer.Contracts.Types {

    /// <summary>
    /// Returned by the IStepServciceLoader.LoadService methods. Allowing
    /// a packaged result of a step service package dto and a step cache.
    /// </summary>
    public class LoadServiceResult {
        public StepServicePackageDto StepServicePackage { get; set; }
        public IStepRunner           StepRunner         { get; set; }
    }
}