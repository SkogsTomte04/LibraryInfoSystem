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
    /// Interaction logic for Registration.xaml
    /// </summary> mongodb+srv://WilliamMoller:Jm7vEC6KYEVl3l6m@cluster0.ivwoew0.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0
    public partial class Registration : Page
    {
        private MongoHandler register = new MongoHandler(DataType.Users);
        private IMongoCollection<DataBaseUser> _usersCollection;
        private List<DataBaseUser> _usersList;

        public Registration()
        {
            InitializeComponent();     
            ConnectToUsers();

        }

        private void ConnectToUsers()
        {
            try
            {
                _usersCollection = register.GetUsersCollection();
                _usersList = register.users;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var firstNameValue = firstName.Text;
            var lastNameValue = lastName.Text;
            var usernameValue = username.Text;
            var passwordValue = password.Text;
            var emailValue = email.Text;
            var phoneNumberValue = phoneNumber.Text;

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

            DataBaseUser newUser = new DataBaseUser(firstNameValue, lastNameValue, usernameValue, passwordValue, emailValue, phoneNumberValue, false);

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

                //if no user exists with the same email
                _usersCollection.InsertOne(newUser);
                MessageBox.Show("User registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while registering the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
