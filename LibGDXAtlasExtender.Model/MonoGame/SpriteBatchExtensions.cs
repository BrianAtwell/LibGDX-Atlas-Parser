
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LibGDXAtlasExtender.Model
{
    public static class SpriteBatchExtensions
    {
        static public void Draw(this SpriteBatch spriteBatch, GDXTextureRegion2D texture, Vector2 position, Color color)
        {
            //spriteBatch.Draw(textureAtlas[0].GetRegion(0).Texture, new Vector2(100, 100), new Rectangle(0,0, textureAtlas[0].GetRegion(0).Width, textureAtlas[0].GetRegion(0).Height), Color.AliceBlue);
            spriteBatch.Draw(texture.Texture, position, color);
        }
    }
}