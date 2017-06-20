using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using LibGDXAtlasParser.Model;
using MonoGame.Extended.Content;

namespace LibGDXAtlasExtender.Model
{
    public class LibGDXAtlasReader : ContentTypeReader<List<TextureAtlas>>
    {
        protected override List<TextureAtlas> Read(ContentReader reader, List<TextureAtlas> existingInstance)
        {
            var assetName = reader.GetRelativeAssetPath(reader.ReadString());
            var texture = reader.ContentManager.Load<Texture2D>(assetName);
            var atlas = new TextureAtlas();

            var textureCount = reader.ReadInt32();

            for(var i=0; i < textureCount; i++)
            {
                reader.ReadString();
            }

            var regionCount = reader.ReadInt32();

            for (var i = 0; i < regionCount; i++)
            {
                atlas.CreateRegion(
                    reader.ReadString(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32(),
                    reader.ReadInt32());
            }

            return atlas;
        }
    }
}
