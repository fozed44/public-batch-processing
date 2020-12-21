namespace Tss.Process.StepServer.Core.Contracts.Interface {

    /// <summary>
    /// Run the step with the given name.
    /// </summary>
    /// <remarks>
    /// A StepRunner is a is on the abstraction layer above the StepExecuter.
    /// </remarks>
    public interface IStepRunner {
        public string Run(string stepName, string input);
    }
}