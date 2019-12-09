using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using MobileApp.Extensions;
using MobileApp.Model.Google.Response;
using MobileApp.Model.Recognition;
using Xamarin.Forms.Internals;
using Word = MobileApp.Model.Recognition.Word;

namespace MobileApp.Recognition
{
    internal class WordProcessor
    {
        public static List<Word> ConvertGoogleResponse(ResponseObject response)
        {
            var list = new List<Word>();
            foreach (var imageResponse in response.Responses)
            {
                foreach (var page in imageResponse.FullTextAnnotation.Pages)
                {
                    foreach (var pageBlock in page.Blocks)
                    {
                        foreach (var paragraph in pageBlock.Paragraphs)
                        {
                            foreach (var word in paragraph.Words)
                            {
                                if (word.Confidence < 0.7)
                                {
                                    continue;
                                }
                                var text = string.Empty;
                                word.Symbols.ForEach(s => text += s.Text);

                                var x = word.BoundingBox.Vertices[0].X;
                                var y = word.BoundingBox.Vertices[0].Y;

                                var bottomVertex =
                                    MathTools.Median(word.Symbols.Select(s => s.BoundingBox.Vertices[2].Y).ToList());

                                var width = word.BoundingBox.Vertices[2].X - x;
                                var height = bottomVertex - y;


                                var rect = new Rect(x, y, width, height);
                                var newWord = new Word(text, rect);
                                list.Add(newWord);
                            }
                        }
                    }
                }
            }
            var monetaryPattern = @"\d+[.,]?\d+|[.,]|\d+";
            list = MergeSplitValues(list, monetaryPattern);

            var postalCodePattern = @"\d{2}|[-]|\d{2}[-]";
            return MergeSplitValues(list, postalCodePattern);
        }

        private static List<Word> MergeSplitValues(List<Word> words, string pattern)
        {
            var monetaryWords = words.Where(x => Regex.IsMatch(x.Text, pattern)).ToList();

            var lines = GroupInLines(monetaryWords);

            foreach (var orderedLine in lines.Select(line => line.OrderBy(x => x.BoundingRect.X).ToList()))
            {
                if (orderedLine.Count < 2)
                {
                    continue;
                }
                var wordsToRemove = new List<Word>();
                var newWords = new List<Word>();
                Word buffer = null;
                for (var i = 1; i < orderedLine.Count; i++)
                {
                    var firstWord = orderedLine[i - 1];
                    var secondWord = orderedLine[i];

                    if (CalculateDistance(firstWord, secondWord) <
                        Math.Min(firstWord.BoundingRect.Height, secondWord.BoundingRect.Height))
                    {
                        if (buffer == null)
                        {
                            buffer = firstWord + secondWord;
                            wordsToRemove.Add(firstWord);
                            wordsToRemove.Add(secondWord);
                        }
                        else
                        {
                            buffer += secondWord;
                            wordsToRemove.Add(secondWord);
                        }
                    }
                    else
                    {
                        if (buffer != null)
                        {
                            i++;
                            newWords.Add(buffer);
                            buffer = null;
                        }
                    }
                }
                if (buffer != null)
                {
                    newWords.Add(buffer);
                }
                words = words.Except(wordsToRemove).ToList();
                words = words.Concat(newWords).ToList();
            }

            return words;
        }

        private static List<List<Word>> GroupInLines(List<Word> words)
        {
            var lines = new List<List<Word>>();

            words = words.OrderBy(x => x.BoundingRect.Y).ToList();

            var currentWord = words.First();
            words.Remove(currentWord);
            var nextLine = new List<Word> { currentWord };

            foreach (var word in words)
            {
                if (nextLine.Count == 0)
                {
                    nextLine.Add(word);
                    currentWord = word;
                }
                else
                {
                    if (BaseRecognizer.GetRelation(currentWord, word) == WordRelation.InLine
                        && BaseRecognizer.GetPixelCorrespondingRatio(currentWord, word, WordRelation.InLine) > 0.7)
                    {
                        nextLine.Add(word);
                    }
                    else
                    {
                        lines.Add(nextLine);
                        nextLine = new List<Word>();
                    }
                }
            }
            return lines;
        }

        private static double CalculateDistance(Word w1, Word w2)
        {
            return MathTools.GetDistance(w1.BoundingRect.GetCentralPoint(), w2.BoundingRect.GetCentralPoint());
        }
    }
}
