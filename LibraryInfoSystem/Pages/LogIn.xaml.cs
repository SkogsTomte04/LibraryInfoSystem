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

        private MongoHandler logIn = new MongoHandler();

        private void register_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            logIn.ConnectToDatabase("GameDataBase", "users");

            string username = usernameTxt.Text;
            string password = passwordTxt.Text;

            if (Validation(username, password)) { MessageBox.Show("Login Successful!", "Success"); }
            else { MessageBox.Show("Failure.", "Error"); }
        }

        private bool Validation(string username, string password)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Username", username);

            foreach(var user in logIn.items)
            {
                if (user.UserId == username)
                {
                   if (user.Password == password) { return true; }
                }
            }
            return false;               
        }        
    }
}
