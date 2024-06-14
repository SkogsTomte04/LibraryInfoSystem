using DnsClient;
using LibraryInfoSystem.Pages;
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
        private readonly string connectionUri = "mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        private IMongoDatabase database;
        public List<DataBaseUser> users;
        public DataBaseUser CurrentUser { get; set; }
        public List<DataBaseItem> items;
        public List<DataBaseDuedate> duedate;
        public List<DataBaseOverdue> overdue;

        public MongoHandler(DataType dataType)
        {
            ConnectToDatabase();
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

        private void ConnectToDatabase()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            database = client.GetDatabase("GameDataBase");
        }

        public IMongoCollection<T> GetCollection<T>(string collection)
        {
            return database.GetCollection<T>(collection);
        }

        public IMongoCollection<DataBaseDuedate> GetDuedateCollection()
        {
            return database.GetCollection<DataBaseDuedate>("duedate_games");
        }

        public IMongoCollection<DataBaseOverdue> GetOverdueCollection()
        {
            return database.GetCollection<DataBaseOverdue>("overdue_games");
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

        private void LoadDuedate()
        {
            try
            {
                var DuedateCollection = GetDuedateCollection();
                duedate = DuedateCollection.AsQueryable().ToList();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void LoadOverdue()
        {
            try
            {
                var OverdueCollection = GetOverdueCollection();
                overdue = OverdueCollection.AsQueryable().ToList();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
        
