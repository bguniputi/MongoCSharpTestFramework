using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.Models
{
    public class TestClassModel
    {
        /// <summary>
        /// Class Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; private set; }
        /// <summary>
        /// Module Id field
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Module_id { get; set; }
        /// <summary>
        /// Test Class name
        /// </summary>
        [BsonElement("TestClassName")]
        public string TestClassName { get; set; }
        /// <summary>
        /// Test Class Order
        /// </summary>
        [BsonElement("Order")]
        public int Order { get; set; }
        /// <summary>
        /// Is Active field
        /// </summary>
        [BsonElement("IsActive")]
        public bool IsActive { get; set; } = true;

    }
}
