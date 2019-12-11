using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MobileApp.Model.Recognition;

namespace MobileApp.Recognition
{
    class DateRecognizer : BaseRecognizer
    {
        public DateRecognizer(List<Word> words) : base(words)
        {
        }

        public DateTime GetSaleDate()
        {
            var datePattern = @"^\d{4}-\d{2}-\d{2}$";
            var dateWord = _words.OrderBy(w => w.BoundingRect.Y).FirstOrDefault(w => Regex.IsMatch(w.Text, datePattern));

            if (dateWord == null)
            {
                return DateTime.Today;
            }

            var success = DateTime.TryParse(dateWord.Text, out var date);

            return success ? date : DateTime.Today;
        }
    }
}
