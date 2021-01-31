using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGDXAtlasExtender.Model;
#if MONOGAME_LIBS
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Content;

namespace LibGDXAtlasExtender.Model
{
    public class LibGDXAtlasReader : ContentTypeReader<List<TextureAtlas>>
    {
        protected override List<TextureAtlas> Read(ContentReader reader, List<TextureAtlas> existingInstance)
        {
            String assetName;
            Texture2D texture;
            List<TextureAtlas> atlasList = new List<TextureAtlas>();
            int textureCount = 0;
            int subTextCount = 0;
            TextureAtlas curAtlas;
            int textWidth;
            int textHeight;
            SurfaceFormat format = SurfaceFormat.Bgr32;
            Microsoft.Xna.Framework.Graphics.TextureFilter minFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.Anisotropic;
            Microsoft.Xna.Framework.Graphics.TextureFilter maxFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.Anisotropic;
            LibGDXAtlasExtender.Model.KeyEnums.Repeat repeat = LibGDXAtlasExtender.Model.KeyEnums.Repeat.none;
            String buffStr;

            textureCount = reader.ReadInt32();

            for (var i = 0; i < textureCount; i++)
            {
                assetName = reader.GetRelativeAssetName(reader.ReadString());
                textWidth = reader.ReadInt32();
                textHeight = reader.ReadInt32();
                buffStr = reader.ReadString();
                KeyTypeUtilities.Parse<SurfaceFormat>(buffStr, ref format);
                buffStr = reader.ReadString();
                KeyTypeUtilities.Parse<Microsoft.Xna.Framework.Graphics.TextureFilter>(buffStr, ref minFilter);
                buffStr = reader.ReadString();
                KeyTypeUtilities.Parse<Microsoft.Xna.Framework.Graphics.TextureFilter>(buffStr, ref maxFilter);
                buffStr = reader.ReadString();
                KeyTypeUtilities.Parse<LibGDXAtlasExtender.Model.KeyEnums.Repeat>(buffStr, ref repeat);

                Console.WriteLine("Assset Name: " + assetName);
                texture = reader.ContentManager.Load<Texture2D>(assetName);
                curAtlas = new TextureAtlas(texture, textWidth, textHeight, format, minFilter, maxFilter, repeat);

                subTextCount = reader.ReadInt32();

                for (int s = 0; s < subTextCount; s++)
                {
                    curAtlas.CreateRegion(reader.ReadString(), reader.ReadBoolean(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(),
                        reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                }
                Console.WriteLine("Width: {0} Height: {1}", curAtlas.GetRegion(0).Width, curAtlas.GetRegion(0).Height);

                atlasList.Add(curAtlas);
            }

            return atlasList;
        }
    }
}
#endif