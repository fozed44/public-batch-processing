using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using Tss.Process.Contracts.Types.Info;
using Tss.Process.StepServer.Contracts.Interface;
using Tss.Process.StepServer.Contracts.Types;
using Tss.Process.StepServer.Domain.Interface;
using Tss.Process.SteServer.Domain.Implementation;

namespace Tss.Process.StepServer.Domain.Implementation {

    /// <summary>
    /// The step service loader creates ProcessPackages from IProcessDefinition
    /// implementations found in an assembly.
    /// </summary>
    public class StepServiceLoader : IStepServiceLoader {

       #region Fields

        private readonly ILog   _log;
        private readonly string _serviceName;
        private readonly string _serviceDescription;

       #endregion

       #region ctor

        public StepServiceLoader(
            ILog   log,
            string serviceName,
            string serviceDescription
        ) {
            _log                = log;
            _serviceName        = serviceName;
            _serviceDescription = serviceDescription;
        }

        public StepServiceLoader(
            string serviceName,
            string serviceDescription
        ) : this (
            LogManager.GetLogger(typeof(StepServiceLoader)),
            serviceName,
            serviceDescription
        ) {}

       #endregion 

       #region IStepServiceLoader

        public LoadServiceResult LoadService(Assembly assembly) {
            if(assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var processDefinitions = EnumerateProcessDefinitions(assembly);

            return new LoadServiceResult {
                StepServicePackage = BuildStepServicePackage(processDefinitions),
                StepRunner         = CreateStepRunner(processDefinitions)
            };
        }

        public LoadServiceResult LoadService(string pathName) {
            if(!Directory.Exists(pathName))
                throw new ArgumentException($"{nameof(pathName)} does not exist.");

            var processDefinitions = EnumerateProcessDefinitions(pathName);

            return new LoadServiceResult {
                StepServicePackage = BuildStepServicePackage(processDefinitions),
                StepRunner         = CreateStepRunner(processDefinitions)
            };
            
        }

       #endregion 

       #region Private

        private IEnumerable<IProcessDefinition> EnumerateProcessDefinitions(Assembly assembly){
            return assembly
                .GetExportedTypes()
                .Where(x => typeof(IProcessDefinition)
                    .IsAssignableFrom(x)
                )
                .Select(x => x as IProcessDefinition);
        }

        private IEnumerable<IProcessDefinition> EnumerateProcessDefinitions(string pathName) {
            var processDefinitions = new List<IProcessDefinition>();

            foreach(var file in Directory.GetFiles(pathName, "*.dll")) {
                var assembly = Assembly.LoadFile(file);
                processDefinitions.AddRange(EnumerateProcessDefinitions(assembly));
            }

            return processDefinitions;
        }

        private IStepExecuterCache CreateStepExecuterCache(IEnumerable<IProcessDefinition> processDefinitions){
            var result = new StepExecuterCache();

            foreach(var processDefinition in processDefinitions)
                result.CacheStepDefinitions(processDefinition.Steps);

            return result;
        }

        private IStepRunner CreateStepRunner(IEnumerable<IProcessDefinition> processDefinitions) {
            return new StepRunner(CreateStepExecuterCache(processDefinitions));
        }

        private IEnumerable<ProcessPackageDto> GetProcessPackages(IEnumerable<IProcessDefinition> processDefinitions) {
            return
                processDefinitions.Select(x => GetProcessPackage(x));            
        }

        private ProcessPackageDto GetProcessPackage (IProcessDefinition processDefinition) {
            return new ProcessPackageDto {
                ProcessInfo = processDefinition.ProcessInfo.MemberwiseClone(),
                Steps       = processDefinition.Steps.Select(x => x.StepInfo.MemberwiseClone())
            };
        }

        private StepServicePackageDto BuildStepServicePackage(IEnumerable<IProcessDefinition> processDefinitions) {
            return new StepServicePackageDto {
                ServiceInfo = new StepServiceInfo {
                    Name        = _serviceName,
                    Description = _serviceDescription,
                },
                Processes = GetProcessPackages(processDefinitions)
            };
        } 

       #endregion 
    }
}