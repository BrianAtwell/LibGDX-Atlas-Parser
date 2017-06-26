using LibGDXAtlasExtender.Model.KeyEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasExtender.Model
{
    /*
        <summary>
            TextureInfo contains information about the main texture.
        </summary>
    */
    public class TextureInfo
    {
        #region Initialization
        /*
            <summary>
                TextureInfo Constructor
            </summary>
        */
        public TextureInfo():this("")
        {
        }

        /*
            <summary>
                TextureInfo Constructor
            </summary>
            <param name=""lImagePath">
                string of the image path. This is the path of the image relative to
                the .atlas 
            </param>
        */
        public TextureInfo(string lImagePath)
        {
            ImagePath = lImagePath;
            Subtexture = new List<SubTextureInfo>();
        }
        #endregion

        #region Fields
        /*
            <summary>
                string of image path. This is the path of the image relative to
                the .atlas 
            </summary>
        */
        public string ImagePath { get; set; }

        /* 
            <summary>
                List of <see cref="SubTextureInfo"/> which contains information
                fabout subtexture or region information.
            </summary>
        */
        public List<SubTextureInfo> Subtexture { get; set; }

        /* 
            <summary>
                int size of the the width of the main texture
            </summary>
        */
        public int Width { get; set; }

        /* 
            <summary>
                int size of the the height of the main texture
            </summary>
        */
        public int Height { get; set; }

        /* 
            <summary>
                Format enum is the color enum of the main texture
            </summary>
        */
        public Format Format { get; set; }

        /*
            <summary>
                <see cref="TextureFilter"> minimum
            </summary>
        */
        public TextureFilter FilterMin { get; set; }

        /*
            <summary>
                <see cref="TextureFilter"> maximum
            </summary>
        */
        public TextureFilter FilterMax { get; set; }

        /*
            <summary>
                <see cref="Repeat"> enum
            </summary>
        */
        public Repeat Repeat { get; set; }
        #endregion
    }
}
