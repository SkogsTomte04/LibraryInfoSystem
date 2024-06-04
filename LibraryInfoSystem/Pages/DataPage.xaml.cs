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

            mongohandler.ConnectToGames();
            foreach (DataBaseItem baseItem in mongohandler.items) //Populate Grid with GameDataBase.games
            {
                

                GameComponent gameComponent = new GameComponent();
                gameComponent.title = baseItem._title;
                gameComponent.price = baseItem._price;
                gameComponent.platform = baseItem._platform;

                gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(Game_Click));

                if (string.IsNullOrWhiteSpace(baseItem._image) == false) //image conversion
                {
                    BitmapSource convertedImage = mongohandler.BitmapFromBase64(baseItem._image);
                    gameComponent.image_cover.Source = convertedImage;
                }
                else { MessageBox.Show("Bitmapconversion error"); }


                
                if(columncount > 4) { GamesStack.RowDefinitions.Add(new RowDefinition()); rowcount++; columncount = 0;} //Creates a new row for every 4th list item

                GamesStack.Children.Add(gameComponent);
                Grid.SetColumn(gameComponent, columncount);
                Grid.SetRow(gameComponent, rowcount);
                
                columncount++;
            }
        }


        private MongoHandler mongohandler = new MongoHandler();

        private void Game_Click(object sender, RoutedEventArgs e)
        {
            GameComponent ClickedButton = sender as GameComponent; // i need a way to identify what button has been clicked

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
