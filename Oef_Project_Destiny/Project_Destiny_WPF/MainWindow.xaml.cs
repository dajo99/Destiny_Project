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
            BtnRegistreren.IsEnabled = true;
            BtnInloggen.IsEnabled = true;
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
            GridMain.Children.Clear();
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Character":
                    usc = new Character();
                    break;
                case "Wapens":
                    usc = new Weapons();
                    break;
                case "Armor":
                    usc = new Armor();
                    break;
                case "SpecialItems":
                    usc = new SpecialItem();
                    break;
                case "Locations":
                    usc = new Locations();  
                    break;

            }
            GridMain.Children.Add(usc);
        }

        private void BtnInloggen_Click(object sender, RoutedEventArgs e)
        {
            
            Inlogscreen inlog = new Inlogscreen();
            inlog.Show();
            BtnRegistreren.IsEnabled = false;
            BtnInloggen.IsEnabled = false;
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
            
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            GridMain.Children.Clear();
            if (item != null && item.IsSelected)
            {
                switch (item.Name)
                {
                    case "Character":
                        usc = new Character();
                        break;
                    case "Wapens":
                        usc = new Weapons();
                        break;
                    case "Armor":
                        usc = new Armor();
                        break;
                    case "SpecialItems":
                        usc = new SpecialItem();
                        break;
                    case "Locations":
                        usc = new Locations();
                        break;
                }
            }
            GridMain.Children.Add(usc);
        }

        private void BtnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            Registerscreen registreer = new Registerscreen();
            registreer.Show();
            BtnRegistreren.IsEnabled = false;
            BtnInloggen.IsEnabled = false;
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new Instellingen();
            GridMain.Children.Add(usc);
        }
    }
}
