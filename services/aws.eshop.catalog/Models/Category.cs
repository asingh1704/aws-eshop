using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aws.eshop.catalog.Models
{
    public class Category
    {
        [BsonId] // Marks this field as the unique identifier (_id in MongoDB)
        [BsonRepresentation(BsonType.ObjectId)] // Allows working with string representations of ObjectId
        public string Id { get; set; }

        [BsonElement("name")]
        public string CategoryName { get; set; }
    }
}
