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
namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Character_Wijzigen.xaml
    /// </summary>
    public partial class CharacterChangeControl : UserControl
    {
        public CharacterChangeControl()
        {
            InitializeComponent();
        }
        MainWindow w = (MainWindow)Application.Current.MainWindow;

        ///om later een optie toe te voegen

        Character c = User.Character;

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            string melding = "";
            if (cmbKeuzes.SelectedItem is string aanpassing)
            {
                if (rbHaar.IsChecked == true)
                {
                    c.HeadOption = aanpassing;
                    melding += "De haarstijl is aangepast!";
                }
                else if (rbGezicht.IsChecked == true)
                {
                    c.Face = aanpassing;
                    melding += "het gezicht van uw karakter is aangepast";
                }
                else if (rbMarking.IsChecked == true)
                {
                    c.Marking = aanpassing;
                    melding += "de marking van je karakter is aangepast";
                }
                
                if (melding != "")
                {
                    int ok = DatabaseOperations.CharacterUpdaten(c);
                    if (ok > 0)
                    {
                        MessageBox.Show(melding + " uw karakter is succesvol geupdate!");
                    }

                }
            }
            else
            {
                MessageBox.Show("er kan geen wijziging gebeuren als er niets geselecteerd is");
            }
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            w.GridMain.Children.Clear();
            UserControl usc = new CharacterControl();
            w.GridMain.Children.Add(usc);
        }

        private void rbMarking_Checked(object sender, RoutedEventArgs e)
        {
            cmbKeuzes.IsEnabled = true;
            cmbKeuzes.ItemsSource = OptiesUiterlijk.TattooOpties;
        }
       
        private void rbHaar_Checked(object sender, RoutedEventArgs e)
        {
            cmbKeuzes.IsEnabled = true;
            cmbKeuzes.ItemsSource = OptiesUiterlijk.HaarOpties;
        }

        private void rbGezicht_Checked(object sender, RoutedEventArgs e)
        {
            cmbKeuzes.IsEnabled = true;
            cmbKeuzes.ItemsSource = OptiesUiterlijk.GezichtOpties;
        }

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string melding = "";
            if (!string.IsNullOrWhiteSpace(txtWijziging.Text))
            {
                if (txtWijziging.Text.Length < 3)
                {
                    MessageBox.Show("het aantal ingevoerde karakters is niet genoeg om een volwaardig attribuut te zijn" + "\n\n " +
                        "minimumlengte is 3");
                }
                else
                {
                    if (rbHaarToevoegen.IsChecked == true)
                    {
                        c.HeadOption = txtWijziging.Text;
                        melding += "Uw eigen haarstijl is succesvol toegevoegt!";
                    }
                    else if (rbGezichtToevoegen.IsChecked == true)
                    {
                        c.Face = txtWijziging.Text;
                        melding += "Uw eigen gezichtskeuze is succesvol toegevoegt!";
                    }
                    else if (rbMarkingToevoegen.IsChecked == true)
                    {
                        c.Marking = txtWijziging.Text;
                        melding += "Uw eigen marking is succesvol toegevoegt!";
                    }
                    else
                    {
                        MessageBox.Show("selecteer een van de bovenstaande opties!");
                        
                    } 
                }
                if (melding != "")
                {
                    int ok = DatabaseOperations.CharacterUpdaten(c);
                    if (ok > 0)
                    {
                        MessageBox.Show(melding + " uw karakter is succesvol geupdate!");
                    }
                    
                }


            }
            else
            {
                MessageBox.Show("er kan geen aanpassing gebeuren als het tekstvak leeg is!");
            }
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbKeuzes.IsEnabled = false;
        }
    }
}
