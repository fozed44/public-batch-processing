namespace Tss.Process.StepServer.Core.Contracts.Interface {

    /// <summary>
    /// Use an implementation of IProcessControllerNotificationInitiator to
    /// notify the process controller of the existance of this step server.
    /// </summary>
    public interface IProcessControllerNotificationInitiator {
        void InitiateProcessControllerNotification();
    }
}