using Models.Interfaces;
using Rules.Interfaces;

namespace Rules
{
    public class ValuableContainerRule : IStackingRule
    {
        public bool Validate(IContainer lower, IContainer upper)
        {
            if (lower.IsValuable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
