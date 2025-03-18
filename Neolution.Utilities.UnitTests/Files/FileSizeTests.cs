namespace Neolution.Utilities.UnitTests.Files
{
    using Neolution.Utilities.Files;
    using Shouldly;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="FileSize"/> class.
    /// </summary>
    public class FileSizeTests
    {
        /// <summary>
        /// Given a size of 0, when GetHumanReadableFileSize is called, then the result should be "0 B".
        /// </summary>
        [Fact]
        public void GivenSizeZero_WhenGetHumanReadableFileSizeCalled_ThenReturnsZeroBytes()
        {
            // Arrange
            const long size = 0;

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("0 B");
        }

        /// <summary>
        /// Given a small file size in bytes, when GetHumanReadableFileSize is called, then it should return the size in bytes (B).
        /// </summary>
        [Fact]
        public void GivenSmallFileSizeInBytes_WhenGetHumanReadableFileSizeCalled_ThenReturnsSizeInBytes()
        {
            // Arrange
            const long size = 500;

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("500 B");
        }

        /// <summary>
        /// Given a file size greater than 1KB, when GetHumanReadableFileSize is called, then it should return the size in KB.
        /// </summary>
        [Fact]
        public void GivenFileSizeGreaterThan1KB_WhenGetHumanReadableFileSizeCalled_ThenReturnsSizeInKB()
        {
            // Arrange
            const long size = 1500; // 1.5 KB

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("1.5 KB");
        }

        /// <summary>
        /// Given a file size greater than 1MB, when GetHumanReadableFileSize is called, then it should return the size in MB.
        /// </summary>
        [Fact]
        public void GivenFileSizeGreaterThan1MB_WhenGetHumanReadableFileSizeCalled_ThenReturnsSizeInMB()
        {
            // Arrange
            const long size = 10 * 1024 * 1024; // 10 MB

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("10 MB");
        }

        /// <summary>
        /// Given a file size greater than 1GB, when GetHumanReadableFileSize is called, then it should return the size in GB.
        /// </summary>
        [Fact]
        public void GivenFileSizeGreaterThan1GB_WhenGetHumanReadableFileSizeCalled_ThenReturnsSizeInGB()
        {
            // Arrange
            const long size = 3L * 1024 * 1024 * 1024; // 3 GB

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("3 GB");
        }

        /// <summary>
        /// Given a file size greater than 1TB, when GetHumanReadableFileSize is called, then it should return the size in TB.
        /// </summary>
        [Fact]
        public void GivenFileSizeGreaterThan1TB_WhenGetHumanReadableFileSizeCalled_ThenReturnsSizeInTB()
        {
            // Arrange
            const long size = 2L * 1024 * 1024 * 1024 * 1024; // 2 TB

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("2 TB");
        }

        /// <summary>
        /// Given a size just under the next unit, when GetHumanReadableFileSize is called, then it should still return the correct human-readable format.
        /// </summary>
        [Fact]
        public void GivenSizeJustUnderNextUnit_WhenGetHumanReadableFileSizeCalled_ThenReturnsCorrectSize()
        {
            // Arrange
            const long size = 1023; // Just under 1 KB

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("1023 B");
        }

        /// <summary>
        /// Given a size at the border of one unit, when GetHumanReadableFileSize is called, then it should round correctly to the next unit.
        /// </summary>
        [Fact]
        public void GivenSizeAtBorderOfUnit_WhenGetHumanReadableFileSizeCalled_ThenRoundsCorrectly()
        {
            // Arrange
            const long size = 1024; // 1 KB

            // Act
            var result = FileSize.GetHumanReadableFileSize(size);

            // Assert
            result.ShouldBe("1 KB");
        }
    }
}
