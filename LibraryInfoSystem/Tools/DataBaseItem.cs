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

        [BsonElement("image")]
        public string? image { get; set; }

        DataBaseItem(string? Title, double? Price, ObservableCollection<string> Platform, string? Image)
        {
            title = Title;
            price = Price;
            platform = Platform;
            image = Image;
        }
        public BitmapSource BitmapFromBase64(string? b64string)

        {

            var bytes = Convert.FromBase64String(b64string);

            using (var stream = new MemoryStream(bytes))

            {

                return BitmapFrame.Create(stream,

                BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

            }

        }
    }
}
