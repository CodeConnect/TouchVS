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

namespace CodeConnect.Touch
{
    /// <summary>
    /// Interaction logic for TouchControl.xaml
    /// </summary>
    public partial class TouchControl : UserControl
    {
        static TouchControl()
        {
        }

        public TouchControl()
        {
            InitializeComponent();
        }

        public TouchControl(int segmentAmount, Window parentWindow)
        {
            InitializeComponent();
            for (int i = 0; i < segmentAmount; i++)
            {
                var segment = TouchControlShapeFactory.GetSegment(segmentAmount, i);
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
                    Text = $"S {i}",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };
                var textCenter = TouchControlShapeFactory.GetTextPosition(segmentAmount, i);
                Canvas.SetLeft(border, textCenter.X - 50);
                Canvas.SetRight(border, textCenter.X + 50);
                Canvas.SetTop(border, textCenter.Y - 20);
                Canvas.SetBottom(border, textCenter.Y + 20);
                border.Child = text;
                touchCanvas.Children.Add(border);

                path.TouchUp += (s, e) =>
                {
                    e.Handled = true;
                    parentWindow.Hide();
                } ;
            }
            /*
            var middleSegment = TouchControlShapeFactory.GetMiddleSegment();
            touchCanvas.Children.Add(middleSegment);
            */
        }
    }
}
