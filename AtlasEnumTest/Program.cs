using LibGDXAtlasParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasEnumTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Format test;
            
            test = (Format)Enum.Parse(typeof(Format), "test");
        }
    }
}
