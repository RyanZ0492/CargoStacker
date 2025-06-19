using System.Collections.Generic;
using Models.Interfaces;
using Models.Results;
using Rules.Interfaces;

namespace Services
{
    public class StackingService : IStackingEvaluator
    {
        // List of rules that will be run in the order provided.
        private readonly List<IStackingRule> stackingRules;

        // Constructor expects a list of rules.
        public StackingService(List<IStackingRule> rules)
        {
            stackingRules = rules;
        }

        public StackingResult ValidateStacking(List<IContainer> containerStack, IContainer candidate)
        {
            // Check each rule one at a time.
            for (int containers = 0; containers < stackingRules.Count; containers++)
            {
                IStackingRule rule = stackingRules[containers];
                bool rulePassed = rule.Validate(containerStack, candidate);

                if (rulePassed == false)
                {
                    int totalAfterCandidate = CalculateTotalWeight(containerStack) + candidate.Weight;
                    return StackingResult.FailureResult("Stacking rule violated: " + rule.FailureMessage, totalAfterCandidate);
                }
            }
            int newTotalWeight = CalculateTotalWeight(containerStack) + candidate.Weight;
            return StackingResult.SuccessResult(newTotalWeight);
        }

        // A simple helper that calculates the total weight.
        private int CalculateTotalWeight(List<IContainer> containerStack)
        {
            int total = 0;
            for (int index = 0; index < containerStack.Count; index++)
            {
                total = total + containerStack[index].Weight;
            }
            return total;
        }
    }
}
