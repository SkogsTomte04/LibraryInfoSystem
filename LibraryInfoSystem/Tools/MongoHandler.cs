using DnsClient;
using LibraryInfoSystem.Pages;
using Microsoft.Win32;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using static MongoDB.Driver.WriteConcern;

namespace LibraryInfoSystem.Tools
{
    public enum DataType
    {
        Users,
        Games
    }
    class MongoHandler
    {
        private readonly string connectionUri = "mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        private IMongoDatabase database;
        private DataType dataType;
        public List<DataBaseUser> users;
        public DataBaseUser CurrentUser { get; set; }
        public List<DataBaseItem> items;

        public MongoHandler(DataType dt)
        {
            dataType = dt;
            ConnectToDatabase();
            
        }

        private void ConnectToDatabase()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            database = client.GetDatabase("GameDataBase");

            switch (dataType)
            {
                case DataType.Users:
                    LoadUsers();
                    break;
                case DataType.Games:
                    LoadItems();
                    break;
                default:
                    throw new ArgumentException("Invalid data type specified.");
            }
        }
        public void RemoveUser(DataBaseUser user)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to permanently delete user: {user.UserId} \nNote: This action can not be undone", "Deleting User From Database", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) { return; }

            var collection = GetCollection<DataBaseUser>("users");
            var filter = Builders<DataBaseUser>.Filter.Eq("_id", user.GetMongoId());
            //var item = await collection.Find(Builders<DataBaseUser>.Filter.Eq("_id", user.GetMongoId())).FirstOrDefaultAsync();

            collection.DeleteOne(filter);
            MessageBox.Show($"Deleted {user.FirstName}");
            
            
        }
        public void EditUser(DataBaseUser newUser)
        {
            var filter = Builders<DataBaseUser>.Filter.Eq("_id", newUser.GetMongoId());
            GetCollection<DataBaseUser>("users").ReplaceOne(filter, newUser);

        }

        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            return database.GetCollection<T>(collection);
        }

        private void LoadUsers()
        {
            try
            {
                var usersCollection = GetCollection<DataBaseUser>("users");
                users = usersCollection.AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadItems()
        {
            try
            {
                var itemsCollection = GetCollection<DataBaseItem>("games");
                items = itemsCollection.AsQueryable().ToList();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public bool AdminValidation(string username)
        {
            foreach (var user in users)
            {
                if (user.UserId == username)
                {
                    if (user.IsAdmin == true) { return true; }
                }
            }
            return false;
        }

        public bool Validation(string username, string password)
        {
            foreach (var user in users)
            {
                if (user.UserId == username)
                {
                    if (user.Password == password)
                    {
                        CurrentUser = user; //Set current user on successful log-in
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public bool LoggedOut()
        {
            return CurrentUser == null;
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
        
