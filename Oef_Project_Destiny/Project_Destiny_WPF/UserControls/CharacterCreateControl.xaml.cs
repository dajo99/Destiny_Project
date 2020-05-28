using Destiny_DAL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Destiny_Models;

namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Character_Maken.xaml
    /// </summary>
    public partial class CharacterCreateControl : UserControl
    {
        public CharacterCreateControl()
        {
            InitializeComponent();
        }




        Destiny_DAL.Character karakter = new Destiny_DAL.Character();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //gaat de lijsten uit de statische klasse OptiesUiterlijk ophalen
            cmbGezicht.ItemsSource = OptiesUiterlijk.GezichtOpties;
            cmbHaar.ItemsSource = OptiesUiterlijk.HaarOpties;
            cmbGender.ItemsSource = OptiesUiterlijk.Gender;
            cmbMarking.ItemsSource = OptiesUiterlijk.TattooOpties;
            //gaat via methodes de juiste rassen en klasses inladen uit de database
            cmbRas.ItemsSource = OphalenRassen();
            cmbKlasse.ItemsSource = ControleKlasses();
            ///een subklasse kan niet gekozen worden zolang de klasse niet is gekozen
            cmbSubklasse.IsEnabled = false;


            ///knop aanmaken instellen bij het laden van het scherm
            btnAanmaken.IsEnabled = false;

        }
        public List<CharacterKlasse> ControleKlasses()
        {
            List<CharacterKlasse> klasses = DatabaseOperations.OphalenCharacterKlasseVoorAanmaken();
            List<CharacterKlasse> returnList = new List<CharacterKlasse>();
            foreach (var item in klasses)
            {
                if (!returnList.Contains(item))
                {
                    returnList.Add(item);
                }

            }
            return returnList;
        }

        //aangezien er in de database ook vijanden zijn met een ras, moet er gevalideerd worden op de rasnamen beschikbaar voor de karakters want deze zijn verschillend van de rassen van de vijanden.
        private List<Ras> OphalenRassen()
        {
            List<Ras> rassen = DatabaseOperations.OphalenRasVoorAanmaken();
            List<Ras> returnLijst = new List<Ras>();
            foreach (Ras item in rassen)
            {
                if (item.Naam == "Awoken" || item.Naam == "Human" || item.Naam == "Exo")
                {
                    if (!returnLijst.Contains(item))
                    {
                        returnLijst.Add(item);
                    }
                }
            }
            return returnLijst;
        }


        //methode die nagaat welke CharacterKlasseId moet worden gezocht in de tabel subklasse aangezien subklasse verschilt per klasse//
        private int OphalenSubklasses()
        {
            int id = 0;
            if (cmbKlasse.SelectedItem is CharacterKlasse klasse)
            {
                switch (klasse.Naam)
                {
                    case "Titan":
                        id = klasse.id;
                        break;

                    case "Hunter":
                        id = klasse.id;
                        break;

                    case "Warlock":
                        id = klasse.id;
                        break;
                }
            }
            return id;
        }


        //gaat nagaan of of er een combobox niet geselecteerd is aan de hand van de indexwaarde en gaat een melding returnen die leeg is als alles is geselecteerd
        private string ValideerComboboxen()
        {

            string melding = "";

            if (cmbGender.SelectedIndex == -1)
            {
                melding += "Selecteer een gender!" + Environment.NewLine;
            }
            if (cmbGezicht.SelectedIndex == -1)
            {
                melding += "Selecteer een gezicht!" + Environment.NewLine;
            }
            if (cmbHaar.SelectedIndex == -1)
            {
                melding += "Selecteer een haartype!" + Environment.NewLine;
            }
            if (cmbKlasse.SelectedIndex == -1)
            {
                melding += "Selecteer een klasse!" + Environment.NewLine;
            }
            if (cmbRas.SelectedIndex == -1)
            {
                melding += "Selecteer een ras!" + Environment.NewLine;
            }
            if (cmbMarking.SelectedIndex == -1)
            {
                melding += "Selecteer een Marking!" + Environment.NewLine;
            }

            if (cmbSubklasse.SelectedIndex == -1)
            {
                melding += "Selecteer een subklasse!" + Environment.NewLine;
            }



            return melding;

        }


        //gaat het karakter zijn attributen instellen aan de hand van de geselecteerde items in de combobox zodat deze later kan geinsert worden in de database
        private void ComboboxItemsInstellenVoorBtnTonen()
        {

            if (cmbGender.SelectedItem is string gender)
            {
                karakter.Gender = gender;
            }
            if (cmbGezicht.SelectedItem is string gezicht)
            {
                karakter.Face = gezicht;
            }
            if (cmbHaar.SelectedItem is string haar)
            {
                karakter.HeadOption = haar;
            }
            if (cmbRas.SelectedItem is Ras ras)
            {
                karakter.RasId = ras.id;
            }

            if (cmbMarking.SelectedItem is string marking)
            {
                karakter.Marking = marking;
            }

            if (cmbKlasse.SelectedItem is CharacterKlasse klasse)
            {
                karakter.CharacterKlasseId = klasse.id;
            }
            if (cmbSubklasse.SelectedItem is CharacterSubklasse sub)
            {
                karakter.CharacterSubklasseId = sub.id;
            }
        }

        private void cmbKlasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //gaat de subklasse ontgrendelen en opvullen met de juiste subklasses aan de hand van de geselecteerde klasse
            cmbSubklasse.IsEnabled = true;
            cmbSubklasse.ItemsSource = DatabaseOperations.OphalenCharacterSubklasseVoorAanmaken(OphalenSubklasses());
        }


        //gaat in een listview de geselecteerde items tonen
        private void btnToon_Click(object sender, RoutedEventArgs e)
        {
            //gaat de variabele foutmelding opvullen met de returnwaarde van de methode
            string foutmelding = ValideerComboboxen();

            //controle om te kijken of er wel foutmeldingen in de variabele zitten
            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                //gaat het karakter al grotendeels instellen
                ComboboxItemsInstellenVoorBtnTonen();
                //vult een lijst met attributen op basis van de geselecteerde items in de comboboxen op, die later in de listview worden getoond
                List<string> attributen = new List<string>() { "Haar: " + cmbHaar.SelectedItem as string , "Ras: " + cmbRas.SelectedItem as string,
                                                              "Gezicht: " + cmbGezicht.SelectedItem as string, "Gender: " + cmbGender.SelectedItem as string,
                                                                "Marking: " + cmbMarking.SelectedItem as string, "Klasse: " + cmbKlasse.SelectedItem as string,
                                                                 "Subklasse: " + cmbSubklasse.SelectedItem as string
                                                                };
                lstToon.ItemsSource = attributen;
                if (MessageBox.Show("ben je zeker van je keuzes? druk dan op ja en de optie om je karakter aan te maken wordt ontgrendeld", "melding", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    btnAanmaken.IsEnabled = true;
                }
                
            }
            //als de variabele "foutmelding" niet leeg is gaat hij deze tonen in een messagebox
            else
            {
                MessageBox.Show(foutmelding);
            }


        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = (MainWindow)Application.Current.MainWindow;

            //Foreign key instellen//
            karakter.AccountId = User.Acc.id;

            //startlevel instellen//
            karakter.Level = 5;

            //gaat het karakter toevoegen en de savechanges opvangen
            int ok = DatabaseOperations.CharacterToevoegen(karakter);
            //gaat de returnwaarde van de savechanges controleren
            if (ok > 0)
            {

                MessageBox.Show("karakter succesvol toegevoegd");

                //maakt de maingrid weer leeg en gaat over naar de usercontrol van CharacterControl
                w.GridMain.Children.Clear();
                UserControl usc = new CharacterControl();
                w.GridMain.Children.Add(usc);
            }
            else
            {
                MessageBox.Show("karakter niet toegevoegd");
            }


        }
    }
}
