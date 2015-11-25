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

                    };
                }
                index++;
            }
            /*
            var middleSegment = TouchControlShapeFactory.GetMiddleSegment();
            touchCanvas.Children.Add(middleSegment);
            */
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
