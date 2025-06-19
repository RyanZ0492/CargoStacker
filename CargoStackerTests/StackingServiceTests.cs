using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using Models.Interfaces;
using Services.Results;

namespace CargoStacker.Tests
{
    [TestClass]
    public class StackingServiceTests
    {
        private StackingService StackingValidator;

        [TestInitialize]
        public void Setup()
        {
            StackingValidator  = new StackingService();
        }

        [TestMethod]
        public void Containers_Under_Weight_Limit_Should_Stack()
        {
            IContainer mediumLower = new RegularContainer(25000);
            IContainer mediumUpper = new RegularContainer(20000);

            StackingResult result = StackingValidator.ValidateStacking(mediumLower, mediumUpper);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Stacking successful.", result.Message);
        }

        [TestMethod]
        public void EmptyContainer_Should_Allow_Stacking()
        {
            IContainer emptyLower = new RegularContainer(4000);
            IContainer regularUpper = new RegularContainer(25000);

            StackingResult result = StackingValidator.ValidateStacking(emptyLower, regularUpper);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Stacking successful.", result.Message);
        }

        [TestMethod]
        public void Containers_At_Exactly_Weight_Limit_Should_Stack()
        {
            IContainer maxLimitLower = new RegularContainer(30000);
            IContainer maxLimitUpper = new RegularContainer(30000);

            StackingResult result = StackingValidator.ValidateStacking(maxLimitLower, maxLimitUpper);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Stacking successful.", result.Message);
        }

        [TestMethod]
        public void ValuableContainer_Should_Not_Allow_Stacking()
        {
            IContainer valuableLower = new ValuableContainer(25000);
            IContainer regularUpper = new RegularContainer(10000);

            StackingResult result = StackingValidator.ValidateStacking(valuableLower, regularUpper);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: ValuableContainerRule", result.Message);
        }

        [TestMethod]
        public void ValuableContainer_With_Minimum_Weight_Should_Not_Allow_Stacking()
        {
            IContainer valuableLower = new ValuableContainer(5000);
            IContainer regularUpper = new RegularContainer(10000);

            StackingResult result = StackingValidator.ValidateStacking(valuableLower, regularUpper);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: ValuableContainerRule", result.Message);
        }

        [TestMethod]
        public void ValuableContainerRule_Should_Reject_Stacking()
        {
            IContainer valuableLower = new ValuableContainer(5000);
            IContainer regularUpper = new RegularContainer(10000);

            StackingResult result = StackingValidator.ValidateStacking(valuableLower, regularUpper);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: ValuableContainerRule", result.Message);
        }

        [TestMethod]
        public void Containers_Exceeding_Weight_Limit_Should_Not_Stack()
        {
            IContainer heavyLower = new RegularContainer(30000);
            IContainer heavyUpper = new RegularContainer(30000);
            IContainer extraUpper = new RegularContainer(30000);

            StackingResult result = StackingValidator.ValidateStacking(heavyLower, heavyUpper);
            StackingResult secondStackResult = StackingValidator.ValidateStacking(heavyUpper, extraUpper);

            Assert.IsTrue(result.Success);
            Assert.IsFalse(secondStackResult.Success);
            Assert.AreEqual("Stacking rule violated: StackWeightLimitRule", secondStackResult.Message);
        }
    }
}
