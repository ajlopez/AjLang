using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjLang.Expressions
{
    public class ConstantExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Evaluate()
        {
            return this.value;
        }
    }
}
