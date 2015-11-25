using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CodeConnect.Touch.Helpers
{
    public static class TouchControlShapeFactory
    {
        const double FULL_CIRCLE = 360d;
        const double INNER_RADIUS = 50d;
        const double OUTER_RADIUS = 180d;
        const double TEXT_RADIUS = 150d;
        public const double DIAMETER = 2 * OUTER_RADIUS;

        public static Geometry GetSegment(int totalSegments, int index)
        {
            var arcAngle = FULL_CIRCLE / totalSegments;
            var startAngle = -arcAngle / 2 + index * arcAngle;
            var endAngle = startAngle + arcAngle;
            var point1 = ToCartesian(startAngle, OUTER_RADIUS);
            var point2 = ToCartesian(endAngle, OUTER_RADIUS);
            var point3 = ToCartesian(endAngle, INNER_RADIUS);
            var point4 = ToCartesian(startAngle, INNER_RADIUS);
            var pathDefinition = $"M {point1.X} {point1.Y} A {OUTER_RADIUS} {OUTER_RADIUS} 0 0 1 {point2.X} {point2.Y} L {point3.X} {point3.Y} A {INNER_RADIUS} {INNER_RADIUS} 0 0 0 {point4.X} {point4.Y} z";
            var geometry = Geometry.Parse(pathDefinition); 
            return geometry;
        }

        private static Point ToCartesian(double angle, double radius)
        {
            // Rotate the figure and covert the angle to radians
            angle = (angle - 90) * Math.PI / 180;
            return new Point(
                x: radius * Math.Cos(angle) + OUTER_RADIUS,
                y: radius * Math.Sin(angle) + OUTER_RADIUS
                );
        }

        public static Point GetTextPosition(int totalSegments, int index)
        {
            var arcAngle = FULL_CIRCLE / totalSegments;
            var targetAngle = index * arcAngle;
            return ToCartesian(targetAngle, TEXT_RADIUS);
        }

        public static UIElement GetMiddleSegment()
        {
            return new Ellipse()
            {

            };
        }
    }
}
