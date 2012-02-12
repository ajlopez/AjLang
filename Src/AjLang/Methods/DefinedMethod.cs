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
        private IEnumerable<ICommand> commands;
        private IEnumerable<string> argnames;
        private int arity;

        public DefinedMethod(IEnumerable<string> argnames, IEnumerable<ICommand> commands)
        {
            this.argnames = argnames;
            this.commands = commands;

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

            foreach (var command in this.commands)
                result = command.Execute(newctx);

            return result;
        }
    }
}
