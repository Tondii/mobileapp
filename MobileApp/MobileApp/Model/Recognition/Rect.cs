using System.Diagnostics;

namespace MobileApp.Model.Recognition
{
    [DebuggerDisplay("X: {X}, Y: {Y}, Width: {Width}, Height: {Height}")]
    public class Rect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Left => X;
        public int Right => X + Width;
        public int Top => Y;
        public int Bottom => Y + Height;

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Point GetCentralPoint()
        {
            return new Point(X + Width / 2, Y + Height / 2);
        }
    }
}
