using System;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Dto;
using Tss.Process.Core.Interface;

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
            throw new NotImplementedException();
        }

       #endregion IProcessMetadataBuilder

    }
}