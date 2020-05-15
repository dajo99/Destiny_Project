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
using Destiny_DAL;
using Destiny_Models;

namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        public AccountControl()
        {
            InitializeComponent();
        }
        //account globaal maken
        Account a;
        MainWindow w = (MainWindow)Application.Current.MainWindow;
        //lijst van regio's handmatig opvullen
        List<string> Regio = new List<string>() { "Europa", "Amerika", "Azië", "Afrika", "Noord-Amerika", "Zuid-Amerika", "Oceanië" };

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbRegio.ItemsSource = Regio;
            ResetInputs();
            ResetEnables(true);
        }


        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer.ValideerAccountGegevens(txtWachtwoord.Password, txtProfielnaam.Text, txtMail.Text, txtHerhaalWachtwoord.Password);
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                a = DatabaseOperations.OphalenAccount(User.Acc.Accountnaam);
                a.Accountnaam = txtProfielnaam.Text;
                a.Achternaam = txtAchternaam.Text;
                a.Voornaam = txtVoornaam.Text;
                a.Mail = txtMail.Text;
                a.Wachtwoord = SecurePassword.EncryptString(txtWachtwoord.Password);
                if (cmbRegio.SelectedItem is string regio)
                {
                    a.Regio = regio;
                }
              
                if (a.IsGeldig())
                {
                    List<Account> accounts = DatabaseOperations.CheckLogin(User.Acc);
                    if (!accounts.Contains(a))
                    {
                        if (MessageBox.Show("Bent u zeker dat u deze wijzigingen wilt uitvoeren?", "Waarschuwing", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                        {
                            int ok = DatabaseOperations.WijzigenAccount(a);
                            if (ok > 0)
                            {
                                User.Acc = a;
                                w.GridMain.Children.Clear();
                                UserControl usc = new AccountControl();
                                w.GridMain.Children.Add(usc);
                            }
                            else
                            {
                                MessageBox.Show("Het account is niet aangepast!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
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
            ResetInputs();
            ResetEnables(true);
        }

        private void ResetEnables(bool initial)
        {
            if (initial == true)
            {
                txtAchternaam.IsEnabled = false;
                txtVoornaam.IsEnabled = false;
                txtMail.IsEnabled = false;
                cmbRegio.IsEnabled = false;
                txtProfielnaam.IsEnabled = false;
                txtWachtwoord.IsEnabled = false;
                btnOpslaan.IsEnabled = false;
                btnWijzigen.IsEnabled = true;
                lblHerhaalWachtwoord.Visibility = Visibility.Hidden;
                txtHerhaalWachtwoord.Visibility = Visibility.Hidden;
            }
            else
            {
                txtAchternaam.IsEnabled = true;
                txtVoornaam.IsEnabled = true;
                txtMail.IsEnabled = true;
                cmbRegio.IsEnabled = true;
                txtProfielnaam.IsEnabled = true;
                txtWachtwoord.IsEnabled = true;
                btnOpslaan.IsEnabled = true;
                btnWijzigen.IsEnabled = false;
                lblHerhaalWachtwoord.Visibility = Visibility.Visible;
                txtHerhaalWachtwoord.Visibility = Visibility.Visible;
            }

        }

        private void ResetInputs()
        {
            txtProfielnaam.Text = User.Acc.Accountnaam;
            txtMail.Text = User.Acc.Mail;
            txtVoornaam.Text = User.Acc.Voornaam;
            txtAchternaam.Text = User.Acc.Achternaam;
            txtWachtwoord.Password = SecurePassword.DecryptString(User.Acc.Wachtwoord);
            cmbRegio.SelectedItem = User.Acc.Regio;
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bent u zeker dat u dit account wilt verwijderen? " + Environment.NewLine + "Dit is onomkeerbaar!",
                "Waarschuwing", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                int ok = DatabaseOperations.VerwijderenAccount(User.Acc);
                if (ok > 0)
                {
                    User.Acc = null;
                    Application.Current.Shutdown();
                    System.Windows.Forms.Application.Restart();
                }
                else
                {
                    MessageBox.Show("Uw account is niet verwijderd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            ResetEnables(false);
        }


    }
}
