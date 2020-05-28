using Destiny_Models;
using Project_Destiny_WPF.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        UserControl usc = null;
        bool StateClosed = true;

        private void mouse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Zorgen dat window verplaatst kan worden
            OnMouseLeftButtonDown(e);
            this.DragMove(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Nieuw scherm oproepen
            GridMain.Children.Clear();
            usc = new WelcomeControl();
            GridMain.Children.Add(usc);

            BtnRegistreren.IsEnabled = true;
            BtnInloggen.IsEnabled = true;
            ListViewMenu.IsEnabled = false;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            //menu vergroten
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            //Menu verkleinen
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        public void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Alle displays wissen
            GridMain.Children.Clear();

            //Kijken welke selectie is gemaakt in listviewmenu en scherm oproepen
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Character":
                    usc = new CharacterControl();
                    break;
                case "Wapens":
                    usc = new WeaponsControl();
                    break;
                case "Armor":
                    usc = new ArmorControl();
                    break;
                case "SpecialItems":
                    usc = new SpecialItemControl();
                    break;
                case "Locations":
                    usc = new LocationsUserControl();  
                    break;

            }
            GridMain.Children.Add(usc);
        }

        private void BtnInloggen_Click(object sender, RoutedEventArgs e)
        {
            
            Window inlog = new LogInWindow();
            inlog.Show();
            DisablingButtons();
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
            if (User.Acc == null)
            {
                usc = new WelcomeControl();
            }
            else
            {
                usc = new LoggedInControl();
            }
            
            GridMain.Children.Add(usc);
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            GridMain.Children.Clear();
            if (item != null && item.IsSelected) 
            {
                switch (item.Name) //Kijken welke selectie is gemaakt in listviewmenu en scherm oproepen
                {
                    case "Character":
                        usc = new CharacterControl();
                        break;
                    case "Wapens":
                        usc = new WeaponsControl();
                        break;
                    case "Armor":
                        usc = new ArmorControl();
                        break;
                    case "SpecialItems":
                        usc = new UserControls.SpecialItemControl();
                        break;
                    case "Locations":
                        usc = new LocationsUserControl();
                        break;
                }
            }
            GridMain.Children.Add(usc);
        }

        private void BtnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            Window registreer = new RegisterWindow();
            registreer.Show();
            DisablingButtons();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new SettingsControl();
            GridMain.Children.Add(usc);
        }

        private void DisablingButtons()
        {
            BtnRegistreren.IsEnabled = false;
            BtnInloggen.IsEnabled = false;
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();
            usc = new AccountControl();
            GridMain.Children.Add(usc);
        }

        private void btnAfmelden_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            //User en profielfoto leegmaken
            User.Acc = null;
            ProfileImage.Source = null;

            //knoppen enablen
            BtnRegistreren.IsEnabled = true;
            BtnInloggen.IsEnabled = true;
            ListViewMenu.IsEnabled = false;
            Accountpanel.Visibility = Visibility.Hidden;
            Loginpanel.Visibility = Visibility.Visible;

            //menu terug aanpassen
            GridNav.Background = (Brush)bc.ConvertFrom("#FF00C7A3");
            GridMenu.Background = (Brush)bc.ConvertFrom("#FF00C7A3");
            App.Current.Resources["font"] = new FontFamily("Segoe UI");
            Uri uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");

            //Nieuw scherm oproepen
            GridMain.Children.Clear();
            usc = new WelcomeControl();
            GridMain.Children.Add(usc);
        }

       
    }
}
