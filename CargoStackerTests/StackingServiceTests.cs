using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using CargoStacker.Services;
using Models.Interfaces;

namespace CargoStacker.Tests
{
    [TestClass]
    public class StackingServiceTests
    {
        private StackingService manager;

        [TestInitialize]
        public void Setup()
        {
            manager = new StackingService();
        }

        // ✅ Weight Limit Test
        [TestMethod]
        public void Containers_Exceeding_Weight_Limit_Should_Not_Stack()
        {
            IContainer lower = new RegularContainer(100000);
            IContainer upper = new RegularContainer(30000);

            Assert.IsFalse(manager.ValidateStacking(lower, upper), "Containers exceeding 120000kg should not stack.");
        }

        // ✅ Valid Stack Test
        [TestMethod]
        public void Containers_Under_Weight_Limit_Should_Stack()
        {
            IContainer lower = new RegularContainer(60000);
            IContainer upper = new RegularContainer(40000);

            Assert.IsTrue(manager.ValidateStacking(lower, upper), "Containers within the weight limit should stack.");
        }

        // ✅ Stacking on Empty Container Test
        [TestMethod]
        public void EmptyContainer_Should_Allow_Stacking()
        {
            IContainer empty = new RegularContainer(4000); // Assuming an empty container weighs 4000kg
            IContainer regular = new RegularContainer(25000);

            Assert.IsTrue(manager.ValidateStacking(empty, regular), "An empty container should allow stacking.");
        }

        // ✅ Stacking Limit Edge Case Test
        [TestMethod]
        public void Containers_At_Exactly_Weight_Limit_Should_Stack()
        {
            IContainer lower = new RegularContainer(70000);
            IContainer upper = new RegularContainer(50000);

            Assert.IsTrue(manager.ValidateStacking(lower, upper), "Containers at exactly 120000kg should stack.");
        }

        // ✅ Valuable Container Test
        [TestMethod]
        public void ValuableContainer_Should_Not_Allow_Stacking()
        {
            IContainer valuable = new ValuableContainer(25000);
            IContainer regular = new RegularContainer(30000);

            Assert.IsFalse(manager.ValidateStacking(valuable, regular), "A valuable container should not allow stacking.");
        }

        // ✅ Stacking on Valuable Container Edge Case
        [TestMethod]
        public void ValuableContainer_With_Minimum_Weight_Should_Not_Allow_Stacking()
        {
            IContainer valuable = new ValuableContainer(5000);
            IContainer regular = new RegularContainer(10000);

            Assert.IsFalse(manager.ValidateStacking(valuable, regular), "A valuable container should never allow stacking, even at minimum weight.");
        }
    }
}
