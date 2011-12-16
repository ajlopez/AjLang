namespace AjLang.Expressions
{
    using System;

    public interface IExpression
    {
        object Evaluate(AjLang.Context context);
    }
}
