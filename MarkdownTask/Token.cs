using System;

namespace MarkdownTask
{
    public enum TokenType
    {
        Text,
        Code,
        OpenCode,
        CloseCode,
        Em,
        OpenEm,
        CloseEm,
        Strong,
        OpenStrong,
        CloseStrong
    }

    public class Token
    {
        public readonly TokenType TokenType;
        public readonly string TextValue;

        public Token(TokenType tokenType, string textValue)
        {
            TokenType = tokenType;
            TextValue = textValue;
        }

        public Token(TokenType tokenType, char value)
        {
            TokenType = tokenType;
            TextValue = value.ToString();
        }

        public Token ToOpenToken()
        {
            switch (TokenType)
            {
                case TokenType.Code:
                    return new Token(TokenType.OpenCode, TextValue);
                case TokenType.Em:
                    return new Token(TokenType.OpenEm, TextValue);
                case TokenType.Strong:
                    return new Token(TokenType.OpenStrong, TextValue);
            }
            throw new InvalidOperationException();
        }

        public Token ToCloseToken()
        {
            switch (TokenType)
            {
                case TokenType.Code:
                    return new Token(TokenType.CloseCode, TextValue);
                case TokenType.Em:
                    return new Token(TokenType.CloseEm, TextValue);
                case TokenType.Strong:
                    return new Token(TokenType.CloseStrong, TextValue);
            }
            throw new InvalidOperationException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Token);
        }

        public bool Equals(Token other)
        {
            if (other == null)
                return false;
            return TokenType == other.TokenType && TextValue == other.TextValue;
        }
    }
}
