﻿// <copyright file="SoundContentSourceTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace RaptorTests.Content
{
    using System.IO;
    using System.IO.Abstractions;
    using System.Reflection;
    using Moq;
    using Raptor.Content;
    using RaptorTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="SoundPathResolver"/> class.
    /// </summary>
    public class SoundContentSourceTests
    {
        private const string ContentName = "test-content";
        private readonly string contentFilePath;
        private readonly string baseDir;
        private readonly string baseContentDir;
        private readonly string atlasContentDir;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoundContentSourceTests"/> class.
        /// </summary>
        public SoundContentSourceTests()
        {
            this.baseDir = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\";
            this.baseContentDir = $@"{this.baseDir}Content\";
            this.atlasContentDir = $@"{this.baseContentDir}Sounds\";
            this.contentFilePath = $"{this.atlasContentDir}{ContentName}";
        }

        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsContentDirectoryNameToCorrectValue()
        {
            // Arrange
            var mockDirectory = new Mock<IDirectory>();

            // Act
            var source = new SoundPathResolver(mockDirectory.Object);
            var actual = source.FileDirectoryName;

            // Assert
            Assert.Equal("Sounds", actual);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void ResolveFilePath_WhenContentItemDoesNotExist_ThrowsException()
        {
            // Arrange
            var mockDirectory = new Mock<IDirectory>();
            mockDirectory.Setup(m => m.GetFiles(this.atlasContentDir))
                .Returns(() =>
                {
                    return new[]
                    {
                        $"{this.baseDir}other-file-A.png",
                        $"{this.baseDir}other-file-B.txt",
                    };
                });

            var resolver = new SoundPathResolver(mockDirectory.Object);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<FileNotFoundException>(() =>
            {
                resolver.ResolveFilePath(ContentName);
            }, $"The sound file '{this.contentFilePath}' does not exist.");
        }

        [Theory]
        [InlineData(".ogg")]
        [InlineData(".mp3")]
        public void ResolveFilePath_WhenBothOggAndMp3FilesExist_ResolvesToOggFile(string extension)
        {
            // Arrange
            var mockDirectory = new Mock<IDirectory>();
            mockDirectory.Setup(m => m.GetFiles(this.atlasContentDir))
                .Returns(() =>
                {
                    return new[]
                    {
                        $"{this.atlasContentDir}other-file.txt",
                        $"{this.contentFilePath}.ogg",
                        $"{this.contentFilePath}.mp3",
                    };
                });

            var resolver = new SoundPathResolver(mockDirectory.Object);

            // Act
            var actual = resolver.ResolveFilePath($"{ContentName}{extension}");

            // Assert
            Assert.Equal($"{this.contentFilePath}.ogg", actual);
        }

        [Fact]
        public void ResolveFilePath_WhenOnlyMp3FilesExist_ResolvesToMp3File()
        {
            // Arrange
            var mockDirectory = new Mock<IDirectory>();
            mockDirectory.Setup(m => m.GetFiles(this.atlasContentDir))
                .Returns(() =>
                {
                    return new[]
                    {
                        $"{this.atlasContentDir}other-file.txt",
                        $"{this.contentFilePath}.mp3",
                    };
                });

            var resolver = new SoundPathResolver(mockDirectory.Object);

            // Act
            var actual = resolver.ResolveFilePath($"{ContentName}.mp3");

            // Assert
            Assert.Equal($"{this.contentFilePath}.mp3", actual);
        }
        #endregion
    }
}
