using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoTestDatabaseLibrary.Models
{
    public class TestResultModel
    {
        /// <summary>
        /// Id field
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; private set; }
        /// <summary>
        /// Testcase id field
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId TestCase_Id { get; set; }
        /// <summary>
        /// TestCycle field
        /// </summary>
        [BsonElement]
        public string TestCycle { get; set; }
        /// <summary>
        /// Result field
        /// </summary>
        [BsonElement]
        [BsonRepresentation(BsonType.String)]
        public string Result { get; set; }
        /// <summary>
        /// Execution DateTime field
        /// </summary>
        [BsonElement]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? ExecutedDTM { get; set; }
        /// <summary>
        /// List of results for the cycle field
        /// </summary>
        [BsonElement(elementName:"Resultsets")]
        public IEnumerable<string> Resultsets { get; set; }
    }
}
