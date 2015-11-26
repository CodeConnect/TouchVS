//------------------------------------------------------------------------------
// <copyright file="EditorExtensibility.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows;
using System.Linq;
using CodeConnect.Touch.Helpers;
using System.Windows.Input;

namespace CodeConnect.Touch
{
    /// <summary>
    /// Sets up a touch event that launches the TouchVS control.
    /// </summary>
    internal sealed class EditorExtensibility
    {
        readonly IWpfTextView view;
        DateTime lastTouchUpTime;
        bool waitingForSecondTouch;
        private Point lastDownPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchAdornment"/> class.
        /// Creates a square image and attaches an event handler to the layout changed event that
        /// adds the the square in the upper right-hand corner of the TextView via the adornment layer
        /// </summary>
        /// <param name="view">The <see cref="IWpfTextView"/> upon which the adornment will be drawn</param>
        public EditorExtensibility(IWpfTextView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.view = view;

            (view as UIElement).TouchDown += TouchAdornment_TouchDown;
            (view as UIElement).TouchUp += TouchAdornment_TouchUp;
        }

        private void TouchAdornment_TouchDown(object sender, TouchEventArgs e)
        {
            lastDownPosition = e.TouchDevice.GetTouchPoint(null).Position;
        }

        /// <summary>
        /// Implementation 2: Double tap to start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TouchAdornment_TouchUp(object sender, TouchEventArgs e)
        {
            // Ignore swipes
            var upPosition = e.TouchDevice.GetTouchPoint(null).Position;
            if (!upPosition.IsCloseTo(lastDownPosition, 15.0d))
            {
                return; 
            }

            var touchUpTime = DateTime.UtcNow;
            if (waitingForSecondTouch && touchUpTime < lastTouchUpTime + TimeSpan.FromSeconds(0.5))
            {
                waitingForSecondTouch = false;
                TouchControl.Show(Model.Commands.EntryPoint, e);
                return;
            }
            waitingForSecondTouch = true;
            lastTouchUpTime = touchUpTime;
        }
    }
}
