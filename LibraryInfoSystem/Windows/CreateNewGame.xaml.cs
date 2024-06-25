using LibraryInfoSystem.Components;
using LibraryInfoSystem.Tools;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;

namespace LibraryInfoSystem.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGame.xaml
    /// </summary>
    public partial class CreateNewGame : Window
    {
        private IMongoCollection<DataBaseItem> mongoCollection;
        public CreateNewGame()
        {
            InitializeComponent();
        }

        private void DropImgGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                Uri uri = new Uri(files[0], UriKind.Absolute);
                ImageSource imgSource = new BitmapImage(uri);
                image_drop.Source = imgSource;
               
            }
        }
        private double GetIfInt(txtBox Box)
        {
            double value = 0;
            if (double.TryParse(Box.Text, out value))
            {
                return value;
            }
            else
            {
                MessageBox.Show($"Failed Parcing of \ntxtBox: {Box.Name} \nvalue: {value}");
                return 0;
            }
        }

        private async void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var userId = new ObjectId();
            var title = Title_txtB.Text;
            var price = GetIfInt(Price_txtB);
            var arrayValue = new List<string>();
            var image_cover = imagetobase64(image_drop);
            var arrayImages = new List<string>();

            if (string.IsNullOrEmpty(title) ||
                price == null ||
                string.IsNullOrEmpty(image_cover))
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataBaseItem newItem = new DataBaseItem(title, price, arrayValue, image_cover, arrayImages);

            try
            {
                //define filter 
                var filter = Builders<DataBaseItem>.Filter.Eq(r => r._title, newItem._title);

                //check if a user with the same email already exists
                var existing = mongoCollection.Find(filter).FirstOrDefault();

                if (existing != null)
                {
                    MessageBox.Show("A user with this email and/or username already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //if no user exists with the same email
                mongoCollection.InsertOne(newItem);

                var confirmFilter = Builders<DataBaseItem>.Filter.Eq(x => x._title, newItem._title);

                var confirm = mongoCollection.Find(confirmFilter).FirstOrDefault();

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

        }
        public static byte[] converterDemo(Image x)
        {
            ImageSourceConverter converter = new ImageSourceConverter();
            byte[] xByte = (byte[])converter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

        private string imagetobase64(Image image)
        {
            byte[] bytes = converterDemo(image);
            string base64string = Convert.ToBase64String(bytes);
            return base64string;
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(imagetobase64(image_drop));
        }
    }
}
