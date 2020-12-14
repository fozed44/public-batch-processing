using System.Collections.Generic;
using System.Reflection;
using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.Core.Interface {

    /// <summary>
    /// Enumerate ProcessMetadata objects from assemblies, or from all of
    /// the assemblies in a directory. 
    /// </summary>
    public interface IProcessMetadataEnumerator {
        IEnumerable<ProcessMetadataDto> EnumerateProcessMetadata(Assembly assembly); 
        IEnumerable<ProcessMetadataDto> EnumerateProcessMetadata(string directoryPath);
    }
}