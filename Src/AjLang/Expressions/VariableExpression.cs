namespace AjLang.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VariableExpression : IExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public object Evaluate(Context context)
        {
            return context.GetValue(this.name);
        }
    }
}

