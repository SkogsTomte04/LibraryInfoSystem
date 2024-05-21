using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LibraryInfoSystem.Tools
{
    class DataBaseItem
    {
        [BsonId]
        protected ObjectId Id { get; set; }

        [BsonElement("title")]
        public string? title { get; set; }

        [BsonElement("price")]
        public double? price { get; set; }

        [BsonElement("platform")]
        public ObservableCollection<string> platform { get; set; }

        DataBaseItem(string? Title, double? Price, ObservableCollection<string> Platform)
        {
            title = Title;
            price = Price;
            platform = Platform;
        }
    }
}
