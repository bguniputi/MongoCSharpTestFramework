using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.Models
{
    public class GlobalTestDataModel
    {
        /// <summary>
        /// Id field
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; private set; }
        /// <summary>
        /// TestCase Id field
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Project_id")]
        public ObjectId Project_id { get; set; }
        /// <summary>
        /// ExtraElements field
        /// </summary>
        [BsonExtraElements]
        [BsonElement(elementName:"ExtraElements")]
        public BsonDocument ExtraElements { get; set; }
        /// <summary>
        /// IsActive field
        /// </summary>
        [BsonElement]
        public bool IsActive { get; set; }

    }
}
