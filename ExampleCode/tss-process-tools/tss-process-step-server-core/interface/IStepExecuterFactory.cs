using Tss.Process.Contracts.Interface;

namespace Tss.Process.StepServer.Core.Interface {
    public interface IStepExecuterFactory {
        IStepExecuter CreateExecuter(IStepDefinition stepDefinition);
    }    
}