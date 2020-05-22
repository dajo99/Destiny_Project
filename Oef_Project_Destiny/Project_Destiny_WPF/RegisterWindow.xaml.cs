﻿using System;
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
            string foutmeldingen = ValidateAccount.ValideerAccountGegevens(txtWachtwoord.Password, txtGebruikersnaam.Text, txtEmailadres.Text, txtHerhaalWachtwoord.Password); ;
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //nieuw account aanmaken en invoergegevens erin zetten
                Account a = new Account();
                a.Accountnaam = txtGebruikersnaam.Text;
                a.Mail = txtEmailadres.Text;
                //wachtwoord encrypteren 
                string ep = SecurePassword.EncryptString(txtWachtwoord.Password);
                a.Wachtwoord = ep;

                if (a.IsGeldig())
                {
                    // Lijst maken met alle accounts maken   
                    //Nieuw account als paramater is nodig zodat ik geen 2de methode moet aanmaken om een lijst van accounts op te vullen (om account te wijzigen heb je een paramter nodig)
                    List<Account> accounts = DatabaseOperations.CheckLogin(new Account()); 
                    
                    //debug voor te zien wat er in de lijst zit
                    foreach (var item in accounts)
                    {
                        Debug.WriteLine(item.Accountnaam);
                    }

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
