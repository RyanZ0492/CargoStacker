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
        public bool TryAddContainer(IContainer newContainer, IStackingEvaluator evaluator)
        {
            var result = evaluator.ValidateStacking(containerList, newContainer);

            if (result.Success)
            {
                containerList.Add(newContainer);
                return true;
            }
            return false;
        }
    }
}