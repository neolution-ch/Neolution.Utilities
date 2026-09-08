namespace Neolution.Utilities.UnitTests.Extensions;

using Microsoft.Extensions.Configuration;

/// <summary>
/// Unit tests for the <see cref="IConfigurationExtensions"/> class.
/// </summary>
public class IConfigurationExtensionsTests
{
    /// <summary>
    /// The same enum used for testing.
    /// </summary>
    public enum AppSettingKeys
    {
        /// <summary>
        /// The test string key
        /// </summary>
        TestStringKey,

        /// <summary>
        /// The test int key
        /// </summary>
        TestIntKey,
    }

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
    /// Test that given the null section when get options called then throws invalid operation exception.
    /// </summary>
    [Fact]
    public void GivenNullSection_WhenGetOptionsCalled_ThenThrowsInvalidOperationException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions"] = "wrong",
            })
            .Build();

        // Act
        var act = () => configuration.GetOptions<SampleOptions>();

        // Assert
        var ex = Should.Throw<InvalidOperationException>(act);
        ex.Message.ShouldBe("Could not create 'SampleOptions'");
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
    /// Test that given the section with no matching properties when get options called then returns instance with defaults.
    /// </summary>
    [Fact]
    public void GivenSectionWithNoMatchingProperties_WhenGetOptionsCalled_ThenReturnsInstanceWithDefaults()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Irrelevant"] = "ignored",
            })
            .Build();

        // Act
        var options = configuration.GetOptions<SampleOptions>();

        // Assert
        options.Name.ShouldBeNull();
        options.Level.ShouldBe(0);
    }

    /// <summary>
    /// Test that given the section with invalid numeric value when get options called then throws invalid operation exception.
    /// </summary>
    [Fact]
    public void GivenSectionWithInvalidNumericValue_WhenGetOptionsCalled_ThenThrowsInvalidOperationException()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Name"] = "Something",
                ["SampleOptions:Level"] = "not-an-int",
            })
            .Build();

        // Act
        var act = () => configuration.GetOptions<SampleOptions>();

        // Assert
        var ex = Should.Throw<InvalidOperationException>(act);
        ex.Message.ShouldBe("Failed to convert configuration value 'not-an-int' at 'SampleOptions:Level' to type 'System.Int32'.");
    }

    /// <summary>
    /// Test that given the valid section with different casing when get options called then binds successfully.
    /// </summary>
    [Fact]
    public void GivenValidSectionWithDifferentCasing_WhenGetOptionsCalled_ThenBindsSuccessfully()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                // lower-case section name to assert case-insensitive lookup
                ["sampleoptions:Name"] = "CaseTest",
                ["sampleoptions:Level"] = "7",
            })
            .Build();

        // Act
        var options = configuration.GetOptions<SampleOptions>();

        // Assert
        options.Name.ShouldBe("CaseTest");
        options.Level.ShouldBe(7);
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
    /// Test that given the null configuration when get value called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullConfiguration_WhenGetValueCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        IConfiguration? configuration = null;

        // Act
        var act = () => configuration!.GetValue<string, AppSettingKeys>(AppSettingKeys.TestStringKey);

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("config");
    }

    /// <summary>
    /// Gets the value should return correct value when key exists.
    /// </summary>
    [Fact]
    public void GivenConfigurationWithExistingKeys_WhenGetValueCalled_ThenReturnsValues()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["TestStringKey"] = "TestValue",
                ["TestIntKey"] = "7",
            })
            .Build();

        // Act
        var value = configuration.GetValue<string, AppSettingKeys>(AppSettingKeys.TestStringKey);
        var intValue = configuration.GetValue<int, AppSettingKeys>(AppSettingKeys.TestIntKey);

        // Assert
        value.ShouldBe("TestValue");
        intValue.ShouldBe(7);
    }

    /// <summary>
    /// Test that given the null configuration when get value with default called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullConfiguration_WhenGetValueWithDefaultCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        IConfiguration? configuration = null;

        // Act
        var act = () => configuration!.GetValue<string, AppSettingKeys>(AppSettingKeys.TestStringKey, "fallback");

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("config");
    }

    /// <summary>
    /// Test that given a missing key when get value with default called then returns the default value.
    /// </summary>
    [Fact]
    public void GivenMissingKey_WhenGetValueWithDefaultCalled_ThenReturnsDefaultValue()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([])
            .Build();

        // Act
        var value = configuration.GetValue<string, AppSettingKeys>(AppSettingKeys.TestStringKey, "fallback");

        // Assert
        value.ShouldBe("fallback");
    }

    /// <summary>
    /// Test that given an existing key when get value with default called then returns configured value.
    /// </summary>
    [Fact]
    public void GivenExistingKey_WhenGetValueWithDefaultCalled_ThenReturnsConfiguredValue()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["TestIntKey"] = "7",
            })
            .Build();

        // Act
        var value = configuration.GetValue<int, AppSettingKeys>(AppSettingKeys.TestIntKey, 99);

        // Assert
        value.ShouldBe(7);
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
