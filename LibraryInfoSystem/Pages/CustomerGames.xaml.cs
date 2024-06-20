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
            _gameCollection = games.GetCollection<DataBaseItem>("games");
            _userCollection = customer.GetCollection<DataBaseUser>("users");
            InitializeComponent();
            build();
        }

        private async void build()
        {
            /*if (SessionManager.ShoppingCart != null)
            {
                foreach (GameComponent custGame in SessionManager.ShoppingCart) //Populate Grid with Shopping Cart Items
                {
                    GameComponent gameComponent = new GameComponent(custGame.dataitem);
                    GamesWrap.Children.Add(gameComponent);
                }
            }

            else
            {
                MessageBox.Show("You currently have no books. Returning to Customer Menu.");
                NavigateToCustomerMenu();
            }*/

           // ObjectId userId = SessionManager.CurrentUser.GetMongoId();
            //var filterUsers = Builders<DataBaseUser>.Filter.Eq("_id", userId);
            //var userDocument = _userCollection.Find(filterUsers).FirstOrDefaultAsync();

            if (SessionManager.CurrentUser != null)
            {
                var games = SessionManager.CurrentUser.Games;
                var gameNames = new List<DataBaseItem>();

                foreach (var game in games)
                {
                    var gameFilter = Builders<DataBaseItem>.Filter.Eq("title", game);
                    var gameDocument = await _gameCollection.Find(gameFilter).FirstOrDefaultAsync();

                    if (gameDocument != null)
                    {
                        gameNames.Add(gameDocument);
                    }
                }

                foreach (DataBaseItem game in gameNames)
                {
                    GameComponent gameComponent = new GameComponent(game);
                    GamesWrap.Children.Add(gameComponent);
                }
            }     
        }


        private void NavigateToCustomerMenu()
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
    }
}