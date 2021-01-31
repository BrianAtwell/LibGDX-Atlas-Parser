using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasExtender.Model.KeyEnums
{
    /*
        <summary>
            This is a TextureFilter Enum based off of LibGDX
        </summary>
    */
    public enum TextureFilter:int
    {
        /*
        Nearest(GL20.GL_NEAREST), Linear(GL20.GL_LINEAR), MipMap(GL20.GL_LINEAR_MIPMAP_LINEAR), MipMapNearestNearest(
            GL20.GL_NEAREST_MIPMAP_NEAREST), MipMapLinearNearest(GL20.GL_LINEAR_MIPMAP_NEAREST), MipMapNearestLinear(
            GL20.GL_NEAREST_MIPMAP_LINEAR), MipMapLinearLinear(GL20.GL_LINEAR_MIPMAP_LINEAR)
            */
        Linear = 9728,
        Nearest = 9729,
        MipMap = 9987,
        MipMapNearestNearest = 9984,
        MipMapLinearNearest = 9985,
        MipMapNearestLinear = 9986,
        MipMapLinearLinear = 9987
    }

#if MONOGAME_LIBS
    public static class TextureFilterExtensions
    {
        public static Microsoft.Xna.Framework.Graphics.TextureFilter ToXnaFilter(this TextureFilter lgdxFilter)
        {
            Microsoft.Xna.Framework.Graphics.TextureFilter xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.Linear;

            switch (lgdxFilter)
            {
                case TextureFilter.Linear:
                    xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.Linear;
                    break;
                case TextureFilter.Nearest:
                    xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.Point;
                    break;
                case TextureFilter.MipMap:
                //case TextureFilter.MipMapLinearLinear:
                    xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.Anisotropic;
                    break;
                case TextureFilter.MipMapLinearNearest:
                    xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.MinPointMagLinearMipPoint;
                    break;
                case TextureFilter.MipMapNearestLinear:
                    xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.MinLinearMagPointMipLinear;
                    break;
                case TextureFilter.MipMapNearestNearest:
                    xnaFilter = Microsoft.Xna.Framework.Graphics.TextureFilter.MinLinearMagPointMipPoint;
                    break;
            }

            return xnaFilter;
        }
    }
#endif
}
