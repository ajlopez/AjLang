namespace AjLang.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Language;

    public class Puts : ICallable
    {
        public object Call(Context context, IList<object> arguments)
        {
            if (arguments != null && arguments.Count > 0)
            {
                Console.WriteLine(arguments[0]);
                return arguments[0];
            }

            Console.WriteLine();
            return null;
        }
    }
}
