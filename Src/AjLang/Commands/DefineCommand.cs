namespace AjLang.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjLang.Methods;

    public class DefineCommand : ICommand
    {
        private string name;
        private IList<string> argnames;
        private IList<ICommand> commands;

        public DefineCommand(string name, IList<string> argnames, IList<ICommand> commands)
        {
            this.name = name;
            this.argnames = argnames;
            this.commands = commands;
        }

        public string Name { get { return this.name; } }

        public IEnumerable<string> ArgumentNames { get { return this.argnames; } }

        public IEnumerable<ICommand> Commands { get { return this.commands; } }

        public object Execute(Context context)
        {
            context.SetValue(this.name, new DefinedMethod(this.argnames, this.commands));
            return null;
        }
    }
}
