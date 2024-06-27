using MongoDB.Bson.Serialization.Attributes;
﻿using LibraryInfoSystem.Tools;
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
using MongoDB.Bson;

namespace LibraryInfoSystem.Components
{
    /// <summary>
    /// Interaction logic for GameComponent.xaml
    /// </summary>
    public partial class GameComponent : UserControl
    {
        public DataBaseItem dataitem;
        public GameComponent(DataBaseItem item)
        {
            InitializeComponent();
            dataitem = item;
        }

        public async Task build()
        {
            title = dataitem._title;
            price = dataitem._price;
            platform= dataitem._platform;

            if(dataitem._image != null)
            {
                image_cover.Source = await Task.Run(() => MongoHandler.convertbitmap(dataitem._image));

            }
            
            //demoImg = await Task.Run(() => convertlist()); slowing down loading

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
            set { SetValue(platformProperty, value); FillStack(value); } //FillStack() is called here to pass value: List<String> = { Xbox, Pc, Playstation}
        }

        public void FillStack(List<string> platform_list)
        {
            foreach (string p in platform_list)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = p;
                textBlock.Style = Resources["Textstyle2"] as Style;
                platform_stack.Children.Add(textBlock);
            }
        }

        [BsonIgnore] public string PlatformAsString
        {
            get
            {
                return string.Join(", ", platform);
            }
            set { }
        }

        public async Task GetDemoImages()
        {
            List<ImageSource> convertedlist = new List<ImageSource>();
            List<Task<ImageSource>> tasks = new List<Task<ImageSource>>();

            if (dataitem._demoimg != null)
            {

                foreach (string img in dataitem._demoimg)
                {
                    tasks.Add(Task.Run(() => MongoHandler.convertbitmap(img)));

                    //ImageSource image = mongoHandler.convertbitmap(img);

                    //convertedlist.Add(image);
                }

                var results = await Task.WhenAll(tasks);

                foreach(var result in results)
                {
                    convertedlist.Add(result);
                }
                demoImg = convertedlist;
            }
            return;
            
        }


        public List<ImageSource> demoImg
        {
            get { return (List<ImageSource>)GetValue(demoImgProperty); }
            set { SetValue(demoImgProperty, value); }
        }


        // Using a DependencyProperty as the backing store for demoImg.  This enables animation, styling, binding, etc...

        public static readonly DependencyProperty titleProperty = DependencyProperty.Register("title", typeof(string), typeof(GameComponent), new PropertyMetadata(null));
        public static readonly DependencyProperty priceProperty = DependencyProperty.Register("price", typeof(double), typeof(GameComponent), new PropertyMetadata(null));
        public static readonly DependencyProperty platformProperty = DependencyProperty.Register("platform", typeof(List<string>), typeof(GameComponent), new PropertyMetadata(null));
        public static readonly DependencyProperty demoImgProperty = DependencyProperty.Register("demoImg", typeof(List<ImageSource>), typeof(GameComponent), new PropertyMetadata(null));

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            await build();
        }
    }
}
