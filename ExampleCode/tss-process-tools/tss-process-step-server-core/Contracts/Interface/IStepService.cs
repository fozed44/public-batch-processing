namespace Tss.Process.StepServer.Core.Contracts.Interface {

    /// <summary>
    /// Returned by the IStepServciceLoader.LoadService methods.
    /// </summary>
    public interface IStepService {
        IStepRunner                             StepRunner                             { get; set; }
        IProcessControllerNotificationInitiator ProcessControllerNotificationInitiator { get; set; }
    }
}