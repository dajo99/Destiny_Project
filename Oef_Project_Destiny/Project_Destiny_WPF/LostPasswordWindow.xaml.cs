﻿using Destiny_DAL;
using Destiny_Models;
using System;
using System.Net.Mail;
using System.Text;
using System.Windows;


namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for LostPassword.xaml
    /// </summary>
    public partial class LostPasswordWindow : Window
    {
        public LostPasswordWindow()
        {
            InitializeComponent();
        }

        MainWindow w = (MainWindow)Application.Current.MainWindow;
        private void BtnLostPassAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            ClosingWindow();
        }

        private void btnResetWachtwoord_Click(object sender, RoutedEventArgs e)
        {
            //Valideren of tekstvakken zijn ingevuld
            string foutmeldingen = ValiderenGegevens();
            
            //instantie aanmaken en opvullen met tekstvakken
            Account a = new Account();
            a.Accountnaam = txtGebruikersnaam.Text;

            //Domein van mail is case insensitive
            a.Mail = User.DomeinNaarLowerCase(txtMail.Text);

            //2de instantie aanmaken om te kijken als het account bestaat in database
            Account user = DatabaseOperations.OphalenAccountViaAccountnaam(txtGebruikersnaam.Text);

            //Account van de admin ophalen om wachtwoord in te stellen van ons mailadres om mail te versturen
            Account admin = DatabaseOperations.OphalenAccountViaAccountnaam("Admin");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                if (user != null)
                {
                    //nodig indien de mail niet verstuurt wordt dat men de verandering van wachtwoord ongedaan kan maken
                    a.Wachtwoord = user.Wachtwoord;

                    if (a.Mail == user.Mail)
                    {
                        //Nieuw wachtwoord instellen 
                        string wachtwoord = CreatePassword(10);
                        user.Wachtwoord = SecurePassword.EncryptString(wachtwoord);

                        int ok = DatabaseOperations.WijzigenAccount(user);
                        if (ok > 0)
                        {
                            try
                            {
                                //mail opstellen en versturen + venster sluiten
                                string message = "Geachte gebruiker\n\n" +
                                    "U ontvangt deze e-mail omdat u een aanvraag hebt gedaan om uw wachtwoord van uw Destiny-Account opnieuw in te stellen." +
                                    "\n\nNieuw wachtwoord: " + wachtwoord +
                                    "\n\nMet vriendelijke groeten\nHet Destiny-Team";

                                CreateMailMessage(message, admin, user);

                                MessageBox.Show("Er is een mail verstuurd naar " + user.Mail, "Mail verzonden", MessageBoxButton.OK, MessageBoxImage.Information);

                                ClosingWindow();

                            }
                            catch (Exception ex)
                            {
                                //Terug het origineel wachtwoord instellen als de mail niet verstuurt is
                                user.Wachtwoord = a.Wachtwoord;
                                DatabaseOperations.WijzigenAccount(user);

                                MessageBox.Show("Er is iets fout gelopen bij het verzenden van de mail!", "Verzenden mislukt!", MessageBoxButton.OK, MessageBoxImage.Error);
                                FileOperations.Foutloggen(ex);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Het wachtwoord is niet gewijzigd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);

                        }

                    }
                    else
                    {
                        MessageBox.Show("De opgegeven mail en accountsnaam komen niet overeen!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Deze gebruikersnaam bestaat niet!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ValiderenGegevens()
        {
            string foutmeldingen = "";

            if (string.IsNullOrWhiteSpace(txtGebruikersnaam.Text))
            {
                foutmeldingen += "Gelieve uw gebruikersnaam in te vullen!" + Environment.NewLine;
            }
            if (string.IsNullOrWhiteSpace(txtMail.Text))
            {
                foutmeldingen += "Gelieve uw mailadres in te vullen!" + Environment.NewLine;
            }

            return foutmeldingen;
        }

        private void CreateMailMessage(string emailBody, Account admin, Account uMail)
        {
            var from = new MailAddress(admin.Mail);
            var to = new MailAddress(uMail.Mail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Wachtwoord gewijzigd";
            message.Body = emailBody;

            //smpt settings van gmail
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            //Admin van destiny linken aan mail
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential()
            {
                UserName = admin.Mail.ToString(),
                Password = SecurePassword.DecryptString(admin.Wachtwoord)
            };
            //Secure Sockets Layer mogelijk maken voor betere beveiliging
            client.EnableSsl = true;

            //mail verzenden
            client.Send(message);
        }

        private string CreatePassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890#'^[]=";
            Random r = new Random();
            StringBuilder res = new StringBuilder();
            int count = 0;

            while (count <= length)
            {
                // op het einde telkens 1 random character toevoegen van de variabele chars aan de builder
                res.Append(chars[r.Next(chars.Length)]);
                count++;
            }
            return res.ToString();
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window login = new LogInWindow();
            login.Show();
        }

        private void ClosingWindow()
        {
            this.Close();
            w.BtnInloggen.IsEnabled = true;
            w.BtnRegistreren.IsEnabled = true;
        }
    }
}
