using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryInfoSystem.Tools
{
    class MongoHandler
    {
        const string connectionUri = "mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        public List<DataBaseUser> items;
        public void ConnectToDatabase(string DataBase, string Collection)
        {

            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase(DataBase);
                IMongoCollection<DataBaseUser> collection = result.GetCollection<DataBaseUser>(Collection);

                items = collection.AsQueryable().ToList<DataBaseUser>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
