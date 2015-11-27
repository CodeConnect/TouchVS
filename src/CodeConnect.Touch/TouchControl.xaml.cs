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
        // Caches the windows with their controls so that they don't need to be regenerated on each touch.
        static Dictionary<TouchBranchCommand, Window> windowCache = new Dictionary<TouchBranchCommand, Window>();
        // Designates area available for the text labels.
        const int TEXT_AVAILABLE_WIDTH = 100;
        const int TEXT_AVAILABLE_HEIGHT = 40;
        // Used to store the current/last window position
        static Point windowPosition = new Point(0, 0);

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
                // Slice shape:
                var segment = TouchControlShapeFactory.GetSegment(segmentAmount, index);
                var path = new Path()
                {
                    Data = segment,
                };
                touchCanvas.Children.Add(path);

                // Invisible border that holds and centers the text label:
                var border = new Border()
                {
                    Width = TEXT_AVAILABLE_WIDTH,
                    Height = TEXT_AVAILABLE_HEIGHT,
                    IsHitTestVisible = false,
                };
                var textCenter = TouchControlShapeFactory.GetTextPosition(segmentAmount, index);
                Canvas.SetLeft(border, textCenter.X - TEXT_AVAILABLE_WIDTH / 2);
                Canvas.SetRight(border, textCenter.X + TEXT_AVAILABLE_WIDTH / 2);
                Canvas.SetTop(border, textCenter.Y - TEXT_AVAILABLE_HEIGHT / 2);
                Canvas.SetBottom(border, textCenter.Y + TEXT_AVAILABLE_HEIGHT / 2);

                // The text label, centered inside the invisible border
                var text = new TextBlock()
                {
                    Text = command.DisplayName,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                border.Child = text;
                touchCanvas.Children.Add(border);

                // Give action to the slice. Looking forward to using extended `is` expression in pattern matching (https://github.com/dotnet/roslyn/issues/206)
                var vsCommand = command as TouchVSCommand;
                var branchCommand = command as TouchBranchCommand;
                if (vsCommand != null)
                {
                    Action<RoutedEventArgs> handle = (RoutedEventArgs args) =>
                    {
                        args.Handled = true;
                        VisualStudioModule.ExecuteCommand(vsCommand.VsCommandName, vsCommand.VsCommandParams);
                        parentWindow.Hide();
                    };

                    path.MouseLeftButtonUp += (s, e) => { handle(e); };
                    path.TouchUp += (s, e) => { handle(e); };
                }
                else if (branchCommand != null)
                {
                    Action<RoutedEventArgs, Point> handle = (RoutedEventArgs args, Point position) =>
                    {
                        args.Handled = true;
                        parentWindow.Hide();
                        Show(branchCommand, windowPosition);
                    };

                    path.MouseLeftButtonUp += (s, e) => { handle(e, e.GetPosition(null)); };
                    path.TouchUp += (s, e) => { handle(e, e.GetTouchPoint(null).Position); };
                }

                index++;
            }
        }

        /// <summary>
        /// Shows a window that contains the TouchControl
        /// </summary>
        /// <param name="entryPoint"></param>
        /// <param name="touchEvent"></param>
        internal static void Show(TouchBranchCommand entryPoint, Point position)
        {
            Window touchWindow;
            if (!windowCache.TryGetValue(entryPoint, out touchWindow))
            {
                touchWindow = new Window()
                {
                    ShowInTaskbar = false,
                    ShowActivated = true,
                    AllowsTransparency = true,
                    Background = new SolidColorBrush(Colors.Transparent),
                    WindowStyle = WindowStyle.None,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Width = TouchControlShapeFactory.DIAMETER + 2, // 2 accounts for 1px margins of the canvas
                    Height = TouchControlShapeFactory.DIAMETER + 2,
                };
                touchWindow.Content = new TouchControl(entryPoint, touchWindow)
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                };
                touchWindow.LostFocus += (s, e) => { touchWindow.Hide(); };
                touchWindow.Deactivated += (s, e) => { touchWindow.Hide(); };
                touchWindow.Closed += (s, e) =>
                {
                    touchWindow.Content = null;
                    touchWindow = null;
                    windowCache.Remove(entryPoint);
                };

                windowCache.Add(entryPoint, touchWindow);
            }

            // Move the window such that "position" point is in the middle, and show the window
            touchWindow.Left = position.X - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Top = position.Y - TouchControlShapeFactory.DIAMETER / 2;
            touchWindow.Show();
            touchWindow.Focus();

            // Update the window position
            windowPosition = position;
        }

        private void initializeBrushes()
        {
            this.Resources["foregroundColor"] = VisualStudioModule.ForegroundBrush;
            this.Resources["backgroundColor"] = VisualStudioModule.BackgroundBrush;
            this.Resources["backgroundHighlightTransparentColor"] = VisualStudioModule.BackgroundHighlightTransparentBrush;
            this.Resources["backgroundTransparentColor"] = VisualStudioModule.BackgroundTransparentBrush;
        }
    }
}
