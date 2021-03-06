using System.Threading.Tasks;
using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.Contracts.Interface {
    public interface IProcessServiceClient {

        /// <summary>
        /// Notify the process server of a new StepServer. A StepServer will call this
        /// method when the step server starts up, allowing the ProcessServer knowledge
        /// of the steps and processes defined by the step server.
        /// </summary>
        Task<string> PublishStepServerPackageAsync(StepServicePackageDto stepServicePackageDto);
    }
}