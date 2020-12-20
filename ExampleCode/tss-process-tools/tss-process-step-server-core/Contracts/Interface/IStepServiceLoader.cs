using System.Reflection;
using Tss.Process.StepServer.Contracts.Types;

namespace Tss.Process.StepServer.Contracts.Interface {

    /// <summary>
    /// 
    /// Load service info from an assembly, or from all of the assemblies in
    /// a directory. 
    /// </summary>
    public interface IStepServiceLoader {
        LoadServiceResult LoadService(Assembly assembly);
        LoadServiceResult LoadService(string pathName);
    } 
}