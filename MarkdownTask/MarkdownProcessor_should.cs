using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarkdownTask
{
    [TestFixture]
    class MarkdownProcessor_Should
    {
        [Test]
        public void MarkNewParagrathByPTag_AtTheBeginningOfTheText()
        {
            var processedText = MarkdownProcessor.Markdown("text");
            Assert.AreEqual(processedText, "<p>text</p>");
        }

        [Test]
        public void MarkNewParagrathByPTag_AfterEachDoubleBreakLine()
        {
            var processedText = MarkdownProcessor.Markdown("text\n   \n123\n\n   ");
            Assert.AreEqual(processedText, "<p>text</p>\n   \n<p>123</p>\n\n<p>   </p>");
        }

        [Test]
        public void DoNotMarkEmptyParagraphs()
        {
            var processedText = MarkdownProcessor.Markdown("\n   \n");
            Assert.AreEqual(processedText, "\n   \n");
        }

        [Test]
        public void DoNotMarkNewParagrath_AfterSingleBreakLine()
        {
            var processedText = MarkdownProcessor.Markdown("text\n123");
            Assert.AreEqual(processedText, "<p>text\n123</p>");
        }
    }
}
