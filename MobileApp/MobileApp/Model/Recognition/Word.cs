using System;
using System.Diagnostics;

namespace MobileApp.Model.Recognition
{
    [DebuggerDisplay("\"{Text}\", {BoundingRect}")]
    public class Word
    {
        public string Text { get; set; }
        public Rect BoundingRect { get; set; }

        private Word()
        {

        }

        public Word(string text, Rect boundingRect)
        {
            Text = text;
            BoundingRect = boundingRect;
        }

        public static Word operator +(Word w1, Word w2)
        {
            if (w2.BoundingRect == null)
            {
                return w1;
            }

            if (w1.BoundingRect == null)
            {
                return w2;
            }

            var x = Math.Min(w1.BoundingRect.X, w2.BoundingRect.X);
            var y = Math.Min(w1.BoundingRect.Y, w2.BoundingRect.Y);
            var width = Math.Max(w1.BoundingRect.Right, w2.BoundingRect.Right) - x;
            var height = Math.Max(w1.BoundingRect.Bottom, w2.BoundingRect.Bottom) - y;
            var newRect = new Rect(x, y, width, height);
            var newWord = new Word($"{w1.Text}{w2.Text}", newRect);

            return newWord;
        }

        public static Word Empty => new Word();
    }
}
