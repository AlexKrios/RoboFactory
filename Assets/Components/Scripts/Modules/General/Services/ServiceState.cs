namespace RoboFactory.General.Services
{
    public enum ServiceState
    {
        Disabled,
        WaitingForDependencies,
        Initializing,
        Failed,
        Ready
    }
}