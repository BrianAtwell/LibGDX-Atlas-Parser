using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextureAtlasExtender.Content.Pipeline.LibGDXImporter;
using Newtonsoft.Json.Linq;
using IniParser;
using IniParser.Model;

namespace JsonTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string packFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\pack.json.atlas";

            //Create an instance of a ini file parser
            FileIniDataParser fileIniData = new FileIniDataParser();

            if (File.Exists("NewTestIniFile.ini"))
                File.Delete("NewTestIniFile.ini");

            // This is a special ini file where we use the '#' character for comment lines
            // instead of ';' so we need to change the configuration of the parser:
            fileIniData.Parser.Configuration.CommentString = "#";
            fileIniData.Parser.Configuration.AllowKeysWithoutSection = true;
            fileIniData.Parser.Configuration.SectionStartChar = '\0';
            fileIniData.Parser.Configuration.SectionEndChar = '\0';



            //Parse the ini file
            IniData parsedData = fileIniData.ReadFile(packFile);

            //Write down the contents of the ini file to the console
            Console.WriteLine("---- Printing contents of the INI file ----\n");
            Console.WriteLine(parsedData);
            Console.WriteLine();

            //Get concrete data from the ini file
            Console.WriteLine("---- Printing setMaxErrors value from GeneralConfiguration section ----");
            Console.WriteLine("setMaxErrors = " + parsedData["GeneralConfiguration"]["setMaxErrors"]);
            Console.WriteLine();

            //Modify the INI contents and save
            Console.WriteLine();

            // Modify the loaded ini file
            IniData modifiedParsedData = ModifyINIData(parsedData);

            //Write down the contents of the modified ini file to the console
            Console.WriteLine("---- Printing contents of the new INI file ----");
            Console.WriteLine(modifiedParsedData);
            Console.WriteLine();

            //Save to a file
            Console.WriteLine("---- Saving the new ini file to the file NewTestIniFile.ini ----");
            Console.WriteLine();

            // Uncomment this to change the new line string used to write the ini file to disk to 
            // force use the windows style new line
            //modifiedParsedData.Configuration.NewLineStr = "\r\n";
            fileIniData.WriteFile("NewTestIniFile.ini", modifiedParsedData);
        }

        static IniData ModifyINIData(IniData modifiedParsedData)
        {
            modifiedParsedData["GeneralConfiguration"]["setMaxErrors"] = "10";
            modifiedParsedData.Sections.AddSection("newSection");
            modifiedParsedData.Sections.GetSectionData("newSection").LeadingComments
                .Add("This is a new comment for the section");
            modifiedParsedData.Sections.GetSectionData("newSection").Keys.AddKey("myNewKey", "value");
            modifiedParsedData.Sections.GetSectionData("newSection").Keys.GetKeyData("myNewKey").Comments
            .Add("new key comment");

            return modifiedParsedData;
        }
    }
}
