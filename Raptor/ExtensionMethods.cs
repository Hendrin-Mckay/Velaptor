﻿// <copyright file="ExtensionMethods.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using OpenTK.Mathematics;
    using SimpleInjector;
    using SimpleInjector.Diagnostics;

    /// <summary>
    /// Provides extensions to various things to help make better code.
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Converts the given <paramref name="radians"/> value into degrees.
        /// </summary>
        /// <param name="radians">The value to convert.</param>
        /// <returns>The radians converted into degrees.</returns>
        public static float ToDegrees(this float radians) => radians * 180.0f / (float)Math.PI;

        /// <summary>
        /// Converts the given <paramref name="degrees"/> value into radians.
        /// </summary>
        /// <param name="degrees">The value to convert.</param>
        /// <returns>The degrees converted into radians.</returns>
        public static float ToRadians(this float degrees) => degrees * (float)Math.PI / 180f;

        /// <summary>
        /// Sets the value to positive if its negative.
        /// </summary>
        /// <param name="value">The value to force.</param>
        /// <returns>The value as a positive number.</returns>
        public static float ForcePositive(this float value) => value < 0 ? value * -1 : value;

        /// <summary>
        /// Sets the value to negative if its positive.
        /// </summary>
        /// <param name="value">The value to force.</param>
        /// <returns>The value as a negative number.</returns>
        public static float ForceNegative(this float value) => value > 0 ? value * -1 : value;

        /// <summary>
        /// Maps the given <paramref name="value"/> from one range to another.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <param name="fromStart">The from starting range value.</param>
        /// <param name="fromStop">The from ending range value.</param>
        /// <param name="toStart">The to starting range value.</param>
        /// <param name="toStop">The to ending range value.</param>
        /// <returns>A value that has been mapped to a range between <paramref name="toStart"/> and <paramref name="toStop"/>.</returns>
        public static float MapValue(this int value, float fromStart, float fromStop, float toStart, float toStop)
            => MapValue((float)value, fromStart, fromStop, toStart, toStop);

        /// <summary>
        /// Maps the given <paramref name="value"/> from one range to another.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <param name="fromStart">The from starting range value.</param>
        /// <param name="fromStop">The from ending range value.</param>
        /// <param name="toStart">The to starting range value.</param>
        /// <param name="toStop">The to ending range value.</param>
        /// <returns>A value that has been mapped to a range between <paramref name="toStart"/> and <paramref name="toStop"/>.</returns>
        public static float MapValue(this float value, float fromStart, float fromStop, float toStart, float toStop)
            => toStart + ((toStop - toStart) * ((value - fromStart) / (fromStop - fromStart)));

        /// <summary>
        /// Maps the given <paramref name="value"/> from one range to another.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <param name="fromStart">The from starting range value.</param>
        /// <param name="fromStop">The from ending range value.</param>
        /// <param name="toStart">The to starting range value.</param>
        /// <param name="toStop">The to ending range value.</param>
        /// <returns>A value that has been mapped to a range between <paramref name="toStart"/> and <paramref name="toStop"/>.</returns>
        public static byte MapValue(this byte value, byte fromStart, byte fromStop, byte toStart, byte toStop)
            => (byte)(toStart + ((toStop - (float)toStart) * ((value - (float)fromStart) / (fromStop - (float)fromStart))));

        /// <summary>
        /// Rotates the <paramref name="vector"/> around the <paramref name="origin"/> at the given <paramref name="angle"/>.
        /// </summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="origin">The origin to rotate the <paramref name="vector"/> around.</param>
        /// <param name="angle">The angle in degrees to rotate <paramref name="vector"/>.  Value must be positive.</param>
        /// <param name="clockWise">Determines the direction the given <paramref name="vector"/> should rotate around the <paramref name="origin"/>.</param>
        /// <returns>The <paramref name="vector"/> rotated around the <paramref name="origin"/>.</returns>
        internal static Vector2 RotateAround(this Vector2 vector, Vector2 origin, float angle, bool clockWise = true)
        {
            var angleRadians = clockWise ? angle.ToRadians() : angle.ToRadians() * -1;

            var cos = (float)Math.Cos(angleRadians);
            var sin = (float)Math.Sin(angleRadians);

            var dx = vector.X - origin.X; // The delta x
            var dy = vector.Y - origin.Y; // The delta y

            var tempX = (dx * cos) - (dy * sin);
            var tempY = (dx * sin) + (dy * cos);

            var x = tempX + origin.X;
            var y = tempY + origin.Y;

            return new Vector2(x, y);
        }

        /// <summary>
        ///     Converts the given <see cref="System.Drawing.Color"/> to a <see cref="Vector4"/>
        ///     with each component holding the color component values.
        /// </summary>
        /// <param name="clr">The color to convert.</param>
        /// <returns>
        ///     A 4 component vector of color values.
        ///     X = red.
        ///     Y = green.
        ///     Z = blue.
        ///     W = alpha.
        /// </returns>
        internal static Vector4 ToVector4(this Color clr) => new Vector4(clr.R, clr.G, clr.B, clr.A);

        /// <summary>
        /// Converts the given <see cref="System.Drawing.Color"/> to a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A color represented by a 4 component vector.</returns>
        internal static Vector4 ToGLColor(this Color value)
        {
            var vec4 = value.ToVector4();
            return vec4.MapValues(0, 255, 0, 1);
        }

        /// <summary>
        /// Maps each component of the vector to from one range to another.
        /// </summary>
        /// <param name="value">The 4 component vector component to map.</param>
        /// <param name="fromStart">The from starting range value.</param>
        /// <param name="fromStop">The from ending range value.</param>
        /// <param name="toStart">The to starting range value.</param>
        /// <param name="toStop">The to ending range value.</param>
        /// <returns>A 4 component vector with each value mapped from one range to another.</returns>
        internal static Vector4 MapValues(this Vector4 value, float fromStart, float fromStop, float toStart, float toStop)
            => new Vector4
            {
                X = value.X.MapValue(fromStart, fromStop, toStart, toStop),
                Y = value.Y.MapValue(fromStart, fromStop, toStart, toStop),
                Z = value.Z.MapValue(fromStart, fromStop, toStart, toStop),
                W = value.W.MapValue(fromStart, fromStop, toStart, toStop),
            };

        /// <summary>
        /// Returns a value indicating whether the given file or directory path
        /// only contains a root drive path with no directories.
        /// </summary>
        /// <param name="fileOrDirPath">The path to check.</param>
        /// <returns>True if there are no directories and is just a root drive.</returns>
        internal static bool IsDirectoryRootDrive(this string fileOrDirPath)
        {
            if (string.IsNullOrEmpty(fileOrDirPath))
            {
                return false;
            }

            var onlyDirPath = Path.HasExtension(fileOrDirPath)
                ? Path.GetDirectoryName(fileOrDirPath)
                : fileOrDirPath;

            if (onlyDirPath.Count(c => c == ':') == 1 && onlyDirPath.Count(c => c == '\\') == 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the last directory name in the given directory or file path.
        /// </summary>
        /// <param name="fileOrDirPath">The path to check.</param>
        /// <returns>The last directory name.</returns>
        /// <remarks>
        /// <para>
        ///     If the <paramref name="fileOrDirPath"/> is a file path, then the file name
        ///     will be stripped and the last directory will be returned.
        /// </para>
        /// <para>
        ///     Example: The path 'C:\temp\dirA\myfile.txt' will return 'dirA'.
        /// </para>
        /// <para>
        ///     If the <paramref name="fileOrDirPath"/> is a directory path, then the
        ///     last directory will be returned.
        /// </para>
        /// <para>
        ///     Example: The path 'C:\temp\dirA\dirB' will return the result 'dirB'.
        /// </para>
        /// </remarks>
        internal static string GetLastDirName(this string fileOrDirPath)
        {
            if (string.IsNullOrEmpty(fileOrDirPath))
            {
                return string.Empty;
            }

            var onlyDirPath = Path.HasExtension(fileOrDirPath)
                ? Path.GetDirectoryName(fileOrDirPath)
                : fileOrDirPath;

            if (string.IsNullOrEmpty(onlyDirPath))
            {
                return string.Empty;
            }

            // If the directory path is just a root drive path
            if (onlyDirPath.IsDirectoryRootDrive())
            {
                var sections = onlyDirPath.Split(':', StringSplitOptions.RemoveEmptyEntries);

                return sections[^1] == Path.DirectorySeparatorChar.ToString()
                    ? onlyDirPath
                    : sections[^1].TrimStart(Path.DirectorySeparatorChar);
            }

            var dirNames = onlyDirPath.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);

            return dirNames[^1];
        }

        /// <summary>
        /// Converts the items of type <see cref="IEnumerable{T}"/> to type <see cref="ReadOnlyCollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of items in the <see cref="IEnumerable{T}"/> list.</typeparam>
        /// <param name="items">The items to convert.</param>
        /// <returns>The items as a read only collection.</returns>
        internal static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> items)
            => new ReadOnlyCollection<T>(items.ToList());

        /// <summary>
        /// Suppresses SimpleInjector diagnostic warnings related to disposing of objects when they
        /// inherit from <see cref="IDisposable"/>.
        /// </summary>
        /// <typeparam name="T">The type to suppress against.</typeparam>
        /// <param name="container">The container that the suppression applies to.</param>
        [ExcludeFromCodeCoverage]
        internal static void SuppressDisposableTransientWarning<T>(this Container container)
        {
            var spriteBatchRegistration = container.GetRegistration(typeof(T))?.Registration;
            spriteBatchRegistration?.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Disposing of objects to be disposed of manually by the library.");
        }

        /// <summary>
        ///     Conditionally registers that a new instance of TImplementation will be returned
        ///     every time a TService is requested (transient) and where the supplied predicate
        ///     returns true. The predicate will only be evaluated a finite number of times;
        ///     the predicate is unsuited for making decisions based on runtime conditions.
        /// </summary>
        /// <typeparam name="TService">The interface or base type that can be used to retrieve the instances.</typeparam>
        /// <typeparam name="TImplementation">The concrete type that will be registered.</typeparam>
        /// <param name="container">The container that the registration applies to.</param>
        /// <param name="predicate">
        ///     The predicate that determines whether the TImplementation can be applied for
        ///     the requested service type. This predicate can be used to build a fallback mechanism
        ///     where multiple registrations for the same service type are made. Note that the
        ///     predicate will be called a finite number of times and its result will be cached
        ///     for the lifetime of the container. It can't be used for selecting a type based
        ///     on runtime conditions.
        /// </param>
        /// <param name="suppressDisposal">True to ignore dispose warnings if the original code invokes dispose.</param>
        /// <remarks>
        ///     This method uses the container's LifestyleSelectionBehavior to select the exact
        ///     lifestyle for the specified type. By default this will be Transient.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when one of the arguments is a null reference.</exception>
        /// <exception cref="InvalidOperationException">Thrown when this container instance is locked and can not be altered.</exception>
        [ExcludeFromCodeCoverage]
        internal static void RegisterConditional<TService, TImplementation>(this Container container, Predicate<PredicateContext> predicate, bool suppressDisposal = false)
            where TService : class
            where TImplementation : class, TService
        {
            container.RegisterConditional<TService, TImplementation>(predicate);

            if (suppressDisposal)
            {
                SuppressDisposableTransientWarning<TService>(container);
            }
        }

        /// <summary>
        ///     Registers that a new instance of TImplementation will be returned every time
        ///     a TService is requested (transient).
        /// </summary>
        /// <typeparam name="TService">The interface or base type that can be used to retrieve the instances.</typeparam>
        /// <typeparam name="TImplementation">The concrete type that will be registered.</typeparam>
        /// <param name="container">The container that the registration applies to.</param>
        /// <param name="suppressDisposal">True to ignore dispose warnings if the original code invokes dispose.</param>
        /// <remarks>
        ///     This method uses the container's LifestyleSelectionBehavior to select the exact
        ///     lifestyle for the specified type. By default this will be Transient.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when one of the arguments is a null reference.</exception>
        /// <exception cref="InvalidOperationException">Thrown when this container instance is locked and can not be altered.</exception>
        [ExcludeFromCodeCoverage]
        internal static void Register<TService, TImplementation>(this Container container, bool suppressDisposal = false)
            where TService : class
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();

            if (suppressDisposal)
            {
                SuppressDisposableTransientWarning<TService>(container);
            }
        }

        /// <summary>
        ///     Registers the specified delegate that allows returning transient instances of
        ///     TService. The delegate is expected to always return a new instance on each call.
        /// </summary>
        /// <typeparam name="TService">The interface or base type that can be used to retrieve instances.</typeparam>
        /// <param name="container">The container that the registration applies to.</param>
        /// <param name="instanceCreator">The delegate that allows building or creating new instances.</param>
        /// <param name="suppressDisposal">True to ignore dispose warnings if the original code invokes dispose.</param>
        /// <exception cref="ArgumentNullException">Thrown when one of the arguments is a null reference.</exception>
        /// <exception cref="InvalidOperationException">Thrown when this container instance is locked and can not be altered.</exception>
        [ExcludeFromCodeCoverage]
        internal static void Register<TService>(this Container container, Func<TService> instanceCreator, bool suppressDisposal = false)
            where TService : class
        {
            container.Register(instanceCreator);

            if (suppressDisposal)
            {
                SuppressDisposableTransientWarning<TService>(container);
            }
        }
    }
}
