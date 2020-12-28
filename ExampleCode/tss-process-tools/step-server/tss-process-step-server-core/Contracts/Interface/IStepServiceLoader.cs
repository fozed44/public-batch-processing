using System.Reflection;

namespace Tss.Process.StepServer.Core.Contracts.Interface {

    /// <summary>
    /// Load service info from an assembly, or from all of the assemblies in
    /// a directory. 
    /// </summary>
    public interface IStepServiceLoader {
        IStepService LoadService(Assembly assembly);
        IStepService LoadService(string pathName);
    } 
}