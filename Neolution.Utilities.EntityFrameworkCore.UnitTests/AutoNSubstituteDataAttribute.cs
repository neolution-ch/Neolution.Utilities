namespace Neolution.Utilities.AspNetCore.UnitTests
{
    using AutoFixture;
    using AutoFixture.AutoNSubstitute;
    using AutoFixture.Xunit2;

    /// <summary>
    /// An attribute that auto-generates data for xUnit theories using AutoFixture and NSubstitute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AutoNSubstituteDataAttribute
        : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoNSubstituteDataAttribute"/> class.
        /// </summary>
        public AutoNSubstituteDataAttribute()
            : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }
}
