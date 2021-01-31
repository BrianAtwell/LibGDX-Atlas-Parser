using LibGDXAtlasParser.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Model
{
    /*
        <see cref="SectionTreeCollection"/> is a wrapper for <see cref="SectionData"/>
    */
    public class SectionTreeCollection : ICloneable
    {
        private readonly Dictionary<string, List<SectionTreeCollection>> _nodes;
        private SectionData _data;
        private SectionTreeCollection _parent;

        private readonly static SectionTreeCollection _root = new SectionTreeCollection(SectionData.Root);

        #region Initialization
        /*
            <summary>
                Creates a new instance of <see cref="SectionTreeCollection"/> class.
                Which is a tree like structure for handling <see cref="SectionData"/>
            </summary>
            <param name="data">
                <see cref="SectionData" that you want this tree node to store.
            </param>
        */
        public SectionTreeCollection(SectionData data)
        {
            _data = data;
            _nodes = new Dictionary<string, List<SectionTreeCollection>>();
            _parent = _root;
        }

        /*
            <summary>
                Creates a new instance of <see cref="SectionTreeCollection"/> class.
                Which is a tree like structure for handling <see cref="SectionData"/>
            </summary>
            <param name="data">
                <see cref="SectionData" that you want this tree node to store.
            </param>
        */
        public SectionTreeCollection(SectionTreeCollection orig, SectionData data)
        {
            _nodes = new Dictionary<string, List<SectionTreeCollection>>();
            _parent = _root;

            _data = orig._data;

            foreach (string sectName in orig._nodes.Keys)
            {
                _nodes.Add(sectName, orig._nodes[sectName].Clone());
            }
        }
        /*
            <summary>
                Access the current SectionTreeCollection through square brackets. Pass a string
                containing the section name of the subsection you want access.
            </summary>
        */
        public List<SectionTreeCollection> this[string sectionName]
        {
            get
            {
                if (_nodes.ContainsKey(sectionName))
                {
                    return _nodes[sectionName];
                }

                return null;
            }

            set
            {
                if(_nodes.ContainsKey(sectionName))
                {
                    _nodes[sectionName] = value;
                }
            }
        }
        #endregion

        #region Fields
        /*
            <summary>
                Get the <see cref="KeyDataCollection"/> of the SectionData of the current tree.
            </summary>
        */
        public KeyDataCollection Keys
        {
            get
            {
                return _data.Keys;
            }
        }

        /*
            <summary>
                Get the <see cref="SectionData"/> of the current <see cref="SectionTreeCollection"/>
            </summary>
        */
        public SectionData Data
        {
            get { return _data; }
            set
            {
                _data = value;
            }
        }

        /*
            <summary>
                level of the SectionTreeCollection relative to the root.
            </summary>
        */
        public int Level
        {
            get
            {
                return _data.Level;
            }
            private set
            {
                _data.Level = value;
            }
        }

        /*
            <summary>
                level of the SectionTreeCollection relative to the root.
            </summary>
        */
        public string SectionName
        {
            get
            {
                return _data.SectionName;
            }
            set
            {
                _data.SectionName = value;
            }
        }

        /*
            <summary>
                The parent to the current tree object
            </summary>
        */
        public SectionTreeCollection Parent
        {
            get { return _parent; }
        }

        /*
            <summary>
                Root of the all trees
            </summary>
        */
        public static SectionTreeCollection Root
        {
            get { return _root; }
        }
        #endregion

        #region Methods
        /*
            <summary>
                Add child to the current <see cref="SectionTreeCollection"/>
            </summary>
        */
        public void Add(SectionTreeCollection child)
        {
            child.Data.Level = this.Level+1;
            if (_nodes.ContainsKey(child._data.SectionName))
            {
                _nodes[child._data.SectionName].Add(child);
            }
            else
            {
                _nodes.Add(child._data.SectionName, new List<SectionTreeCollection>() { child });
            }
            child._parent = this;
        }

        /*
            <summary>
                Get the children of the current tree
            </summary>
        */
        public List<SectionTreeCollection> GetChildren()
        {
            List<SectionTreeCollection> result = new List<SectionTreeCollection>();
            foreach(string key in _nodes.Keys)
            {
                result.AddRange(_nodes[key]);
            }
            return result;
        }

        /*
            <summary>
                Get the number of children of the current object
            </summary>
        */
        public int GetChildCount()
        {
            return _nodes.Count();
        }

        /*
            <summary>
                Get the node count
            </summary>
        */
        public int GetNodeCount()
        {
            int result = 0;
            foreach (string key in _nodes.Keys)
            {
                result += _nodes[key].Count;
            }
            result++;
            return result;
        }
        
        /*
            <summary>
                Get a list of strings of direct subsections
            </summary>
        */
        public List<string> GetDirectSubSections()
        {
            List<string> result = new List<string>();
            foreach(string key in _nodes.Keys)
            {
                result.Add(key);
            }
            return result;
        }

        /*
            <summary>
            Get a list of strings of all subsections
            </summary>
        */
        public List<string> GetSections()
        {
            List<string> result = new List<string>();

            foreach (string key in _nodes.Keys)
            {
                foreach(SectionTreeCollection trees in _nodes[key])
                {
                    result.AddRange(trees.GetSections());
                }
                //result.Union(_nodes[key].GetSections());
            }

            if(!result.Contains(this.SectionName))
                result.Add(this.SectionName);
            return result;
        }

        /*
            <summary>
                Compute the level of current instance.
            </summary>
        */
        public int GetComputedLevel()
        {
            SectionTreeCollection ptr;
            int lvlCount = 0;
            ptr = this;

            while(!ptr.IsNull() && !ptr.IsRoot())
            {
                ptr = ptr.Parent;
                lvlCount++;
            }

            return lvlCount;
        }

        /*
            <summary>
                Remove the child <see cref="SectionTreeCollect"/> with the section name
                passed as sectionName.
            </summary>
        */
        public bool Remove(string sectionName, SectionTreeCollection child)
        {
            if (_nodes.ContainsKey(sectionName))
            {
                if(_nodes[sectionName].Contains(child))
                {
                    _nodes[sectionName].Remove(child);
                    if(_nodes[sectionName].Count == 0)
                    {
                        _nodes.Remove(sectionName);
                    }
                    return true;
                }
            }
            return false;
        }

        /*
            <summary>
                Is the current instance null.
            </summary>
        */
        public bool IsNull()
        {
            return this == null;
        }

        /*
            <summary>
                Is the parent root.
            </summary>
        */
        public bool IsRoot()
        {
            return this.Parent == _root;
        }

        /*
            <summary>
                Is the current tree's data a root
            </summary>
        */
        public bool IsDataRoot()
        {
            return _data.IsRoot();
        }
        #endregion

        #region ICloneable Members
        /*
            <summary>
                Copy the current instance and return it.
            </summary>
        */

        public object Clone()
        {
            return new SectionTreeCollection(this, (SectionData)this.Data.Clone());
        }
        #endregion
    }
}
