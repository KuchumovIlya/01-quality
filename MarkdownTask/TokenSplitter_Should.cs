using NUnit.Framework;

namespace MarkdownTask
{
    [TestFixture]
    class TokenSplitter_Should
    {
        [Test]
        public void MatchPairEmTags()
        {
            var tokens = new[] {new Token(TokenType.Em, ""), new Token(TokenType.Em, "")};
            var expected = new[] { new Token(TokenType.OpenEm, ""), new Token(TokenType.CloseEm, "") };
            var result = TokenSplitter.MatchPairTokens(tokens);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void MatchPairStrongTags()
        {
            var tokens = new[] { new Token(TokenType.Strong, ""), new Token(TokenType.Strong, "") };
            var expected = new[] { new Token(TokenType.OpenStrong, ""), new Token(TokenType.CloseStrong, "") };
            var result = TokenSplitter.MatchPairTokens(tokens);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void MatchPairCodeTags()
        {
            var tokens = new[] { new Token(TokenType.Code, ""), new Token(TokenType.Code, "") };
            var expected = new[] { new Token(TokenType.OpenCode, ""), new Token(TokenType.CloseCode, "") };
            var result = TokenSplitter.MatchPairTokens(tokens);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void MatchTokensGreedy()
        {
            var tokens = new[] { new Token(TokenType.Code, ""), new Token(TokenType.Em, ""),
                new Token(TokenType.Code, ""), new Token(TokenType.Em, "")};
            var expected = new[] { new Token(TokenType.OpenCode, ""), new Token(TokenType.Em, ""),
                new Token(TokenType.CloseCode, ""), new Token(TokenType.Em, "")};
            var result = TokenSplitter.MatchPairTokens(tokens);
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void MatchNearestPairTokens()
        {
            var tokens = new[] { new Token(TokenType.Em, ""), new Token(TokenType.Em, ""),
                new Token(TokenType.Em, ""), new Token(TokenType.Em, "")};
            var expected = new[] { new Token(TokenType.OpenEm, ""), new Token(TokenType.CloseEm, ""),
                new Token(TokenType.OpenEm, ""), new Token(TokenType.CloseEm, "")};
            var result = TokenSplitter.MatchPairTokens(tokens);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
