using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoTestDatabaseLibrary.Models
{
    public class TestCaseModel
    {

        /// <summary>
        /// Id field
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; private set; }
        /// <summary>
        /// Scenraio Id field
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId TestClass_id { get; set; }
        /// <summary>
        /// TestCase field
        /// </summary>
        [BsonElement("TestCaseName")]
        public string TestCaseName { get; set; }
        /// <summary>
        /// Testcase description field
        /// </summary>
        [BsonElement("TestCaseDescription")]
        public string TestCaseDescription { get; set; }
        /// <summary>
        /// Testcase type field
        /// </summary>
        [BsonElement("TestCaseType")]
        public string TestCaseType { get; set; }
        /// <summary>
        /// Created By field (Employee id)
        /// </summary>
        [BsonElement("CreatedBy")]
        public int? CreatedBy { get; set;}
        /// <summary>
        /// Created Datetime field
        /// </summary>
        [BsonElement("CreatedDTM")]
        public DateTime ? CreatedDTM { get; set; }
        /// <summary>
        /// Modified by field (Employee Id)
        /// </summary>
        [BsonElement("ModifiedBy")]
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Modified datetime field
        /// </summary>
        [BsonElement("ModifiedDTM")]
        public DateTime ModifiedDTM { get; private set; } = DateTime.UtcNow;
        /// <summary>
        /// Testcase priority field
        /// </summary>
        [BsonElement("TestCasePriority")]
        public string TestCasePriority { get; set; }
        /// <summary>
        /// Test category field
        /// </summary>
        [BsonElement("TestCategory")]
        public string TestCategory { get; set; }
        /// <summary>
        ///  TestCase version field
        /// </summary>
        [BsonElement("Version")]
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [BsonElement("DependentTestName")]
        public string DependentTestCaseName{ get; set; }
        /// <summary>
        ///  TestCase order field
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
