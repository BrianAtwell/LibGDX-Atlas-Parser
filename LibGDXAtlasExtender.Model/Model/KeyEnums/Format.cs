using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace LibGDXAtlasExtender.Model.KeyEnums
{
    /*
        <summary>
            The enum Format is a color from LibGDX
        </summary>
    */
    public enum Format
    {
        Alpha, Intensity, LuminanceAlpha, RGB565, RGBA4444, RGB888, RGBA8888
    }

    public static class FormatExtensions
    {
        public static SurfaceFormat ToXnaFormat(this Format lgdxFormat)
        {
            SurfaceFormat xnaFormat = SurfaceFormat.Color;

            switch(lgdxFormat)
            {
                case Format.Alpha:
                    xnaFormat = SurfaceFormat.Alpha8;
                    break;
                case Format.Intensity:
                    xnaFormat = SurfaceFormat.Single;
                    break;
                case Format.LuminanceAlpha:
                    xnaFormat = SurfaceFormat.HalfVector2;
                    break;
                case Format.RGB565:
                    xnaFormat = SurfaceFormat.Bgr565;
                    break;
                case Format.RGBA4444:
                    xnaFormat = SurfaceFormat.Bgra4444;
                    break;
                case Format.RGB888:
                    xnaFormat = SurfaceFormat.Bgr32SRgb;
                    break;
                case Format.RGBA8888:
                    xnaFormat = SurfaceFormat.Bgra32SRgb;
                    break;
            }

            return xnaFormat;
        }
    }
}
