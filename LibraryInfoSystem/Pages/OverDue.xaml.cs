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
                overdue = mongohandler.overdue;
                originalOverdue = new List<DataBaseOverdue>(overdue);
                OverDueDataGrid.ItemsSource = overdue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading overdue data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            overdue = overdue.OrderBy(d => d._title[0]).ToList();
            OverDueDataGrid.ItemsSource = overdue;
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            overdue = overdue.OrderByDescending(d => d._title[0]).ToList();
            OverDueDataGrid.ItemsSource = overdue;
        }

        private void DefaultSort_Click(object sender, RoutedEventArgs e)
        {
            // Reload the original data to reset the sorting
            LoadOverdueData();
        }
    }
}
