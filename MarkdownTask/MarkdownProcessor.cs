using System;
using System.Linq;

namespace MarkdownTask
{
    public class MarkdownProcessor
    {
        public static string Markdown(string text)
        {
            text = text.Replace("\r\n", "\n");
            var paragraphsWithSeparators = new ParagraphSplitter(text).SplitOnParagraphsWithSeparators();
            return string.Join("", paragraphsWithSeparators.Select(MarkdownParagraph));
        }

        private static string MarkdownParagraphBySelectTags(string paragraph)
        {
            var matchedTokens = TokenSplitter.MatchPairTokens(TokenSplitter.SplitOnTokens(paragraph).ToArray());
            var markedParagraph = "";

            var inCode = false;
            var inStrong = false;
            var inEm = false;

            foreach (var matchedToken in matchedTokens)
            {
                var oldLength = markedParagraph.Length;

                switch (matchedToken.TokenType)
                {
                    case TokenType.OpenCode:
                        inCode = true;
                        markedParagraph += "<code>";
                        break;
                    
                    case TokenType.CloseCode:
                        markedParagraph += "</code>";
                        inCode = false;
                        break;
                    
                    case TokenType.OpenStrong:
                        if (!inCode)
                        {
                            inStrong = true;
                            markedParagraph += "<strong>";
                        }
                        break;
                    
                    case TokenType.CloseStrong:
                        if (inStrong)
                        {
                            markedParagraph += "</strong>";
                            inStrong = false;
                        }
                        break;
                    
                    case TokenType.OpenEm:
                        if (!inCode && !inStrong)
                        {
                            inEm = true;
                            markedParagraph += "<em>";
                        }
                        break;
                    
                    case TokenType.CloseEm:
                        if (inEm)
                        {
                            markedParagraph += "</em>";
                            inEm = false;
                        }
                        break;
                }

                if (markedParagraph.Length == oldLength)
                    markedParagraph += matchedToken.TextValue;
            }

            return markedParagraph;
        }

        private static string MarkdownParagraphByPTag(Tuple<string, string> paragraphWithSeparator)
        {
            return paragraphWithSeparator.Item1.Length == 0 ?
                paragraphWithSeparator.Item2 : "<p>" + paragraphWithSeparator.Item1 + "</p>" + paragraphWithSeparator.Item2;
        }

        private static string MarkdownParagraph(Tuple<string, string> paragraphWithSeparator)
        {
            var markedParagraphBySelectTags = MarkdownParagraphBySelectTags(paragraphWithSeparator.Item1);
            return MarkdownParagraphByPTag(Tuple.Create(markedParagraphBySelectTags, paragraphWithSeparator.Item2));
        }
    }
}
