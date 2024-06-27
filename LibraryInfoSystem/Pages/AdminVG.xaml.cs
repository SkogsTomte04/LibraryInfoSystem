using LibraryInfoSystem.Components;
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
    /// Interaction logic for AdminVB.xaml
    /// </summary>
    public partial class AdminVG : Page
    {
        public AdminVG()
        {
            InitializeComponent();
            UpdateGamesWrap();
        }
        private void PopulateStack()
        {
            foreach (DataBaseItem baseGame in SessionManager.Items) //Populate Grid with GameDataBase.games
            {
                GameComponent gameComponent = CreateComponent(baseGame);

                GamesWrap.Children.Add(gameComponent);
            }
        }
        private GameComponent CreateComponent(DataBaseItem baseItem)
        {
            GameComponent gameComponent = new GameComponent(baseItem);
            gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditGame_Click));
            
            return gameComponent;
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

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGame createNewGame = new CreateNewGame();
            createNewGame.Show();
        }
        private void EditGame_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
