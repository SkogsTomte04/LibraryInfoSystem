using LibraryInfoSystem.Tools;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryInfoSystem.Pages
{
    public partial class OverDue : Page
    {
        private List<DataBaseOverdue> overdue = new List<DataBaseOverdue>();
        private List<DataBaseOverdue> originalOverdue = new List<DataBaseOverdue>();

        public OverDue()
        {
            InitializeComponent();
            LoadOverdueData();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/Welcome.xaml", UriKind.Relative));
        }

        private void LoadOverdueData()
        {
            try
            {
                overdue = SessionManager.ItemsOverdue;
                originalOverdue = new List<DataBaseOverdue>(overdue);
                OverDueDataGrid.ItemsSource = overdue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading overdue data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var overdues = (List<DataBaseOverdue>)OverDueDataGrid.ItemsSource;

                foreach (var overdue in overdues)
                {
                    var filter = Builders<DataBaseOverdue>.Filter.Eq(d => d.Id, overdue.Id);
                    var update = Builders<DataBaseOverdue>.Update
                        .Set(d => d._title, overdue._title)
                        .Set(d => d._userId, overdue._userId)
                        .Set(d => d._bookedDate, overdue._bookedDate)
                        .Set(d => d._deadlineDate, overdue._deadlineDate)
                        .Set(d => d._isAdmin, overdue._isAdmin);

                    MongoHandler.GetCollection<DataBaseOverdue>("overdue_games").UpdateOne(filter, update);
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
            overdue = overdue.OrderBy(d => d._title[0]).ToList();
            OverDueDataGrid.ItemsSource = overdue;
        }

        private void SortByLowestPrice_Click(object sender, RoutedEventArgs e)
        {
            overdue = overdue.OrderByDescending(d => d._title[0]).ToList();
            OverDueDataGrid.ItemsSource = overdue;
        }

        private void SortByHighestPrice_Click(object sender, RoutedEventArgs e)
        {
            // Reload the original data to reset the sorting
            LoadOverdueData();
        }
    }
}

