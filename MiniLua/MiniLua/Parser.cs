using System;
using System.Collections.Generic;
using Assembler;
namespace MiniLua
{
    public sealed class Parser
    {
        Converter c = new Converter();
        List<Variable<Object>> variables = new List<Variable<Object>>();
        Keyword[] keyWords = 
            
          {
            new Keyword() {  keyWord = "if"     },
            new Keyword() {  keyWord = "then"   },
            new Keyword() {  keyWord = "else"   },
            new Keyword() {  keyWord = "elif"   },
            new Keyword() {  keyWord = "while"  },
            new Keyword() {  keyWord = "do"     },
            new Keyword() {  keyWord = "end"    },
        };
        static Parser p = new Parser();
        public static void Parse(string input)
        {
            string[] lines = input.Split(';');
            p.parse(lines);
        }
        public bool parse(string[] lines)
        {
            foreach (string line in lines)
            {
                return parseLine(line);
            }
            return true;
        }
        public bool parseLine(string line)
        {
            try
            {
                assemble(line);
            }
            catch
            {
                return false;
            }
            return true;
        }
        bool equalsAny(object thing, out string thangy)
        {
            foreach (Keyword k in keyWords)
            {
                if (((string)thing) == k.keyWord)
                {
                    thangy = k.keyWord;
                    return true;
                }
            }
            thangy = null;
            return false;
        }
        Variable<Object> match(Variable<Object> v)
        {
            foreach (Variable<Object> av in variables)
            {
                if (av.name == v.name)
                {
                    return av ;
                }
            }
            return null;
        }
        public void assemble(string line)
        {
            if (line == null) return;
            string[] lineChunks = line.Split(' ');
            if (lineChunks.Length < 1) return;
            string ifdoes = null;
            if (equalsAny(lineChunks[0], out ifdoes))
            {
                if (ifdoes == "while")
                {
                    c.processLine("while (");
                }
                else if (ifdoes == "do" || ifdoes == "then") {
                    c.processLine(ifdoes == "do"?"){":"{");
                }
                else if (ifdoes == "end") {
                    c.processLine("}");
                } else if (ifdoes == "else")
                {
                    c.processLine("}else{");
                }
                else if (ifdoes == "elif")
                {
                    c.processLine("}else if (");
                }
            } else
            {
                c.processLine(line + ";");
            }
        }
    }
}
