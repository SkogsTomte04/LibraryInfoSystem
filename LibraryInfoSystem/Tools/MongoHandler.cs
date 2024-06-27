using DnsClient;
using LibraryInfoSystem.Pages;
using Microsoft.Win32;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MongoDB.Driver.WriteConcern;

namespace LibraryInfoSystem.Tools
{
    public enum DataType
    {
        Users,
        Games,
        Duedate,
        Overdue
    }

    class MongoHandler
    {
        private static readonly string connectionUri = "mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        private static IMongoDatabase database;
        public static List<DataBaseUser> users;
        public static List<DataBaseItem> items;
        public static List<DataBaseDuedate> duedate;
        public static List<DataBaseOverdue> overdue;


        public static void ConnectToDatabase(DataType dataType)
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
                case DataType.Duedate:
                    LoadDuedate();
                    break;
                case DataType.Overdue:
                    LoadOverdue();
                    break;
                default:
                    throw new ArgumentException("Invalid data type specified.");
            }
        }
        public static void RemoveUser(DataBaseUser user)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to permanently delete user: {user.UserId} \nNote: This action can not be undone", "Deleting User From Database", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) { return; }

            var collection = GetCollection<DataBaseUser>("users");
            var filter = Builders<DataBaseUser>.Filter.Eq("_id", user.GetMongoId());
            //var item = await collection.Find(Builders<DataBaseUser>.Filter.Eq("_id", user.GetMongoId())).FirstOrDefaultAsync();

            collection.DeleteOne(filter);
            MessageBox.Show($"Deleted {user.FirstName}");
            
            
        }
        public static void EditUser(DataBaseUser newUser)
        {
            var filter = Builders<DataBaseUser>.Filter.Eq("_id", newUser.GetMongoId());
            GetCollection<DataBaseUser>("users").ReplaceOne(filter, newUser);

        }

        public static IMongoCollection<T> GetCollection<T>(string collection)
        {
            return database.GetCollection<T>(collection);
        }
        private static void LoadUsers()
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

        private static void LoadItems()
        {

            //watch.Start();
            try
            {
                var filter = Builders<DataBaseItem>.Filter.Empty;
                var itemsCollection = GetCollection<DataBaseItem>("games");
                
                items = itemsCollection.Find(filter).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //watch.Stop();
            //MessageBox.Show($"Load Items elapsed seconds: {watch.Elapsed}");
        }

        private static void LoadDuedate()
        {
            try
            {
                var DuedateCollection = GetCollection<DataBaseDuedate>("duedate_games");
                duedate = DuedateCollection.AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void LoadOverdue()
        {
            try
            {
                var OverdueCollection = GetCollection<DataBaseOverdue>("overdue_games");
                overdue = OverdueCollection.AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static bool AdminValidation(string username)
        {
            LoadUsers();
            foreach (var user in users)
            {
                if (user.UserId == username)
                {
                    if (user.IsAdmin == true) { return true; }
                }
            }
            return false;
        }

        public static bool Validation(string username, string password)
        {
            ConnectToDatabase(DataType.Users);
            foreach (var user in users)
            {
                if (user.UserId == username)
                {
                    if (user.Password == password)
                    {
                        SessionManager.InitializeSession(user); //Set current user on successful log-in
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsLoggedIn()
        {
            return SessionManager.CurrentUser != null;
        }

        public static bool LoggedOut()
        {
            return SessionManager.CurrentUser == null;
        }

        public static ImageSource convertbitmap(string bit)
        {
            BitmapSource convertedImage = null;
            if (string.IsNullOrWhiteSpace(bit) == false) //image conversion
            {
                convertedImage = BitmapFromBase64(bit);
                return convertedImage;
            }
            else { MessageBox.Show("Bitmapconversion error"); return null; }

            
        }

        private static BitmapSource BitmapFromBase64(string? b64string)
        {
            var bytes = Convert.FromBase64String(b64string);
            using (var stream = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}
