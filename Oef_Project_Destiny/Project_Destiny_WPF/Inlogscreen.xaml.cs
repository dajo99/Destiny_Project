using Destiny_DAL;
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
                if (a.Wachtwoord == wachtwoorden[idx])
                {
                    this.Close();
                    w.Accountnaam.Text = a.Accountnaam;
                    w.Loginpanel.Visibility = Visibility.Hidden;
                    w.Accountpanel.Visibility = Visibility.Visible;
                    
                }
                else
                {
                    MessageBox.Show("wachtwoord fout", "fout");
                }
            }
            else
            {
                MessageBox.Show("Gebruikersnaam en wachtwoord komen niet overeen", "fout");
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
