using NUnit.Framework;

namespace MarkdownTask
{
    [TestFixture]
    class MarkdownProcessor_Should
    {
        [Test]
        public void MarkNewParagraphByPTag_AtTheBeginningOfTheText()
        {
            var processedText = MarkdownProcessor.Markdown("text");
            Assert.AreEqual(processedText, "<p>text</p>");
        }

        [Test]
        public void MarkNewParagraphByPTag_AfterEachDoubleBreakLine()
        {
            var processedText = MarkdownProcessor.Markdown("text\n   \n123\n\n   ");
            Assert.AreEqual(processedText, "<p>text</p>\n   \n<p>123</p>\n\n<p>   </p>");
        }

        [Test]
        public void NotMarkEmptyParagraphs()
        {
            var processedText = MarkdownProcessor.Markdown("\n   \n12\n\n\n\n");
            Assert.AreEqual(processedText, "\n   \n<p>12</p>\n\n\n\n");
        }

        [Test]
        public void NotMarkNewParagraph_AfterSingleBreakLine()
        {
            var processedText = MarkdownProcessor.Markdown("text\n123");
            Assert.AreEqual(processedText, "<p>text\n123</p>");
        }

        [Test]
        public void MarkTextByCodeTag_BetweenSingleQuotes()
        {
            var processedText = MarkdownProcessor.Markdown("'123'");
            Assert.AreEqual(processedText, "<p><code>123</code></p>");
        }

        [Test]
        public void MarkTextByEmTag_BetweenSignleUnderline()
        {
            var processedText = MarkdownProcessor.Markdown("_123_");
            Assert.AreEqual(processedText, "<p><em>123</em></p>");
        }

        [Test]
        public void MarkTextByStrongTag_BetweenDoubleUnderline()
        {
            var processedText = MarkdownProcessor.Markdown("__123__");
            Assert.AreEqual(processedText, "<p><strong>123</strong></p>");
        }

        [Test]
        public void UseBackslashAsEscapeCharacter_ForSelectionTags()
        {
            var processedText = MarkdownProcessor.Markdown("\\'123\\'\\_123\\_\\_\\_123\\_\\_");
            Assert.AreEqual(processedText, "<p>'123'_123___123__</p>");
        }

        [Test]
        public void UseBackslashAsEscapeCharacter_ForBackslash()
        {
            var processedText = MarkdownProcessor.Markdown("\\\\'123'");
            Assert.AreEqual(processedText, "<p>\\<code>123</code></p>");
        }

        [Test]
        public void NotUseBackslashAsEscapeCharacter_ForNewline()
        {
            var processedText = MarkdownProcessor.Markdown("123\\\n\n123");
            Assert.AreEqual(processedText, "<p>123\\</p>\n\n<p>123</p>");
        }

        [Test]
        public void NotMergeNeighboursQuotes()
        {
            var processedText = MarkdownProcessor.Markdown("''''");
            Assert.AreEqual(processedText, "<p><code></code><code></code></p>");
        }

        [Test]
        public void MergeNeighboursUnderlines()
        {
            var processedText = MarkdownProcessor.Markdown("_1__2_ __1____2__");
            Assert.AreEqual(processedText, "<p><em>1__2</em> <strong>1____2</strong></p>");
        }

        [Test]
        public void NotSplitUnderlineSequences()
        {
            var processedText = MarkdownProcessor.Markdown("___1__");
            Assert.AreEqual(processedText, "<p>___1__</p>");
        }

        [Test]
        public void NotMergeEscapedUnderlines()
        {
            var processedText = MarkdownProcessor.Markdown("\\__123_ \\___123__");
            Assert.AreEqual(processedText, "<p>_<em>123</em> _<strong>123</strong></p>");
        }

        [Test]
        public void EscapeUnderline_BetweenQuotes()
        {
            var processedText = MarkdownProcessor.Markdown("'_123_' '__123__'");
            Assert.AreEqual(processedText, "<p><code>_123_</code> <code>__123__</code></p>");
        }

        [Test]
        public void NotEscapeQuotes_BetweenUnderlines()
        {
            var processedText = MarkdownProcessor.Markdown("_'123'_ __'123'__");
            Assert.AreEqual(processedText, "<p><em><code>123</code></em> <strong><code>123</code></strong></p>");
        }

        [Test]
        public void NotEscapeDoubleUnderlines_BetweenSingleUnderlines()
        {
            var processedText = MarkdownProcessor.Markdown("_a__123__a_");
            Assert.AreEqual(processedText, "<p><em>a<strong>123</strong>a</em></p>");
        }

        [Test]
        public void EscapeSingleUnderlines_BetweenDoubleUnderlines()
        {
            var processedText = MarkdownProcessor.Markdown("__a_123_a__");
            Assert.AreEqual(processedText, "<p><strong>a_123_a</strong></p>");
        }

        [Test]
        public void EscapeNotPairQuote()
        {
            var processedText = MarkdownProcessor.Markdown("'123");
            Assert.AreEqual(processedText, "<p>'123</p>");
        }

        [Test]
        public void EscapeNotPairUnderlines()
        {
            var processedText = MarkdownProcessor.Markdown("_123 __123");
            Assert.AreEqual(processedText, "<p>_123 __123</p>");
        }

        [Test]
        public void GreedySplitOnPairs()
        {
            var processedText = MarkdownProcessor.Markdown("_1'2_'");
            Assert.AreEqual(processedText, "<p><em>1'2</em>'</p>");
        }

        [Test]
        public void SplitPairs_BetweenPTag()
        {
            var processedText = MarkdownProcessor.Markdown("_1\n\n2_");
            Assert.AreEqual(processedText, "<p>_1</p>\n\n<p>2_</p>");
        }
    }
}
