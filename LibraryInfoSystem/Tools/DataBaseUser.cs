using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LibraryInfoSystem.Tools
{
    public class DataBaseUser
    {

        [BsonId]
        protected ObjectId Id { get; set; }

        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [BsonElement("isAdmin")]
        public Boolean? IsAdmin { get; set; }

        public DataBaseUser(string? firstName, string? lastName, string? userId, string? password, string? email, string? phoneNumber, Boolean? isAdmin)
        {
            FirstName = firstName;
            LastName = lastName;
            UserId = userId;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
        }

        public ObjectId GetMongoId()
        {
            return Id;
        }
    }
}
