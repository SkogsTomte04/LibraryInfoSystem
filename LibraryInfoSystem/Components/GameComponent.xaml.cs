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

namespace LibraryInfoSystem.Components
{
    /// <summary>
    /// Interaction logic for GameComponent.xaml
    /// </summary>
    public partial class GameComponent : UserControl
    {
        public GameComponent()
        {
            InitializeComponent();
        }


        public string? title
        {
            get { return (string)GetValue(titleProperty); }
            set { SetValue(titleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty titleProperty =
            DependencyProperty.Register("title", typeof(string), typeof(GameComponent), new PropertyMetadata(null));



        public double? price
        {
            get { return (double)GetValue(priceProperty); }
            set { SetValue(priceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for price.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty priceProperty =
            DependencyProperty.Register("price", typeof(double), typeof(GameComponent), new PropertyMetadata(null));



        public List<string>? platform
        {
            get { return (List<string>)GetValue(platformProperty); }
            set { SetValue(platformProperty, value); }
        }

        // Using a DependencyProperty as the backing store for platform.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty platformProperty =
            DependencyProperty.Register("platform", typeof(List<string>), typeof(GameComponent), new PropertyMetadata(null));



        






    }
}
