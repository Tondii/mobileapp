using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MobileApp.Extensions;
using MobileApp.Model.Recognition;

namespace MobileApp.Recognition
{
    internal class TotalAmountRecognizer : BaseRecognizer
    {
        private readonly List<string> _totalAmountKeywords = new List<string>()
        {
            "suma"
        };

        public TotalAmountRecognizer(List<Word> words) : base(words)
        {
        }

        public float GetTotalAmount()
        {
            var keywords = _words.Where(x => x.Text.ToLower().ContainsAny(_totalAmountKeywords)).ToList();

            if (!keywords.Any())
            {
                var amounts = _words.Where(x => IsValidAmount(x.Text))
                    .Select(x => float.Parse(x.Text))
                    .ToList();
                return amounts.Max();
            }

            var biggestKeyword = keywords.OrderByDescending(x => x.BoundingRect.Height)
                .First();

            var currentWord = biggestKeyword;
            var rightWord = GetFromRight(currentWord);
            while (rightWord != null && !IsValidAmount(rightWord.Text))
            {
                currentWord = rightWord;
                rightWord = GetFromRight(currentWord, true);
            }

            if (rightWord != null && IsValidAmount(rightWord.Text))
            {
                return float.Parse(Regex.Replace(rightWord.Text, ",", "."));
            }
            return 0.00F;
        }
    }
}
