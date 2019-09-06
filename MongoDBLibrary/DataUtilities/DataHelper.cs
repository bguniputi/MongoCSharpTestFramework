using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoCSharpLibrary.DataUtilities
{
    public static class DataHelper
    {
        static DataHelper()
        {

        }
        /// <summary>
        /// Convert Json string to BSON
        /// </summary>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static BsonDocument ConvertToBson(this string Json)
        {
            return BsonDocument.Parse(Json);
        }
        /// <summary>
        /// Convert object to JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
