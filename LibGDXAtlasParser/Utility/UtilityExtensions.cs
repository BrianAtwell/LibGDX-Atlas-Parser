using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGDXAtlasParser.Model;

namespace LibGDXAtlasParser.Utility
{
    public static class UtilityExtensions
    {
        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            List<T> clonedList = new List<T>();
            
            foreach(T elem in list)
            {
                clonedList.Add((T)elem.Clone());
            }

            return clonedList;
        }

        /*
        public static List<SectionTreeCollection> Clone<SectionTreeCollection>(this List<SectionTreeCollection> list)
        {
            List<SectionTreeCollection> clonedList = new List<SectionTreeCollection>();

            foreach (SectionTreeCollection elem in list)
            {
                clonedList.Add((SectionTreeCollection)elem.Clone());
            }

            return clonedList;
        }
        */
    }
}
