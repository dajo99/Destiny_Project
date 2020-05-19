using Destiny_DAL;
using Destiny_Models;
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
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class WeaponsControl : UserControl
    {
        public WeaponsControl()
        {
            InitializeComponent();
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ZoekenWapens();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst voor te zoeken op zeldzaamheid
            List<string> zeldzaamheidlijst = new List<string>() { "All", "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbZeldzaamheid.ItemsSource = zeldzaamheidlijst;
            cmbZeldzaamheid.SelectedItem = "All";

            //lijst van categoriën om wapens op te zoeken
            List<string> categorieLijst = new List<string>() { "All", "heavy", "primary", "special"};
            cmbCategorie.ItemsSource = categorieLijst;
            cmbCategorie.SelectedItem = "All";


            //Lijst van zeldzaamheden om een wapen toe te voegen
            GeneralWapens.ZeldzaamheidLijst = new List<string>() { "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbDbZeldzaamheid.ItemsSource = GeneralWapens.ZeldzaamheidLijst;


            //lijst van categoriën om een wapen toe te voegen
            GeneralWapens.Categorielijst = new List<string>() { "heavy", "primary", "special" };
            cmbDbCategorie.ItemsSource = GeneralWapens.Categorielijst;

            //lijst van damagetypes om een wapen toe te voegen
            GeneralWapens.Damagetypelijst = new List<string>() { "Kinetic", "Arc", "Light" };
            cmbDbDamageType.ItemsSource = GeneralWapens.Damagetypelijst;

            ZoekenWapens();

        }

        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenWapens();
        }

        private void cmbZeldzaamheid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenWapens();
        }

        private void dbWapens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbWapens.SelectedItem is Wapen w)
            {
                txtNaam.Text = w.Item.Naam;
                txtImpact.Text = w.Impact.ToString();
                txtMagazine.Text = w.Magazine.ToString();
                txtLight.Text = w.LightAmount.ToString();
                cmbDbCategorie.SelectedItem = w.Soort.ToString();
                cmbDbZeldzaamheid.SelectedItem = w.Item.Zeldzaamheid.ToString();
                cmbDbDamageType.SelectedItem = w.Damagetype.Naam.ToString();
               
            }
        }

        private void ZoekenWapens()
        {
            string categorie = cmbCategorie.SelectedItem as String;
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;
            
            //kijken als "All" geselecteerd is in lijst 
            
            if (cmbCategorie.SelectedIndex != 0 && cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaCategorieEnZeldzaamheid(tbZoekWapen.Text, categorie, zeldzaamheid);
            }
            else if (cmbCategorie.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaCategorie(tbZoekWapen.Text, categorie);
            }
            else if (cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaZeldzaamheid(tbZoekWapen.Text, zeldzaamheid);
            }
            else
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaNaam(tbZoekWapen.Text);
            }
            

            
        }

        private void btnAddWeapon_Click(object sender, RoutedEventArgs e)
        {
            GeneralItems.Items = DatabaseOperations.OphalenItems();
            string foutmeldingen = Valideer("cmbDbZeldzaamheid");
            foutmeldingen += Valideer("cmbDbCategorie");
            foutmeldingen += Valideer("cmbDbDamageType");
            foutmeldingen += Valideer("Impact");
            foutmeldingen += Valideer("Magazine");
            foutmeldingen += Valideer("Light");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                string zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
                string categorie = cmbDbCategorie.SelectedItem as string;
                string damagetype = cmbDbDamageType.SelectedItem as string;
                Item i = new Item();
                Wapen w = new Wapen();

                i.Naam = txtNaam.Text;
                i.Zeldzaamheid = zeldzaamheid;
                w.Soort = categorie;
                w.id = i.id;
                w.Impact = GeneralWapens.ConversieToInt(txtImpact.Text);
                w.Magazine = GeneralWapens.ConversieToInt(txtMagazine.Text);
                w.LightAmount = GeneralWapens.ConversieToInt(txtLight.Text);

                if (i.IsGeldig())
                {
                    if (!GeneralItems.Items.Contains(i))
                    {
                        int ok = DatabaseOperations.ToevoegenWapen(i, w);
                        if (ok > 0)
                        {
                            ZoekenWapens();
                            WissenVelden();
                        }
                        else
                        {
                            MessageBox.Show("Item is niet toegevoegd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Een exotic item met dezelfde naam kan niet 2x voorkomen!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(i.Error, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string Valideer(string columnName)
        {
            if (columnName == "dbWapens" && dbWapens.SelectedItem == null)
            {
                return "Selecteer een Wapen!" + Environment.NewLine;
            }
            if (columnName == "cmbDbZeldzaamheid" && cmbDbZeldzaamheid.SelectedItem == null)
            {
                return "Selecteer een zeldzaamheid!" + Environment.NewLine;
            }
            if (columnName == "cmbDbCategorie" && cmbDbCategorie.SelectedItem == null)
            {
                return "Selecteer een categorie!" + Environment.NewLine;
            }
            if (columnName == "cmbDbDamageType" && cmbDbDamageType.SelectedItem == null)
            {
                return "Selecteer een damagetype!" + Environment.NewLine;
            }
            if (columnName == "Impact" && !string.IsNullOrWhiteSpace(txtImpact.Text) && int.TryParse(txtImpact.Text, out int impact) && impact < 0)
            {
                return "Impact moet een positief nummeriek getal zijn!" + Environment.NewLine;
            }
            if (columnName == "Magazine" && !string.IsNullOrWhiteSpace(txtMagazine.Text) && int.TryParse(txtMagazine.Text, out int magazine) && magazine < 0)
            {
                return "Magazine moet een positief nummeriek getal zijn!" + Environment.NewLine;
            }
            if (columnName == "Light" && !string.IsNullOrWhiteSpace(txtLight.Text) && int.TryParse(txtLight.Text, out int light) && light < 0)
            {
                return "Light moet een positief nummeriek getal zijn!" + Environment.NewLine;
            }
            return "";
        }

        private void WissenVelden()
        {
            txtNaam.Text = "";
            txtImpact.Text = "";
            txtMagazine.Text = "";
            txtLight.Text = "";
            cmbDbZeldzaamheid.SelectedIndex = -1;
            cmbDbCategorie.SelectedIndex = -1;
            cmbDbDamageType.SelectedIndex = -1;

        }
    }
}
