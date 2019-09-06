using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.Models
{
    
    public class TestDataModel
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
        [BsonElement(elementName:"TestCase_Id")]
        public ObjectId TestCase_Id { get; set; }
        /// <summary>
        /// ExtraElements field
        /// </summary>
        [BsonExtraElements]
        [BsonElement(elementName: "ExtraElements")]
        public BsonDocument ExtraElements { get; set; }
        /// <summary>
        /// Order field
        /// </summary>
        [BsonElement(elementName: "Order")]
        public int Order { get; set; }
        /// <summary>
        /// IsActive field
        /// </summary>
        [BsonElement(elementName:"IsActive")]
        public bool IsActive { get; set; }

        
    }
}
