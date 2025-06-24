using System.Collections.Generic;
using Models.Interfaces;
using Rules.Interfaces;

namespace Rules
{
    public class StackWeightLimitRule : IStackingRule
    {
        public string FailureMessage
        {
            get { return "StackTotalWeightRule"; }
        }

        public bool Validate(List<IContainer> containerStack, IContainer candidate)
        {
            int combinedWeight = 0;

            for (int containers = 0; containers < containerStack.Count; containers++)
            {
                combinedWeight = combinedWeight + containerStack[containers].Weight;
            }
            combinedWeight = combinedWeight + candidate.Weight;

            if (combinedWeight <= 120000)
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
