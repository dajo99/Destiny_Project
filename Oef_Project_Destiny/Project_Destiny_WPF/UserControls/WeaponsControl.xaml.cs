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


    }
}
