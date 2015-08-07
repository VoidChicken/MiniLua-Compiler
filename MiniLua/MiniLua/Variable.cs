using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniLua
{
    class Variable<T>
    {
        public string name = "";
        public T def = default(T);
        public bool toBool()
        {
            return (typeof(T) == typeof(bool)?bool.Parse(def.ToString()):false);
        }
    }
}
