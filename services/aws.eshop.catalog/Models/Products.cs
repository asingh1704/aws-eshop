using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aws.eshop.catalog.Models
{
    public class Product
    {
        [BsonId] // Marks this field as the unique identifier (_id in MongoDB)
        [BsonRepresentation(BsonType.ObjectId)] // Allows working with string representations of ObjectId
        public string Id { get; set; }


        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("discription")]
        public string Discription { get; set; }

        [BsonElement("name")]
        public string ProductName { get; set; }

        [BsonElement("imgurl")]
        public string ImageUrl { get; set; }

        [BsonElement("qty")]
        public decimal Qty { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
