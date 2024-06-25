using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace LibraryInfoSystem.Tools
{
    public class DataBaseOverdue
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string[] _title { get; set; }

        [BsonElement("userId")]
        public string? _userId { get; set; }

        [BsonElement("bookedDate")]
        public string? _bookedDate { get; set; }

        [BsonElement("deadlineDate")]
        public string? _deadlineDate { get; set; }

        [BsonElement("payment")]

        public string? _dueDate { get; set; }

        [BsonElement("isAdmin")]
        public bool? _isAdmin { get; set; }

        [BsonElement("status")]
        public string? _status { get; set; }
    }
}
