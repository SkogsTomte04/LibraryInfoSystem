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
        private MongoHandler mongohandler = new MongoHandler(DataType.Duedate);
        private List<DataBaseDuedate> dueDates;
        private MongoHandler user = new MongoHandler(DataType.Users);

        public DueDate()
        {
            InitializeComponent();
            LoadDueDateData();
        }

        private void LoadDueDateData()
        {
            try
            {
                dueDates = mongohandler.duedate;
                DueDateDataGrid.ItemsSource = dueDates;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading due date data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (user.CurrentUser != null)
            {

            }
            
            try
            {
                var dueDates = (List<DataBaseDuedate>)DueDateDataGrid.ItemsSource;

                foreach (var dueDate in dueDates)
                {
                    var filter = Builders<DataBaseDuedate>.Filter.Eq(d => d.Id, dueDate.Id);
                    var update = Builders<DataBaseDuedate>.Update
                        .Set(d => d._title, dueDate._title)
                        .Set(d => d._userId, dueDate._userId)
                        .Set(d => d._bookedDate, dueDate._bookedDate)
                        .Set(d => d._deadlineDate, dueDate._deadlineDate)
                        .Set(d => d._isAdmin, dueDate._isAdmin);

                    mongohandler.GetCollection<DataBaseDuedate>("duedate_games").UpdateOne(filter, update);
                }

                MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving changes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAlphabetically_Click(object sender, RoutedEventArgs e)
        {
            dueDates = dueDates.OrderBy(d => d._title).ToList();
            DueDateDataGrid.ItemsSource = null;
            DueDateDataGrid.ItemsSource = dueDates;
        }
    }
}
