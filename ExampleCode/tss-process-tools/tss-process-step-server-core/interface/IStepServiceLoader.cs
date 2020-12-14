using System.Reflection;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.StepServer.Core {

    /// <summary>
    /// 
    /// Load service info from an assembly, or from all of the assemblies in
    /// a directory. 
    ///
    /// </summary>
    public interface IStepServiceLoader {
        StepServiceInfo LoadService(Assembly assembly);
        StepServiceInfo LoadService(string pathName);
    } 
}