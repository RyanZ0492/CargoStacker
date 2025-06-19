namespace Rules.Interfaces
{
    public interface IStackingRule
    {
        bool Validate(List<Models.Interfaces.IContainer> stack, Models.Interfaces.IContainer candidate);
        string FailureMessage { get; }
    }
}
