using System.Diagnostics;

namespace MobileApp.Model.Recognition
{
    [DebuggerDisplay("{Text}, {BoundingRect}")]
    public class Word
    {
        public string Text { get; set; }
        public Rect BoundingRect { get; set; }

        public Word(string text, Rect boundingRect)
        {
            Text = text;
            BoundingRect = boundingRect;
        }
    }
}
