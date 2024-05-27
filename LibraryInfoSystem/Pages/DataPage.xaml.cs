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
    /// Interaction logic for DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        public DataPage()
        {

            InitializeComponent();
            build();
        }
        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {

        }
        public void build()
        {
            mongohandler.ConnectToGames();
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
        private MongoHandler mongohandler = new MongoHandler();
    }
}
