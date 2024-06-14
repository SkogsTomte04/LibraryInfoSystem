using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders.BinaryEncoders;
using System.Diagnostics;
using System.Windows;

namespace LibraryInfoSystem.Tools
{
    class DataBaseItem
    {
        [BsonId]
        protected ObjectId Id { get; set; }

        [BsonElement("title")]
        public string? _title { get; set; }

        [BsonElement("price")]
        public double? _price { get; set; }

        [BsonElement("platform")]
        public List<string>? _platform { get; set; }

        [BsonElement("image")]
        public string? _image { get; set; }

        [BsonElement("demoimg")]
        public List<string>? _demoimg { get; set; }
    }

}
