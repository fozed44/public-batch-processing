using System.Collections.Generic;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.Contracts.Types.Dto {
    public class StepServicePackageDto {
        public StepServiceInfo                ServiceInfo { get; set; }
        public IEnumerable<ProcessPackageDto> Processes   { get; set; }
    }
}