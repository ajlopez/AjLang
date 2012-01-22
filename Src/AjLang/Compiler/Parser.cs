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
                    IExpression expr = new VariableExpression(token.Value);
                    token = this.NextToken();

                    if (token != null && token.Type != TokenType.Separator && token.Type != TokenType.Operator && token.Type != TokenType.EndOfLine)
                    {
                        this.PushToken(token);
                        return new CallExpression(expr, new IExpression[] { this.ParseExpression() });
                    }

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
            IExpression expression = this.ParseExpression();

            if (expression == null)
                return null;

            Token token = this.NextToken();

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
