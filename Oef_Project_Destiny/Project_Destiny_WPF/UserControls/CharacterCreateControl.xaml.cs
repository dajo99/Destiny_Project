using Destiny_DAL;
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

            cmbGezicht.ItemsSource = OphalenOpties("gezicht");
            cmbHaar.ItemsSource = OphalenOpties("haar");
            cmbGender.ItemsSource = OphalenOpties("gender");
            cmbMarking.ItemsSource = OphalenOpties("tattoo");
            cmbRas.ItemsSource = OphalenRassen();
            cmbKlasse.ItemsSource = DatabaseOperations.OphalenCharacterKlasseVoorAanmaken();
            ///een subklasse kan niet gekozen worden zolang de klasse niet is gekozen
            cmbSubklasse.IsEnabled = false;


            ///knop aanmaken instellen bij het laden van het scherm
            btnAanmaken.IsEnabled = false;

        }

        public List<string> OphalenOpties(string beschrijving)
        {


            List<string> lijst = new List<string>();



            if (beschrijving == "haar")
            {
                lijst.Add("krullen");
                lijst.Add("lang");
                lijst.Add("kort");
            }

            if (beschrijving == "tattoo")
            {
                lijst.Add("streep");
                lijst.Add("geen marking");
                lijst.Add("gezicht tattoo");
            }
            if (beschrijving == "gezicht")
            {
                lijst.Add("jong");
                lijst.Add("oud");
                lijst.Add("krijger");
            }
            if (beschrijving == "gender")
            {

                lijst.Add("Man");
                lijst.Add("Vrouw");

            }


            return lijst;

        }
        private List<Ras> OphalenRassen()
        {
            List<Ras> rassen = DatabaseOperations.OphalenRasVoorAanmaken();
            List<Ras> returnLijst = new List<Ras>();
            foreach (Ras item in rassen)
            {
                if (item.Naam == "Awoken" || item.Naam == "Human" || item.Naam == "Exo")
                {
                    returnLijst.Add(item);
                }
            }
            return returnLijst;
        }


        ///methode die nagaat welke CharacterKlasseId moet worden gezocht in de tabel subklasse aangezien subklasse verschilt per klasse//
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
                karakter.Ras = ras;
            }

            if (cmbMarking.SelectedItem is string marking)
            {
                karakter.Marking = marking;
            }

            if (cmbKlasse.SelectedItem is CharacterKlasse klasse && cmbSubklasse.SelectedItem is CharacterSubklasse subklasse)
            {
                karakter.CharacterKlasse = klasse;
                if (karakter.CharacterKlasse.id == subklasse.CharacterKlasseId)
                {
                    karakter.CharacterKlasse.CharacterSubklasses.Add(subklasse);
                }
            }
        }

        private void cmbKlasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            cmbSubklasse.IsEnabled = true;
            cmbSubklasse.ItemsSource = DatabaseOperations.OphalenCharacterSubklasseVoorAanmaken(OphalenSubklasses());

        }



        private void btnToon_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = ValideerComboboxen();
            ComboboxItemsInstellenVoorBtnTonen();
            List<string> attributen = new List<string>();

            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                attributen.Add("haar: " + karakter.HeadOption);
                attributen.Add("ras: " + karakter.Ras.Naam + karakter.RasId); ;
                attributen.Add("gezicht: " + karakter.Face);
                attributen.Add("gender: " + karakter.Gender);
                attributen.Add("marking: " + karakter.Marking);
                attributen.Add("Klasse: " + karakter.CharacterKlasse.Naam);

                foreach (var item in karakter.CharacterKlasse.CharacterSubklasses)
                {
                    if (item == cmbSubklasse.SelectedItem as CharacterSubklasse)
                    {
                        attributen.Add("Subklasse: " + item.Naam);
                    }

                }
                lstToon.ItemsSource = attributen;
                btnAanmaken.IsEnabled = true;
                MessageBox.Show("Als je zeker bent van je keuzes dan kan je nu het karakter aanmaken", "melding",MessageBoxButton.OK,MessageBoxImage.Information);
            }
           

        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            User.Acc.id = DatabaseOperations.IdOphalenAccount(User.Acc.Accountnaam);
            karakter.AccountId = User.Acc.id;
            //startlevel//
            karakter.Level = 5;
            int ok = DatabaseOperations.CharacterToevoegen(karakter);
            if (ok > 0)
            {
                User.Karakters.Add(karakter);
                MessageBox.Show("karakter succesvol toegevoegt");
            }
            
        }
    }
}
