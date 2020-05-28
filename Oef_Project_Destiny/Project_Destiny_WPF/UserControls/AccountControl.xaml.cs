using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Destiny_DAL;
using Destiny_Models;
using Microsoft.Win32;

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

        Account a;
        //The Application.Current.MainWindow property to return a valid reference to a window somewhere in your application
        MainWindow w = (MainWindow)Application.Current.MainWindow;

        //lijst van regio's handmatig opvullen
        List<string> Regio = new List<string>() { "Europa", "Amerika", "Azië", "Afrika", "Noord-Amerika", "Zuid-Amerika", "Oceanië" };

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Comboboc linken aan lijst regio's
            cmbRegio.ItemsSource = Regio;
            ResetInputs();
            ResetEnables(true);
        }


        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            //Account van user ophalen uit database
            a = DatabaseOperations.OphalenAccountViaAccountnaam(User.Acc.Accountnaam);

            //Account opvullen met nieuwe invoer
            a.Accountnaam = txtProfielnaam.Text;
            a.Achternaam = txtAchternaam.Text;
            a.Voornaam = txtVoornaam.Text;
            a.Mail = User.DomeinNaarLowerCase(txtMail.Text); // Domein van mailaccount is case insensitive
            a.Wachtwoord = txtWachtwoord.Password;

            if (UploadFoto.Source != null)
            {
                //Het datatype in de database is varbinary dus de string moet eerst omgezet worden.
                a.Image = Encoding.ASCII.GetBytes(op.FileName);
            }

            if (cmbRegio.SelectedItem is string regio)
            {
                a.Regio = regio;
            }

            if (a.IsGeldig())
            {
                if (a.Wachtwoord == txtHerhaalWachtwoord.Password)
                {
                    //Wachtwoord encrypteren
                    a.Wachtwoord = SecurePassword.EncryptString(a.Wachtwoord);

                    //Controle of account al aangepast is.
                    List<Account> accounts = DatabaseOperations.OphalenAccountViaAccount(User.Acc);
                    if (!accounts.Contains(a))
                    {
                        if (MessageBox.Show("Bent u zeker dat u deze wijzigingen wilt uitvoeren?", "Waarschuwing",
                            MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                        {
                            //Bij een return waarde groter als nul zal er wijziging gebeurd zijn. 
                            int ok = DatabaseOperations.WijzigenAccount(a);
                            if (ok > 0)
                            {
                                // User terug updaten met nieuwe gegevens
                                User.Acc = a;

                                w.Accountnaam.Content = User.Acc.Accountnaam;// als accountnaam veranderd is, naam terug aanpassen in menubalke vanboven

                                //UserControl terug refreshen
                                w.GridMain.Children.Clear();
                                UserControl usc = new AccountControl();
                                w.GridMain.Children.Add(usc);

                                if (UploadFoto.Source != null) //Kijken als pad bestaat
                                {
                                    //Bitmap instantie maken om afbeeldingbestand te lezen
                                    w.ProfileImage.Source = new BitmapImage(new Uri(op.FileName));
                                }

                                ResetInputs();
                                ResetEnables(true);
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
                    MessageBox.Show("Wachtwoorden komen niet overeen!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(a.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void ResetEnables(bool initial)
        {
            //Eerste staat
            if (initial == true)
            {
                txtAchternaam.IsEnabled = false;
                txtVoornaam.IsEnabled = false;
                txtMail.IsEnabled = false;
                cmbRegio.IsEnabled = false;
                txtProfielnaam.IsEnabled = false;
                txtWachtwoord.IsEnabled = false;
                btnOpslaan.IsEnabled = false;
                BtnUploaden.IsEnabled = false;
                
                lblHerhaalWachtwoord.Visibility = Visibility.Hidden;
                txtHerhaalWachtwoord.Visibility = Visibility.Hidden;
                btnWijzigen.Visibility = Visibility.Visible;
                btnAnnuleren.Visibility = Visibility.Hidden;
            }
            //2de staat 
            else
            {
                txtAchternaam.IsEnabled = true;
                txtVoornaam.IsEnabled = true;
                txtMail.IsEnabled = true;
                cmbRegio.IsEnabled = true;
                txtProfielnaam.IsEnabled = true;
                txtWachtwoord.IsEnabled = true;
                btnOpslaan.IsEnabled = true;
                BtnUploaden.IsEnabled = true;

                lblHerhaalWachtwoord.Visibility = Visibility.Visible;
                txtHerhaalWachtwoord.Visibility = Visibility.Visible;
                btnWijzigen.Visibility = Visibility.Hidden;
                btnAnnuleren.Visibility = Visibility.Visible;
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
                    //Account van gebruiker updaten
                    User.Acc = null;

                    //Applicatie heropstarten
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

        //Deze klasse opent een nieuw dialoogvenster om afbeeldingen te uploaden
        OpenFileDialog op = new OpenFileDialog();
        private void BtnUploaden_Click(object sender, RoutedEventArgs e)
        {
            //titel geven aan dialoogvenster
            op.Title = "Select a picture";
            //Zorgen dat er alleen ondersteunde bestandstypes geupload kunnen worden
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                UploadFoto.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            ResetInputs();
            ResetEnables(true);
        }
    }
}
