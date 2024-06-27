using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryInfoSystem.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGame.xaml
    /// </summary>
    public partial class CreateNewGame : Window
    {
        private IMongoCollection<DataBaseItem> _gamesCollection;

        private Uri DropImagePath;
        private Uri DropImagePathAbsolute;

        private Uri coverimage_path;
        private string base64image = "";
        private string title = "";
        private double price = 0;
        private List<string> platforms = new List<string>();
        private List<string> demoimagesbase64 = new List<string>();

        public CreateNewGame()
        {
            InitializeComponent();
            ConnectToGames();
        }

        private void DropImgGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                DropImagePath = new Uri(files[0], UriKind.Relative);
                DropImagePathAbsolute = new Uri(files[0], UriKind.Absolute);

                ImageSource imgSource = new BitmapImage(DropImagePathAbsolute);
                image_drop.Source = imgSource;

               
            }
        }
        private string ImagetoBase64(Uri path)
        {

            byte[] bytes = System.IO.File.ReadAllBytes(path.ToString());
            return Convert.ToBase64String(bytes);
          
        }


        private async void Register_button_Click(object sender, RoutedEventArgs e)
        {
            if(coverimage_path !=  null)
            {
                base64image = ImagetoBase64(coverimage_path);
            }

            title = title_txtbox.Text;
            price = txtboxtoDouble(price_txtbox.Text);
            platforms = getPlatforms();
            demoimagesbase64 = getdemoImages();

            DataBaseItem game = new DataBaseItem(title, price, platforms, base64image, demoimagesbase64);

            await registeruser(game);

        }
        private List<string> getdemoImages()
        {
            List<string> list = new List<string>();
            Uri path;
            foreach(System.Windows.Controls.Image img in demoimage_Stack.Children)
            {
                path = new Uri(img.Tag.ToString(), UriKind.Relative);
                list.Add(ImagetoBase64(path));
            }
            return list;
        }

        private List<string> getPlatforms()
        {
            List<string> list = new List<string>();
            foreach (txtBox platform in Platform_Stack.Children)
            {
                list.Add(platform.Text);
            }
            return list;

        }

        private double txtboxtoDouble(string str)
        {
            double x = 0;

            if (double.TryParse(str, out x)) { }
            else { MessageBox.Show("intconversion Error: CreateNewGame.xaml"); }

            return x;
        }

        private void AddCoverimage_Click(object sender, RoutedEventArgs e)
        {
            coverimage_path = DropImagePath;
            CoverImage_image.Source = new BitmapImage(DropImagePathAbsolute);
        }

        private void AddDemoimage_Click(object sender, RoutedEventArgs e)
        {
            if(demoimage_Stack.Children.Count < 3)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();

                ImageSource imgSource = new BitmapImage(DropImagePathAbsolute);
                image.Source = imgSource;
                image.Tag = DropImagePath.ToString();  //IMAGE NAME IS THE RELATIVE PATH TO ROOT IMAGE STORED LOCALY ON COMPUTER.

                demoimage_Stack.Children.Add(image);
            }
            else
            {
                MessageBox.Show("Only allowed 3 demo images");
            }
            
        }




        /// <summary>
        /// Mongo handler stuff below
        /// </summary>
        private void ConnectToGames()
        {
            try
            {
                _gamesCollection = MongoHandler.GetCollection<DataBaseItem>("games");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async Task registeruser(DataBaseItem game)
        {
            await Task.Run(() =>
            {

                try
                {
                    //define filter 
                    var filter = Builders<DataBaseItem>.Filter.Eq(r => r._title, game._title);

                    //check if a user with the same email already exists
                    var existing = _gamesCollection.Find(filter).FirstOrDefault();

                    if (existing != null)
                    {
                        MessageBox.Show("A game with this title already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    //if no user exists with the same email
                    _gamesCollection.InsertOne(game);

                    var confirmFilter = Builders<DataBaseItem>.Filter.Eq(x => x._title, game._title);

                    var confirm = _gamesCollection.Find(confirmFilter).FirstOrDefault();

                    //confirm whether the userId has been injected into the collection or not.
                    if (confirm != null)
                    {
                        MessageBox.Show("User registered successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to upload to the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while registering the user: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            });
        }
    }
}
