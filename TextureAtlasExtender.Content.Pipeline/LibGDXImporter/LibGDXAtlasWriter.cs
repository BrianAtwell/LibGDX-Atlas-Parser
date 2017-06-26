using LibGDXAtlasExtender.Model;
using LibGDXAtlasParser.Model;
using LibGDXAtlasExtender.Model.KeyEnums;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextureAtlasExtender.Content.Pipeline.LibGDXImporter
{
    [ContentTypeWriter]
    public class LibGDXAtlasWriter : ContentTypeWriter<TextureAtlasFile>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(TextureAtlasFile).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(LibGDXAtlasReader).AssemblyQualifiedName;
        }


        protected override void Write(ContentWriter writer, TextureAtlasFile input)
        {
            string assetName;

            writer.Write(input.Textures.Count);
            foreach(TextureInfo textInfo in input.Textures)
            {
                //writer.Write(textInfo.ImagePath);
                assetName = Path.GetFileNameWithoutExtension(textInfo.ImagePath);
                Debug.Assert(assetName != null, "assetName != null");

                writer.Write(assetName);
                writer.Write(textInfo.Width);
                writer.Write(textInfo.Height);
                writer.Write(textInfo.Format.ToXnaFormat().ToString());
                writer.Write(textInfo.FilterMin.ToXnaFilter().ToString());
                writer.Write(textInfo.FilterMax.ToXnaFilter().ToString());
                writer.Write(textInfo.Repeat.ToString());

                writer.Write(textInfo.Subtexture.Count());

                foreach (SubTextureInfo subtexture in textInfo.Subtexture)
                {
                    writer.Write(subtexture.Name);
                    writer.Write(subtexture.Rotate);
                    writer.Write(subtexture.X);
                    writer.Write(subtexture.Y);
                    writer.Write(subtexture.Width);
                    Console.WriteLine("width: {0} height: {1}", subtexture.Width, subtexture.Height);
                    writer.Write(subtexture.Height);
                    writer.Write(subtexture.OrigWidth);
                    writer.Write(subtexture.OrigHeight);
                    writer.Write(subtexture.OffsetWidth);
                    writer.Write(subtexture.OffsetHeight);
                    writer.Write(subtexture.Index);
                }
            }
        }
    }
}
