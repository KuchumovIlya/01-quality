﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            text.GetEnumerator().MoveNext();
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
                        return GetNextParagraphWithSeparator(paragraphStartPosition, firstBreakLinePosition, currentPosition);
                    firstBreakLinePosition = currentPosition - 1;
                }
                else if (currentChar != ' ')
                    firstBreakLinePosition = -1;
            }

            return GetNextParagraphWithSeparator(paragraphStartPosition, currentPosition, currentPosition);
        }

        private Tuple<string, string> GetNextParagraphWithSeparator (int paragraphStartPosition,
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
