using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.Models
{
    public class TestModuleModel
    {
        /// <summary>
        /// Module Id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; private set; }
        /// <summary>
        /// Module Type
        /// </summary>
        [BsonElement("ModuleType")]
        public string ModuleType { get; set; }
        /// <summary>
        /// Created Datatime
        /// </summary>
        [BsonElement("CreatedDTM")]
        public DateTime ? CreatedDTM { get; set; }
        /// <summary>
        /// Modified Datetime
        /// </summary>
        [BsonElement("ModifiedDTM")]
        public DateTime ModifiedDTM { get; private set; } = DateTime.UtcNow;
        /// <summary>
        /// Is Active
        /// </summary>
        [BsonElement("IsActive")]
        public bool IsActive { get; set; }
    }
}
