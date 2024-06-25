using LibraryInfoSystem.Tools;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInfoSystem.Pages
{
    public partial class AdminOverDue : Page
    {
        private MongoHandler mongohandler = new MongoHandler(DataType.Overdue);
        private List<DataBaseOverdue> overDueList = new List<DataBaseOverdue>();
        private List<DataBaseOverdue> originalOverDueList = new List<DataBaseOverdue>();

        public AdminOverDue()
        {
            InitializeComponent();
            LoadOverDueData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/AdminMenu.xaml", UriKind.Relative));
        }

        private void LoadOverDueData()
        {
            try
            {
                overDueList = mongohandler.overdue;
                originalOverDueList = new List<DataBaseOverdue>(overDueList);
                AdminOverDueDataGrid.ItemsSource = overDueList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading overdue data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            overDueList = overDueList.OrderBy(d => d._title[0]).ToList();
            AdminOverDueDataGrid.ItemsSource = overDueList;
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            overDueList = overDueList.OrderByDescending(d => d._title[0]).ToList();
            AdminOverDueDataGrid.ItemsSource = overDueList;
        }

        private void DefaultSort_Click(object sender, RoutedEventArgs e)
        {
            // Reload the original data to reset the sorting
            LoadOverDueData();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var overdue in overDueList)
                {
                    var filter = Builders<DataBaseOverdue>.Filter.Eq(d => d.Id, overdue.Id);
                    var update = Builders<DataBaseOverdue>.Update
                        .Set(d => d._title, overdue._title)
                        .Set(d => d._userId, overdue._userId)
                        .Set(d => d._bookedDate, overdue._bookedDate)
                        .Set(d => d._deadlineDate, overdue._deadlineDate)
                        .Set(d => d._status, overdue._status)
                        .Set(d => d._isAdmin, overdue._isAdmin);

                    mongohandler.GetCollection<DataBaseOverdue>("overdue_games").UpdateOne(filter, update);
                }

                MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving changes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
