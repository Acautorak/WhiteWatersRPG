public class AnyActionStartedMessage
{
    public readonly BaseAction action;

    public AnyActionStartedMessage(BaseAction action)
    {
        this.action = action;
    }
}