using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tss_Process_Data.Data {
    public class StepExecution {
        public long      StepExecutionId    { get; set; }
        public long      StepMetadataId     { get; set; }
        public long      ProcessExecutionId { get; set; }
        public string    InputJson          { get; set; } 
        public string    OutputJson         { get; set; }
        public DateTime? Start              { get; set; }
        public DateTime? End                { get; set; } 

        public StepMetadata     StepMetadata     { get; set; }

        public ProcessExecution ProcessExecution { get; set;}
    }
}