using Destiny_DAL;
using Destiny_Models;
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
        }

        private void BtnInloggen_Click(object sender, RoutedEventArgs e)
        {
            Account a = new Account();
            a.Accountnaam = txtGebruikersnaam.Text;
            a.Wachtwoord = txtWachtwoord.Password;


            List<Account> accounts = DatabaseOperations.CheckLogin();
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
                    this.Close();
                    w.Accountnaam.Text = a.Accountnaam;
                    w.Loginpanel.Visibility = Visibility.Hidden;
                    w.Accountpanel.Visibility = Visibility.Visible;
                    
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

        private void BtnGeenAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Registerscreen register = new Registerscreen();
            register.Show();
        }
    }
}
