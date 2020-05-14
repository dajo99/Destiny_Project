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
            cmbKlasse.ItemsSource = ControleKlasses();
            ///een subklasse kan niet gekozen worden zolang de klasse niet is gekozen
            cmbSubklasse.IsEnabled = false;


            ///knop aanmaken instellen bij het laden van het scherm
            btnAanmaken.IsEnabled = false;

        }
        public List<CharacterKlasse>ControleKlasses()
        {
            List<CharacterKlasse>klasses = DatabaseOperations.OphalenCharacterKlasseVoorAanmaken();
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
                    if (!returnLijst.Contains(item))
                    {
                        returnLijst.Add(item);
                    }
                    
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
            }

            if (cmbMarking.SelectedItem is string marking)
            {
                karakter.Marking = marking;
            }

            if (cmbKlasse.SelectedItem is CharacterKlasse klasse)
            {
                karakter.CharacterKlasseId = klasse.id;
                
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
                attributen.Add("haar: " + cmbHaar.SelectedItem as string);
                attributen.Add("ras: " + cmbRas.SelectedItem as string); ;
                attributen.Add("gezicht: " + cmbGezicht.SelectedItem as string);
                attributen.Add("gender: " + cmbGender.SelectedItem as string);
                attributen.Add("marking: " + cmbMarking.SelectedItem as string);
                attributen.Add("Klasse: " + cmbKlasse.SelectedItem as string);
                attributen.Add("Subklasse: " + cmbSubklasse.SelectedItem as string);
                  
                lstToon.ItemsSource = attributen;
                MessageBox.Show("Als je zeker bent van je keuzes dan kan je nu het karakter aanmaken", "melding",MessageBoxButton.OK,MessageBoxImage.Information);
                btnAanmaken.IsEnabled = true;
            }
           

        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = (MainWindow)Application.Current.MainWindow;
            karakter.AccountId = User.Acc.id;

            ///startlevel///
            karakter.Level = 5;
            int ok = DatabaseOperations.CharacterToevoegen(karakter);
            if (ok > 0)
            {
                
                MessageBox.Show("karakter succesvol toegevoegd");
                w.GridMain.Children.Clear();
                UserControl usc = new CharacterControl();
                w.GridMain.Children.Add(usc);
            }
           

        }
    }
}
