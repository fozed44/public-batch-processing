using System;

namespace Tss.Process.Data.Entities {
    public class StepServiceInfo {
        public long     StepServiceInfoId { get; set; }
        public string   Url               { get; set; }
        public DateTime Created           { get; set; }
        public DateTime LastStarted       { get; set; }
    }
}