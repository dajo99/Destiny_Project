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
    public partial class Inlogscreen : Window
    {
        public Inlogscreen()
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
                Destiny_DAL.Account a = new Destiny_DAL.Account();
                a.Accountnaam = txtGebruikersnaam.Text;
                a.Wachtwoord = txtWachtwoord.Password;


                List<Destiny_DAL.Account> accounts = DatabaseOperations.CheckLogin();
                List<string> namen = new List<string>();
                List<string> wachtwoorden = new List<string>();

                foreach (var account in accounts)
                {
                    namen.Add(account.Accountnaam);
                    wachtwoorden.Add(account.Wachtwoord);
                }
                if (namen.Contains(a.Accountnaam))
                {
                    int idx = namen.IndexOf(a.Accountnaam);
                    //deëncrypteren van database-wachtwoord van account;
                    string dp = SecurePassword.DecryptString(wachtwoorden[idx]);
                    if (a.Wachtwoord == dp)
                    {
                        User.Acc = a; //nodig om account te onthouden van persoon
                        this.Close();
                        w.Accountnaam.Text = a.Accountnaam;
                        w.Loginpanel.Visibility = Visibility.Hidden;
                        w.Accountpanel.Visibility = Visibility.Visible;
                        w.ListViewMenu.IsEnabled = true;

                        w.GridMain.Children.Clear();
                        UserControl usc = new Ingelogd();
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
            Registerscreen register = new Registerscreen();
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
    }
}
