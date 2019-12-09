using System;
using System.Collections.Generic;
using System.Linq;
using MobileApp.Model.Recognition;

namespace MobileApp.Extensions
{
    internal static class MathTools
    {
        public static int Median(List<int> list)
        {
            if (list == null || list.Count == 0)
            {
                throw new ArgumentNullException(nameof(list), "can't be null or empty.");
            }

            list = list.OrderBy(x => x).ToList();

            if (!IsEven(list.Count))
            {
                return list.ElementAt((list.Count + 1) / 2 - 1);
            }

            var middleValue1 = list.ElementAt(list.Count / 2);
            var middleValue2 = list.ElementAt(list.Count / 2 - 1);
            return (middleValue1 + middleValue2) / 2;
        }

        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static double GetDistance(Point p1, Point p2)
        {
            return GetDistance(p1.X, p1.Y, p2.X, p2.Y);
        }

    }
}
