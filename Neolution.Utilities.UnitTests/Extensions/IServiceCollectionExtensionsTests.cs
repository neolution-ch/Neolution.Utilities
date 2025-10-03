namespace Neolution.Utilities.UnitTests.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

/// <summary>
/// Unit tests for the <see cref="IServiceCollectionExtensions"/> class.
/// </summary>
public class IServiceCollectionExtensionsTests
{
    /// <summary>
    /// Test that given the null service collection when add options called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullServiceCollection_WhenAddOptionsCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        ServiceCollection? serviceCollection = null;
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?> { ["SampleOptions:Name"] = "X" })
            .Build();

        // Act
        var act = () => serviceCollection!.AddOptions<SampleOptions>(configuration);

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("serviceCollection");
    }

    /// <summary>
    /// Test that given the null configuration when add options called then throws argument null exception.
    /// </summary>
    [Fact]
    public void GivenNullConfiguration_WhenAddOptionsCalled_ThenThrowsArgumentNullException()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        IConfiguration? configuration = null;

        // Act
        var act = () => serviceCollection.AddOptions<SampleOptions>(configuration!);

        // Assert
        var ex = Should.Throw<ArgumentNullException>(act);
        ex.ParamName.ShouldBe("configuration");
    }

    /// <summary>
    /// Test that given the configuration with options section when add options called then options are configured.
    /// </summary>
    [Fact]
    public void GivenConfigurationWithOptionsSection_WhenAddOptionsCalled_ThenOptionsAreConfigured()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Name"] = "Configured",
                ["SampleOptions:Level"] = "7",
            })
            .Build();
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddOptions<SampleOptions>(configuration);
        var provider = serviceCollection.BuildServiceProvider();

        // Assert
        var options = provider.GetRequiredService<IOptions<SampleOptions>>().Value;
        options.Name.ShouldBe("Configured");
        options.Level.ShouldBe(7);
    }

    /// <summary>
    /// Test that given the configuration without options section when add options called then options use defaults.
    /// </summary>
    [Fact]
    public void GivenConfigurationWithoutOptionsSection_WhenAddOptionsCalled_ThenOptionsUseDefaults()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([])
            .Build();
        var serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddOptions<SampleOptions>(configuration);
        var provider = serviceCollection.BuildServiceProvider();

        // Assert
        var options = provider.GetRequiredService<IOptions<SampleOptions>>().Value;
        options.Name.ShouldBeNull();
        options.Level.ShouldBe(0);
    }

    /// <summary>
    /// Test that given the service collection when add options called then same instance is returned for fluent chaining.
    /// </summary>
    [Fact]
    public void GivenServiceCollection_WhenAddOptionsCalled_ThenReturnsSameInstance()
    {
        // Arrange
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection([])
            .Build();
        var services = new ServiceCollection();

        // Act
        var returned = services.AddOptions<SampleOptions>(configuration);

        // Assert
        returned.ShouldBeSameAs(services);
    }

    /// <summary>
    /// Test that given multiple registrations with different configurations when add options called twice then last registration wins.
    /// </summary>
    [Fact]
    public void GivenMultipleRegistrations_WhenAddOptionsCalledTwice_ThenLastRegistrationWins()
    {
        // Arrange
        var configuration1 = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Name"] = "First",
                ["SampleOptions:Level"] = "1",
            })
            .Build();
        var configuration2 = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SampleOptions:Name"] = "Second",
                ["SampleOptions:Level"] = "2",
            })
            .Build();

        var services = new ServiceCollection();

        // Act
        services.AddOptions<SampleOptions>(configuration1);
        services.AddOptions<SampleOptions>(configuration2);
        var provider = services.BuildServiceProvider();

        // Assert
        var value = provider.GetRequiredService<IOptions<SampleOptions>>().Value;
        value.Name.ShouldBe("Second");
        value.Level.ShouldBe(2);
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
