using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Model
{
     /* <summary>
            Information associated with a key from a json.atlas file
            from libgdx.
            Includes multiple values as a List of strings.
    
        </summary>
     */
    public class KeyData : ICloneable
    {
        #region Non-public Members
        private string _keyName;
        private List<string> _values;
        #endregion

        #region Initialization
        /*
        <summary>
            Initializes a new instance of the <see cref="KeyData"/> class
        </summary>
        */
        public KeyData(string keyName)
        {
            _keyName = keyName;
            _values = new List<string>();
        }

        /*
        <summary>
            Initializes a new instance of the <see cref="KeyData"/> class
        </summary>
        */
        public KeyData(string keyName, List<string> values)
        {
            _keyName = keyName;
            _values = values;
        }

        /*
        <summary>
            Initializes a new instance of the <see cref="KeyData"/> class
        </summary>
        */
        public KeyData(KeyData ori)
        {
            _keyName = ori.KeyName;
            _values = ori.Values;
        }
        #endregion

        #region Fields
        /*
        <summary>
            Set/get Values a List of strings
        </summary>
        */
        public List<string> Values {
            get { return _values; }
            set { _values = new List<string>(value);  }
        }

        /*
        <summary>
            Set/get string KeyName
        </summary>
        */
        public string KeyName
        {
            get { return _keyName; }
            set { if (value != string.Empty) _keyName = value; }
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
            return new KeyData(this);
        }
        #endregion
    }
}
