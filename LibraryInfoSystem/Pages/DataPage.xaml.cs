using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        }
        public void build()
        {
            foreach (DataBaseItem baseItem in mongohandler.items) //Populate Grid with GameDataBase.games
            {
                GameComponent gameComponent = createcomponent(baseItem);

                GamesWrap.Children.Add(gameComponent);
            }

        }

        private GameComponent createcomponent(DataBaseItem baseItem)
        {
            
            GameComponent gameComponent = new GameComponent(baseItem);
            gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(Game_Click));
            return gameComponent;
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

        private void Grid_Initialized(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            build();
        }
    }
}
