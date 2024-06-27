using LibraryInfoSystem.Tools;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInfoSystem.Pages
{
    public partial class DueDate : Page
    {
        private IMongoCollection<DataBaseDuedate> _duedateCollection;

        public DueDate()
        {
            _duedateCollection = MongoHandler.GetCollection<DataBaseDuedate>("duedate_games");
            InitializeComponent();
            LoadDueDateData();
        }

        private async void LoadDueDateData()
        {
            try
            {   
                var user = SessionManager.CurrentUser.UserId;
                var userFilter = Builders<DataBaseDuedate>.Filter.Eq("userId", user);
                //var userDocument = await _duedateCollection.Find(userFilter).FirstOrDefaultAsync();
                var listDocument = await _duedateCollection.Find(userFilter).ToListAsync();

                foreach (var dueDate in listDocument)
                {
                    var row = new
                    {
                        TitlesString = string.Join(", ", dueDate._title),
                        dueDate._userId,
                        dueDate._bookedDate,
                        dueDate._deadlineDate
                    };
                    DueDateDataGrid.Items.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading due date data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
