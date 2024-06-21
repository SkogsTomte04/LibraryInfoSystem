using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public void build()
        {
            int columncount = 0;
            int rowcount = 0;

            foreach (DataBaseItem baseItem in mongohandler.items) //Populate Grid with GameDataBase.games
            {


                GameComponent gameComponent = createcomponent(baseItem);

                GamesWrap.Children.Add(gameComponent);

                columncount++;
            }
        }

        private GameComponent createcomponent(DataBaseItem baseItem)
        {
            GameComponent gameComponent = new GameComponent();
            gameComponent.title = baseItem._title;
            gameComponent.price = baseItem._price;
            gameComponent.platform = baseItem._platform;

            gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(Game_Click));
            gameComponent.image_cover.Source = convertbitmap(baseItem._image);

            if (baseItem._demoimg != null)
            {
                List<ImageSource> convertedlist = new List<ImageSource>();
                foreach (string img in baseItem._demoimg)
                {
                    convertedlist.Add(convertbitmap(img));
                }

                gameComponent.demoImg = convertedlist;
            }
            

            return gameComponent;
        }

        private ImageSource convertbitmap(string bit)
        {
            if (string.IsNullOrWhiteSpace(bit) == false) //image conversion
            {
                BitmapSource convertedImage = mongohandler.BitmapFromBase64(bit);
                return convertedImage;
            }
            else { MessageBox.Show("Bitmapconversion error"); return null; }
        }

        private MongoHandler mongohandler = new MongoHandler(DataType.Games);

        private void Game_Click(object sender, RoutedEventArgs e)
        {
            GameComponent ClickedButton = sender as GameComponent;

            if(ClickedButton != null)
            {
                ProductPage page = new ProductPage(ClickedButton);
                this.NavigationService.Navigate(page);
            }
            else
            {
                MessageBox.Show("Null");
            }

        }

    }
}
