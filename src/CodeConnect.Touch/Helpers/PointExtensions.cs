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

        public static Point FixCoordinates(this Point point)
        {
            var left = Application.Current.MainWindow.Left;
            var top = Application.Current.MainWindow.Top;
            return new Point(point.X + left, point.Y + top);
        }
    }
}
