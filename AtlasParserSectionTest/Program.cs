using LibGDXAtlasParser;
using LibGDXAtlasParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasParserSectionTest
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
            string packFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\test2.json.atlas";

            tree = lParser.ReadFile(packFile);

            PrintSectionTree(tree);
        }

        public void PrintSectionTree(SectionTreeCollection ltree)
        {
            string leftPad = "";
            if (ltree.Level > 0)
            {
                leftPad = "".PadLeft(ltree.Level);
            }
            Console.WriteLine("SectionName:" + ltree.SectionName + " ChildCount:" + ltree.GetChildCount());

            foreach (SectionTreeCollection childTree in ltree.GetChildren())
            {
                PrintSectionTree(childTree);
            }
        }
    }
}
