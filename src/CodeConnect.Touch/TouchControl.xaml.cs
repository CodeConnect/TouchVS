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
