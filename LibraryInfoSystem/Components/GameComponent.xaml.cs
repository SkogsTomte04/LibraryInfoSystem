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

        public double? price
        {
            get { return (double)GetValue(priceProperty); }
            set { SetValue(priceProperty, value); }
        }

        public List<string>? platform
        {
            get { return (List<string>)GetValue(platformProperty); }
            set { SetValue(platformProperty, value); }
        }



        public static readonly DependencyProperty titleProperty = DependencyProperty.Register("title", typeof(string), typeof(GameComponent), new PropertyMetadata(null));
        public static readonly DependencyProperty priceProperty = DependencyProperty.Register("price", typeof(double), typeof(GameComponent), new PropertyMetadata(null));
        public static readonly DependencyProperty platformProperty = DependencyProperty.Register("platform", typeof(List<string>), typeof(GameComponent), new PropertyMetadata(null));

    }
}
