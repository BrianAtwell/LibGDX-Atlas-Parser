using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibGDXAtlasParser.Model
{
    /*
        <summary>
            KeyTypeUtilities contains a collection of methods
        </summary>
    */
    public class KeyTypeUtilities<T>
    {
        #region Static Members
        /*
            <summary>
                Generic method to convert a string to an enum.
            </summary>
            <param name="typeStr">
                string representing the enum value to be converted to an enum.
            </param>
            <param name="myType">
                ref of the enum to return the converted value.
            </param>
        */
        public static void Parse(string typeStr, ref T myType)
        {
            try
            {
                myType = (T)Enum.Parse(typeof(T), typeStr);
                if (Enum.IsDefined(typeof(T), myType) | myType.ToString().Contains(","))
                    Console.WriteLine("Converted '{0}' to {1}.", typeStr, myType.ToString());
                else
                    Console.WriteLine("{0} is not an underlying value of the Colors enumeration.", typeStr);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("'{0}' is not a member of the Colors enumeration.", typeStr);
            }
        }
        #endregion
    }
}
