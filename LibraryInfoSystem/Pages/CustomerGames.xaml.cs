using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using MongoDB.Bson;
using MongoDB.Driver;
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
    /// Interaction logic for CustomerGames.xaml
    /// </summary>
    public partial class CustomerGames : Page
    {
        private MongoHandler customer = new MongoHandler(DataType.Users);
        private MongoHandler games = new MongoHandler(DataType.Games);
        private IMongoCollection<DataBaseUser> _userCollection;
        private IMongoCollection<DataBaseItem> _gameCollection;
        public CustomerGames()
        {
            customer.UpdateDataBase();
            _gameCollection = games.GetCollection<DataBaseItem>("games");
            _userCollection = customer.GetCollection<DataBaseUser>("users");
            InitializeComponent();
        }

        private void build()
        {
            var user = SessionManager.CurrentUser.UserId; // currentuser in mongohandler is null
            if (user != null)
            {
                var games = SessionManager.CurrentUser.Games;
                var gameNames = new List<DataBaseItem>();

                foreach (var game in games)
                {
                    var gameFilter = Builders<DataBaseItem>.Filter.Eq("title", game);
                    var gameDocument = _gameCollection.Find(gameFilter).FirstOrDefault(); ;

                    if (gameDocument != null)
                    {
                        gameNames.Add(gameDocument);
                        MessageBox.Show(gameDocument._title);
                    }
                }

                foreach (DataBaseItem game in gameNames)
                {
                    GameComponent gameComponent = new GameComponent(game);
                    GamesWrap.Children.Add(gameComponent);
                }
            }


        }    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new CustomerMenu());
            }
            else
            {
                MessageBox.Show("NavigationService is null. Cannot navigate to Customer Menu.");
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            build();
        }
    }
}