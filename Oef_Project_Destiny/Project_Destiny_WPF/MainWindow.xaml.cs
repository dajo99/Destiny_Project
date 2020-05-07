using Project_Destiny_WPF.UserControls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool StateClosed = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        UserControl usc = null;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new Welkom();
            GridMain.Children.Add(usc);
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        public void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Wapens":
                    GridMain.Children.Clear();
                    usc = new Weapons();
                    GridMain.Children.Add(usc);
                    break;
                case "Character":

                    GridMain.Children.Clear();
                    usc = new Character(this);
                    GridMain.Children.Add(usc);
                    break;

                case "Locations":
                    GridMain.Children.Clear();
                    usc = new Locations();
                    GridMain.Children.Add(usc);
                    break;

            }
        }

        private void BtnInloggen_Click(object sender, RoutedEventArgs e)
        {
            Inlogscreen inlog = new Inlogscreen();
            inlog.Show();
        }

        private void BtnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Ben je zeker dat je wilt afsluiten?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }           
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new Instellingen();
            GridMain.Children.Add(usc);
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                switch (item.Name)
                {
                    case "Wapens":
                        GridMain.Children.Clear();
                        usc = new Weapons();
                        GridMain.Children.Add(usc);
                        break;
                    case "Character":

                        GridMain.Children.Clear();
                        usc = new Character(this);
                        GridMain.Children.Add(usc);
                        break;

                    case "Locations":
                        GridMain.Children.Clear();
                        usc = new Locations();
                        GridMain.Children.Add(usc);
                        break;
                }
            }
        }
    }
}
