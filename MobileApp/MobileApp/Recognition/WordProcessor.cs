using System.Collections.Generic;
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
                                if (word.Confidence < 0.9)
                                {
                                    continue;
                                }
                                var text = string.Empty;
                                word.Symbols.ForEach(s => text += s.Text);

                                var x = word.BoundingBox.Vertices[0].X;
                                var y = word.BoundingBox.Vertices[0].Y;
                                var width = word.BoundingBox.Vertices[2].X - x;
                                var height = word.BoundingBox.Vertices[2].Y - y;

                                var rect = new Rect(x, y, width, height);
                                var newWord = new Word(text, rect);
                                list.Add(newWord);
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}
