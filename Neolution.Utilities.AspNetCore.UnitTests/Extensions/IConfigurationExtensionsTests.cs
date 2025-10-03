namespace Neolution.Utilities.AspNetCore.UnitTests.Extensions;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Unit tests for the <see cref="IConfigurationExtensions"/> class.
/// </summary>
public class IConfigurationExtensionsTests
{
    /// <summary>
    /// Test that given the null configuration when get options called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullConfiguration_WhenGetOptionsCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        IConfiguration? configuration = null;

        // Act
        var act = () => configuration!.GetOptions<SampleOptions>();

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("config");
    }

    /// <summary>
    /// Test that given the missing section when get options called then throws invalid operation exception.
    /// </summary>
    [Fact]
    public void GivenMissingSection_WhenGetOptionsCalled_ThenThrowsInvalidOperationException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([]) // No section for SampleOptions
            .Build();

        // Act
        var act = () => configuration.GetOptions<SampleOptions>();

        // Assert
        var ex = Should.Throw<InvalidOperationException>(act);
        ex.Message.ShouldBe("Could not find configuration section 'SampleOptions'");
    }

    /// <summary>
    /// Test that given the valid section when get options called then binds and returns options.
    /// </summary>
    [Fact]
    public void GivenValidSection_WhenGetOptionsCalled_ThenBindsAndReturnsOptions()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Name"] = "TestName",
                ["SampleOptions:Level"] = "42",
            })
            .Build();

        // Act
        var options = configuration.GetOptions<SampleOptions>();

        // Assert
        options.Name.ShouldBe("TestName");
        options.Level.ShouldBe(42);
    }

    /// <summary>
    /// Test that given the null configuration when get section called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullConfiguration_WhenGetSectionCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        IConfiguration? configuration = null;

        // Act
        var act = () => configuration!.GetSection<SampleOptions>();

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("config");
    }

    /// <summary>
    /// Test that given the existing section when get section called then returns section.
    /// </summary>
    [Fact]
    public void GivenExistingSection_WhenGetSectionCalled_ThenReturnsSection()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Name"] = "X",
            })
            .Build();

        // Act
        var section = configuration.GetSection<SampleOptions>();

        // Assert
        section.Key.ShouldBe("SampleOptions");
        section.Exists().ShouldBeTrue();
    }

    /// <summary>
    /// Test that given the missing section when get section called then returns non existing section.
    /// </summary>
    [Fact]
    public void GivenMissingSection_WhenGetSectionCalled_ThenReturnsNonExistingSection()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([])
            .Build();

        // Act
        var section = configuration.GetSection<SampleOptions>();

        // Assert
        section.Key.ShouldBe("SampleOptions");
        section.Exists().ShouldBeFalse();
    }

    /// <summary>
    /// The sample options class used for testing.
    /// </summary>
    public class SampleOptions
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }
    }
}
