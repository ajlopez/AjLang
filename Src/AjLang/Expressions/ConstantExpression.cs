namespace AjLang.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConstantExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Evaluate(Context context)
        {
            return this.value;
        }
    }
}
