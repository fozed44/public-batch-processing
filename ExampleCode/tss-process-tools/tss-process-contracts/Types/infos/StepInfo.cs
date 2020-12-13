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
        public string Name              { get; set; }
        public string Description       { get; set; }
        public int    Ordinal           { get; set; }
        public string InputTypename     { get; set; }
        public string OutputTypename    { get; set; }
    }
}