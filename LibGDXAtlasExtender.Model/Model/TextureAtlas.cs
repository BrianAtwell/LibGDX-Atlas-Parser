using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasExtender.Model
{
    /*
        <summary>
            Provides data about LibGDX textures and <see cref="Texture2D"/> methods for Monogame library
        </summary>
    */
    public class TextureAtlas : IEnumerable<GDXTextureRegion2D>
    {
        private readonly Dictionary<string, int> _regionMap;

        private readonly List<GDXTextureRegion2D> _regions;

        public Texture2D Texture { get; }

        /*  <summary>
                Initializes a new texture atlas with an empty list of regions.
            </summary>
            <param name="texture"> <see cref="Texture2D " /> image used to draw on screen.</param>
        */
        public TextureAtlas(Texture2D texture)
        {
            Texture = texture;
            _regions = new List<GDXTextureRegion2D>();
            _regionMap = new Dictionary<string, int>();
        }

        /*
            <summary>
                Creates a new texture region and adds it to the list of the <see cref="TextureAtlas" />' regions.
            </summary>
            <param name="name">
                <see cref="string"/> Name of the region
            </param>
            <param name="x">
                X coordinate of the upper left corner of the texture region.
            </param>
            <param name="y">
                Y coordinate of the upper left corner of the texture region.
            </param>
            <param name="width">
                Width of the texture region
            </param>
            <param name="height">
                Height of the texture region
            </param>
            <param name="offsetWidth">
                Offset of the width of the texture region
            </param>
            <param name="offsetHeight">
                Offset of the height of the texture region
            </param>
            <param name="origWidth">
                Original width of the texture region
            </param>
            <param name="origHeight">
                Original height of the texture region
            </param>
            <param name="rotate">
                Indicates if the texture region is rotated
            </param>
            <param name="index">
                Index of the texture region
            </param>
        */
        public GDXTextureRegion2D CreateRegion(string name, int x, int y, int width, int height, int offsetWidth,
            int offsetHeight, int origWidth, int origHeight, bool rotate, int index)
        {
            if (_regionMap.ContainsKey(name))
                throw new InvalidOperationException($"Region {name} already exists in the texture atlas");

            var region = new GDXTextureRegion2D(Texture, name, x, y, width, height, offsetWidth, offsetHeight,
                origWidth, origHeight, rotate, index);
            var dictIndex = _regions.Count;
            _regions.Add(region);
            _regionMap.Add(name, dictIndex);
            return region;
        }

        /*
            <summary>
                Creates a new texture region and adds it to the list of the <see cref="TextureAtlas" />' regions.
            </summary>
            <param name="name">
                <see cref="string"/> Name of the region
            </param>
            <param name="x">
                X coordinate of the upper left corner of the texture region.
            </param>
            <param name="y">
                Y coordinate of the upper left corner of the texture region.
            </param>
            <param name="width">
                Width of the texture region
            </param>
            <param name="height">
                Height of the texture region
            </param>
        */
        public GDXTextureRegion2D CreateRegion(string name, int x, int y, int width, int height)
        {
            if (_regionMap.ContainsKey(name))
                throw new InvalidOperationException($"Region {name} already exists in the texture atlas");

            var region = new GDXTextureRegion2D(Texture, name, x, y, width, height);
            var dictIndex = _regions.Count;
            _regions.Add(region);
            _regionMap.Add(name, dictIndex);
            return region;
        }

        public GDXTextureRegion2D this[int index] => GetRegion(index);
        public GDXTextureRegion2D this[string name] => GetRegion(name);

        /*  <summary>
                Gets a list of regions in the <see cref="TextureAtlas" />.
            </summary>
        */
        public IEnumerable<GDXTextureRegion2D> Regions => _regions;

        /*
            <summary>
                Gets the number of regions in the <see cref="GDXTextureAtlas" />.
            </summary>
        */
        public int RegionCount => _regions.Count;

        /*
            <summary>
                Gets the enumerator of the <see cref="GDXTextureAtlas" />' list of regions.
            </summary>
            <returns>The <see cref="IEnumerator" /> of regions.</returns>
        */
        public IEnumerator<GDXTextureRegion2D> GetEnumerator()
        {
            return _regions.GetEnumerator();
        }

        /*
            <summary>
                Gets the enumerator of the <see cref="GDXTextureAtlas" />' list of regions.
            </summary>
            <returns>The <see cref="IEnumerator" /> of regions</returns>
        */
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /*
            <summary>
                Gets the <see cref="GDXTextureRegion"/> at position index.
            </summary>
            <param name="index">
                The <see cref="int"/> position of the GDXTextureRegion you would like to access.
            </param>
        */
        public GDXTextureRegion2D GetRegion(int index)
        {
            if ((index < 0) || (index >= _regions.Count))
                throw new IndexOutOfRangeException();

            return _regions[index];
        }

        /*
            <summary>
                Gets the <see cref="GDXTextureRegion"/> at with name.
            </summary>
            <param name="index">
                The name of the GDXTextureRegion you would like to access.
            </param>
        */
        public GDXTextureRegion2D GetRegion(string name)
        {
            int index;

            if (_regionMap.TryGetValue(name, out index))
                return GetRegion(index);

            throw new KeyNotFoundException(name);
        }

        /*
            <summary>
                Removes the texture region at position index
            </summary>
            <param name="index">
                Index of the texture region to remove
            </parm>
        */
        public void RemoveRegion(int index)
        {
            if (_regionMap.ContainsValue(index))
            {
                _regionMap.Remove(GetRegion(index).Name);
                _regions.RemoveAt(index);
            }
        }

        /*
            <summary>
                Removes the texture region with the specificed name
            </summary>
            <param name="name">
                Name of the texture region to remove
            </param>
        */
        public void RemoveRegion(string name)
        {
            int index = 0;

            if (_regionMap.ContainsKey(name))
            {
                _regionMap.Remove(name);
                _regions.RemoveAt(index);
            }
            else
                throw new KeyNotFoundException(name);
        }
    }
}
