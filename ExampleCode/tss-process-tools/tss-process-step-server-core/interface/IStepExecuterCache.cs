namespace Tss.Process.StepServer.Domain.Interface {

    /// <summary>
    /// Store for IStepExecuters.
    /// </summary>
    public interface IStepExecuterCache {
        IStepExecuter GetStepExecuter(string stepName);
    }
}