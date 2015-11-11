using System;
using System.Collections.Generic;

namespace MarkdownTask
{
    public class ParagraphSplitter
    {
        private readonly string text;
        private int currentPosition;

        public ParagraphSplitter(string text)
        {
            this.text = text;
            currentPosition = 0;
        }

        public bool HasNextParagraph()
        {
            return currentPosition < text.Length;
        }

        public Tuple<string, string> GetNextParagraphWithSeparator()
        {
            if (!HasNextParagraph())
                throw new InvalidOperationException();

            var paragraphStartPosition = currentPosition;
            var firstBreakLinePosition = -1;
            while (currentPosition < text.Length)
            {
                var currentChar = text[currentPosition++];

                if (currentChar == '\n')
                {
                    if (firstBreakLinePosition != -1)
                        return GetParagraphWithSeparator(paragraphStartPosition, firstBreakLinePosition, currentPosition);
                    firstBreakLinePosition = currentPosition - 1;
                }
                else if (currentChar != ' ')
                    firstBreakLinePosition = -1;
            }

            return GetParagraphWithSeparator(paragraphStartPosition, currentPosition, currentPosition);
        }

        public IEnumerable<Tuple<string, string>> SplitOnParagraphsWithSeparators()
        {
            while (HasNextParagraph())
                yield return GetNextParagraphWithSeparator();
        }

        private Tuple<string, string> GetParagraphWithSeparator (int paragraphStartPosition,
            int firstBreakLinePosition, int positionAfterSecondBreakLine)
        {
            var paragraphLength = firstBreakLinePosition - paragraphStartPosition;
            var paragraph = text.Substring(paragraphStartPosition, paragraphLength);
            var separatorLength = positionAfterSecondBreakLine - firstBreakLinePosition;
            var separator = text.Substring(firstBreakLinePosition, separatorLength);
            return Tuple.Create(paragraph, separator);
        }
    }
}
