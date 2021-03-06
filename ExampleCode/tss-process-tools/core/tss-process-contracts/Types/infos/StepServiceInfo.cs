using System.Collections.Generic;

namespace Tss.Process.Contracts.Types.Info {

    /// <summary>
    /// StepServiceInfo
    ///
    ///     Used in process definitions to define a process.
    ///
    ///     This type, along with ProcessInfo and StepInfo is used
    ///     within process definitions that are to be created in step
    ///     servers. Because of that, they do not require the db keys
    ///     defined in the metadata or entities.
    /// </summary>
    public class StepServiceInfo {
        public string Name        { get; set; }
        public string Description { get; set; }

        public new StepServiceInfo MemberwiseClone()
            => (StepServiceInfo)base.MemberwiseClone();
    }
}