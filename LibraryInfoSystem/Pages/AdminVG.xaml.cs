using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using LibraryInfoSystem.Windows;
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
using System.Windows.Markup;
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
        public AdminVG()
        {
            InitializeComponent();
            UpdateGamesWrap();
        }
        private void PopulateStack()
        {
            foreach (DataBaseItem baseGame in mongohandler.items) //Populate Grid with GameDataBase.games
            {
                GameComponent gameComponent = CreateComponent(baseGame);

                GamesWrap.Children.Add(gameComponent);
            }
        }
        private async Task buildparalelasync()
        {
            List<Task> tasks = new List<Task>();
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            foreach (DataBaseItem baseItem in mongohandler.items) //Populate Grid with GameDataBase.games
            {
                GameComponent gameComponent = CreateComponent(baseItem);
                tasks.Add(Task.Run(() => AddComponentParalelAsync(gameComponent)));
            }
            await Task.WhenAll(tasks);

            watch.Stop();
            MessageBox.Show($"elapsed time: {watch.ElapsedMilliseconds / 1000} Seconds");

        }
        public async Task AddComponentParalelAsync(GameComponent component)
        {
            await Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    component.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditGame_Click));
                    GamesWrap.Children.Add(component);

                });
            });


        }
        private GameComponent CreateComponent(DataBaseItem baseItem)
        {
            GameComponent gameComponent = new GameComponent();
            gameComponent.title = baseItem._title;
            gameComponent.price = baseItem._price;
            gameComponent.platform = baseItem._platform;

            gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditGame_Click));
            gameComponent.image_cover.Source = mongohandler.convertbitmap(baseItem._image);

            if (baseItem._demoimg != null)
            {
                List<ImageSource> convertedlist = new List<ImageSource>();
                foreach (string img in baseItem._demoimg)
                {
                    convertedlist.Add(mongohandler.convertbitmap(img));
                }

                gameComponent.demoImg = convertedlist;
<<<<<<< Updated upstream
            }
=======
            }*/
            gameComponent.AddHandler(Button.ClickEvent, new RoutedEventHandler(EditGame_Click));

>>>>>>> Stashed changes
            return gameComponent;
        }
        private async void UpdateGamesWrap()
        {
            GamesWrap.Children.Clear();
            mongohandler.UpdateDataBase();
            //PopulateStack();
            await buildparalelasync();
        }

        private void UpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            UpdateGamesWrap();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            CreateNewUser createNewUser = new CreateNewUser();
            createNewUser.Show();
        }


        //Admin Controls

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            CreateNewGame createNewGame = new CreateNewGame();
            createNewGame.Show();
        }
        private void EditGame_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
