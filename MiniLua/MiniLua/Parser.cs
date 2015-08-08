using System;
using System.Collections.Generic;
using Assembler;
namespace MiniLua
{
    public sealed class Parser
    {
        Converter c = new Converter();
        public struct t
        {
            public t(Parser p)
            {
                this.p = p;
            }
            Parser p;
            public void close(string to)
            {
                p.c.finish(to);
            }
        }
        Keyword[] keyWords =

          {
            new Keyword() {  keyWord = "if"     },
            new Keyword() {  keyWord = "then"   },
            new Keyword() {  keyWord = "else"   },
            new Keyword() {  keyWord = "elif"   },
            new Keyword() {  keyWord = "while"  },
            new Keyword() {  keyWord = "do"     },
            new Keyword() {  keyWord = "end"    },
            new Keyword() {  keyWord = "import" },
            new Keyword() {  keyWord = "local"  }
        };
        static Parser p = new Parser();
        public static t Parse(string input)
        {
            string[] lines = input.Split(';');
            p.parse(lines);
            return new t(p);
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
                else if (ifdoes == "import")
                {
                    c.processLine("#include <"+lineChunks[1]);
                }
                else if (ifdoes == "local")
                {
                    c.processLine("#define " + lineChunks[1].Split('=')[0] + " " + lineChunks[1].Split('=')[1]);
                }
                else if (ifdoes == "function")
                {
                    c.processLine("int " + lineChunks[1] + "{");
                }
            } else
            {
                c.processLine(line + ";");
            }
        }
    }
}
