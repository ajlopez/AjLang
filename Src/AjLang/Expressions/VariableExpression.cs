﻿namespace AjLang.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Language;

    public class VariableExpression : IExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            object result = context.GetValue(this.name);

            if (result is ICallable)
                result = ((ICallable)result).Call(context, null);

            return result;
        }
    }
}

