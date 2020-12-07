using System.Collections.Generic;
using TssProcess.Data;

namespace TssProcess.Types {
    public interface IProcessDefinition {
        ProcessMetadata       ProcessMetadata { get; } 
        List<IStepDefinition> Steps           { get; }
    }
}
