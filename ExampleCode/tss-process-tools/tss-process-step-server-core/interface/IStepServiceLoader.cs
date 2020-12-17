using System.Reflection;
using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.StepServer.Core {

    /// <summary>
    /// 
    /// Load service info from an assembly, or from all of the assemblies in
    /// a directory. 
    ///
    /// </summary>
    public interface IStepServiceLoader {
        StepServicePackageDto LoadService(Assembly assembly);
        StepServicePackageDto LoadService(string pathName);
    } 
}