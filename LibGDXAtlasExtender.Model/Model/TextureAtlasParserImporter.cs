using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGDXAtlasParser.Model;
using LibGDXAtlasExtender.Model.KeyEnums;
using LibGDXAtlasParser;

namespace LibGDXAtlasExtender.Model
{
    /*
        <summary>
            TextureAtlasImporter imports a TextureAtlasFile using DataParser from a 
            LibGDX texture Atlas file.
        </summary>
    */
    public static class TextureAtlasParserImporter
    {
        #region Methods
        /*
            <summary>
                Imports <see cref="TextureAtlasFile"/> from a json.atlas file to
                a <see cref="TextureAtlasFile"/>.
            </summary>
            <param name="filename">
                A json.atlas file contains texture regions and attributes.
            </param>
        */
        public static TextureAtlasFile ImportFromFile(string filename)
        {
            FileParser lParser = new FileParser();
            SectionTreeCollection tree;
            TextureAtlasFile textFile = new TextureAtlasFile();
            TextureInfo textInfo;
            SubTextureInfo subtextInfo;
            List<string> values;

            tree = lParser.ReadFile(filename);

            foreach (SectionTreeCollection textureTree in tree.GetChildren())
            {
                textInfo = new TextureInfo(textureTree.SectionName);

                // Set Texture the Properties
                values = textureTree.Keys["size"];
                if (values != null && values.Count == 2)
                {
                    try
                    {
                        textInfo.Width = int.Parse(values[0]);
                        textInfo.Height = int.Parse(values[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        textInfo.Width = 0;
                        textInfo.Height = 0;

                    }
                }
                values = textureTree.Keys["filter"];
                if (values != null && values.Count == 2)
                {
                    TextureFilter filter = TextureFilter.Linear;
                    KeyTypeUtilities.Parse<TextureFilter>(values[0], ref filter);
                    textInfo.FilterMin = filter;
                    KeyTypeUtilities.Parse<TextureFilter>(values[1], ref filter);
                    textInfo.FilterMax = filter;
                }
                values = textureTree.Keys["format"];
                if (values != null && values.Count == 1)
                {
                    Format format = Format.Alpha;
                    KeyTypeUtilities.Parse<Format>(values[0], ref format);
                    textInfo.Format = format;
                }
                values = textureTree.Keys["repeat"];
                if (values != null && values.Count == 1)
                {
                    Repeat repeat = Repeat.xy;
                    KeyTypeUtilities.Parse<Repeat>(values[0], ref repeat);
                    textInfo.Repeat = repeat;
                }

                foreach (SectionTreeCollection subTextureTree in textureTree.GetChildren())
                {
                    subtextInfo = new SubTextureInfo(subTextureTree.SectionName);
                    // Set Subtexture the Properties
                    values = subTextureTree.Keys["xy"];
                    if (values != null && values.Count == 2)
                    {
                        try
                        {
                            subtextInfo.X = int.Parse(values[0]);
                            subtextInfo.Y = int.Parse(values[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.X = 0;
                            subtextInfo.Y = 0;

                        }
                    }
                    values = subTextureTree.Keys["size"];
                    if (values != null && values.Count == 2)
                    {
                        try
                        {
                            subtextInfo.Width = int.Parse(values[0]);
                            subtextInfo.Height = int.Parse(values[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.Width = 0;
                            subtextInfo.Height = 0;

                        }
                    }
                    values = subTextureTree.Keys["orig"];
                    if (values != null && values.Count == 2)
                    {
                        try
                        {
                            subtextInfo.OrigWidth = int.Parse(values[0]);
                            subtextInfo.OrigHeight = int.Parse(values[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.OrigWidth = 0;
                            subtextInfo.OrigHeight = 0;

                        }
                    }
                    values = subTextureTree.Keys["offset"];
                    if (values != null && values.Count == 2)
                    {
                        try
                        {
                            subtextInfo.OffsetWidth = int.Parse(values[0]);
                            subtextInfo.OffsetHeight = int.Parse(values[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.OffsetWidth = 0;
                            subtextInfo.OffsetHeight = 0;

                        }
                    }
                    values = subTextureTree.Keys["index"];
                    if (values != null && values.Count == 1)
                    {
                        try
                        {
                            subtextInfo.Index = int.Parse(values[0]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.Index = -1;

                        }
                    }
                    values = subTextureTree.Keys["rotate"];
                    if (values != null && values.Count == 1)
                    {
                        try
                        {
                            subtextInfo.Rotate = bool.Parse(values[0]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.Rotate = false;

                        }
                    }

                    values = subTextureTree.Keys["fps"];
                    if (values != null && values.Count == 1)
                    {
                        try
                        {
                            subtextInfo.FPS = float.Parse(values[0]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.FPS = null;

                        }
                    }

                    values = subTextureTree.Keys["duration"];
                    if (values != null && values.Count == 1)
                    {
                        try
                        {
                            subtextInfo.Duration = int.Parse(values[0]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            subtextInfo.Duration = null;

                        }
                    }
                    textInfo.Subtexture.Add(subtextInfo);
                }
                textFile.Textures.Add(textInfo);
            }

            return textFile;
        }

        /*
            <summary>
                Exports <see cref="TextureAtlasFile"/> from a json.atlas file to
                a <see cref="TextureAtlasFile"/>.
            </summary>
            <param name="filename">
                A json.atlas file contains texture regions and attributes.
            </param>
        */
        public static void TestExportToFile(TextureAtlasFile textAtlas, string filename)
        {
            using (FileStream fs = File.OpenWrite(filename))
            {
                using (StreamWriter sr = new StreamWriter(fs))
                {
                    foreach (TextureInfo textInfo in textAtlas.Textures)
                    {
                        sr.WriteLine("\n" + textInfo.ImagePath);
                        sr.WriteLine("size: " + textInfo.Width + ", " + textInfo.Height);
                        sr.WriteLine("format: " + textInfo.Format);
                        sr.WriteLine("filter: " + textInfo.FilterMin + ", " + textInfo.FilterMax);
                        sr.WriteLine("repeat: " + textInfo.Repeat);
                        foreach (SubTextureInfo subtextInfo in textInfo.Subtexture)
                        {
                            sr.WriteLine(subtextInfo.Name);
                            sr.WriteLine("  rotate: " + subtextInfo.Rotate);
                            sr.WriteLine("  xy: " + subtextInfo.X + ", " + subtextInfo.Y);
                            sr.WriteLine("  size: " + subtextInfo.Width + ", " + subtextInfo.Height);
                            sr.WriteLine("  orig: " + subtextInfo.OrigWidth + ", " + subtextInfo.OrigHeight);
                            sr.WriteLine("  orig: " + subtextInfo.OffsetWidth + ", " + subtextInfo.OffsetHeight);
                            sr.WriteLine("  index: " + subtextInfo.Index);
                            if(subtextInfo.FPS != null)
                            {
                                sr.WriteLine("  fps: " + subtextInfo.FPS);
                            }
                            if (subtextInfo.Duration != null)
                            {
                                sr.WriteLine("  duration: " + subtextInfo.Duration);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
