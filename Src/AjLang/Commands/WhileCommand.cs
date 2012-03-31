namespace AjLang.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Methods;
    using AjLang.Expressions;
    using System.Collections;
    using AjLang.Language;

    public class WhileCommand : ICommand
    {
        private IExpression expression;
        private ICommand command;

        public WhileCommand(IExpression expression, ICommand command)
        {
            this.expression = expression;
            this.command = command;
        }

        public IExpression Expression { get { return this.expression; } }

        public ICommand Command { get { return this.command; } }

        public object Execute(Context context)
        {
            while (Predicates.IsTrue(this.expression.Evaluate(context)))
                this.command.Execute(context);

            return null;
        }
    }
}
