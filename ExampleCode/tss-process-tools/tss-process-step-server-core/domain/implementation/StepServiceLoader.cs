using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.StepServer.Core.Domain.Implementation {
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

        public StepServicePackageDto LoadService(Assembly assembly) {
            if(assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            return BuildStepServicePackage(EnumerateProcessPackages(assembly));
        }

        public StepServicePackageDto LoadService(string pathName) {
            if(!Directory.Exists(pathName))
                throw new ArgumentException($"{nameof(pathName)} does not exist.");

            var processPackages = new List<ProcessPackageDto>();


            foreach(var file in Directory.GetFiles(pathName, "*.dll")) {
                var assembly = Assembly.LoadFile(file);
                processPackages.AddRange(EnumerateProcessPackages(assembly));
            }

            return BuildStepServicePackage(processPackages);
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

        private IEnumerable<ProcessPackageDto> EnumerateProcessPackages(Assembly assembly){
            return 
                EnumerateProcessDefinitions(assembly)
                    .Select(x => GetProcessPackage(x));
        }

        private ProcessPackageDto GetProcessPackage (IProcessDefinition processDefinition) {
            return new ProcessPackageDto {
                ProcessInfo = processDefinition.ProcessInfo.MemberwiseClone(),
                Steps       = processDefinition.Steps.Select(x => x.StepInfo.MemberwiseClone())
            };
        }

        private StepServicePackageDto BuildStepServicePackage(IEnumerable<ProcessPackageDto> packageDtos) {
            return new StepServicePackageDto {
                ServiceInfo = new StepServiceInfo {
                    Name        = _serviceName,
                    Description = _serviceDescription,
                },
                Processes = packageDtos 
            };
        } 

        #endregion 
    }
}