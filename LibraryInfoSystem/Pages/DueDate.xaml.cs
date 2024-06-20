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
        private List<DataBaseDuedate> dueDates = new List<DataBaseDuedate>();
        private List<DataBaseDuedate> originalDueDates = new List<DataBaseDuedate>();

        public DueDate()
        {
            InitializeComponent();
            LoadDueDateData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/Welcome.xaml", UriKind.Relative));
        }

        private void LoadDueDateData()
        {
            try
            {
                dueDates = mongohandler.duedate;
                originalDueDates = new List<DataBaseDuedate>(dueDates);
                DueDateDataGrid.ItemsSource = dueDates;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading due date data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            dueDates = dueDates.OrderBy(d => d._title).ToList();
            DueDateDataGrid.ItemsSource = dueDates;
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            dueDates = dueDates.OrderByDescending(d => d._title).ToList();
            DueDateDataGrid.ItemsSource = dueDates;
        }

        private void DefaultSort_Click(object sender, RoutedEventArgs e)
        {
            // Reload the original data to reset the sorting
            LoadDueDateData();
        }
    }
}
