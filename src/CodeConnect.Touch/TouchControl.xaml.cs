using CodeConnect.Touch.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CodeConnect.Touch.Model;

namespace CodeConnect.Touch
{
    /// <summary>
    /// Interaction logic for TouchControl.xaml
    /// </summary>
    public partial class TouchControl : UserControl
    {
        /// <summary>
        /// Creates a touch control
        /// </summary>
        /// <param name="entryCommand"></param>
        /// <param name="parentWindow"></param>
        public TouchControl(TouchBranchCommand entryCommand, Window parentWindow)
        {
            InitializeComponent();
            initializeBrushes();

            var segmentAmount = entryCommand.Commands.Count();
            int index = 0;
            foreach (var command in entryCommand.Commands)
            {
                var segment = TouchControlShapeFactory.GetSegment(segmentAmount, index);
                var path = new Path()
                {
                    Data = segment,
                };
                touchCanvas.Children.Add(path);
                var border = new Border()
                {
                    Width = 100,
                    Height = 40,
                    IsHitTestVisible = false,
                };
                var text = new TextBlock()
                {
                    Text = command.DisplayName,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                var textCenter = TouchControlShapeFactory.GetTextPosition(segmentAmount, index);
                Canvas.SetLeft(border, textCenter.X - 50);
                Canvas.SetRight(border, textCenter.X + 50);
                Canvas.SetTop(border, textCenter.Y - 20);
                Canvas.SetBottom(border, textCenter.Y + 20);
                border.Child = text;
                touchCanvas.Children.Add(border);

                var vsCommand = command as TouchVSCommand;
                var branchCommand = command as TouchBranchCommand;
                if (vsCommand != null)
                {
                    path.TouchUp += (s, e) =>
                    {
                        e.Handled = true;
                        VisualStudioModule.ExecuteCommand(vsCommand.VsCommandName, vsCommand.VsCommandParams);
                        parentWindow.Hide();
                    };
                }
                else if (branchCommand != null)
                {
                    path.TouchUp += (s, e) =>
                    {
                        e.Handled = true;
                        parentWindow.Hide();
                        Show(branchCommand, e);
                    };
                }
                index++;
            }
        }

        /// <summary>
        /// Shows a window that contains the TouchControl
        /// </summary>
        /// <param name="entryPoint"></param>
        /// <param name="touchEvent"></param>
        internal static void Show(TouchBranchCommand entryPoint, TouchEventArgs touchEvent)
        {
            var position = touchEvent.GetTouchPoint(null).Position.FixCoordinates(touchEvent.Source as DependencyObject);

            var touchWindow = new Window()
            {
                //ShowInTaskbar = false, commented out for debugging
                ShowActivated = true,
                AllowsTransparency = true,
                Background = new SolidColorBrush(Colors.Transparent),
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Width = 2 * TouchControlShapeFactory.DIAMETER,
                Height = 2 * TouchControlShapeFactory.DIAMETER,
            };
            touchWindow.Content = new TouchControl(entryPoint, touchWindow)
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            touchWindow.LostFocus += (s, e) => { touchWindow.Hide(); };
            touchWindow.Deactivated += (s, e) => { touchWindow.Hide(); };
            touchWindow.Closed += (s, e) => touchWindow = null;
            touchWindow.Left = position.X - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Top = position.Y - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Show();
            touchWindow.Focus();
        }

        private void initializeBrushes()
        {
            this.Resources["foregroundColor"] = VisualStudioModule.ForegroundBrush;
            this.Resources["backgroundColor"] = VisualStudioModule.BackgroundBrush;
            this.Resources["foregroundTransparentColor"] = VisualStudioModule.BackgroundHighlightTransparentBrush;
            this.Resources["backgroundTransparentColor"] = VisualStudioModule.BackgroundTransparentBrush;
        }
    }
}
