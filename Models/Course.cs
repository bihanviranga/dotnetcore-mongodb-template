using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetcore_mongodb_template
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
