using Models.Interfaces;

namespace Rules.Interfaces
{
    public interface IStackingRule
    {
        bool Validate(IContainer lower, IContainer upper);
    }
}
