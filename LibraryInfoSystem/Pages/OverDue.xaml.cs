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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/Welcome.xaml", UriKind.Relative));
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

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            overdues = overdues.OrderBy(d => d._title).ToList();
            OverDueDataGrid.ItemsSource = null;
            OverDueDataGrid.ItemsSource = overdues;
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            overdues = overdues.OrderByDescending(d => d._title).ToList();
            OverDueDataGrid.ItemsSource = null;
            OverDueDataGrid.ItemsSource = overdues;
        }

        private void DefaultSort_Click(object sender, RoutedEventArgs e)
        {
            LoadOverDueData();
        }
    }
}
