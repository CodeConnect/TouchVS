using System;
using Xunit;
using CodeConnect.Touch.Helpers;

namespace CodeConnect.Touch.Tests
{
    public class RenderingTests
    {
        [Fact]
        public void SegmentCreationEntryPoint()
        {
            var s1 = TouchControlShapeFactory.GetSegment(5, 0);
            var s2 = TouchControlShapeFactory.GetSegment(5, 1);
            var s3 = TouchControlShapeFactory.GetSegment(5, 2);
            var s4 = TouchControlShapeFactory.GetSegment(5, 3);
            var s5 = TouchControlShapeFactory.GetSegment(5, 4);
        }
    }
}
