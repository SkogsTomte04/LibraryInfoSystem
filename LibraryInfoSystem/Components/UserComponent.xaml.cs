using LibraryInfoSystem.Tools;
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

namespace LibraryInfoSystem.Components
{
    /// <summary>
    /// Interaction logic for UserComponent.xaml
    /// </summary>
    public partial class UserComponent : UserControl
    {
        public DataBaseUser User;
        public UserComponent(DataBaseUser user)
        {
            InitializeComponent();
            User = user;
            build();
        }
        private void build()
        {
            firstname = User.FirstName;
            lastname = User.LastName;
            userid = User.UserId;
            email = User.Email;
            password = User.Password;
            phonenumber = User.PhoneNumber;
            isadmin = User.IsAdmin;
        }


        public string firstname
        {
            get { return (string)GetValue(firstnameProperty); }
            set { SetValue(firstnameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for firstname.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty firstnameProperty =
            DependencyProperty.Register("firstname", typeof(string), typeof(UserComponent), new PropertyMetadata(""));
        public string lastname
        {
            get { return (string)GetValue(lastnameProperty); }
            set { SetValue(lastnameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for firstname.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty lastnameProperty =
            DependencyProperty.Register("lastname", typeof(string), typeof(UserComponent), new PropertyMetadata(""));



        public string userid
        {
            get { return (string)GetValue(useridProperty); }
            set { SetValue(useridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for userid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty useridProperty =
            DependencyProperty.Register("userid", typeof(string), typeof(UserComponent), new PropertyMetadata(""));



        public string password
        {
            get { return (string)GetValue(passwordProperty); }
            set { SetValue(passwordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty passwordProperty =
            DependencyProperty.Register("password", typeof(string), typeof(UserComponent), new PropertyMetadata(""));



        public string email
        {
            get { return (string)GetValue(emailProperty); }
            set { SetValue(emailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for email.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty emailProperty =
            DependencyProperty.Register("email", typeof(string), typeof(UserComponent), new PropertyMetadata(""));



        public string phonenumber
        {
            get { return (string)GetValue(phonenumberProperty); }
            set { SetValue(phonenumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for phonenumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty phonenumberProperty =
            DependencyProperty.Register("phonenumber", typeof(string), typeof(UserComponent), new PropertyMetadata(""));



        public bool? isadmin
        {
            get { return (bool?)GetValue(isadminProperty); }
            set { SetValue(isadminProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isadmin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isadminProperty =
            DependencyProperty.Register("isadmin", typeof(bool?), typeof(UserComponent), new PropertyMetadata(false));

        private void EditUser_Button_Click(object sender, RoutedEventArgs e)
        {
            if(EditUser_Stack.Visibility == Visibility.Collapsed)
            {
                EditUser_Stack.Visibility = Visibility.Visible;
                UserInfo_Stack.Visibility = Visibility.Collapsed;
                ButtonControls_Grid.Visibility = Visibility.Visible;
            } else if(UserInfo_Stack.Visibility == Visibility.Collapsed)
            {
                UserInfo_Stack.Visibility = Visibility.Visible;
                EditUser_Stack.Visibility = Visibility.Collapsed;
                ButtonControls_Grid.Visibility = Visibility.Collapsed;

                build();
            }
        }

        private void RemoveUser_Button_Click(object sender, RoutedEventArgs e)
        {
            MongoHandler.RemoveUser(User);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            User.FirstName = fn.Text; User.LastName = ln.Text; User.Email = em.Text; User.UserId = ui.Text; User.Password = pas.Text; User.PhoneNumber = ph.Text; User.IsAdmin = ad.IsChecked;
            MongoHandler.EditUser(User);
            build();
        }

    }
}
