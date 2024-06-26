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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryInfoSystem.Windows
{
    /// <summary>
    /// Interaction logic for CreateNewGame.xaml
    /// </summary>
    public partial class CreateNewGame : Window
    {
        private Uri ImagePath;

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
                ImagePath = uri;
                ImageSource imgSource = new BitmapImage(uri);
                image_drop.Source = imgSource;

                MessageBox.Show(ImagePath.ToString());
               
            }
        }



    }
}
