


namespace Models.Interfaces
{
    public interface IContainer
    {
        int Weight { get; }
        bool IsValuable { get; }
        bool CanStackOnTop(IContainer lower);
    }
}
