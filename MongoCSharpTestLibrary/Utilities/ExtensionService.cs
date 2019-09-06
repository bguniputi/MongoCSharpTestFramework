using NextGenTestLibrary.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace NextGenTestLibrary.Utilities
{
    public static class ExtensionService
    {
        /// <summary>
        /// Get source in ascending order
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<int, Type> GetOrder(this IEnumerable<Type> source)
        {

            IDictionary<int, Type> keyValuePairs = new SortedDictionary<int, Type>();
            if (source == null)
            {
                throw new ArgumentNullException("source is null");
            }

            foreach (Type item in source)
            {               
                if (Attribute.GetCustomAttribute(item, typeof(TestOrderAttribute)) is TestOrderAttribute attribute)
                {
                    if (!keyValuePairs.ContainsKey(attribute.Order))
                    {
                        keyValuePairs.Add(attribute.Order, item);
                    }
                    
                }
            }

            return keyValuePairs;
        }
    }

}
