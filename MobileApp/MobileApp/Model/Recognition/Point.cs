using System.Diagnostics;

namespace MobileApp.Model.Recognition
{
    [DebuggerDisplay("X: {X}, Y: {Y}")]
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
