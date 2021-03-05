﻿// <copyright file="IPathResolver.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.Content
{
    /// <summary>
    /// Resolves file paths.
    /// </summary>
    public interface IPathResolver
    {
        /// <summary>
        /// Gets or sets the root directory of the content.
        /// </summary>
        string RootDirectory { get; set; }

        /// <summary>
        /// Gets or sets the name of the content directory.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     This directory is not a path. It is just a name and is always located
        ///     as a child directory of the <see cref="RootDirectory"/>.
        /// </para>
        /// <para>
        ///     If the value is a file path, the file name will be stripped and the
        ///     deepest child directory name will be used.
        /// </para>
        /// </remarks>
        string FileDirectoryName { get; set; }

        /// <summary>
        /// Resolves the full file path to a content item that matches the given <paramref name="contentName"/>.
        /// </summary>
        /// <param name="contentName">The name of the file with or without the file extension.</param>
        /// <returns>
        ///     The <see cref="RootDirectory"/>, <see cref="FileDirectoryName"/> and file directory name combined.
        /// </returns>
        string ResolveFilePath(string contentName);
    }
}
