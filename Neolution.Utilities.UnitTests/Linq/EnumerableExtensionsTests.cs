namespace Neolution.Utilities.UnitTests.Linq
{
    using System.Collections.Generic;
    using Neolution.Utilities.Linq;
    using Shouldly;
    using Xunit;

    public class EnumerableExtensionsTests
    {
        /// <summary>
        /// Given a valid collection and action, when ForEach is called, then it should perform the action on each element.
        /// </summary>
        [Fact]
        public void GivenValidCollectionAndAction_WhenForEachCalled_ThenPerformsActionOnEachElement()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3 }.AsEnumerable();
            var result = new List<int>();

            // Act
            items.ForEach(x => result.Add(x));

            // Assert
            result.ShouldBe([1, 2, 3]);
        }

        /// <summary>
        /// Given a collection of structs and a predicate, when StructFirstOrDefault is called, then it should return the first matching element.
        /// </summary>
        [Fact]
        public void GivenCollectionOfStructsAndPredicate_WhenStructFirstOrDefaultCalled_ThenReturnsFirstMatchingElement()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4 };

            // Act
            var result = items.StructFirstOrDefault(x => x > 2);

            // Assert
            result.ShouldBe(3);
        }

        /// <summary>
        /// Given a collection of structs and a predicate, when StructFirstOrDefault is called and no elements match, then it should return null.
        /// </summary>
        [Fact]
        public void GivenCollectionOfStructsAndPredicate_WhenStructFirstOrDefaultCalledAndNoElementsMatch_ThenReturnsNull()
        {
            // Arrange
            var items = new List<int> { 1, 2, 3, 4 };

            // Act
            var result = items.StructFirstOrDefault(x => x > 4);

            // Assert
            result.ShouldBeNull();
        }
    }
}

