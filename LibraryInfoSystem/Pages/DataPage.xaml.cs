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
        private MongoHandler mongohandler = new MongoHandler(DataType.Games);

        public DataPage()
        {

            InitializeComponent();
        }
        private async Task buildasync()
        {
            List<GameComponent> dataBaseContainer = new List<GameComponent>();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int i = 0, maxwidth = 3;
            foreach (DataBaseItem baseItem in mongohandler.items) //Populate Grid with GameDataBase.games
            {
                i++;
                GameComponent gameComponent = createcomponent(baseItem);
                
                dataBaseContainer.Add(gameComponent);
                if (i == maxwidth)
                {
                    i = 0;
                    await addComponentsasync(dataBaseContainer);
                    dataBaseContainer.Clear();
                }
                
            }

            watch.Stop();
            MessageBox.Show($"elapsed time: {watch.ElapsedMilliseconds / 1000} Seconds");

        }
        public async Task addComponentsasync(List<GameComponent> list)
        {
            
            await Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    foreach (GameComponent gameComponent in list)
                    {
                        GamesWrap.Children.Add(gameComponent);
                    }
                    
                });
            });
             
        }

        private GameComponent createcomponent(DataBaseItem baseItem)
        {
            GameComponent gameComponent = new GameComponent(baseItem);
            gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(Game_Click));
            return gameComponent;

        }
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

        private async void Grid_Loaded(object sender, EventArgs e)
        {
            await buildasync();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            GamesWrap.Children.Clear();
            await buildasync();
        }
    }
}
