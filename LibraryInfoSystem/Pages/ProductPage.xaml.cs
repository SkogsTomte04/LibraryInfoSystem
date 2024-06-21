using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        GameComponent? GameItem;
        public ProductPage()
        {
            
            InitializeComponent();
        }

        public ProductPage(GameComponent obj) : this()
        {
            GameItem = obj;
            this.Loaded += new RoutedEventHandler(ProductPage_Loaded);
        }


        private void Grid_Click(object sender, RoutedEventArgs e)
        {

            ImageButton clickedbutton = sender as ImageButton;
            if (clickedbutton != null)
            {
                Cover_img.Source = clickedbutton.SourceImage;
            }
            else { MessageBox.Show("Null"); }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        { 
            AddToCart(GameItem);
        }

        void ProductPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            LoadImages();

            Title.Text = $"{GameItem.title}";
            price.Text = $"{GameItem.price}";

            platform_stack.Children.Clear();

            foreach(string str in GameItem.platform)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = str;
                platform_stack.Children.Add(textBlock);
            }
        }
        private async void LoadImages()
        {
            await GameItem.GetDemoImages();
            Cover_img.Source = GameItem.image_cover.Source;
            Img1.SourceImage = Cover_img.Source;
            if (GameItem.demoImg != null)
            {
                Img2.SourceImage = GameItem.demoImg[0];
                Img3.SourceImage = GameItem.demoImg[1];
                Img4.SourceImage = GameItem.demoImg[2];
            }
        }

        public static void AddToCart(GameComponent obj) 
        {           
            if (obj == null)
            {
                MessageBox.Show("Game item is null. Cannot add to cart.");
                return;
            }
            
            SessionManager.ShoppingCart.Add(obj);

            MessageBox.Show("Added to Cart.");
        }
    }

}
