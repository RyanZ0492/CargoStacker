using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models.Interfaces;
using Rules;
using Rules.Interfaces;
using Models;

namespace CargoStacker.Tests.Rules
{
    public class DummyContainer : IContainer
    {
        public int Weight { get; set; }
        public bool IsValuable { get; set; }

        public bool CanStackOnTop(IContainer lower)
        {
            return true;
        }

        public DummyContainer(int weight, bool isValuable = false)
        {
            Weight = weight;
            IsValuable = isValuable;
        }
    }

    [TestClass]
    public class StackWeightLimitRuleTests
    {
        [TestMethod]
        public void Validate_ReturnsTrue_WhenCombinedWeightIsWithinLimit()
        {
            var rule = new StackWeightLimitRule();
            var stack = new List<IContainer>
            {
                new DummyContainer(30000)
            };
            var candidate = new DummyContainer(90000);


            bool result = rule.Validate(stack, candidate);


            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_ReturnsFalse_WhenCombinedWeightExceedsLimit()
        {
            var rule = new StackWeightLimitRule();
            var stack = new List<IContainer>
            {
                new DummyContainer(30000)
            };
            var candidate = new DummyContainer(100000);


            bool result = rule.Validate(stack, candidate);


            Assert.IsFalse(result);
        }
    }
}
