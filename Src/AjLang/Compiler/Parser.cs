namespace AjLang.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Expressions;
    using System.Globalization;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public IExpression ParseExpression()
        {
            Token token = this.NextToken();

            if (token == null)
                return null;

            switch (token.Type)
            {
                case TokenType.Integer:
                    return new ConstantExpression(int.Parse(token.Value, CultureInfo.InvariantCulture));
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }
    }
}
