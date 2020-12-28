using System;
using System.Collections.Generic;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.Contracts.Types.Dto {

    [Obsolete]
    public class ProcessPackageDto {
        public ProcessInfo           ProcessInfo { get; set; }
        public IEnumerable<StepInfo> Steps       { get; set; }
    }
}