using System.Windows;
using System.Windows.Controls;
using Destiny_DAL;
using Destiny_Models;

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

        //gaat de keuzes uit de combobox opslagen bij het character in de database
        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            string melding = "";
            //gaat controleren of er een aanpassing geselecteerd is in de keuze box
            if (cmbKeuzes.SelectedItem is string aanpassing)
            {
                //gaat na welke radiobutton er is geselecteerd en vult de melding op met de juiste waarde
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
                //als de melding niet leeg is wordt het karakter geupdate
                if (melding != "")
                {
                    int ok = DatabaseOperations.CharacterUpdaten(c);
                    if (ok > 0)
                    {
                        MessageBox.Show(melding + " uw karakter is succesvol geupdate!");
                    }

                }
            }
            //als er niets geselecteerd is
            else
            {
                MessageBox.Show("er kan geen wijziging gebeuren als er niets geselecteerd is");
            }
        }

        //gaat terug naar de usercontrol van CharacterControl
        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            w.GridMain.Children.Clear();
            UserControl usc = new CharacterControl();
            w.GridMain.Children.Add(usc);
        }

        //de volgende 3 methodes gaan de combobox opvullen afhankelijk van welke radiobutton dat er checked is
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

        //deze methode gaat de eigenaangemaakte optie valideren en erna toevoegen
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string melding = "";
            if (!string.IsNullOrWhiteSpace(txtWijziging.Text))
            {
                //valideert op de lengte van het tekstveld
                if (txtWijziging.Text.Length < 3)
                {
                    MessageBox.Show("het aantal ingevoerde karakters is niet genoeg om een volwaardig attribuut te zijn" + "\n\n " +
                        "minimumlengte is 3");
                }
                else
                {
                    //kijkt welke radiobuttons er checked zijn en vult de melding op met de juiste waarde
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
                    //als er niets geselecteerd is
                    else
                    {
                        MessageBox.Show("selecteer een van de bovenstaande opties!");
                        
                    } 
                }
                //als de melding niet leeg is dan wordt het character geupdate
                if (melding != "")
                {
                    int ok = DatabaseOperations.CharacterUpdaten(c);
                    if (ok > 0)
                    {
                        MessageBox.Show(melding + " uw karakter is succesvol geupdate!");
                    }
                    
                }
            }
            //als het tekstvak leeg is
            else
            {
                MessageBox.Show("er kan geen aanpassing gebeuren als het tekstvak leeg is!");
            }
           
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //zet de combobox op false
            cmbKeuzes.IsEnabled = false;
        }
    }
}
