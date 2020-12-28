namespace Tss.Process.Contracts.Types.Info {

    /// <summary>
    /// StepInfo
    ///
    ///     Used in process definitions to define a process.
    ///
    ///     This type, along with ProcessInfo and ServiceInfo is used
    ///     within process definitions that are to be created in step
    ///     servers. Because of that, they do not require the db keys
    ///     defined in the metadata or entities.
    /// </summary>
    public class StepInfo {

        // Names the step. This name must be globally unique.
        public string Name              { get; set; }

        // Description of the step.
        public string Description       { get; set; }

        // Ordinal position of this step within its parent process.
        public int    Ordinal           { get; set; }

        // Fully qualified name of the type that is the input to
        // the step.
        public string InputTypename     { get; set; }

        // Fully qualified name of the type that is the output of
        // the step.
        public string OutputTypename    { get; set; }
        
        public new StepInfo MemberwiseClone() 
            => (StepInfo)base.MemberwiseClone();
    }
}