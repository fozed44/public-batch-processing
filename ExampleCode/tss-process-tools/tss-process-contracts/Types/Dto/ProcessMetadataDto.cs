using System;

namespace TssProcessContracts.Types.Dto {
    public class ProcessMetadataDto {
        public long     ProcessMetadataId     { get; set; }
        public long     StepServiceInfoId     { get; set; }
        public string   Name                  { get; set; }
        public string   Description           { get; set; }
        public bool     AllowMultiple         { get; set; }
        public bool     AllowMultiplePerCycle { get; set; }
        public DateTime Created               { get; set; }
        public string   Version               { get; set; } 
    }
}