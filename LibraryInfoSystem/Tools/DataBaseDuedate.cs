using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;

namespace LibraryInfoSystem.Tools
{
    public class DataBaseDuedate
    {
        [BsonId]
        public ObjectId Id { get; set; }  // Changed to public

        [BsonElement("title")]
        public List<string>? _title { get; set; }

        [BsonElement("userId")]
        public string? _userId { get; set; }  // Changed to string

        [BsonElement("bookedDate")]
        public string? _bookedDate { get; set; }

        [BsonElement("deadlineDate")]
        public string? _deadlineDate { get; set; }

        [BsonElement("isAdmin")]
        public bool? _isAdmin { get; set; }

        public DataBaseDuedate(List<string>? title, string? userId, string? bookedDate, string? deadlineDate, bool? isAdmin)
        {
            _title = title;
            _userId = userId;
            _bookedDate = bookedDate;
            _deadlineDate = deadlineDate;
            _isAdmin = isAdmin;
        }

        public List<string> GetTitle()
        {
            return _title;
        }
    }
}
