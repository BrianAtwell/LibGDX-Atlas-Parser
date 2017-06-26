using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasExtender.Model
{
    /*
        <summary>
            Stores Textures regions from LibGDX importer
        </summary>
    */
    public class GDXTextureRegion2D
    {
        /*
            <summary>
                Creates a LibGDX texture region <see cref="GDXTextureRegion2D"/>
            </summary>
        */
        public GDXTextureRegion2D(Texture2D texture, int x, int y, int width, int height)
            : this(texture, null, x, y, width, height, 0, 0, width, height, false, -1)
        {
        }

        /*
            <summary>
                Creates a LibGDX texture region <see cref="GDXTextureRegion2D"/>
            </summary>
        */
        public GDXTextureRegion2D(Texture2D texture, string name, int x, int y, int width, int height)
            : this(texture, name, x, y, width, height, 0, 0, width, height, false, -1)
        {
        }

        /*
            <summary>
                Creates a LibGDX texture region <see cref="GDXTextureRegion2D"/>
            </summary>
        */
        public GDXTextureRegion2D(Texture2D texture, string name, int x, int y, int width, int height,
            int offsetWidth, int offsetHeight, int origWidth, int origHeight, bool rotate,
            int index)
        {
            Texture = texture;
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            OffsetWidth = offsetWidth;
            OffsetHeight = offsetHeight;
            OrigWidth = origWidth;
            OrigHeight = origHeight;
            Rotate = rotate;
            Index = index;
        }

        #region Fields
        /*
            <summary>
                Name of the subtexture <see cref="string"/>
            </summary>
        */
        public string Name { get;}

        /*
            <summary>
                X <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int X { get;}

        /*
            <summary>
                Y <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int Y { get; }

        /*
            <summary>
                Width <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int Width { get; }

        /*
            <summary>
                Height <see cref="int"/> position of the subtexture region on the main texture
            </summary>
        */
        public int Height { get; }

        /*
            <summary>
                Rotate <see cref="bool"/> of the subtexture region on the main texture
            </summary>
        */
        public bool Rotate { get; }

        /*
            <summary>
                OrigWidth <see cref="int"/> Orig Width of the subtexture region on the main texture
            </summary>
        */
        public int OrigWidth { get; }

        /*
            <summary>
                OrigHeight <see cref="string"/> Orig Height of the subtexture region on the main texture
            </summary>
        */
        public int OrigHeight { get; }

        /*
            <summary>
                OffsetWidth <see cref="string"/> Offset Width of the subtexture region on the main texture
            </summary>
        */
        public int OffsetWidth { get; }

        /*
            <summary>
                OffsetHeight <see cref="string"/> Offset Height of the subtexture region on the main texture
            </summary>
        */
        public int OffsetHeight { get; }

        /*
            <summary>
                Index <see cref="string"/> index of the subtexture region on the main texture
            </summary>
        */
        public int Index { get; }

        public Texture2D Texture { get; protected set; }

        public Rectangle Bounds => new Rectangle(X, Y, Width, Height);

        public override string ToString()
        {
            return $"{Name ?? string.Empty}";
        }
        #endregion
    }
}
