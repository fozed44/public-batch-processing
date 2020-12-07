using log4net;
using TssProcess.Core;
using TssProcess.Data.Entities;

namespace Tss_Process_Core.Domain.Implementations {

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
        public ProcessMetadata BuildProcessMetadata(IProcessDefinition processDefinition) {

            return new ProcessMetadata {
                Name = processDefinition.ProcessMetadata.Name,
                Description = processDefinition.ProcessMetadata.Description
            }
        }

       #endregion IProcessMetadataBuilder

    }
}