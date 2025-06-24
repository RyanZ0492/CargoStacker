using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models.Interfaces;
using Rules;
using Rules.Interfaces;
using Models; // Verondersteld dat RegularContainer en ValuableContainer hierin zitten

namespace CargoStacker.Tests.Rules
{
    [TestClass]
    public class ValuableContainerRuleTests
    {
        [TestMethod]
        public void Validate_ReturnsTrue_WhenStackIsEmpty()
        {
            var rule = new ValuableContainerRule();
            var emptyStack = new List<IContainer>();
            var candidate = new RegularContainer(25000);

            bool result = rule.Validate(emptyStack, candidate);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_ReturnsTrue_WhenTopContainerIsRegular()
        {
            var rule = new ValuableContainerRule();
            var stack = new List<IContainer> { new RegularContainer(30000) };
            var candidate = new ValuableContainer(5000);

            bool result = rule.Validate(stack, candidate);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_ReturnsFalse_WhenTopContainerIsValuable()
        {
            var rule = new ValuableContainerRule();
            var stack = new List<IContainer> { new ValuableContainer(25000) };
            var candidate = new RegularContainer(10000);

            bool result = rule.Validate(stack, candidate);

            Assert.IsFalse(result);
        }
    }
}
