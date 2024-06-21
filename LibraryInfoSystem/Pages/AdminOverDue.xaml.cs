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
        private List<DataBaseOverdue> overdueItems = new List<DataBaseOverdue>();
        private List<DataBaseOverdue> originalOverdueItems = new List<DataBaseOverdue>();

        public AdminOverDue()
        {
            InitializeComponent();
            LoadOverdueData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/AdminMenu.xaml", UriKind.Relative));
        }

        private void LoadOverdueData()
        {
            try
            {
                overdueItems = mongohandler.overdue;
                originalOverdueItems = new List<DataBaseOverdue>(overdueItems);
                OverdueDataGrid.ItemsSource = overdueItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading overdue data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            overdueItems = overdueItems.OrderBy(d => d._userId).ToList();
            OverdueDataGrid.ItemsSource = overdueItems;
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            overdueItems = overdueItems.OrderByDescending(d => d._userId).ToList();
            OverdueDataGrid.ItemsSource = overdueItems;
        }

        private void DefaultSort_Click(object sender, RoutedEventArgs e)
        {
            // Reload the original data to reset the sorting
            LoadOverdueData();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = (List<DataBaseOverdue>)OverdueDataGrid.ItemsSource;

                foreach (var item in items)
                {
                    var filter = Builders<DataBaseOverdue>.Filter.Eq(d => d.Id, item.Id);
                    var update = Builders<DataBaseOverdue>.Update
                        .Set(d => d._title, item._title)
                        .Set(d => d._userId, item._userId)
                        .Set(d => d._bookedDate, item._bookedDate)
                        .Set(d => d._deadlineDate, item._deadlineDate)
                        .Set(d => d._isAdmin, item._isAdmin)
                        .Set(d => d._status, item._status);

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
