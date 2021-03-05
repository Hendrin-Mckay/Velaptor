﻿// <copyright file="ContentPathResolverTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace RaptorTests.Content
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Raptor.Content;
    using RaptorTests.Fakes;
    using RaptorTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ContentPathResolver"/> class.
    /// </summary>
    public class ContentPathResolverTests
    {
        private const string ContentName = "test-content";
        private static readonly string BaseDir = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\";

        /// <summary>
        /// Gets test data for the <see cref="RootDirectory_WhenSettingValue_ReturnsCorrectValue"/> test.
        /// </summary>
        public static IEnumerable<object[]> ContentRootPaths =>
            new List<object[]>
            {
                new object[] { null, @$"{BaseDir}Content\" },
                new object[] { @"C:\temp\", @"C:\temp\Content\" },
                new object[] { @"C:\temp", @"C:\temp\Content\" },
            };

        #region Prop Tests
        [Theory]
        [MemberData(nameof(ContentRootPaths))]
        public void RootDirectory_WhenSettingValue_ReturnsCorrectValue(string rootDirectory, string expected)
        {
            // Arrange
            var resolver = new ContentPathResolverFake();

            // Act
            resolver.RootDirectory = rootDirectory;
            var actual = resolver.RootDirectory;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FileDirectoryName_WhenSettingWithDirectoryPath_CorrectlySetsResult()
        {
            // Arrange
            var resolver = new ContentPathResolverFake();

            // Act
            resolver.FileDirectoryName = @"C:\temp\test-dir-name";
            var actual = resolver.FileDirectoryName;

            // Assert
            Assert.Equal("test-dir-name", actual);
        }

        [Fact]
        public void FileDirectoryName_WhenValueIsNullOrEmpty_ThrowsExeption()
        {
            // Arrange
            var resolver = new ContentPathResolverFake();

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<Exception>(() =>
            {
                resolver.FileDirectoryName = null;
            }, "The 'FileDirectoryName' must not be null or empty.");
        }

        [Theory]
        [InlineData("test-content")]
        [InlineData("test-content.json")]
        public void ResolveFilePath_WhenInvoked_ResolvesContentFilePath(string contentName)
        {
            // Arrange
            var resolver = new ContentPathResolverFake();

            // Act
            var actual = resolver.ResolveFilePath(contentName);

            // Assert
            Assert.Equal(ContentName, actual);
        }

        [Fact]
        public void ResolveFilePath_WhenContentNameIsNullOrEmpty_ThrowsException()
        {
            // Arrange
            var resolver = new ContentPathResolverFake();

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                resolver.ResolveFilePath(null);
            }, "The parameter must not be null or empty. (Parameter 'contentName')");
        }

        [Fact]
        public void ResolveFilePath_WhenContentNameEndsWithDirSeparator_ThrowsException()
        {
            // Arrange
            var resolver = new ContentPathResolverFake();

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentException>(() =>
            {
                resolver.ResolveFilePath($@"{ContentName}\");
            }, $@"The '{ContentName}\' cannot end with a folder.  It must end with a file name with or without the extension. (Parameter 'contentName')");
        }
        #endregion
    }
}
