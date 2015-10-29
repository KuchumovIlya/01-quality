using System.Collections.Generic;

namespace MarkdownTask
{
    public class TokenSplitter
    {
        public static Token[] MatchPairTokens(Token[] tokens)
        {
            var matchedTokens = (Token[])tokens.Clone();
            MatchPairTokens(matchedTokens, 0, matchedTokens.Length - 1);
            return matchedTokens;
        }

        private static void MatchPairTokens(IList<Token> tokens, int first, int last)
        {
            if (first > last)
                return;

            if (tokens[first].TokenType == TokenType.Text)
            {
                MatchPairTokens(tokens, first + 1, last);
                return;
            }

            for (var position = first + 1; position <= last; position++)
            {
                if (tokens[first].TokenType != tokens[position].TokenType) 
                    continue;
                
                tokens[first] = tokens[first].ToOpenToken();
                tokens[position] = tokens[position].ToCloseToken();
                MatchPairTokens(tokens, first + 1, position - 1);
                MatchPairTokens(tokens, position + 1, last);
                return;
            }
        }

        public static IEnumerable<Token> SplitOnTokens(string text)
        {
            var currentPosition = 0;
            while (currentPosition < text.Length)
            {
                switch (text[currentPosition])
                {
                    case '\'':
                        yield return new Token(TokenType.Code, "'");
                        currentPosition++;
                        break;
                    case '_':
                        var token = ParseUnderlineToken(text, currentPosition);
                        currentPosition += token.TextValue.Length;
                        yield return token;
                        break;
                    case '\\':
                        if (currentPosition + 1 < text.Length)
                        {
                            yield return new Token(TokenType.Text, text[currentPosition + 1]);
                            currentPosition += 2;
                        }
                        else
                            yield return new Token(TokenType.Text, text[currentPosition++]);
                        break;
                    default:
                        yield return new Token(TokenType.Text, text[currentPosition++]);
                        break;
                }
            }
        }

        private static Token ParseUnderlineToken(string text, int startPosition)
        {
            var endPosition = startPosition;
            while (endPosition < text.Length && text[endPosition] == '_')
                endPosition++;
            var value = new string('_', endPosition - startPosition);

            if (value == "_")
                return new Token(TokenType.Em, value);
            if (value == "__")
                return new Token(TokenType.Strong, value);
            return new Token(TokenType.Text, value);
        }
    }
}
