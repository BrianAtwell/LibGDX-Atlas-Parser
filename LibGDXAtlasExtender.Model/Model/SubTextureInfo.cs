using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasExtender.Model
{
    /*
        <summary>
            <see cref="SubTexture"/> handles texture regions from a json.atlas file.
        </summary>
    */
    public class SubTextureInfo
    {
        #region Initialization
        /*
            <summary>
                Creates an <see cref="SubTexture"/> Object that stores texture region and data.
            </summary>
        */
        public SubTextureInfo()
        {
        }

        /*
            <summary>
                Creates an <see cref="SubTexture"/> Object that stores texture region and data.
            </summary>
            <param name="name">
                Name of the subtexture region.
            </param>
        */
        public SubTextureInfo(string name)
        {
            Name = name;
        }

        /*
            <summary>
                Creates an <see cref="SubTexture"/> Object that stores texture region and data.
            </summary>
            <param name="name">
                Name of the subtexture region.
            </param>
            <param name="x">
                X <see cref="int"/> position of the subtexture region on the main texture
            </param>
            <param name="y">
                Y <see cref="int"/> position of the subtexture region on the main texture
            </param>
            <param name="width">
                Width <see cref="int"/> of the subtexture region on the main texture
            </param>
            <param name="height">
                Height <see cref="int"/> of the subtexture region on the main texture
            </param>
        */
        public SubTextureInfo(string name, int x, int y, int width, int height)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /*
            <summary>
                Creates an <see cref="SubTexture"/> Object that stores texture region and data.
            </summary>
            <param name="name">
                Name of the subtexture region.
            </param>
            <param name="x">
                X <see cref="string"/> position of the subtexture region on the main texture
            </param>
            <param name="y">
                Y <see cref="string"/> position of the subtexture region on the main texture
            </param>
            <param name="width">
                Width <see cref="string"/> of the subtexture region on the main texture
            </param>
            <param name="height">
                Height <see cref="string"/> of the subtexture region on the main texture
            </param>
        */
        public SubTextureInfo(string name, string x, string y, string width, string height)
        {
            Name = name;
            X = int.Parse(x);
            Y = int.Parse(y);
            Width = int.Parse(width);
            Height = int.Parse(height);
        }
        #endregion

        #region Fields
        /*
            <summary>
                Name of the subtexture <see cref="string"/>
            </summary>
        */
        public string Name { get; set; }

        /*
            <summary>
                X <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int X { get; set; }

        /*
            <summary>
                Y <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int Y { get; set; }

        /*
            <summary>
                Width <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int Width { get; set; }

        /*
            <summary>
                Height <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int Height { get; set; }

        /*
            <summary>
                Rotate <see cref="bool"/> of the subtexture region on the main texture
            </summary>
        */
        public bool Rotate { get; set; }

        /*
            <summary>
                OrigWidth <see cref="int"/> Orig Width of the subtexture region on the main texture
            </summary>
        */
        public int OrigWidth { get; set; }

        /*
            <summary>
                OrigHeight <see cref="string"/> Orig Height of the subtexture region on the main texture
            </summary>
        */
        public int OrigHeight { get; set; }

        /*
            <summary>
                OffsetWidth <see cref="string"/> Offset Width of the subtexture region on the main texture
            </summary>
        */
        public int OffsetWidth { get; set; }

        /*
            <summary>
                OffsetHeight <see cref="string"/> Offset Height of the subtexture region on the main texture
            </summary>
        */
        public int OffsetHeight { get; set; }

        /*
            <summary>
                Index <see cref="string"/> index of the subtexture region on the main texture
            </summary>
        */
        public int Index { get; set; }
        #endregion
    }
}
