using LibraryInfoSystem.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace LibraryInfoSystem.Pages
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var clickedButton = e.OriginalSource as NavButton;
                if (clickedButton != null && clickedButton.NavUri != null)
                {
                    NavigationService?.Navigate(clickedButton.NavUri);
                }
                else
                {
                    MessageBox.Show("Navigation URI is not set for the clicked button.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}