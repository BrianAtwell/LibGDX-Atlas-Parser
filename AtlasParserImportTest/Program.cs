using LibGDXAtlasParser;
using LibGDXAtlasParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasParserImportTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();

            prog.run(args);
        }

        public void run(string[] args)
        {
            FileParser lParser = new FileParser();
            SectionTreeCollection tree;
            string packFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\test2.json.atlas";

            tree = lParser.ReadFile(packFile);

            PrintSectionTree(tree);
        }

        public void PrintSectionTree(SectionTreeCollection ltree)
        {
            string leftPad="";
            if (ltree.Level > 0)
            {
                leftPad = "".PadLeft(ltree.Level);
            }
            Console.WriteLine("".PadLeft(50,'-'));
            Console.WriteLine("SectionName:" + ltree.SectionName + " ChildCount:"+ ltree.GetChildCount());

            foreach(KeyData lkey in ltree.Keys)
            {
                Console.Write(leftPad+"Key:" +lkey.KeyName);
                foreach(string lValue in lkey.Values)
                {
                    Console.Write(" Value:"+lValue);
                }
                Console.WriteLine("");
            }

            foreach(SectionTreeCollection childTree in ltree.GetChildren())
            {
                PrintSectionTree(childTree);
            }
        }
    }
}
