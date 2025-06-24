using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Models.Interfaces;
using Rules;
using Rules.Interfaces;
using Models;

namespace CargoStacker.Tests.Rules
{
    [TestClass]
    public class TopContainerRuleTests
    {
        [TestMethod]
        public void Validate_ReturnsTrue_WhenStackIsEmpty()
        {
            var rule = new TopContainerRule();
            var emptyStack = new List<IContainer>();
            var candidate = new RegularContainer(20000);

            bool result = rule.Validate(emptyStack, candidate);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_ReturnsTrue_WhenCandidateCanStackOnTop()
        {
           
            var rule = new TopContainerRule();
            var stack = new List<IContainer> { new RegularContainer(25000) };
            var candidate = new RegularContainer(20000);

            bool result = rule.Validate(stack, candidate);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_ReturnsFalse_WhenCandidateCannotStackOnTop()
        {
            var rule = new TopContainerRule();
            var stack = new List<IContainer> { new ValuableContainer(25000) };
            
            var candidate = new RegularContainer(20000);

            bool result = rule.Validate(stack, candidate);

            Assert.IsFalse(result);
        }
    }
}
