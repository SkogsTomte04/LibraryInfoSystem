using LibraryInfoSystem.Components;
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
        private Uri DropImagePath;
        private Uri DropImagePathAbsolute;

        private Uri coverimage_path;
        private string base64image = "";
        private string title = "";
        private double price = 0;
        private List<string> platforms = new List<string>();

        public CreateNewGame()
        {
            InitializeComponent();
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


        private void Register_button_Click(object sender, RoutedEventArgs e)
        {
            base64image = ImagetoBase64(coverimage_path);

            title = title_txtbox.Text;
            price = txtboxtoDouble(price_txtbox.Text);
            platforms = getPlatforms();

            MessageBox.Show($"Image Path: {DropImagePath}\nTitle: {title}\nPrice: ${price}\nPlatform 1: {platforms[0]}");


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
            if(demoimage_Stack.Children.Count < 4)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();

                ImageSource imgSource = new BitmapImage(DropImagePathAbsolute);
                image.Source = imgSource;

                demoimage_Stack.Children.Add(image);
            }
            else
            {
                MessageBox.Show("Only allowed 3 demo images");
            }
            
        }
    }
}
