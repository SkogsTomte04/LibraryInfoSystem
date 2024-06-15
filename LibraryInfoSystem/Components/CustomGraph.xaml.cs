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
    /// Interaction logic for CustomGraph.xaml
    /// </summary>
    public partial class CustomGraph : UserControl
    {
        public CustomGraph()
        {
            InitializeComponent();
        }
        protected Color DefaultColor1 = Colors.White, DefaultColor2 = Colors.LightGray;
        private void build()
        {
            //Create row definitions to create a upper bound for the graph <like a ceiling>
            


            for (int i = 0; i < GraphMax; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                MainGrid.RowDefinitions.Add(rowDefinition);
            }
            
            
            int tempcounter = 0;
            bool color = false;
            foreach (RowDefinition row in MainGrid.RowDefinitions)
            {
                
                Rectangle rectangle = new Rectangle();

                switch(color)
                {
                    case true:
                        {
                            rectangle.Fill = new SolidColorBrush(DefaultColor1);
                            color = false;
                            break;
                        }

                    case false:
                        {
                            rectangle.Fill = new SolidColorBrush(DefaultColor2);
                            color = true;
                            break;
                        }
                }
                Grid.SetRow(rectangle, tempcounter);
                MainGrid.Children.Add(rectangle);
                tempcounter++;
            }
            
        }

        public int GraphMax
        {
            get { return (int)GetValue(GraphMaxProperty); }
            set { SetValue(GraphMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GraphMax.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GraphMaxProperty =
            DependencyProperty.Register("GraphMax", typeof(int), typeof(CustomGraph), new PropertyMetadata(0));

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            build();
        }
    }
}
