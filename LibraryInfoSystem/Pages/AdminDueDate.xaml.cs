﻿using LibraryInfoSystem.Tools;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Linq;

namespace LibraryInfoSystem.Pages
{
    public partial class AdminDueDate : Page
    {
        private MongoHandler mongohandler = new MongoHandler(DataType.Duedate);
        private List<DataBaseDuedate> dueDates = new List<DataBaseDuedate>();
        private List<DataBaseDuedate> originalDueDates = new List<DataBaseDuedate>();

        public AdminDueDate()
        {
            InitializeComponent();
            LoadDueDateData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Uri("Pages/AdminMenu.xaml", UriKind.Relative));
        }

        private void LoadDueDateData()
        {
            try
            {
                dueDates = mongohandler.duedate;
                originalDueDates = new List<DataBaseDuedate>(dueDates);
                AdminDueDateDataGrid.ItemsSource = dueDates;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading due date data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            dueDates = dueDates.AsEnumerable().OrderBy(d => d._title).ToList();
            AdminDueDateDataGrid.ItemsSource = dueDates;
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            dueDates = dueDates.AsEnumerable().OrderByDescending(d => d._title).ToList();
            AdminDueDateDataGrid.ItemsSource = dueDates;
        }

        private void DefaultSort_Click(object sender, RoutedEventArgs e)
        {
            LoadDueDateData();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dueDates = (List<DataBaseDuedate>)AdminDueDateDataGrid.ItemsSource;

                foreach (var dueDate in dueDates)
                {
                    var filter = Builders<DataBaseDuedate>.Filter.Eq(d => d.Id, dueDate.Id);
                    var update = Builders<DataBaseDuedate>.Update
                        .Set(d => d._title, dueDate._title)
                        .Set(d => d._userId, dueDate._userId)
                        .Set(d => d._bookedDate, dueDate._bookedDate)
                        .Set(d => d._deadlineDate, dueDate._deadlineDate)
                        .Set(d => d._isAdmin, dueDate._isAdmin);

                    mongohandler.GetDuedateCollection().UpdateOne(filter, update);
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
