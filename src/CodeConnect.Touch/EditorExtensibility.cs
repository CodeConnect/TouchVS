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
    /// Adornment class that draws a square box in the top right hand corner of the viewport
    /// </summary>
    internal sealed class EditorExtensibility
    {
        readonly IWpfTextView view;
        Window touchWindow;
        DateTime lastTouchUpTime;
        bool waitingForSecondTouch;

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
            touchWindow = null;

            (view as UIElement).TouchDown += TouchAdornment_TouchDown;
            (view as UIElement).TouchUp += TouchAdornment_TouchUp;
        }

        /// <summary>
        /// Implementtion 1: Touch with three fingers to start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TouchAdornment_TouchDown(object sender, TouchEventArgs e)
        {
            if ((sender as UIElement).TouchesOver.Count() == 3)
            {
                e.Handled = true;
                var touchPosition = (sender as UIElement).TouchesOver.Select(t => t.GetTouchPoint(null).Position).GetAverage();
                showTouchControl(touchPosition);
            }
        }

        /// <summary>
        /// Implementation 2: Double tap to start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TouchAdornment_TouchUp(object sender, TouchEventArgs e)
        {
            var touchUpTime = DateTime.UtcNow;
            if (waitingForSecondTouch && touchUpTime < lastTouchUpTime + TimeSpan.FromSeconds(1))
            {
                waitingForSecondTouch = false;
                var touchPosition = (sender as UIElement).TouchesOver.Select(t => t.GetTouchPoint(null).Position).GetAverage().FixCoordinates();
                showTouchControl(touchPosition);
                return;
            }
            waitingForSecondTouch = true;
            lastTouchUpTime = touchUpTime;
        }

        private void showTouchControl(Point position)
        {
            if (touchWindow == null)
            {
                touchWindow = new Window()
                {
                    //ShowInTaskbar = false, commented out for debugging
                    ShowActivated = true,
                    AllowsTransparency = true,
                    Background = new SolidColorBrush(Colors.Transparent),
                    WindowStyle = WindowStyle.None,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Width = 2*TouchControlShapeFactory.DIAMETER,
                    Height = 2*TouchControlShapeFactory.DIAMETER,
                };
                touchWindow.Content = new TouchControl(Model.Commands.EntryPoint, touchWindow)
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                };
                touchWindow.LostFocus += (s, e) => { touchWindow.Hide(); (view as UIElement).Focus(); };
                touchWindow.Deactivated += (s, e) => { touchWindow.Hide(); (view as UIElement).Focus(); };
                touchWindow.Closed += (s, e) => touchWindow = null;
            }
            touchWindow.Left = position.X - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Top = position.Y - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Show();
            touchWindow.Focus();
        }
    }
}
