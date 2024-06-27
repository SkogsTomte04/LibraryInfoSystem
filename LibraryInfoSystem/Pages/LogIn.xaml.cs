using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryInfoSystem.Tools;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LibraryInfoSystem.Pages
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {

        public LogIn()
        {
            InitializeComponent();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTxt.Text;
            string password = passwordTxt.Text;

            if (MongoHandler.Validation(username, password))
            {
                if (MongoHandler.AdminValidation(username))
                {
                    MessageBox.Show("Welcome, Admin.", "admin");
                    var ClickedButton = e.OriginalSource as NavButton;
                    NavigationService.Navigate(ClickedButton.NavUri);
                }
                else
                {
                    MessageBox.Show($"Welcome.", SessionManager.CurrentUser.FirstName);
                    var ClickedButton = e.OriginalSource as NavButton;
                    ClickedButton.NavUri = new Uri("/Pages/CustomerMenu.xaml", UriKind.Relative);
                    NavigationService.Navigate(ClickedButton.NavUri);
                }
            }
            else { MessageBox.Show("Failure.", "Error"); return; }

            

            MongoHandler.IsLoggedIn();
        }
    }
}
