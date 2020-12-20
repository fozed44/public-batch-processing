using Tss.Process.Contracts.Interface;

namespace Tss.Process.StepServer.Domain.Interface {

    /// <summary>
    /// Execute a step given a step definition. The step definition
    /// assiciated with the executer will be attached to the execter
    /// by the implementations constructor.
    /// </summary>
    public interface IStepExecuter {
        string Execute(string input);        
    }
}