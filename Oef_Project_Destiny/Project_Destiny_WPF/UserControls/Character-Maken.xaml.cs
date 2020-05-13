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
    public partial class Character_Maken : UserControl
    {
        public Character_Maken()
        {
            InitializeComponent();
        }

        ///heb dit zo moeten doen omdat wij in de database een misrekening hadden gemaakt destijds met de tabellen
        Destiny_DAL.CharacterCustomization customization = null;

        ///dit gaat ervoor zorgen dat de comboboxen de juiste waardes gaan krijgen en niet al de waardes
        Destiny_DAL.Character karakter = new Destiny_DAL.Character();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGezicht.ItemsSource = OphalenOpties("gezicht");
            cmbHaar.ItemsSource = OphalenOpties("haar");
            cmbGender.ItemsSource = OphalenOpties("gender");
            cmbMarking.ItemsSource = OphalenOpties("tattoo");

            cmbKlasse.ItemsSource = DatabaseOperations.OphalenCharacterKlasseVoorAanmaken();
            ///een subklasse kan niet gekozen worden zolang de klasse niet is gekozen
            cmbSubklasse.IsEnabled = false;
            cmbRas.ItemsSource = OphalenRas();
            
            ///knop aanmaken instellen bij het laden van het scherm
            btnAanmaken.IsEnabled = false;



            
           
        }


        public List<string>OphalenOpties(string beschrijving)
        {
           List<CharacterCustomization> opties = DatabaseOperations.OphalenCharacterOptiesVoorAanmaken();
            List<string> lijst = new List<string>();
              
            foreach (CharacterCustomization item in opties)
            {

                if (beschrijving == "haar")
                {
                        lijst.Add(item.HeadOption); 
                }

                if (beschrijving == "tattoo")
                {
                    if (!lijst.Contains(item.Marking))
                    {
                        lijst.Add(item.Marking);
                    }
                    
                }
                if (beschrijving == "gezicht")
                {
                    if (!lijst.Contains(item.Face))
                    {
                        lijst.Add(item.Face);
                    }
                }
                if (beschrijving == "gender")
                {
                    if (!lijst.Contains(item.Gender))
                    {
                        lijst.Add(item.Gender);
                            ///in de database stond niets voor vrouw en nee wij zijn niet seksistisch///
                            ///het was ook engels in de database "male"
                        lijst.Add("female");
                    }
                }
                
                
            }
            return lijst;
           
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

        private List <string> OphalenRas()
        {
            List<CharacterCustomization> rassen = DatabaseOperations.OphalenCharacterOptiesVoorAanmaken();
            List<string> returnLijst = new List<string>();

            foreach  (var item in rassen)
            {
                List<Ras> list = new List<Ras>();

                if (item.Ras.Naam == "Awoken" || item.Ras.Naam == "Human" || item.Ras.Naam == "Exo")
                {
                    returnLijst.Add(item.Ras.Naam);
                }
            }
            return returnLijst;
        }
       
 
        

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
           
            if (customization != null)
            {
                karakter.CharacterCustomizationId = customization.id;
            }
            
   
        }

      
        private void cmbRas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void cmbSubklasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbKlasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbSubklasse.IsEnabled = true;
            cmbSubklasse.ItemsSource = DatabaseOperations.OphalenCharacterSubklasseVoorAanmaken(OphalenSubklasses());
        }

        private void cmbGezicht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbHaar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void ComboboxItemsInstellenVoorBtnOpslagen()
        {
            if (cmbGender.SelectedItem is string gender)
            {
                customization.Gender = gender;
            }
            if (cmbGezicht.SelectedItem is string gezicht)
            {
                customization.Face = gezicht;
            }
            if (cmbHaar.SelectedItem is string haar)
            {
                customization.HeadOption = haar;
            }
            if (cmbRas.SelectedItem is Ras ras)
            {
                MessageBox.Show(ras.Naam + ras.id);
                customization.RasId = ras.id;
                MessageBox.Show("dit is de ras id");
            }

            if (cmbMarking.SelectedItem is string marking)
            {
                customization.Marking = marking;
            }
         
        }

        private void btnOpslagen_Click(object sender, RoutedEventArgs e)
        {
            ComboboxItemsInstellenVoorBtnOpslagen();

            string foutmelding = ValideerComboboxen();

            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                customization = new Destiny_DAL.CharacterCustomization();
               
                int ok = DatabaseOperations.InstellingenVanAanmakenOpslaanInDatabase(customization);
                if (ok > 0)
                {
                    btnAanmaken.IsEnabled = true;
                    MessageBox.Show("Instellingen zijn succesvol opgeslagen!");
                    lstOverzicht.Items.Add("Haar: " + customization.HeadOption);
                    lstOverzicht.Items.Add("Tattoo:" + customization.Marking);
                    lstOverzicht.Items.Add("Gender: " + customization.Gender);
                    lstOverzicht.Items.Add("Gezicht" + customization.Face);
                    lstOverzicht.Items.Add("Ras" + customization.Ras);
                }
                else
                {
                    MessageBox.Show("fout bij het opslagen van de instellingen");
                }

            }
            else
            {
                MessageBox.Show(foutmelding);
            }
        }
    }
}
