using System.Collections.Generic;
using Models.Results;

namespace Models.Interfaces
{
    public interface IStackingEvaluator
    {
        // The method takes the current stack (as a List of IContainer) and a candidate container,
        // and returns a StackingResult (which indicates success or failure and contains a message and total weight).
        StackingResult ValidateStacking(List<IContainer> stack, IContainer candidate);
    }
}