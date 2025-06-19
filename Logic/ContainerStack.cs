using System;
using System.Collections.Generic;
using System.Linq;
using Models.Interfaces;

namespace Models
{
    public class ContainerStack
    {
        private readonly List<IContainer> containerList = new();
        public IReadOnlyList<IContainer> Containers => containerList.AsReadOnly();
        public int TotalWeight => containerList.Sum(container => container.Weight);

        public bool TryAddContainer(IContainer newContainer, IStackingEvaluator evaluator)
        {
            var result = evaluator.ValidateStacking(containerList, newContainer);
            Console.WriteLine($"Validation result: {result.Success} - {result.Message}");
            if (result.Success)
            {
                containerList.Add(newContainer);
                return true;
            }
            return false;
        }
    }
}
