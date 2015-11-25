using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeConnect.Touch.Helpers
{
    public static class PointExtensions
    {
        public static Point GetAverage(this IEnumerable<Point> points)
        {
            var averageX = points.Average(p => p.X);
            var averageY = points.Average(p => p.Y);
            return new Point(averageX, averageY);
        }

        public static Point FixCoordinates(this Point point, DependencyObject sourceElement = null)
        {
            Window parentWindow;
            if (sourceElement == null)
            {
                parentWindow = Application.Current.MainWindow;
            }
            else
            {
                parentWindow = Window.GetWindow(sourceElement);
            }

            // Don't throw on errors, just degrade UX
            if (parentWindow == null)
            {
                return point;
            }

            var left = parentWindow.Left;
            var top = parentWindow.Top;
            return new Point(point.X + left, point.Y + top);
        }
    }
}
