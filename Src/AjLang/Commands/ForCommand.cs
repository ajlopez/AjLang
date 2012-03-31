namespace AjLang.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Methods;
    using AjLang.Expressions;
    using System.Collections;

    public class ForCommand : ICommand
    {
        private string varname;
        private IExpression expression;
        private ICommand command;

        public ForCommand(string varname, IExpression expression, ICommand command)
        {
            this.varname = varname;
            this.expression = expression;
            this.command = command;
        }

        public string VariableName { get { return this.varname; } }

        public IExpression Expression { get { return this.expression; } }

        public ICommand Command { get { return this.command; } }

        public object Execute(Context context)
        {
            IEnumerable elements = (IEnumerable) this.expression.Evaluate(context);

            if (elements == null)
                return null;

            foreach (var element in elements)
            {
                context.SetValue(this.varname, element);
                this.command.Execute(context);
            }
            return null;
        }
    }
}
