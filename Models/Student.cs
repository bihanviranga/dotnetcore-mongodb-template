using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetcore_mongodb_template.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Major { get; set; }

        // List<string> is the array of ObjectId's we store
        // as foreign keys.
        // List<Course> is used in the controller only.
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Courses { get; set; }
        [BsonIgnore]
        public List<Course> CourseList { get; set; }
    }
}
