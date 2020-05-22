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
            List<Wapenklasse> categorieLijst = DatabaseOperations.OphalenWapenCategorie();
            Wapenklasse w = new Wapenklasse();
            w.Naam = "All";
            categorieLijst.Insert(0, w);
            cmbCategorie.ItemsSource = categorieLijst;
            cmbCategorie.DisplayMemberPath = "Naam";
            cmbCategorie.SelectedIndex = 0;


            //Lijst van zeldzaamheden om een wapen toe te voegen
            GeneralItems.ZeldzaamheidLijst = new List<string>() { "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbDbZeldzaamheid.ItemsSource = GeneralItems.ZeldzaamheidLijst;


            //lijst van categoriën om een wapen toe te voegen
            cmbDbCategorie.ItemsSource = DatabaseOperations.OphalenWapenCategorie();
            cmbDbCategorie.DisplayMemberPath = "Naam";

            //lijst van damagetypes om een wapen toe te voegen
            cmbDbDamageType.ItemsSource = DatabaseOperations.OphalenWapenDamagetype();
            cmbDbDamageType.DisplayMemberPath = "Naam";

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
            WissenVelden();

            if (dbWapens.SelectedItem is Wapen w)
            {
                txtNaam.Text = w.Item.Naam;
                txtImpact.Text = w.Impact.ToString();
                txtMagazine.Text = w.Magazine.ToString();
                txtLight.Text = w.LightAmount.ToString();
                cmbDbCategorie.SelectedItem = w.Wapenklasse;
                cmbDbZeldzaamheid.SelectedItem = w.Item.Zeldzaamheid;
                cmbDbDamageType.SelectedItem = w.Damagetype;
               
            }
           
        }

        private void ZoekenWapens()
        {
            Wapenklasse w = cmbCategorie.SelectedItem as Wapenklasse;
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;
            
            //kijken als "All" geselecteerd is in lijst 
            
            if (w != null && cmbCategorie.SelectedIndex != 0 && cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaCategorieEnZeldzaamheid(tbZoekWapen.Text, w.id, zeldzaamheid);
            }
            else if (w != null && cmbCategorie.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaCategorie(tbZoekWapen.Text, w.id);
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
                Wapenklasse wk = cmbDbCategorie.SelectedItem as Wapenklasse;
                Damagetype da = cmbDbDamageType.SelectedItem as Damagetype;
                Item it = new Item();
                Wapen wa = new Wapen();
                it.Naam = txtNaam.Text;
                it.Zeldzaamheid = zeldzaamheid;
                wa.WapenklasseId = wk.id;
                wa.id = it.id;
                wa.DamagetypeId = da.id;
                wa.Impact = GeneralItems.ConversieToInt(txtImpact.Text);
                wa.Magazine = GeneralItems.ConversieToInt(txtMagazine.Text);
                wa.LightAmount = GeneralItems.ConversieToInt(txtLight.Text);

                if (it.IsGeldig())
                {
                    if (!GeneralItems.Items.Contains(it))
                    {
                        int ok = DatabaseOperations.ToevoegenWapen(it, wa);
                        if (ok > 0)
                        {
                            ZoekenWapens();
                            WissenVelden();
                        }
                        else
                        {
                            MessageBox.Show("Wapen is niet toegevoegd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Een exotic item met dezelfde naam kan niet 2x voorkomen!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(it.Error, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        


        private void btnChangeWeapon_Click(object sender, RoutedEventArgs e)
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
                Wapenklasse wk = cmbDbCategorie.SelectedItem as Wapenklasse;
                Damagetype da = cmbDbDamageType.SelectedItem as Damagetype;
                Wapen w = dbWapens.SelectedItem as Wapen;
                GeneralItems.Items.Remove(w.Item);

                w.Item.Naam = txtNaam.Text;
                w.Item.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
                w.Impact = GeneralItems.ConversieToInt(txtImpact.Text);
                w.Magazine = GeneralItems.ConversieToInt(txtMagazine.Text);
                w.LightAmount = GeneralItems.ConversieToInt(txtLight.Text);

                w.WapenklasseId = wk.id;
                w.Wapenklasse = wk;
                w.DamagetypeId = da.id;
                w.Damagetype = da;
                

                

                if (w.Item.IsGeldig())
                {

                    if (!GeneralItems.Items.Contains(w.Item))
                    {
                        int ok = DatabaseOperations.AanpassenWapens(w.Item, w);

                        if (ok > 0)
                        {
                            ZoekenWapens();
                            WissenVelden();
                        }

                        else
                        {
                            MessageBox.Show("Wapen is niet gewijzigd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Een exotic item met dezelfde naam kan niet 2x voorkomen!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show(w.Item.Error, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ZoekenWapens();
            WissenVelden();

        }

        private void btnRemoveWeapon_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("dbWapens");
            string errors = "";

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                for (int i = 0; i < dbWapens.SelectedItems.Count; i++)
                {
                    Wapen w = dbWapens.SelectedItems[i] as Wapen;
                    int ok = DatabaseOperations.VerwijderenWapen(w.Item, w);
                    if (ok == 0)
                    {
                        errors += w.Item.Naam[i] + " is niet verwijderd!" + Environment.NewLine;
                    }
                }
                if(!string.IsNullOrWhiteSpace(errors))
                {
                    MessageBox.Show(errors, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZoekenWapens();
            WissenVelden();
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
