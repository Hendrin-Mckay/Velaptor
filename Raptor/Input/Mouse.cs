﻿// <copyright file="Mouse.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace Raptor.Input
{
    /// <summary>
    /// Provides functionality for the mouse.
    /// </summary>
    public class Mouse : IMouse
    {
        private static int xPos;
        private static int yPos;
        private static int scrollWheelValue;

        /// <inheritdoc/>
        public MouseState GetMouseState()
        {
            var result = default(MouseState);

            result.SetPosition(xPos, yPos);
            result.SetScrollWheelValue(scrollWheelValue);

            // Set all of the states for the buttons
            foreach (var state in IMouse.ButtonStates)
            {
                result.SetButtonState(state.Key, state.Value);
            }

            return result;
        }

        /// <summary>
        /// Sets the position of the mouse state using the given <paramref name="x"/> and <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The X position of the mouse.</param>
        /// <param name="y">The Y position of the mouse.</param>
        internal static void SetPosition(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        /// <summary>
        /// Sets the value of the mouse scroll wheel.
        /// </summary>
        /// <param name="value">The value to set the scroll to.</param>
        internal static void SetScrollWheelValue(int value) => scrollWheelValue = value;

        /// <summary>
        /// Sets the state of the given <paramref name="mouseButton"/> to the given <paramref name="state"/>.
        /// </summary>
        /// <param name="mouseButton">The button to set.</param>
        /// <param name="state">The state to set the button to.</param>
        internal static void SetButtonState(MouseButton mouseButton, bool state) => IMouse.ButtonStates[mouseButton] = state;
    }
}
