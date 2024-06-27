using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for Registration.xaml
    /// </summary> mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0
    public partial class Registration : Page
    {
        private IMongoCollection<DataBaseUser>? _usersCollection;

        public Registration()
        {
            _usersCollection = MongoHandler.GetCollection<DataBaseUser>("users");
            InitializeComponent();     
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var mailDress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstNameValue = firstName.Text;
            var lastNameValue = lastName.Text;
            var usernameValue = username.Text;
            var passwordValue = password.Text;
            var emailValue = email.Text;
            var phoneNumberValue = phoneNumber.Text;
            var arrayValue = new List<string>();

            //make sure all fields are filled in
            if (string.IsNullOrEmpty(firstNameValue) ||
                string.IsNullOrEmpty(lastNameValue) ||
                string.IsNullOrEmpty(usernameValue) ||
                string.IsNullOrEmpty(passwordValue) ||
                string.IsNullOrEmpty(emailValue) ||
                string.IsNullOrEmpty(phoneNumberValue))
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsValidEmail(emailValue) == false)
            {
                MessageBox.Show("Your email is invalid. Please try again.");
                return;
            } 
            

            DataBaseUser newUser = new DataBaseUser(firstNameValue, lastNameValue, usernameValue, passwordValue, emailValue, phoneNumberValue, false, arrayValue);

            try
            {
                //define filter 
                var filter = Builders<DataBaseUser>.Filter.Eq(r => r.Email, newUser.Email) | Builders<DataBaseUser>.Filter.Eq(r => r.UserId, newUser.UserId);

                //check if a user with the same email already exists
                var existing = _usersCollection.Find(filter).FirstOrDefault();

                if (existing != null)
                {
                    MessageBox.Show("A user with this email and/or username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //insert new user
                _usersCollection.InsertOne(newUser);

                var confirmFilter = Builders<DataBaseUser>.Filter.Eq(x => x.Email, newUser.Email);

                var confirm = _usersCollection.Find(confirmFilter).FirstOrDefault();

                //confirm whether the userId has been injected into the collection or not.
                if (confirm != null)
                {
                    MessageBox.Show("User registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }                
                else
                {
                    MessageBox.Show("Failed to upload to the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while registering the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
