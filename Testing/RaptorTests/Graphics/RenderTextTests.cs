﻿// <copyright file="RenderTextTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace RaptorTests.Graphics
{
    using System.Drawing;
    using Moq;
    using Raptor;
    using Raptor.Graphics;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Text"/> class.
    /// </summary>
    public class RenderTextTests
    {
        #region Prop Tests
        [Fact]
        public void Color_WhenSettingValue_ProperlySetsInternalValue()
        {
            // Arrange
            var mockInternalText = new Mock<IText>();
            mockInternalText.SetupProperty(m => m.Color, Color.FromArgb(0, 0, 0, 0));
            var renderText = new RenderText();
            var expected = Color.FromArgb(44, 11, 22, 33);

            // Act
            renderText.Color = Color.FromArgb(44, 11, 22, 33);
            var actual = renderText.Color;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Color_WhenComparingColorsThatAreNotEqual_ReturnsFalse()
        {
            // Arrange
            var colorA = Color.FromArgb(4, 1, 2, 3);
            var colorB = Color.FromArgb(44, 11, 22, 33);

            // Act & Assert
            Assert.NotEqual(colorA, colorB);
        }
        #endregion

        #region Overloaded Operator Tests
        [Theory]
        [InlineData("Hello ", "World", "Hello World")]
        [InlineData(null, "World", "World")]
        [InlineData("Hello ", null, "Hello ")]
        [InlineData(null, null, "")]
        public void AddOperator_WhenAddingTwoRenderTexts_ReturnsCorrectResult(string stringA, string stringB, string expected)
        {
            // Arrange
            var mockTextA = new Mock<IText>();
            mockTextA.SetupProperty(m => m.Text);

            var mockTextB = new Mock<IText>();
            mockTextB.SetupProperty(m => m.Text);

            var textA = new RenderText()
            {
                Text = stringA,
            };
            var textB = new RenderText()
            {
                Text = stringB,
            };

            // Act
            var actual = textA + textB;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Hello ", "World", "Hello World")]
        [InlineData(null, "World", "World")]
        [InlineData("Hello ", null, "Hello ")]
        [InlineData(null, null, "")]
        public void AddOperator_WhenAddingRenderTextAndString_ReturnsCorrectResult(string stringA, string stringB, string expected)
        {
            // Arrange
            var mockText = new Mock<IText>();
            mockText.SetupProperty(m => m.Text);

            var textA = new RenderText()
            {
                Text = stringA,
            };

            // Act
            var actual = textA + stringB;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Hello ", "World", "Hello World")]
        [InlineData(null, "World", "World")]
        [InlineData("Hello ", null, "Hello ")]
        [InlineData(null, null, "")]
        public void AddOperator_WhenAddingStringAndRenderText_ReturnsCorrectResult(string stringA, string stringB, string expected)
        {
            // Arrange
            var mockText = new Mock<IText>();
            mockText.SetupProperty(m => m.Text);

            var textB = new RenderText()
            {
                Text = stringB,
            };

            // Act
            var actual = stringA + textB;

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
