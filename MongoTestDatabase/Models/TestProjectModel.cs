using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.Models
{
    public class TestProjectModel
    {
        /// <summary>
        /// Id Key field
        /// </summary>
        [BsonId]
        public ObjectId _id { get; private set; }
        /// <summary>
        /// Projectname field
        /// </summary>
        [BsonElement]
        public string ProjectName { get; set; }
        /// <summary>
        /// Regulatory field
        /// </summary>
        [BsonElement]
        public string Regulator { get; set; }
        /// <summary>
        /// Created DateTime field
        /// </summary>
        [BsonElement]
        public DateTime ? CreatedDTM { get; set; }
        /// <summary>
        /// IsActive field
        /// </summary>
        [BsonElement]
        public bool IsActive { get; set; } = true;


    }
}
