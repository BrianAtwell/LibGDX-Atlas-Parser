using LibGDXAtlasParser.Exceptions;
using LibGDXAtlasParser.Model;
using LibGDXAtlasParser.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser
{
    public class FileParser
    {
        private DataParser _parser;

        public FileParser()
        {
            _parser = new DataParser();
        }

        public SectionTreeCollection ReadFile(string filename)
        {
            if(!File.Exists(filename))
            {
                throw new ArgumentException("Bad filename.");
            }

            try
            {
                // (FileAccess.Read) we want to open the ini only for reading 
                // (FileShare.ReadWrite) any other process should still have access to the ini file 
                using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return ReadData(sr);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new ParseException(String.Format("Could not parse file {0}", filename), ex);
            }
        }

        public SectionTreeCollection ReadData(StreamReader reader)
        {
            if(reader == null)
                throw new ArgumentNullException("reader");
            _parser.Parse(reader.ReadToEnd()+"\n");
            /*
            while (!reader.EndOfStream)
            {
                try
                {
                    _parser.Parse(reader.ReadLine());
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
            */
            return _parser.DataTree;
        }
    }
}
