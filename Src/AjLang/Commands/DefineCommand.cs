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
        private ICommand command;

        public DefineCommand(string name, IList<string> argnames, ICommand command)
        {
            this.name = name;
            this.argnames = argnames;
            this.command = command;
        }

        public string Name { get { return this.name; } }

        public IEnumerable<string> ArgumentNames { get { return this.argnames; } }

        public ICommand Command { get { return this.command; } }

        public object Execute(Context context)
        {
            context.SetValue(this.name, new DefinedMethod(this.argnames, this.command));
            return null;
        }
    }
}
