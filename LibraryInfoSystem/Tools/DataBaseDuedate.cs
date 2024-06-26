using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;

namespace LibraryInfoSystem.Tools
{
    public class DataBaseDuedate
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

        [BsonElement("isAdmin")]
        public bool? _isAdmin { get; set; }

        public DataBaseDuedate(string[] title, string? userId, string? bookedDate, string? deadlineDate, bool? isAdmin)
        {
            _title = title;
            _userId = userId;
            _bookedDate = bookedDate;
            _deadlineDate = deadlineDate;
            _isAdmin = isAdmin;
        }
    }
}
