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
using System.Windows.Shapes;

namespace LibraryInfoSystem.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class CreateNewUser : Window
    {
        public CreateNewUser()
        {
            InitializeComponent();
        }

        private async void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = isAdmin.IsChecked == true;
            MessageBox.Show(isChecked.ToString());

            /*var firstNameValue = firstName.Text;
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
            }*/
            //this.Close();
        }

    }
}
