using System;
using System.Collections.Generic;

namespace MiniLua
{
    public sealed class Parser
    {
        List<Variable<Object>> variables = new List<Variable<Object>>();
        Keyword[] keyWords = 
            
          {
            new Keyword() {  keyWord = "if"     },
            new Keyword() {  keyWord = "then"   },
            new Keyword() {  keyWord = "else"   },
            new Keyword() {  keyWord = "elif"   },
            new Keyword() {  keyWord = "while"  },
            new Keyword() {  keyWord = "true"   },
            new Keyword() {  keyWord = "false"  },
            new Keyword() {  keyWord = "do"     },
            new Keyword() {  keyWord = "end"    },
        };
        static Parser p = new Parser();
        public static void Parse(string input,int cur, int linaes)
        {
            string[] lines = input.Split(';');
            p.parse(lines, cur,linaes);
        }
        public bool parse(string[] lines, int cur ,int end)
        {
            foreach (string line in lines)
            {
                return parseLine(line);
            }
            if (openBody && cur == end)
            {
                return false;
            }
            return true;
        }
        public bool parseLine(string line)
        {
            try
            {
                execute(line);
            }
            catch
            {
                return false;
            }
            return true;
        }
        bool openBody = false;
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
        public void execute(string line)
        {
            if (line == null) return;
            string[] lineChunks = line.Split(' ');
            if (lineChunks.Length < 1) return;
            string ifdoes = null;
            if (equalsAny(lineChunks[0], out ifdoes))
            {
                if (ifdoes == "while")
                {
                    Variable<Object> v = new Variable<object>();
                    if (bool.Parse(lineChunks[1]) || (v = match(new Variable<object>() { name = lineChunks[1]})).toBool())
                    {
                        if (lineChunks[2] != "do")
                        {
                            throw new Exception("Expecting: do");
                        } else
                        {
                            openBody = true;
                        }
                    }
                }
            }
        }
    }
}
