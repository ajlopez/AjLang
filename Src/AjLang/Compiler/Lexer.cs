namespace AjLang.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class Lexer
    {
        private TextReader reader;

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            int ich = this.reader.Read();

            while (ich != -1 && char.IsWhiteSpace((char)ich))
                ich = this.reader.Read();

            if (ich == -1)
                return null;

            string value = "" + ((char)ich);

            for (ich = this.reader.Read(); ich != -1 && char.IsLetterOrDigit((char)ich); ich = this.reader.Read())
                value += (char)ich;

            return new Token(value, TokenType.Name);
        }
    }
}
