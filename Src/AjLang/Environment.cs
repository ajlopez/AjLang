namespace AjLang
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Environment
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }

        public object GetValue(string name)
        {
            if (!this.values.ContainsKey(name))
                return null;
            
            return this.values[name];
        }
    }
}
