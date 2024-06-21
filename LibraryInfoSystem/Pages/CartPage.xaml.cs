using DnsClient;
using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using Microsoft.Win32;
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
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        private MongoHandler customer = new MongoHandler(DataType.Users);
        private MongoHandler customerDue = new MongoHandler(DataType.Duedate);
        private IMongoCollection<DataBaseUser> _userCollection;
        private IMongoCollection<DataBaseItem>? _gameCollection;
        private IMongoCollection<DataBaseDuedate> _duedateCollection;

        public CartPage()
        {
            _userCollection = customer.GetCollection<DataBaseUser>("users");
            _gameCollection = customer.GetCollection<DataBaseItem>("games");
            _duedateCollection = customer.GetCollection<DataBaseDuedate>("duedate_games");
            InitializeComponent();
            DisplayCart();
        }

        private void DisplayCart()
        {
            if (SessionManager.ShoppingCart != null && SessionManager.ShoppingCart.Any())
            {
                ItemsContainer.Children.Clear(); // Clear any existing items

                foreach (var item in SessionManager.ShoppingCart)
                {
                    var itemPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(0, 0, 0, 5),
                    };

                    var titleTextBlock = new TextBlock
                    {
                        Text = item.title,
                        FontWeight = FontWeights.Bold,
                        Width = 200,
                        Margin = new Thickness(5)
                    };

                    var platformsTextBlock = new TextBlock
                    {
                        Text = item.PlatformAsString,
                        Width = 300,
                        Margin = new Thickness(5)
                    };

                    var priceTextBlock = new TextBlock
                    {
                        Text = $"{item.price:C}",
                        Width = 100,
                        Margin = new Thickness(5)
                    };

                    itemPanel.Children.Add(titleTextBlock);
                    itemPanel.Children.Add(platformsTextBlock);
                    itemPanel.Children.Add(priceTextBlock);

                    ItemsContainer.Children.Add(itemPanel);
                }

                double totalPrice = (double)SessionManager.ShoppingCart.Sum(item => item.price);
                TotalPriceTextBlock.Text = $"Total Price: {totalPrice:C}";
            }
            else
            {
                ItemsContainer.Children.Clear();
                TotalPriceTextBlock.Text = "Total Price: $0.00";
                MessageBox.Show("Shopping cart is empty.");
            }
        }

        private async void CustGamesBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (GameComponent item in SessionManager.ShoppingCart)
            {

                string gameName = item.dataitem._title;

                var filterGames = Builders<DataBaseItem>.Filter.Eq("title", gameName);
                var gamesDocument = await _gameCollection.Find(filterGames).FirstOrDefaultAsync();

                ObjectId userId = SessionManager.CurrentUser.GetMongoId();
                var filterUsers = Builders<DataBaseUser>.Filter.Eq("_id", userId);
                var userDocument = await _userCollection.Find(filterUsers).FirstOrDefaultAsync();
                var update = Builders<DataBaseUser>.Update.Push("games", gameName);

                var updateResult = await _userCollection.UpdateOneAsync(filterUsers, update);

                if (updateResult.ModifiedCount > 0)
                {

                }
                else
                {
                    Console.WriteLine("Error.");
                }

                string name = SessionManager.CurrentUser.UserId;
                var filterDue = Builders<DataBaseDuedate>.Filter.Eq("UserId", name);
                var dueAccount = _duedateCollection.Find(filterDue).FirstOrDefault();

                DateTime bookedDateTime = DateTime.Today;
                string bookedDate = bookedDateTime.ToString("dd-MM-yyyy");
                DateTime dueDateTime = bookedDateTime.AddDays(30);
                string dueDate = dueDateTime.ToString("dd-MM-yyyy");

                if (dueAccount != null)
                {
                    var updateDue = Builders<DataBaseDuedate>.Update.Push("title", gameName);
                }

                if (dueAccount == null)
                {
                    List<string> gameTitle = new List<string>();
                    gameTitle.Add(gameName);
                    var uId = name;
                    var booked = bookedDate;
                    var deadline = dueDate;
                    bool isAdmin = false;

                    DataBaseDuedate newCustomerDue = new DataBaseDuedate(gameTitle, uId, booked, deadline, isAdmin);
                    _duedateCollection.InsertOne(newCustomerDue);
                }


            }

            MessageBox.Show("Your booking has been confirmed. You have 30 days to return the game/games. Taking you to the 'View Your Games' section.");
            SessionManager.ShoppingCart.Clear();
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);

        }
    }
}
