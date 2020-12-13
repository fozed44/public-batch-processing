using System;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using Tss.Process.Core.Contracts.Interface;

namespace Tss.Process.Core.Domain.Implementations {

    /// <summary>
    /// ProcessMetadataBuilder
    ///
    ///   Build process meta data objects from process definitions that
    ///   will be defined in a step server.
    /// </summary>
    internal class ProcessMetadataBuilder : IProcessMetadataBuilder {

       #region Fields

        private readonly ILog _log;

       #endregion Fields

       #region ctor

        public ProcessMetadataBuilder(ILog log){
            _log = log;
        }

       #endregion ctor

       #region IProcessMetadataBuilder
        public ProcessMetadataDto BuildProcessMetadata(IProcessDefinition processDefinition) {

            if(processDefinition.ProcessMetadataDto.ProcessMetadataId != 0)
                throw new ArgumentException($"{nameof(ProcessMetadataDto.ProcessMetadataId)} must be 0.");
                
            if(processDefinition.ProcessMetadataDto.StepServiceInfoId != 0)
                throw new ArgumentException($"{nameof(ProcessMetadataDto.StepServiceInfoId)} must be 0.");

            return new ProcessMetadataDto {
                Name        = processDefinition.ProcessMetadataDto.Name,
                Description = processDefinition.ProcessMetadataDto.Description
            };
        }

       #endregion IProcessMetadataBuilder

    }
}