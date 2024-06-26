﻿using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using LibraryInfoSystem.Windows;
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

namespace LibraryInfoSystem.Pages
{
    /// <summary>
    /// Interaction logic for AdminVM.xaml
    /// </summary>
    public partial class AdminVM : Page
    {
        public AdminVM()
        {
            InitializeComponent();
            UpdateGamesWrap();
        }
        private void PopulateStack()
        {
            foreach (DataBaseUser baseUser in SessionManager.Users) //Populate Grid with GameDataBase.games
            {
                UserComponent gameComponent = CreateComponent(baseUser);

                GamesWrap.Children.Add(gameComponent);
            }
        }
        private UserComponent CreateComponent(DataBaseUser baseUser)
        {
            UserComponent userComponent = new UserComponent(baseUser);

            userComponent.RemoveUser_Button.Click += RemoveUser; 

            return userComponent;
        }
        private void UpdateGamesWrap()
        {
            GamesWrap.Children.Clear();
            SessionManager.updateHandler();
            PopulateStack();
        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            UpdateGamesWrap();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            CreateNewUser createNewUser = new CreateNewUser();
            createNewUser.Show();
        }


        //Admin Controls
        private void RemoveUser(object sender, RoutedEventArgs e)
        {
            UpdateGamesWrap();
        }
    }
}
