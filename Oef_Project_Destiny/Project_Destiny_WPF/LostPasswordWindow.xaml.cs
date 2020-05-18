using Destiny_DAL;
using Destiny_Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LostPassword.xaml
    /// </summary>
    public partial class LostPasswordWindow : Window
    {
        public LostPasswordWindow()
        {
            InitializeComponent();
        }

        private void BtnLostPassAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = (MainWindow)Application.Current.MainWindow;

            this.Close();
            w.BtnInloggen.IsEnabled = true;
            w.BtnRegistreren.IsEnabled = true;
        }

        private void btnResetWachtwoord_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = ValiderenGegevens();
            Account a = new Account();
            a.Accountnaam = txtGebruikersnaam.Text;
            a.Mail = txtMail.Text;
            Account b = DatabaseOperations.OphalenAccount(txtGebruikersnaam.Text);
            //Account nodig om wachtwoord op te halen mail
            Account c = DatabaseOperations.OphalenAccount("Admin");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                if (b != null)
                {
                    //nodig indien de mail niet verstuurt wordt dat men de verandering van wachtwoord ongedaan kan maken
                    a.Wachtwoord = b.Wachtwoord;

                    if (a.Mail == b.Mail)
                    {
                        //Nieuw wachtwoord instellen 
                        string wachtwoord = CreatePassword(10);
                        b.Wachtwoord = SecurePassword.EncryptString(wachtwoord);

                        int ok = DatabaseOperations.WijzigenAccount(b);
                        if (ok > 0)
                        {
                            try
                            {
                                //mail maken en versturen
                                string message = "Geachte gebruiker\n\n" +
                                    "U ontvangt deze e-mail omdat u een aanvraag hebt gedaan om uw wachtwoord van uw Destiny-Account opnieuw in te stellen." +
                                    " \n\nNieuw wachtwoord: " + wachtwoord +
                                    "\n\nMet vriendelijke groeten\nHet Destiny-Team";
                                CreateMailMessage(message, b.Mail, c);
                                MessageBox.Show("Er is een mail verstuurd naar " + b.Mail, "Mail verzonden", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                //Terug het origineel wachtwoord instellen als de mail niet verstuurt is
                                b.Wachtwoord = a.Wachtwoord;
                                int opnieuwWijzigen = DatabaseOperations.WijzigenAccount(b);

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

        private void CreateMailMessage(string emailBody, string uMail, Account a)
        {
            var from = new MailAddress(a.Mail);
            var to = new MailAddress(uMail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Wachtwoord gewijzigd";
            message.Body = emailBody;

            //smpt settings van gmail
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            //Admin van destiny linken aan mail
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential()
            {
                UserName = a.Mail.ToString(),
                Password = SecurePassword.DecryptString(a.Wachtwoord)
            };
            //Secure Sockets Layer enbalen voor betere beveiliging
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
                // op het einde telkens 1 random character toevoegen van de variabele chars
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
    }
}
