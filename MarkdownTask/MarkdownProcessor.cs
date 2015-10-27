using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownTask
{
    public class MarkdownProcessor
    {
        public static string Markdown(string text)
        {
            var markedParagraphs = MarkdownParagraphs(SplitOnParagraphsWithSeparators(text));
            return string.Join("", markedParagraphs);
        }

        private static IEnumerable<Tuple<string, string>> SplitOnParagraphsWithSeparators(string text)
        {
            var paragraphSplitter = new ParagraphSplitter(text);
            var paragraphsWithSeparators = new List<Tuple<string, string>>();
            while (paragraphSplitter.HasNextParagraph())
                paragraphsWithSeparators.Add(paragraphSplitter.GetNextParagraphWithSeparator());
            return paragraphsWithSeparators;
        }

        private static IEnumerable<string> MarkdownParagraphs(IEnumerable<Tuple<string, string>> paragraphsWithSeparators)
        {
            return paragraphsWithSeparators
                .Select(tuple => tuple.Item1.Length == 0 ? tuple.Item2 : "<p>" + tuple.Item1 + "</p>" + tuple.Item2);
        }
    }
}
