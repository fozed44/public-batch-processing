using System;
using System.Collections.Generic;

namespace Tss_Process_Data.Data {
    public class ProcessExecution {
        public long      ProcessExecutionId     { get; set; }
        public long      ProcessMetadataId      { get; set; }
        public long?     CurrentStepExecutionId { get; set; }
        public long      CycleId                { get; set; }
        public string    MachineName            { get; set; }
        public DateTime? Start                  { get; set; }
        public DateTime? End                    { get; set; }
    
        public List<StepExecution> Steps                { get; set; }
        public ProcessMetadata     ProcessMetadata      { get; set; }
        public StepExecution       CurrentStepExecution { get; set; }
    }
}
