using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Destiny_DAL;
using Destiny_Models;
using Project_Destiny_WPF.UserControls;
using System.Diagnostics;

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

        MainWindow w = (MainWindow)Application.Current.MainWindow;
        private void BtnRegistrerenAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w.BtnInloggen.IsEnabled = true;
            w.BtnRegistreren.IsEnabled = true;
        }

        private void BtnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            //nieuw account aanmaken en invoergegevens erin zetten
            Account a = new Account();
            a.Accountnaam = txtGebruikersnaam.Text;
            a.Mail =  User.DomeinNaarLowerCase(txtEmailadres.Text);
            a.Wachtwoord = txtWachtwoord.Password;
            a.ThemaColor = "Teal";
            a.ThemaFont = "Segoe UI";

            if (a.IsGeldig())
            {
                if (a.Wachtwoord == txtHerhaalWachtwoord.Password)
                {
                    //wachtwoord encrypteren 
                    a.Wachtwoord = SecurePassword.EncryptString(a.Wachtwoord);

                    // Lijst maken met alle accounts maken   
                    //Nieuw account als paramater is nodig zodat ik geen 2de methode moet aanmaken om een lijst van accounts op te vullen (om account te wijzigen heb je een paramter nodig)
                    List<Account> accounts = DatabaseOperations.OphalenAccountViaAccount(new Account());

                    if (!accounts.Contains(a))//Kijken als er al een acocunt met deze accountnaam in database zit
                    {
                        int ok = DatabaseOperations.ToevoegenAccount(a);
                        if (ok > 0)
                        {
                            User.Acc = a; //nodig om account van de gebruiker te onthouden

                            this.Close();

                            w.Accountnaam.Content = a.Accountnaam;//Menubalk naam veranderen
                            w.Loginpanel.Visibility = Visibility.Hidden; //Andere menu items weergeven
                            w.Accountpanel.Visibility = Visibility.Visible;//Inloggen en registreren buttons verbergen
                            w.ListViewMenu.IsEnabled = true; //navigatie op on zetten om te kunnen gebruiken

                            //Nieuwe usercontrol oproepen
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
                    MessageBox.Show("Wachtwoorden komen niet overeen!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(a.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
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
