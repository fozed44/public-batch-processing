using System.Collections.Generic;
using System.Reflection;

namespace TssProcessCore.Contracts.Interface {

    /// <summary>
    /// Enumerate ProcessMetadata objects from assemblies, or from all of
    /// the assemblies in a directory. 
    /// </summary>
    public interface IProcessMetadataEnumerator {
        IEnumerable<ProcessMetadata> EnumerateProcessMetadata(Assembly assembly); 
        IEnumerable<ProcessMetadata> EnumerateProcessMetadata(string directoryPath);
    }
}