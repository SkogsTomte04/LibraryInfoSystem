using LibraryInfoSystem.Tools;
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
        private List<DataBaseDuedate> duedateList;

        public DueDate()
        {
            InitializeComponent();
            duedateList = new List<DataBaseDuedate>();
            LoadDueDateData();
        }

        private void LoadDueDateData()
        {
            try
            {
                foreach (var dueDate in mongohandler.duedate)
                {
                    MessageBox.Show($"Title: {dueDate._title}\n" +
                                    $"Price: {dueDate._price}\n" +
                                    $"User ID: {dueDate._userId}\n" +
                                    $"Booked Date: {dueDate._bookedDate}\n" +
                                    $"Deadline Date: {dueDate._deadlineDate}\n" +
                                    $"Payment Method: {dueDate._paymentMethod}\n" +
                                    $"Admin: {dueDate._isAdmin}\n", "Due Date Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading due date data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintDueDates()
        {
            foreach (var dueDate in duedateList)
            {
                Console.WriteLine($"Title: {dueDate._title}");
                Console.WriteLine($"Price: {dueDate._price}");
                Console.WriteLine($"User ID: {string.Join(", ", dueDate._userId)}");
                Console.WriteLine($"Booked Date: {dueDate._bookedDate}");
                Console.WriteLine($"Deadline Date: {dueDate._deadlineDate}");
                Console.WriteLine($"Payment Method: {dueDate._paymentMethod}");
                Console.WriteLine($"Admin: {dueDate._isAdmin}\n");
            }
        }
    }
}
