using LibGDXAtlasParser;
using LibGDXAtlasParser.Model;
using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Extended.Content.Pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextureAtlasExtender.Content.Pipeline.LibGDXImporter
{
    [ContentImporter(".taj", DefaultProcessor = "TexturePackerProcessor", DisplayName = "LibGDX TexturePacker JSON Importer - TextureAtlasExtender")]
    public class LibGDXAtlasImporter : ContentImporter<TextureAtlasFile>
    {
        public override TextureAtlasFile Import(string filename, ContentImporterContext context)
        {
            return TextureAtlasParserImporter.ImportFromFile(filename);
        }
    }
}
