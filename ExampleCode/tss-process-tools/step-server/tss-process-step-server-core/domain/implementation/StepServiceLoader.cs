using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using Tss.Process.Contracts.Types.Info;
using Tss.Process.StepServer.Contracts.Types;
using Tss.Process.StepServer.Core.Contracts.Interface;
using Tss.Process.StepServer.Domain.Interface;
using Tss.Process.SteServer.Domain.Implementation;

namespace Tss.Process.StepServer.Domain.Implementation {

    /// <summary>
    /// The step service loader creates ProcessPackages from IProcessDefinition
    /// implementations found in an assembly.
    /// </summary>
    public class StepServiceLoader : IStepServiceLoader {

       #region Fields

        private readonly ILog                  _log;
        private readonly string                _serviceName;
        private readonly string                _serviceDescription;
        private readonly IProcessServiceClient _processServiceClient;
        
       #endregion

       #region ctor

        public StepServiceLoader(
            string                serviceName,
            string                serviceDescription,
            IProcessServiceClient processServiceClient,
            ILog                  log
        ) {
            _log                  = log;
            _serviceName          = serviceName;
            _serviceDescription   = serviceDescription;
            _processServiceClient = processServiceClient;
        }

        public StepServiceLoader(
            string                serviceName,
            string                serviceDescription,
            IProcessServiceClient processServiceClient
        ) : this (
            serviceName,
            serviceDescription,
            processServiceClient,
            LogManager.GetLogger(typeof(StepServiceLoader))
        ) {}

       #endregion 

       #region IStepServiceLoader

        public IStepService LoadService(Assembly assembly) {
            if(assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var processDefinitions = EnumerateProcessDefinitions(assembly);

            return new StepService {
                StepServicePackageDto 
                    = BuildStepServicePackage(processDefinitions),
                StepRunner
                    = CreateStepRunner(processDefinitions)
            };
        }

        public IStepService LoadService(string pathname) {
            if(!Directory.Exists(pathname))
                throw new ArgumentException($"{nameof(pathname)}: {pathname} -- does not exist.");

            var processDefinitions = EnumerateProcessDefinitions(pathname);

            return new StepService {
                StepServicePackageDto 
                    = BuildStepServicePackage(processDefinitions),
                StepRunner
                    = CreateStepRunner(processDefinitions)
            };
            
        }

       #endregion 

       #region Private

        private IEnumerable<IProcessDefinition> EnumerateProcessDefinitions(Assembly assembly){
            
            _log?.Info($"Searching {assembly.GetName()} for process definitions.");

            var result = new List<IProcessDefinition>();
            
            // We are gong to loop through the assembly's types rather
            // than use ling so that we can get good logs.
            foreach(var type in assembly.GetExportedTypes()) {

                // Note: we are just getting the type objects here. We still need
                // to create instances of these types.
                if(typeof(IProcessDefinition).IsAssignableFrom(type)) {
                    _log.Info($"-- Found type {type.Name}");

                    var instance = Activator.CreateInstance(type);
                    var casted = instance as IProcessDefinition;

                    result.Add(Activator.CreateInstance(type) as IProcessDefinition);
                }
            }
            
            if(result.Count == 0)
                _log?.Info("None found");

            return result;
        }

        private IEnumerable<IProcessDefinition> EnumerateProcessDefinitions(string pathName) {
            _log?.Info($"Searching for process definitions in {pathName}");
            var processDefinitions = new List<IProcessDefinition>();

            foreach(var file in Directory.GetFiles(pathName, "*.dll")) {
                var assembly = Assembly.LoadFile(file);
                processDefinitions.AddRange(EnumerateProcessDefinitions(assembly));
            }

            return processDefinitions;
        }

        private IStepExecuterCache CreateStepExecuterCache(IEnumerable<IProcessDefinition> processDefinitions){
            var result = new StepExecuterCache();

            foreach(var processDefinition in processDefinitions) {
                _log?.Info($"Caching executers for process {processDefinition.ProcessInfo.Name}");
                result.CacheStepDefinitions(processDefinition.Steps);
            }

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