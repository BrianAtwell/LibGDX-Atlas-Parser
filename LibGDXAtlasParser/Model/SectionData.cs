using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Model
{
    /*
        <summary>
            Information associated with a section from a json.atlas file
            from libgdx.
            Includes the Keys/value data, level, and the name of the section.
        </summary>
    */
    public class SectionData : ICloneable
    {
        private int _level;
        private KeyDataCollection _keys;
        private string _sectionName;
        private readonly static SectionData _root = new SectionData("Root",-1);

        #region Initialization
        /*
            <summary>
                Creates a new <see cref="SectionData"/> which stores information about
                a section.
            </summary>
            <param name="sectionName">
                Name of the section
            </param>
            <param name="level">
                level of the section, how many sections are above this section.
            </param>
        */
        public SectionData(string sectionName, int level)
        {
            _sectionName = sectionName;
            if (level >= -1)
                _level = level;
            else
                _level = -1;
            _keys = new KeyDataCollection();
        }

        /*
            <summary>
                Creates a new <see cref = "SectionData" /> which stores information about
                   a section.
            </summary>
            <param name = "sectionName" >
                Name of the section
            </param>
            <param name = "level" >
                level of the section, how many sections are above this section.
            </param>
        */
        public SectionData(SectionData orig)
        {
            _sectionName = orig._sectionName;
            _keys = (KeyDataCollection)orig._keys.Clone();
            _level = orig._level;
        }

        /*
            <summary>
                Creates a new <see cref="SectionData"/> which stores information about
                a section.
            </summary>
            <param name="sectionName">
                Name of the section
            </param>
        */
        public SectionData(string sectionName) : this(sectionName, -1)
        {
        }
        #endregion

        #region Fields
        /*
            <summary>
                Gets the KeyDataCollection that contains the keys associated
                with this section
            </summary>
        */
        public KeyDataCollection Keys
        {
            get
            {
                return _keys;
            }
        }

        /*
            <summary>
                Gets and sets the level of this section. This 
                is the number of sections above this section.
            </summary>
        */
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                if(value >= -1)
                {
                    _level = value;
                }
                else
                {
                    _level = -1;
                }
            }
        }

        /*
            <summary>
                Gets or Sets the name of the section.
            </summary>
        */
        public string SectionName
        {
            get
            {
                return _sectionName;
            }
            set
            {
                if(value != string.Empty)
                {
                    _sectionName = value;
                }
            }
        }

        /*
            <summary>
                root of the sectionData
            </summary>
        */
        public static SectionData Root
        {
            get
            {
                return _root;
            }
        }

        /*
            <summary>
                Is the level set.
            </summary>
        */
        public bool IsLevelSet()
        {
            return Level > -1;
        }

        /*
            <summary>
                Is this SectionData or node root
            </summary>
        */
        public bool IsRoot()
        {
            return this == _root;
        }
        #endregion

        #region ICloneable Members
        /*
        <summary>
            Creates a copy of the current instance
        </summary>
        <return>
            returns object that is a copy of this instance
        </return>
        */
        public object Clone()
        {
            return new SectionData(this);
        }
        #endregion
    }
}
