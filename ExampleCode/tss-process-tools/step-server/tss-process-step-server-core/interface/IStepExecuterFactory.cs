using Tss.Process.Contracts.Interface;

namespace Tss.Process.StepServer.Domain.Interface {
    public interface IStepExecuterFactory {
        IStepExecuter CreateExecuter(IStepDefinition stepDefinition);
    }    
}