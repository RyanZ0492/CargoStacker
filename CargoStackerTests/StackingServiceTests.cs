using Models;
using Models.Interfaces;
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
            // Create a list of rules that you want your service to use.
            var stackingRules = new List<IStackingRule>
            {
                new ValuableContainerRule(),
                new TopContainerRule(),
                new StackWeightLimitRule()
                
            };

            // Now inject the list of rules into your service.
            StackingValidator = new StackingService(stackingRules);
        }

        [TestMethod]
        public void Containers_Under_Weight_Limit_Should_Stack()
        {
            // Start with one container and add a second one
            var stack = new List<IContainer> { new RegularContainer(25000) };
            var candidate = new RegularContainer(20000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Stacking successful.", result.Message);
        }

        [TestMethod]
        public void EmptyContainer_Should_Allow_Stacking()
        {
            // With an empty stack, adding a container should succeed.
            var stack = new List<IContainer>();
            var candidate = new RegularContainer(25000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("Stacking successful.", result.Message);
        }

        [TestMethod]
        public void Containers_At_Exactly_Weight_Limit_Should_Stack()
        {
            var stack = new List<IContainer> { new RegularContainer(30000) };
            var candidate = new RegularContainer(30000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            // If the total equals exactly 60000 (for this pair), then stacking is allowed.
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Stacking successful.", result.Message);
        }

        [TestMethod]
        public void ValuableContainer_Should_Not_Allow_Stacking()
        {
            // Valuable container on the bottom prevents stacking.
            var stack = new List<IContainer> { new ValuableContainer(25000) };
            var candidate = new RegularContainer(10000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: ValuableContainerRule", result.Message);
        }


        [TestMethod]
        public void ValuableContainer_With_Minimum_Weight_Should_Not_Allow_Stacking()
        {
            // Even with a light valuable container, its presence should trigger the valuable rule.
            var stack = new List<IContainer> { new ValuableContainer(5000) };
            var candidate = new RegularContainer(10000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            // ValuableContainerRule will be fired because lower container is valuable.
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: ValuableContainerRule", result.Message);
        }

        [TestMethod]
        public void ValuableContainerRule_Should_Reject_Stacking()
        {
            // This test flips roles. If a candidate is valuable but lower is not, then the candidate's
            // stacking restriction (from its override) should prevent it from stacking.
            var stack = new List<IContainer> { new RegularContainer(10000) };
            var candidate = new ValuableContainer(5000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            // For a valuable candidate, candidate.CanStackOnTop returns false.
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: Container restriction", result.Message);
        }

        [TestMethod]
        public void Containers_Exceeding_Weight_Limit_Should_Not_Stack()
        {
            // Build a stack that is almost at the limit, so that adding another container would exceed it.
            var stack = new List<IContainer>
            {
                new RegularContainer(30000),
                new RegularContainer(30000),
                new RegularContainer(30000),
                new RegularContainer(30000)
            };
            var candidate = new RegularContainer(4000);

            var result = StackingValidator.ValidateStacking(stack, candidate);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("Stacking rule violated: StackTotalWeightRule", result.Message);
        }
    }
}
