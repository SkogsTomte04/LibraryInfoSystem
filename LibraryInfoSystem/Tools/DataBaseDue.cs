using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoSystem.Tools
{
    public class DataBaseDue
    {
        [BsonId]
        protected ObjectId Id { get; set; }

        [BsonElement("title")]
        public string? _title { get; set; }

        [BsonElement("price")]
        public double? _price { get; set; }

        [BsonElement("userId")]
        public List<string>? _userId { get; set; }

        [BsonElement("bookedDate")]
        public string? _bookedDate { get; set; }
        [BsonElement("paymentDate")]
        public string? _paymentDate { get; set; }
        [BsonElement("paymentMethod")]
        public string? _paymentMethod { get; set; }
    }
}
