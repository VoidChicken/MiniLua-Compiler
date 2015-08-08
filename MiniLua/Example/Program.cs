using System;
using System.IO;
using MiniLua;
namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Parse(File.ReadAllText("test.ml")).close("test");
        }
    }
}
