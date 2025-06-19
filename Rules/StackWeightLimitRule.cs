using Models.Interfaces;
using Rules.Interfaces;

namespace Rules
{
    public class StackWeightLimitRule : IStackingRule
    {
            public bool Validate(IContainer lower, IContainer upper)
            {
                int totalStackingWeight = lower.Weight + upper.Weight;

                if (totalStackingWeight <= 120000)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

