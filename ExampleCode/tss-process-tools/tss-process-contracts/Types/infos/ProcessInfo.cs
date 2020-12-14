using System;

namespace Tss.Process.Contracts.Types.Info {

    /// <summary>
    /// ProcessInfo
    ///
    ///     Used in process definitions to define a process. This type, along
    ///     with ServiceInfo and StepInfo are used within process definitions
    ///     that are to be created in step servers. Because of that, they do
    ///     not require the db keys defined in the metadata or entities.
    /// </summary>
    public class ProcessInfo {
        public string   Name                  { get; set; }
        public string   Description           { get; set; }
        public bool     AllowMultiple         { get; set; }
        public bool     AllowMultiplePerCycle { get; set; }
        public DateTime Created               { get; set; }
        public string   Version               { get; set; } 

        public new ProcessInfo MemberwiseClone() 
            => (ProcessInfo)base.MemberwiseClone();
    }
}