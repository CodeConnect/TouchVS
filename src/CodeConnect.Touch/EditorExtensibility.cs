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

namespace CodeConnect.Touch
{
    /// <summary>
    /// Adornment class that draws a square box in the top right hand corner of the viewport
    /// </summary>
    internal sealed class EditorExtensibility
    {
        private readonly IWpfTextView view;
        private Window touchWindow;
        private Point lastTouchPosition;

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
        }

        private void TouchAdornment_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            if ((sender as UIElement).TouchesOver.Count() == 3)
            {
                e.Handled = true;
                lastTouchPosition = (sender as UIElement).TouchesOver.Select(t => t.GetTouchPoint(null).Position).GetAverage();
                showTouchControl();
            }
        }



        private void showTouchControl()
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
                touchWindow.Content = new TouchControl(5, touchWindow)
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                };
                touchWindow.LostFocus += (s, e) => { touchWindow.Hide(); (view as UIElement).Focus(); };
                touchWindow.Deactivated += (s, e) => { touchWindow.Hide(); (view as UIElement).Focus(); };
                touchWindow.Closed += (s, e) => touchWindow = null;
            }
            touchWindow.Left = lastTouchPosition.X - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Top = lastTouchPosition.Y - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Show();
        }
    }
}
