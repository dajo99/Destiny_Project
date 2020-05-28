using Destiny_Models;
using System;
using System.Media;
using System.Windows;

namespace Project_Destiny_WPF
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

 
        private void BtnSchermAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
        private string SpelSpelen()
        {
            //deze methode gaat via een random bepalen welke keuze de computer maakt en returned deze
            Random r = new Random();
            string antwoord = "";
           

           int  computer = r.Next(1, 4);

            switch (computer)
            {
                case 1:
                    antwoord = "papier";
                    break;
                case 2:
                    antwoord = "steen";
                    break;
                case 3:
                    antwoord = "schaar";
                    break;

              
            }
            return antwoord;

            
        }
        private void ControleWinnaar(string speler, string computer)
        {
            SoundPlayer winst = new SoundPlayer("Short_triumphal_fanfare-John_Stracke-815794903.wav");
            SoundPlayer verlies = new SoundPlayer("fail-trombone-01.wav");
            string tekst = "";

            //gaat controleren op gelijkspel
            if (speler == computer)
            {
                tekst += "de computer koos ook voor " + computer + "\n het level van je karakter is niet gestegen";
                verlies.Play();
            }

            else 
            {
                //gaat controleren op winst zo ja dan wordt het level van het character geupdate
                if ((speler == "papier" && computer == "steen") || (speler == "schaar" && computer == "papier") || (speler == "steen" && computer == "schaar"))
                {
                    tekst += "gefeliciteerd je hebt gewonnen!\nde computer koos voor " + computer;
                    winst.Play();
                    User.Character.Level++;
                }

                //bij verlies
                else
                {
                    tekst += "de computer koos voor " + computer + " en wint\nhet level van je karakter is niet gestegen";
                    verlies.Play();
                }
            }
            lblResultaat.Content = tekst;
            lblResultaat.Content += "\n huidig level: " + User.Character.Level;
        }
        private void BtnPapier_Click(object sender, RoutedEventArgs e)
        {
          ControleWinnaar("papier", SpelSpelen());
           
        }

        private void BtnSteen_Click(object sender, RoutedEventArgs e)
        {
          ControleWinnaar("steen", SpelSpelen());
        }

        private void BtnSchaar_Click(object sender, RoutedEventArgs e)
        {
          ControleWinnaar("schaar", SpelSpelen());
        }
    }
}
