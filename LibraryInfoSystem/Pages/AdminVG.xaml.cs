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
    /// Interaction logic for AdminVB.xaml
    /// </summary>
    public partial class AdminVG : Page
    {
        private MongoHandler mongohandler = new MongoHandler(DataType.Games);
        private MongoHandler admin = new MongoHandler(DataType.Users);
        public AdminVG()
        {
            InitializeComponent();
            build();
        }

        public void build()
        {
            foreach (DataBaseItem baseItem in mongohandler.items)
            {
                GameComponent gameComponent = new GameComponent();
                gameComponent.title = baseItem._title;
                gameComponent.price = baseItem._price;
                gameComponent.platform = baseItem._platform;

                if (string.IsNullOrWhiteSpace(baseItem._image) == false)
                {
                    BitmapSource convertedImage = mongohandler.BitmapFromBase64(baseItem._image);
                    gameComponent.image_cover.Source = convertedImage;
                }
                else { MessageBox.Show("Bitmapconversion error"); }

                GamesStack.Children.Add(gameComponent);
            }
        }

       /* private void CoolButton_Click(object sender, RoutedEventArgs e)
        {
            // CoolButton Clicked! Let's show our InputBox.
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // YesButton Clicked! Let's hide our InputBox and handle the input text.
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            // Do something with the Input
            String input = InputTextBox.Text;
            MyListBox.Items.Add(input); // Add Input to our ListBox.

            // Clear InputBox.
            InputTextBox.Text = String.Empty;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked! Let's hide our InputBox.
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            // Clear InputBox.
            InputTextBox.Text = String.Empty;
        } */

    }
}
