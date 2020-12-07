using TssProcess.Data.Entities;

namespace TssProcessCore.Contracts.Interface {

    /// <summary>
    /// Builds a ProcessMetadata object from a given implementation of
    /// IProcessDefinition.
    /// </summary>
    public interface IProcessMetadataBuilder {
        ProcessMetadata BuildProcessMetadata(IProcessDefinition processDefinition);
    }
}