﻿// <copyright file="FreeTypeExtensions.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.NativeInterop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using FreeTypeSharp.Native;
    using Raptor.Exceptions;
    using Raptor.Graphics;

    /// <summary>
    /// Provides extensions to free type library operations to help simplify working with free type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class FreeTypeExtensions : IFreeTypeExtensions
    {
        private readonly IFreeTypeInvoker freeTypeInvoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FreeTypeExtensions"/> class.
        /// </summary>
        /// <param name="freeTypeInvoker">Makes calls to the native FreeType library.</param>
        public FreeTypeExtensions(IFreeTypeInvoker freeTypeInvoker) => this.freeTypeInvoker = freeTypeInvoker;

        /// <inheritdoc/>
        public IntPtr CreateFontFace(IntPtr freeTypeLibPtr, string fontFilePath)
        {
            var result = this.freeTypeInvoker.FT_New_Face(freeTypeLibPtr, fontFilePath, 0);

            if (result == IntPtr.Zero)
            {
                throw new LoadFontException("An invalid pointer value of zero was returned when creating a new font face.");
            }

            return result;
        }

        /// <inheritdoc/>
        public (byte[] pixelData, int width, int height) CreateGlyphImage(IntPtr facePtr, char glyphChar, uint glyphIndex)
        {
            var face = Marshal.PtrToStructure<FT_FaceRec>(facePtr);

            this.freeTypeInvoker.FT_Load_Glyph(facePtr, glyphIndex, FT.FT_LOAD_RENDER);

            int width;
            int height;
            byte[] glyphBitmapData;

            unsafe
            {
                width = (int)face.glyph->bitmap.width;
                height = (int)face.glyph->bitmap.rows;

                glyphBitmapData = new byte[width * height];
                Marshal.Copy(face.glyph->bitmap.buffer, glyphBitmapData, 0, glyphBitmapData.Length);
            }

            return (glyphBitmapData, width, height);
        }

        /// <inheritdoc/>
        public Dictionary<char, GlyphMetrics> CreateGlyphMetrics(IntPtr facePtr, Dictionary<char, uint> glyphIndices)
        {
            var result = new Dictionary<char, GlyphMetrics>();

            foreach (var glyphKeyValue in glyphIndices)
            {
                GlyphMetrics metric = default;

                this.freeTypeInvoker.FT_Load_Glyph(
                    facePtr,
                    glyphKeyValue.Value,
                    FT.FT_LOAD_BITMAP_METRICS_ONLY);

                unsafe
                {
                    var face = Marshal.PtrToStructure<FT_FaceRec>(facePtr);

                    // TODO: Create helper method that can be used to simplify this 65 bit logic
                    if (Environment.Is64BitProcess)
                    {
                        metric.Ascender = (int)face.size->metrics.ascender.ToInt64() >> 6;
                        metric.Descender = (int)face.size->metrics.descender.ToInt64() >> 6;
                        metric.Glyph = glyphKeyValue.Key;
                        metric.CharIndex = glyphKeyValue.Value;

                        metric.XMin = (int)face.bbox.xMin.ToInt64() >> 6;
                        metric.XMax = (int)face.bbox.xMax.ToInt64() >> 6;
                        metric.YMin = (int)face.bbox.yMin.ToInt64() >> 6;
                        metric.YMax = (int)face.bbox.yMax.ToInt64() >> 6;

                        metric.GlyphWidth = (int)face.glyph->metrics.width.ToInt64() >> 6;
                        metric.GlyphHeight = (int)face.glyph->metrics.height.ToInt64() >> 6;
                        metric.HorizontalAdvance = (int)face.glyph->metrics.horiAdvance.ToInt64() >> 6;
                        metric.HoriBearingX = (int)face.glyph->metrics.horiBearingX.ToInt64() >> 6;
                        metric.HoriBearingY = (int)face.glyph->metrics.horiBearingY.ToInt64() >> 6;
                    }
                    else
                    {
                        metric.Ascender = face.size->metrics.ascender.ToInt32() >> 6;
                        metric.Descender = face.size->metrics.descender.ToInt32() >> 6;
                        metric.Glyph = glyphKeyValue.Key;
                        metric.CharIndex = glyphKeyValue.Value;

                        metric.XMin = face.bbox.xMin.ToInt32() >> 6;
                        metric.XMax = face.bbox.xMax.ToInt32() >> 6;
                        metric.YMin = face.bbox.yMin.ToInt32() >> 6;
                        metric.YMax = face.bbox.yMax.ToInt32() >> 6;

                        metric.GlyphWidth = face.glyph->metrics.width.ToInt32() >> 6;
                        metric.GlyphHeight = face.glyph->metrics.height.ToInt32() >> 6;
                        metric.HorizontalAdvance = face.glyph->metrics.horiAdvance.ToInt32() >> 6;
                        metric.HoriBearingX = face.glyph->metrics.horiBearingX.ToInt32() >> 6;
                        metric.HoriBearingY = face.glyph->metrics.horiBearingY.ToInt32() >> 6;
                    }
                }

                result.Add(glyphKeyValue.Key, metric);
            }

            return result;
        }

        /// <inheritdoc/>
        public Dictionary<char, uint> GetGlyphIndices(IntPtr facePtr, char[] glyphChars)
        {
            if (glyphChars is null)
            {
                return new Dictionary<char, uint>();
            }

            var result = new Dictionary<char, uint>();

            for (var i = 0; i < glyphChars.Length; i++)
            {
                var glyphChar = glyphChars[i];

                // Get the glyph image and the character map index
                var charIndex = this.freeTypeInvoker.FT_Get_Char_Index(facePtr, glyphChar);

                result.Add(glyphChar, charIndex);
            }

            return result;
        }

        /// <inheritdoc/>
        public void SetCharacterSize(IntPtr facePtr, int sizeInPoints, uint horiResolution, uint vertResolution)
        {
            var sizeInPointsPtr = (IntPtr)(sizeInPoints << 6);

            this.freeTypeInvoker.FT_Set_Char_Size(
                facePtr,
                sizeInPointsPtr,
                sizeInPointsPtr,
                horiResolution,
                vertResolution);
        }
    }
}
