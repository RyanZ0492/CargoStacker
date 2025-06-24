using Models;
using Rules.Interfaces;
using Rules;
using Services;

namespace CargoStacker.Tests
{
    [TestClass]
    public class StackingServiceTests
    {
        private StackingService StackingValidator;

        [TestInitialize]
        public void Setup()
        {
            var stackingRules = new List<IStackingRule>
            {
                new ValuableContainerRule(),
                new TopContainerRule(),
                new StackWeightLimitRule()
            };

            StackingValidator = new StackingService(stackingRules);
        }

        [TestMethod]
        public void Containers_Under_Weight_Limit_Should_Stack()
        {
            var stack = new ContainerStack();

            stack.TryAddContainer(new RegularContainer(25000), StackingValidator);

            var candidate = new RegularContainer(20000);
            bool added = stack.TryAddContainer(candidate, StackingValidator);

            Assert.IsTrue(added);
            Assert.AreEqual(2, stack.Containers.Count);
        }

        [TestMethod]
        public void EmptyContainer_Should_Allow_Stacking()
        {
            var stack = new ContainerStack();

            var candidate = new RegularContainer(25000);
            bool added = stack.TryAddContainer(candidate, StackingValidator);

            Assert.IsTrue(added);
            Assert.AreEqual(1, stack.Containers.Count);
        }

        [TestMethod]
        public void Containers_At_Exactly_Weight_Limit_Should_Stack()
        {
            var stack = new ContainerStack();
            stack.TryAddContainer(new RegularContainer(30000), StackingValidator);

            var candidate = new RegularContainer(30000);
            bool added = stack.TryAddContainer(candidate, StackingValidator);

            Assert.IsTrue(added);
            Assert.AreEqual(2, stack.Containers.Count);
        }

        [TestMethod]
        public void ValuableContainer_Should_Not_Allow_Stacking()
        {
            var stack = new ContainerStack();
            stack.TryAddContainer(new ValuableContainer(25000), StackingValidator);

            var candidate = new RegularContainer(10000);
            bool added = stack.TryAddContainer(candidate, StackingValidator);

            Assert.IsFalse(added);
            Assert.AreEqual(1, stack.Containers.Count);
        }

        [TestMethod]
        public void ValuableContainer_With_Minimum_Weight_Should_Not_Allow_Stacking()
        {
            var stack = new ContainerStack();
            stack.TryAddContainer(new ValuableContainer(5000), StackingValidator);

            var candidate = new RegularContainer(10000);
            bool added = stack.TryAddContainer(candidate, StackingValidator);

            Assert.IsFalse(added);
            Assert.AreEqual(1, stack.Containers.Count);
        }

        [TestMethod]
        public void Containers_Exceeding_Weight_Limit_Should_Not_Stack()
        {
            var stack = new ContainerStack();
            stack.TryAddContainer(new RegularContainer(30000), StackingValidator);
            stack.TryAddContainer(new RegularContainer(30000), StackingValidator);
            stack.TryAddContainer(new RegularContainer(30000), StackingValidator);
            stack.TryAddContainer(new RegularContainer(30000), StackingValidator);

            var candidate = new RegularContainer(4000);
            bool added = stack.TryAddContainer(candidate, StackingValidator);

            Assert.IsFalse(added);
            Assert.AreEqual(4, stack.Containers.Count);
        }
    }
}