using Models.Interfaces;
using Services.Results;
using Rules.Interfaces;
using Rules;

namespace Services
{
    public class StackingService : IStackingService
    {
        private readonly List<IStackingRule> rules;

        public StackingService()
        {
            rules = new List<IStackingRule>
            {
                new ValuableContainerRule(),
                new StackWeightLimitRule()
            };
        }

        public StackingResult ValidateStacking(IContainer lower, IContainer upper)
        {
            if (upper.CanStackOnTop(lower) == false)
            {
                return StackingResult.FailureResult("Stacking rule violated: Container restriction", lower.Weight + upper.Weight);
            }

            foreach (var rule in rules)
            {
                bool rulePassed = rule.Validate(lower, upper);
                if (rulePassed == false)
                {
                    string ruleName = rule.GetType().Name;
                    return StackingResult.FailureResult($"Stacking rule violated: {ruleName}", lower.Weight + upper.Weight);
                }
            }

            return StackingResult.SuccessResult(lower.Weight + upper.Weight);
        }


    }
}
