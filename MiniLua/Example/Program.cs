using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniLua;
namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
           while (true) Parser.Parse(Console.ReadLine(), 10, 22);
        }
    }
}
