using System.Collections.Generic;
using Models.Interfaces;
using Rules.Interfaces;

namespace Rules
{
    public class TopContainerRule : IStackingRule
    {
        // If the rule fails, this message is used.
        public string FailureMessage
        {
            get { return "Container restriction"; }
        }

        public bool Validate(List<IContainer> containerStack, IContainer candidate)
        {
            if (containerStack.Count == 0)
            {
                return true;
            }
            else
            {
                // Get the last container in the list.
                int lastIndex = containerStack.Count - 1;
                IContainer topContainer = containerStack[lastIndex];
                bool allowed = candidate.CanStackOnTop(topContainer);
                if (allowed == true)
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
}
