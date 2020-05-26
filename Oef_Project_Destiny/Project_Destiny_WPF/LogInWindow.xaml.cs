using Destiny_DAL;
using Destiny_Models;
using Project_Destiny_WPF.UserControls;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for Inlogscreen.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        MainWindow w = (MainWindow)Application.Current.MainWindow;
        Uri uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
        private void BtnInlogAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w.BtnInloggen.IsEnabled = true;
            w.BtnRegistreren.IsEnabled = true;
        }

        private void BtnInloggen_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = ValiderenGegevens();

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Account a = new Account();
                a.Accountnaam = txtGebruikersnaam.Text;
                a.Wachtwoord = txtWachtwoord.Password;

                Account b = DatabaseOperations.OphalenAccount(a.Accountnaam);
                if (b != null)
                {
                    string dp = SecurePassword.DecryptString(b.Wachtwoord); //deëncrypteren van database-wachtwoord van account;
                    if (a.Wachtwoord == dp)
                    {
                        User.Acc = b; //nodig om account te onthouden van persoon

                        if (b.ThemaFont != "")
                        {
                            SetFont(b.ThemaFont);
                        }


                        if (b.ThemaColor != "")
                        {
                            SetThemeColor(b.ThemaColor);
                        }

                        //gaat aanpassingen doen in app.xaml
                        System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(3);
                        System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });

                        this.Close();
                        w.Accountnaam.Content = b.Accountnaam;
                        if (b.Image != null)
                        {
                            string profielImage = Encoding.ASCII.GetString(b.Image);
                            w.ProfileImage.Source = new BitmapImage(new Uri(profielImage));
                        }
                        
                        w.Loginpanel.Visibility = Visibility.Hidden;
                        w.Accountpanel.Visibility = Visibility.Visible;
                        w.ListViewMenu.IsEnabled = true;

                        w.GridMain.Children.Clear();
                        UserControl usc = new LoggedInControl();
                        w.GridMain.Children.Add(usc);
                    }

                    else
                    {
                        MessageBox.Show("De opgegeven gebruikersnaam en het wachtwoord komen niet overeen!", "foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Deze gebruikersnaam bestaat niet!", "foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen, "foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGeenAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window register = new RegisterWindow();
            register.Show();
        }

        private string ValiderenGegevens()
        {
            string foutmeldingen = "";

            if (string.IsNullOrWhiteSpace(txtGebruikersnaam.Text))
            {
                foutmeldingen += "Gelieve uw gebruikersnaam in te vullen!" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtWachtwoord.Password))
            {
                foutmeldingen += "Gelieve uw wachtwoord in te vullen!" + Environment.NewLine;
            }
            return foutmeldingen;
        }

        private void BtnWachtwoordVergeten_Click(object sender, RoutedEventArgs e)
        {
            Window lostpass = new LostPasswordWindow();
            lostpass.Show();
            this.Close();
        }

        private void SetFont(string font)
        {
            switch (font)
            {
                case "Segoe UI":
                    //gaat aanpassingen doen in app.xaml
                    App.Current.Resources["font"] = new FontFamily(font);

                    break;

                case "Comic Sans MS":
                    //gaat aanpassingen doen in app.xaml
                    App.Current.Resources["font"] = new FontFamily(font);
                    break;

            }
        }

        private void SetThemeColor(string color)
        {
            
            var bc = new BrushConverter();
            switch (color)
            {
                case "Teal":
                    //Zo kan ik met Hex - kleurwaarden werken
                    w.GridNav.Background = (Brush)bc.ConvertFrom("#FF00C7A3");
                    w.GridMenu.Background = (Brush)bc.ConvertFrom("#FF00C7A3");


                    uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
                    break;

                case "DeepPurple":
                    w.GridNav.Background = Brushes.IndianRed;
                    w.GridMenu.Background = Brushes.IndianRed;

                    uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml");
                    break;

            }
            //gaat aanpassingen doen in app.xaml
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(3);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });
        }


    }
}
