namespace AjLang.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DefineCommand : ICommand
    {
        private string name;
        private IList<ICommand> commands;

        public DefineCommand(string name, IList<ICommand> commands)
        {
            this.name = name;
            this.commands = commands;
        }

        public string Name { get { return this.name; } }

        public IEnumerable<ICommand> Commands { get { return this.commands; } }

        public void Execute(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
