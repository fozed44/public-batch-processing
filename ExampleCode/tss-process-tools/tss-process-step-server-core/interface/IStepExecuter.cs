using Tss.Process.Contracts.Interface;

namespace Tss.Process.StepServer.Core.Interface {

    /// <summary>
    ///
    /// Execute a step given a step definition.
    ///
    /// </summary>
    public interface IStepExecuter {
        string Execute(IStepDefinition stepDefinition, string input);        
    }
}