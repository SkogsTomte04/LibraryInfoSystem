using LibraryInfoSystem.Components;
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

namespace LibraryInfoSystem.Pages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        GameComponent? ObjectValue;
        public ProductPage()
        {
            InitializeComponent();
        }
        public ProductPage(GameComponent obj) : this()
        {
            ObjectValue = obj;
            this.Loaded += new RoutedEventHandler(ProductPage_Loaded);
            

        }
        void ProductPage_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text = $"Value passed from page1 is: {ObjectValue.price}";
        }
        private void Build()
        {
            
        }
    }
}
