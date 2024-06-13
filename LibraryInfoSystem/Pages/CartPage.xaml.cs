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
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            DisplayCart();
        }

        private void DisplayCart()
        {
            if (ProductPage.ShoppingCart != null && ProductPage.ShoppingCart.Any())
            {
                ItemsContainer.Children.Clear(); // Clear any existing items

                foreach (var item in ProductPage.ShoppingCart)
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

                double totalPrice = (double)ProductPage.ShoppingCart.Sum(item => item.price);
                TotalPriceTextBlock.Text = $"Total Price: {totalPrice:C}";
            }
            else
            {
                ItemsContainer.Children.Clear();
                TotalPriceTextBlock.Text = "Total Price: $0.00";
                MessageBox.Show("Shopping cart is empty.");
            }
        }


    }
}
