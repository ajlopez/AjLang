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

        public DefinedMethod(IEnumerable<ICommand> commands)
        {
            this.commands = commands;
        }

        public object Call(Context context, IList<object> arguments)
        {
            object result = null;
            Context newctx = new Context(context);

            foreach (var command in this.commands)
                result = command.Execute(newctx);

            return result;
        }
    }
}
