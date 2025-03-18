namespace Neolution.Utilities.UnitTests.Globalization
{
    using System.Globalization;
    using Neolution.Utilities.Globalization;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="CultureInfoExtensions"/> class.
    /// </summary>
    public class CultureInfoExtensionsTests
    {
        /// <summary>
        /// Given a neutral CultureInfo (e.g., "de" for German), when GetLanguageCode is called,
        /// then it should return the neutral culture language code.
        /// </summary>
        [Fact]
        public void GivenNeutralCultureInfo_WhenGetLanguageCodeCalled_ThenReturnsCultureName()
        {
            // Arrange
            var cultureInfo = new CultureInfo("de");

            // Act
            var result = cultureInfo.GetLanguageCode();

            // Assert
            result.ShouldBe("de");
        }

        /// <summary>
        /// Given a specific CultureInfo (e.g., "de-CH" for German - Switzerland), when GetLanguageCode is called,
        /// then it should return the parent culture language code (e.g., "de").
        /// </summary>
        [Fact]
        public void GivenSpecificCultureInfo_WhenGetLanguageCodeCalled_ThenReturnsParentCultureName()
        {
            // Arrange
            var cultureInfo = new CultureInfo("de-CH");

            // Act
            var result = cultureInfo.GetLanguageCode();

            // Assert
            result.ShouldBe("de");
        }
    }
}
