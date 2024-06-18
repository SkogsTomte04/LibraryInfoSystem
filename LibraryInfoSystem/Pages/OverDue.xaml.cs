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
        private MongoHandler mongohandler = new MongoHandler(DataType.Overdue);
        private List<DataBaseOverdue> overdues;

        public OverDue()
        {
            InitializeComponent();
            LoadOverDueData();
        }

        private void LoadOverDueData()
        {
            try
            {
                overdues = mongohandler.overdue;
                OverDueDataGrid.ItemsSource = overdues;
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
                        .Set(d => d._price, overdue._price)
                        .Set(d => d._userId, overdue._userId)
                        .Set(d => d._bookedDate, overdue._bookedDate)
                        .Set(d => d._deadlineDate, overdue._deadlineDate)
                        .Set(d => d._paymentMethod, overdue._paymentMethod)
                        .Set(d => d._isAdmin, overdue._isAdmin)
                        .Set(d => d._status, overdue._status);

                    mongohandler.GetOverdueCollection().UpdateOne(filter, update);
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
            overdues = overdues.OrderBy(d => d._title).ToList();
            OverDueDataGrid.ItemsSource = null;
            OverDueDataGrid.ItemsSource = overdues;
        }

        private void SortByLowestPrice_Click(object sender, RoutedEventArgs e)
        {
            overdues = overdues.OrderBy(d => d._price).ToList();
            OverDueDataGrid.ItemsSource = null;
            OverDueDataGrid.ItemsSource = overdues;
        }

        private void SortByHighestPrice_Click(object sender, RoutedEventArgs e)
        {
            overdues = overdues.OrderByDescending(d => d._price).ToList();
            OverDueDataGrid.ItemsSource = null;
            OverDueDataGrid.ItemsSource = overdues;
        }
    }
}

