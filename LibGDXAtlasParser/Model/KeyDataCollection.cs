using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Model
{
    /*
        <summary>
            Represents a collection of KeyData
        </summary>
    */
    public class KeyDataCollection : ICloneable, IEnumerable<KeyData>
    {
        #region Protected Members
        protected IEqualityComparer<string> _searchComparer;
        protected readonly Dictionary<string, KeyData> _keyData;
        #endregion

        #region Initialization
        /*
            <summary>
                Initializes a new instance of the <see cref="KeyDataCollection"/> class.
            </summary>
        */
        public KeyDataCollection(): this(EqualityComparer<string>.Default)
        {
        }

        /*
            <summary>
                Initializes a new instance of the <see cref="KeyDataCollection"/> class.
                With <see cref="IEqualityComparer<string>"/> class Parameter
            </summary>
            <param name="searchComparer">
                A search comparer for search by key name in the collection.
            </param>
        */
        public KeyDataCollection(IEqualityComparer<string> searchComparer)
        {
            _searchComparer = searchComparer;
            _keyData = new Dictionary<string, KeyData>();
        }

        /*
            <summary>
                Initializes a new instance of the <see cref="KeyDataCollection"/> class.
                With <see cref="IEqualityComparer<string>"/> class Parameter
            </summary>
            <param name="searchComparer">
                A search comparer for search by key name in the collection.
            </param>
        */
        public KeyDataCollection(KeyDataCollection orig, IEqualityComparer<string> searchComparer)
        {
            _searchComparer = searchComparer;
            _keyData = new Dictionary<string, KeyData>();

            foreach(KeyData key in orig)
            {
                if(_keyData.ContainsKey(key.KeyName))
                {
                    _keyData[key.KeyName] = (KeyData)key.Clone();
                }
                else
                {
                    _keyData.Add(key.KeyName, (KeyData)key.Clone());
                }
            }
        }
        #endregion

        #region Fields
        /*
            <summary>
                Sets or gets a key
            </summary>
            <remarks>
                When you set a key that does not exist in the 
                collection it adds a new key with the set value.
                When you get a key if it does not exists then it 
                will return null.
            </remarks>
            <param name="keyName">
                The name of the key you want to get, set, or add.
            </param>
        */
        public List<string> this[string keyName]
        {
            get
            {
                if (_keyData.ContainsKey(keyName))
                {
                    return _keyData[keyName].Values;
                }

                return null;
            }

            set
            {
                if (!_keyData.ContainsKey(keyName))
                {
                    this.Add(keyName, value);
                }
                else
                {
                    _keyData[keyName].Values = value;
                }
            }
        }
        #endregion

        #region Methods
        /*
            <summary>
                Add a key to the collection with a key name keyName.
            </summary>
            <param name="keyName">
                The string of the Key to add to the collection.
            </param>
        */
        public void Add(string keyName)
        {
            _keyData.Add(keyName, new KeyData(keyName));
        }

        /*
            <summary>
                Add a key to the collection with a key name keyName and values.
            </summary>
            <param name="keyName">
                The string of the Key to add to the collection.
            </param>
            <param name="values">
                A List of strings to set as the values of the collection.
            </param>
        */
        public void Add(string keyName, List<string> values)
        {
            _keyData.Add(keyName, new KeyData(keyName, values));
        }

        /*
            <summary>
                Add a key to the collection with a key name keyName and values.
            </summary>
            <param name="keyName">
                The string of the Key to add to the collection.
            </param>
            <param name="data">
                A KeyData which contains the Key name and values as List of strings
            </param>
        */
        public void Add(KeyData data)
        {
            _keyData.Add(data.KeyName, new KeyData(data.KeyName, data.Values));
        }

        /*
            <summary>
                Checks if the collection contains the Key keyName.
            </summary>
            <param name="keyName">
                Name of the Key you want to check if it exists in the collect.
            </param>
            <return>
                Returns a boolean.
            </return>
        */
        public bool ContainsKey(string keyName)
        {
            return _keyData.ContainsKey(keyName);
        }

        /*
            <summary>
                Counts the number of Key/Values in the collection
            </summary>
            <return>
                Returns integer
            </return>
        */
        public int Count()
        {
            return _keyData.Count;
        }

        /*
            <summary>
                Gets the Key pass in through keyName
            </summary>
            <return>
                Return KeyData, data about a key including KeyName and List of strings that represents a value
            </retun>
        */
        public KeyData GetKeyData(string keyName)
        {
            if(ContainsKey(keyName))
            {
                return _keyData[keyName];
            }
            return null;
        }

        /*
            <summary>
                Merges all keys from keyDataToMerge into this instance
            </summary>
        */
        public void Merge(KeyDataCollection keyDataToMerge)
        {
        }

        /*
            <summary>
                Removes all keys from the collection.
            </summary>
        */
        public void Clear()
        {
            _keyData.Clear();
        }

        /*
            <summary>
                Removes the key/value with the Key with the name pass in keyName
            </summary>
        */
        public void Remove(string keyName)
        {
            _keyData.Remove(keyName);
        }

        /*
            <summary>
                Sets the Key specified by keyData.KeyName and sets keyData
            </summary>
        */
        public void SetKeyData(KeyData keyData)
        {
            if(_keyData.ContainsKey(keyData.KeyName))
            {
                _keyData[keyData.KeyName] = keyData;
            }
            else
            {
                _keyData.Add(keyData.KeyName, keyData);
            }
        }
        #endregion

        #region IEnumerator Methods
        /*
            <summary>
                Gets a eunumerator of KeyData to iterate through the collection.
            </summary>
            <return>
                Returns a strong type IEnumerator<KeyData> an enumerator of KeyData
            </return>
        */
        public IEnumerator<KeyData> GetEnumerator()
        {
            foreach(string key in _keyData.Keys)
            {
                yield return _keyData[key];
            }
        }

        /*
            <summary>
                Gets a Enumerator of the collection
            </summary>
            <return>
                returns a weak type IEnumerator
            </return>
        */
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _keyData.GetEnumerator();
        }
        #endregion


        #region ICloneable Members
        /*
            <summary>
                Creates a copy of the current instance
            </summary>
            <return>
                Returns a  copy of the current instance
            </return>
        */
        public object Clone()
        {
            return new KeyDataCollection(this, _searchComparer);

        }
        #endregion
    }
}
