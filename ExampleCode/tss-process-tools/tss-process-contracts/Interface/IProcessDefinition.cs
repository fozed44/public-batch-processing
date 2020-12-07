using System.Collections.Generic;

namespace TssProcessContracts.Interface {
    public interface IProcessDefinition {
        ProcessMetadataDto    ProcessMetadata { get; } 
        List<IStepDefinition> Steps           { get; }
    }
}