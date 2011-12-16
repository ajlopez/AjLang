namespace AjLang.Expressions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VariableExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public object Evaluate(Environment environment)
        {
            return environment.GetValue(this.name);
        }
    }
}

