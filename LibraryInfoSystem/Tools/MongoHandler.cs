﻿using LibraryInfoSystem.Pages;
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
    class MongoHandler
    {
        private const string connectionUri = "mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        public List<DataBaseUser> users;
        public List<DataBaseItem> items;

        public void ConnectToUsers()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection

            try
            {
                var result = client.GetDatabase("GameDataBase");
                IMongoCollection<DataBaseUser> collection = result.GetCollection<DataBaseUser>("users");
                users = collection.AsQueryable().ToList<DataBaseUser>();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ConnectToGames()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            // Send a ping to confirm a successful connection

            try
            {
                var result = client.GetDatabase("GameDataBase");
                IMongoCollection<DataBaseItem> collection = result.GetCollection<DataBaseItem>("games");
                items = collection.AsQueryable().ToList<DataBaseItem>();
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
