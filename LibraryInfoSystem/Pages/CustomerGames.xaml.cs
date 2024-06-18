using LibraryInfoSystem.Components;
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

namespace LibraryInfoSystem.Pages
{
    /// <summary>
    /// Interaction logic for CustomerGames.xaml
    /// </summary>
    public partial class CustomerGames : Page
    {
        public CustomerGames()
        {
            InitializeComponent();
            build();
        }

        private void build()
        {

            if (SessionManager.ShoppingCart != null) 
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

        /*private GameComponent customerGame(DataBaseItem custGame)
        {
            GameComponent item = new GameComponent();
            item.title = custGame._title;
            item.price = custGame._price;
            item.platform = custGame._platform;

            

            if (custGame._demoimg != null)
            {
                List<ImageSource> convertedlist = new List<ImageSource>();
                foreach (string img in custGame._demoimg)
                {
                    convertedlist.Add(page.convertbitmap(img));
                }

                item.demoImg = convertedlist;
            }

            return item;
        } */
    }
}