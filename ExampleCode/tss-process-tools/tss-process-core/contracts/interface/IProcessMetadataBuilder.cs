using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.Core.Interface {

    /// <summary>
    /// Builds a ProcessMetadata object from a given implementation of
    /// IProcessDefinition.
    /// </summary>
    public interface IProcessMetadataBuilder {
        ProcessMetadataDto BuildProcessMetadata(IProcessDefinition processDefinition);
    }
}