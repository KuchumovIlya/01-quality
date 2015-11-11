using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MarkdownTask
{
    [TestFixture]
    class ParagraphSplitter_Should
    {
        [Test]
        public void SplitOnParagraphsWithSeparators_ByDoubleBreaklineWithSpaces()
        {
            var paragraphSplitter = new ParagraphSplitter("1\n   \n2");
            var expected = new List<Tuple<string, string> >
                {Tuple.Create("1", "\n   \n"), Tuple.Create("2", "")};
            var result = paragraphSplitter.SplitOnParagraphsWithSeparators();
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void NotSplitOnParagraph_ByDoubleBreaklineWithPrintableChars()
        {
            var paragraphSplitter = new ParagraphSplitter("1\np\n2");
            var expected = new List<Tuple<string, string>> { Tuple.Create("1\np\n2", "")};
            var result = paragraphSplitter.SplitOnParagraphsWithSeparators();
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void NotAddEmptyParagraph_InTheEnd()
        {
            var paragraphSplitter = new ParagraphSplitter("1\n\n");
            var expected = new List<Tuple<string, string>> { Tuple.Create("1", "\n\n") };
            var result = paragraphSplitter.SplitOnParagraphsWithSeparators();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
