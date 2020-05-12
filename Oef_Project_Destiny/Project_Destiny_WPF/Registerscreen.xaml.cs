using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using Destiny_DAL;
using Destiny_Models;

using System.ComponentModel.DataAnnotations;//voor mail te checken

namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for Registerscreen.xaml
    /// </summary>
    public partial class Registerscreen : Window
    {
        public Registerscreen()
        {
            InitializeComponent();
        }
        private void BtnRegistrerenAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w.BtnInloggen.IsEnabled = true;
            w.BtnRegistreren.IsEnabled = true;

        }
        MainWindow w = (MainWindow)Application.Current.MainWindow;

        private string ValideerGegevens()
        {
            string password = txtWachtwoord.Password;
            //Lengte van ingegeven gebruikersnaam checken
            if (txtGebruikersnaam.Text.Length < 3)
            {
                return "Gebruikersnaam moet langer zijn dan 3 characters";
            }
            //checken als email-adres valide
            if (!new EmailAddressAttribute().IsValid(txtEmailadres.Text))
            {
                return "Het opgegeven mailadres bestaat niet!";
            }
            //Wachtwoordvalidatie
            if (password.Length < 6)
            {
                return "Lengte van wachtwoord moet langer zijn dan 6 characters!";
            }
            if (password != txtHerhaalWachtwoord.Password)
            {
                return "Wachtwoorden komen niet overeen!";
            }
            return "";
        }

        private void BtnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = ValideerGegevens();
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //nieuw account aanmaken
                Account a = new Account();
                a.Accountnaam = txtGebruikersnaam.Text;
                a.Mail = txtEmailadres.Text;
                //wachtwoord encrypteren
                string ep = SecurePassword.EncryptString(txtWachtwoord.Password);
                a.Wachtwoord = ep;
                if (a.IsGeldig())
                {
                    List<Account> accounts = new List<Account>();
                    accounts = DatabaseOperations.CheckLogin();
                    if (!accounts.Contains(a))
                    {
                        int ok = DatabaseOperations.ToevoegenAccount(a);
                        if (ok > 0)
                        {
                            this.Close();
                            w.Accountnaam.Text = a.Accountnaam;
                            w.Loginpanel.Visibility = Visibility.Hidden;
                            w.Accountpanel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            MessageBox.Show("Account is niet toegevoegd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(a.Accountnaam + " is al in gebruik!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show(a.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAlAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Inlogscreen inlog = new Inlogscreen();
            inlog.Show();
        }
    }
}
