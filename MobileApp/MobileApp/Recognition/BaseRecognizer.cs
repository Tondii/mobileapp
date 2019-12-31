using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MobileApp.Model.Recognition;

namespace MobileApp.Recognition
{
    internal class BaseRecognizer
    {
        protected List<Word> _words { get; }
        public BaseRecognizer(List<Word> words)
        {
            _words = words;
        }

        protected Word GetFromRight(Word word, bool strongRelation = false)
        {
            if (strongRelation)
            {
                return _words.Where(x => GetRelation(word, x) == WordRelation.InLine && HasStrongRelation(word, x, WordRelation.InLine))
                    .OrderBy(x => x.BoundingRect.X)
                    .FirstOrDefault(x => x.BoundingRect.Left > word.BoundingRect.Right);
            }
            return _words.Where(x => GetRelation(word, x) == WordRelation.InLine)
                .OrderBy(x => x.BoundingRect.X)
                .FirstOrDefault(x => x.BoundingRect.Left > word.BoundingRect.Right);
        }

        protected Word GetFromLeft(Word word)
        {
            return _words.Where(x => GetRelation(word, x) == WordRelation.InLine)
                .OrderBy(x => x.BoundingRect.X)
                .FirstOrDefault(x => x.BoundingRect.Right < word.BoundingRect.Left);
        }

        protected Word GetFromTop(Word word)
        {
            return _words.Where(x => GetRelation(word, x) == WordRelation.InColumn)
                .OrderBy(x => x.BoundingRect.Y)
                .FirstOrDefault(x => x.BoundingRect.Bottom < word.BoundingRect.Top);
        }

        protected Word GetFromBottom(Word word)
        {
            return _words.Where(x => GetRelation(word, x) == WordRelation.InColumn)
                .OrderBy(x => x.BoundingRect.Y)
                .FirstOrDefault(x => x.BoundingRect.Top > word.BoundingRect.Bottom);
        }

        public static WordRelation GetRelation(Word w1, Word w2)
        {
            if (w1 == null || w2 == null)
            {
                return WordRelation.NoExists;
            }

            if (w1.BoundingRect.Y >= w2.BoundingRect.Top && w1.BoundingRect.Y <= w2.BoundingRect.Bottom
                || w2.BoundingRect.Y >= w1.BoundingRect.Top && w2.BoundingRect.Y <= w1.BoundingRect.Bottom)
            {
                return WordRelation.InLine;
            }

            if (w1.BoundingRect.X >= w2.BoundingRect.Left && w1.BoundingRect.X <= w2.BoundingRect.Right
                || w2.BoundingRect.X >= w1.BoundingRect.Left && w2.BoundingRect.X <= w1.BoundingRect.Right)
            {
                return WordRelation.InColumn;
            }

            return WordRelation.NoRelation;
        }

        protected bool IsValidAmount(string text)
        {
            const string amountPattern = @"\d{2,}[,.]\d{2}";
            return Regex.IsMatch(text, amountPattern);
        }

        private bool HasStrongRelation(Word w1, Word w2, WordRelation relation)
        {
            var ratio = GetPixelCorrespondingRatio(w1, w2, relation);
            return ratio > 0.7;
        }

        public static double GetPixelCorrespondingRatio(Word w1, Word w2, WordRelation relation)
        {
            switch (relation)
            {
                case WordRelation.InLine:
                    var y1 = w1.BoundingRect.Top;
                    var y2 = w1.BoundingRect.Bottom;
                    var y3 = w2.BoundingRect.Top;
                    var y4 = w2.BoundingRect.Bottom;

                    if (y1 > y3)
                    {
                        return (double)(y4 - y1) / Math.Min(w1.BoundingRect.Height, w2.BoundingRect.Height);
                    }
                    return (double)(y2 - y3) / Math.Min(w1.BoundingRect.Height, w2.BoundingRect.Height);

                case WordRelation.InColumn:
                    var x1 = w1.BoundingRect.Left;
                    var x2 = w1.BoundingRect.Right;
                    var x3 = w2.BoundingRect.Left;
                    var x4 = w2.BoundingRect.Right;

                    if (x1 > x3)
                    {
                        return (double)(x4 - x1) / Math.Min(w1.BoundingRect.Width, w2.BoundingRect.Width);
                    }
                    return (double)(x2 - x3) / Math.Min(w1.BoundingRect.Width, w2.BoundingRect.Width);

                default:
                    throw new ArgumentOutOfRangeException(nameof(relation), relation, null);
            }
        }
    }
}
