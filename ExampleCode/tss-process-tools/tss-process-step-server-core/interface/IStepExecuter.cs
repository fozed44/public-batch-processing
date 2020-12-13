using Tss.Process.Contracts.Interface;

namespace Tss.Process.StepServer.Core.Interface {
    public interface IStepExecuter {
        string Execute(IStepDefinition stepDefinition, string input);
    }
}