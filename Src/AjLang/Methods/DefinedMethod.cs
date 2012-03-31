namespace AjLang.Methods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Language;
    using AjLang.Commands;

    public class DefinedMethod : ICallable
    {
        private ICommand command;
        private IEnumerable<string> argnames;
        private int arity;

        public DefinedMethod(IEnumerable<string> argnames, ICommand command)
        {
            this.argnames = argnames;
            this.command = command;

            if (argnames != null)
                this.arity = argnames.Count();
        }

        public object Call(Context context, IList<object> arguments)
        {
            int argarity = arguments == null ? 0 : arguments.Count;

            if (this.arity != argarity)
                throw new InvalidOperationException(string.Format("Wrong number of arguments ({0} for {1})", argarity, this.arity));

            object result = null;
            Context newctx = new Context(context);

           result = this.command.Execute(newctx);

            return result;
        }
    }
}
