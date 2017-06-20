using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibGDXAtlasParser.Model
{
    /*
        <summary>
            Json.atlas file data as Textures and functions such as import and export
            from libgdx.
        </summary>
    */
    public class TextureAtlasFile
    {
        #region Initialization
        /*
            <summary>
                Creates a <see cref="TextureAtlasFile"/> object that contains a List of <see cref="TextureInfo"/>
            </summary>
        */
        public TextureAtlasFile()
        {
            Textures = new List<TextureInfo>();
        }
        #endregion

        #region Fields
        /*
            <summary>
                A List of <see cref="TextureInfo"/>, that contains a path,
                texture regions, and texture attributes.
            </summary>
        */
        public List<TextureInfo> Textures { get; set; }
        #endregion
    }
}
