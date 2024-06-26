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

        private MongoHandler logIn = new MongoHandler(DataType.Users); 

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

            if (logIn.Validation(username, password))
            {
                if (logIn.AdminValidation(username))
                {
                    MessageBox.Show("Welcome, Admin.", "admin");
                    var ClickedButton = e.OriginalSource as NavButton;
                    NavigationService.Navigate(ClickedButton.NavUri);
                }
                else
                {
                    SessionManager.InitializeSession(logIn.CurrentUser);
                    MessageBox.Show("Welcome.", "user");
                    var ClickedButton = e.OriginalSource as NavButton;
                    ClickedButton.NavUri = new Uri("/Pages/CustomerMenu.xaml", UriKind.Relative);
                    NavigationService.Navigate(ClickedButton.NavUri);
                }
            }
            else { MessageBox.Show("Failure.", "Error"); return; }

            

            logIn.IsLoggedIn();
        }
    }
}
