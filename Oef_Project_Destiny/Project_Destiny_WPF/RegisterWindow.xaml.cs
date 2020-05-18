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
using Project_Destiny_WPF.UserControls;

namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for Registerscreen.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
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

        

        private void BtnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer.ValideerAccountGegevens(txtWachtwoord.Password, txtGebruikersnaam.Text, txtEmailadres.Text, txtHerhaalWachtwoord.Password); ;
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
                    // deze parameter is oorspronkelijk nodig omdat deze niet null kan zijn en is vereist om account te kunnen wijzigen!          
                    List<Account> accounts = DatabaseOperations.CheckLogin(new Account());
                    if (!accounts.Contains(a))
                    {
                        int ok = DatabaseOperations.ToevoegenAccount(a);
                        if (ok > 0)
                        {
                            User.Acc = a; //nodig om account te onthouden van persoon
                            this.Close();
                            w.Accountnaam.Content = a.Accountnaam;
                            w.Loginpanel.Visibility = Visibility.Hidden;
                            w.Accountpanel.Visibility = Visibility.Visible;
                            w.ListViewMenu.IsEnabled = true;

                            w.GridMain.Children.Clear();
                            UserControl usc = new LoggedInControl();
                            w.GridMain.Children.Add(usc);
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
            Window inlog = new LogInWindow();
            inlog.Show();
        }
    }
}
