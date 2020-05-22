using Destiny_DAL;
using Destiny_Models;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                Uri uri = null;
                var bc = new BrushConverter();
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
                        
                        switch (b.ThemaFont)
                        {
                            case "Segoe UI":
                                //gaat aanpassingen doen in app.xaml
                                App.Current.Resources["font"] = new FontFamily(b.ThemaFont);
                                
                                break;

                            case "Comic Sans MS":
                                //gaat aanpassingen doen in app.xaml
                                App.Current.Resources["font"] = new FontFamily(b.ThemaFont);
                                break;

                        }

                        switch (b.ThemaColor)
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
    }
}
