using System.Collections.Generic;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.Contracts.Interface {
    public interface IProcessDefinition {
        ProcessInfo    ProcessInfo { get; } 
        List<StepInfo> Steps       { get; }
    }
}