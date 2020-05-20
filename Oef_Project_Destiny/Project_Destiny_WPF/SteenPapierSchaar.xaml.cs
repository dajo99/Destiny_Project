using Destiny_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSchermAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       
        private void SpelSpelen(string keuze)
        {
            SoundPlayer winst = new SoundPlayer("Short_triumphal_fanfare-John_Stracke-815794903.wav");
            SoundPlayer verlies = new SoundPlayer("fail-trombone-01.wav");
            Random r = new Random();
            int resultaat = r.Next(1, 4);
           switch (resultaat)
            {
                case 1:
                    if (keuze == "Steen")
                    {
                        lblResultaat.Content = "De computer koos voor papier en wint,\n het level van je karakter is niet gestegen";
                        verlies.Play();
                    }
                    else if(keuze == "Papier")
                    {
                        lblResultaat.Content = "De computer koos ook voor papier dus is het gelijk,\n het level van je karakter is niet gestegen";
                        verlies.Play();
                    }
                    else
                    {
                        lblResultaat.Content = "Gefeliciteerd je hebt gewonnen!\n de computer koos voor papier";
                        User.Character.Level++;
                        winst.Play();
                        
                    }
               break;
                case 2:
                    if (keuze == "Steen")
                    {
                        lblResultaat.Content = "Gefeliciteerd je hebt gewonnen!\n de computer koos voor schaar"; 
                        User.Character.Level++;
                        winst.Play();
                    }
                    else if (keuze == "Papier")
                    {
                        lblResultaat.Content = "De computer koos voor schaar en wint,\n het level van je karakter is niet gestegen";
                        verlies.Play();
                    }
                    else
                    {
                        lblResultaat.Content = "De computer koos ook voor schaar dus is het gelijk,\n het level van je karakter is niet gestegen ";
                        verlies.Play();
                    }
                    break;
                case 3:
                    if (keuze == "Steen")
                    {
                        lblResultaat.Content = "De computer koos voor voor steen dus is het gelijk,\n het level van je karakter is niet gestegen";
                        verlies.Play();
                    }
                    else if (keuze == "Papier")
                    {
                        lblResultaat.Content = "Gefeliciteerd je hebt gewonnen!\n de computer koos voor steen";
                        User.Character.Level++;
                        winst.Play();
                    }
                    else
                    {
                        lblResultaat.Content = "De computer koos voor steen en wint,\n het level van je karakter is niet gestegen";
                        
                        verlies.Play();
                        
                    }
                    break;
                default:
                    break;
            }

            lblResultaat.Content += "\n huidig level: " + User.Character.Level;
        }

        private void BtnPapier_Click(object sender, RoutedEventArgs e)
        {
            SpelSpelen("Papier");
        }

        private void BtnSteen_Click(object sender, RoutedEventArgs e)
        {
            SpelSpelen("Steen");
        }

        private void BtnSchaar_Click(object sender, RoutedEventArgs e)
        {
            SpelSpelen("Schaar");
        }
    }
}
