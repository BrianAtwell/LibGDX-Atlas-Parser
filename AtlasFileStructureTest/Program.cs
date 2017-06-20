using LibGDXAtlasParser;
using LibGDXAtlasParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasFileStructureTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileParser lParser = new FileParser();
            string packFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Resources\\test2.json.atlas";
            TextureAtlasFile textFile;

            textFile = TextureAtlasFile.ImportFromFile(packFile);

            textFile.TestExportToFile("packOutTest.atlas");
        }
    }
}
