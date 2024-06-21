using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LibraryInfoSystem.Tools
{
    public class DataBaseDuedate
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string[] _title { get; set; }

        [BsonElement("price")]
        public double? _price { get; set; }

        [BsonElement("userId")]
        public string? _userId { get; set; }

        [BsonElement("bookedDate")]
        public string? _bookedDate { get; set; }

        [BsonElement("deadlineDate")]
        public string? _deadlineDate { get; set; }

        [BsonElement("paymentMethod")]
        public string? _paymentMethod { get; set; }

        [BsonElement("isAdmin")]
        public bool? _isAdmin { get; set; }
    }
}
