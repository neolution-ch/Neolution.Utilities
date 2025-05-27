namespace Neolution.Utilities.AspNetCore.UnitTests
{
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoFixture;
    using Microsoft.AspNetCore.Http;
    using NSubstitute;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="FormFileExtensions"/> class.
    /// </summary>
    public class FormFileExtensionsTests
    {
        /// <summary>
        /// Tests that <see cref="FormFileExtensions.GetByteArrayAsync(IFormFile, CancellationToken)"/> returns the correct byte array when given a valid <see cref="IFormFile"/>.
        /// </summary>
        /// <param name="fixture">The fixture used to create mock objects.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Theory, AutoNSubstituteData]
        public async Task GivenValidFormFile_WhenGetByteArrayAsyncCalled_ThenReturnsCorrectByteArray(IFixture fixture)
        {
            // Arrange
            const string content = "Hello, World!";
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var formFile = fixture.Create<IFormFile>();
            var cancellationToken = CancellationToken.None;

            // Mock CopyToAsync to write the contentBytes to the provided stream
            formFile
                .CopyToAsync(Arg.Any<Stream>(), cancellationToken)
                .Returns(callInfo =>
                {
                    var stream = callInfo.Arg<Stream>();
                    stream.Write(contentBytes, 0, contentBytes.Length);
                    return Task.CompletedTask;
                });

            // Act
            var result = await formFile.GetByteArrayAsync(cancellationToken);

            // Assert
            result.ShouldBe(contentBytes);
        }
    }
}
