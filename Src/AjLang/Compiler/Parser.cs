namespace AjLang.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Expressions;
    using System.Globalization;
    using AjLang.Commands;

    public class Parser
    {
        private Lexer lexer;
        private Stack<Token> tokens = new Stack<Token>();

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
                
                case TokenType.Name:
                    string name = token.Value;

                    token = this.NextToken();

                    if (token != null && token.Type != TokenType.Separator && token.Type != TokenType.Operator && token.Type != TokenType.EndOfLine)
                    {
                        this.PushToken(token);
                        return new CallExpression(new NameExpression(name), new IExpression[] { this.ParseExpression() });
                    }

                    if (token != null && token.Type == TokenType.Separator && token.Type == TokenType.Separator && token.Value == "(")
                    {
                        this.PushToken(token);
                        return new CallExpression(new NameExpression(name), this.ParseArguments());
                    }

                    IExpression expr = new VariableExpression(name);

                    if (token != null)
                        this.PushToken(token);

                    return expr;

                case TokenType.String:
                    return new ConstantExpression(token.Value);
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        public IList<ICommand> ParseCommands()
        {
            ICommand command = this.ParseCommand();

            if (command == null)
                return null;

            IList<ICommand> commands = new List<ICommand>();
            commands.Add(command);

            while ((command = this.ParseCommand()) != null)
                commands.Add(command);

            return commands;
        }

        public ICommand ParseCommand()
        {
            Token token = this.NextToken();

            while (token != null && token.Type == TokenType.EndOfLine)
                token = this.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Name && token.Value == "def")
                return this.ParseDefineCommand();

            if (token.Type == TokenType.Name && token.Value == "end")
            {
                this.PushToken(token);
                return null;
            }

            this.PushToken(token);

            IExpression expression = this.ParseExpression();

            if (expression == null)
                return null;

            token = this.NextToken();

            if (token == null || token.Type == TokenType.EndOfLine || (token.Type == TokenType.Separator && token.Value == ";"))
                return new ExpressionCommand(expression);

            if (expression is VariableExpression && token.Type == TokenType.Operator && token.Value == "=")
            {
                string name = ((VariableExpression)expression).Name;
                IExpression expr = this.ParseExpression();
                this.ParseEndOfCommand();

                return new SetVariableCommand(name, expr);
            }

            throw new ParserException("Error in Parser");
        }

        private IEnumerable<IExpression> ParseArguments()
        {
            IList<IExpression> arguments = new List<IExpression>();

            this.ParseToken("(", TokenType.Separator);

            while (!this.TryParseToken(")", TokenType.Separator))
            {
                if (arguments.Count > 0)
                    this.ParseToken(",", TokenType.Separator);
                arguments.Add(this.ParseExpression());
            }

            return arguments;
        }

        private DefineCommand ParseDefineCommand()
        {
            string name = this.ParseName();
            IList<string> argnames = null;

            if (this.TryParseToken("(", TokenType.Separator))
                argnames = this.ParseArgumentNames();

            this.ParseEndOfCommand();

            IList<ICommand> commands = this.ParseCommands();

            string end = this.ParseName();

            if (end != "end")
                throw new ParserException("'end' Expected");

            this.ParseEndOfCommand();

            return new DefineCommand(name, argnames, new CompositeCommand(commands));
        }

        private void ParseEndOfCommand()
        {
            Token token = this.NextToken();

            if (token == null)
                return;

            if (token.Type == TokenType.Separator && token.Value == ";")
                return;

            if (token.Type == TokenType.EndOfLine)
                return;

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private string ParseName()
        {
            Token token = this.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new ParserException("Name expected");

            return token.Value;
        }

        private IList<string> ParseArgumentNames()
        {
            IList<string> argnames = new List<string>();

            while (!this.TryParseToken(")", TokenType.Separator))
            {
                if (argnames.Count > 0)
                    this.ParseToken(",", TokenType.Separator);

                argnames.Add(this.ParseName());
            }

            return argnames;
        }

        private bool TryParseToken(string value, TokenType type)
        {
            Token token = this.NextToken();

            if (token != null && token.Value == value && token.Type == type)
                return true;

            this.PushToken(token);

            return false;
        }

        private void ParseToken(string value, TokenType type)
        {
            Token token = this.NextToken();

            if (token == null || token.Value != value || token.Type != type)
                throw new ParserException(string.Format("Expected '{0}'", value));
        }

        private Token NextToken()
        {
            if (this.tokens.Count>0)
                return this.tokens.Pop();

            return this.lexer.NextToken();
        }

        private void PushToken(Token token)
        {
            this.tokens.Push(token);
        }
    }
}
