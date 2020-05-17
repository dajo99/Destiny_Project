using Destiny_DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Interaction logic for SpecialItem.xaml
    /// </summary>
    public partial class SpecialItemControl : UserControl
    {
        public SpecialItemControl()
        {
            InitializeComponent();
        }
        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenItems();
            /*
            SpecialItemCategorie categorie = cmbCategorie.SelectedItem as SpecialItemCategorie;
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;
            if (categorie != null && !string.IsNullOrWhiteSpace(zeldzaamheid))
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaCategorieEnZeldzaamheid(tbZoekItem.Text, categorie.id, zeldzaamheid);
            }
            else if (categorie != null)
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaCategorie(tbZoekItem.Text, categorie.id);
            }*/
        }

        private void cmbZeldzaamheid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenItems();
            /*
            SpecialItemCategorie categorie = cmbCategorie.SelectedItem as SpecialItemCategorie;
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;
            if (categorie != null && !string.IsNullOrWhiteSpace(zeldzaamheid))
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaCategorieEnZeldzaamheid(tbZoekItem.Text, categorie.id, zeldzaamheid);
            }
            else if (!string.IsNullOrWhiteSpace(zeldzaamheid))
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaZeldzaamheid(tbZoekItem.Text, zeldzaamheid);
            }*/
        }

        private void dbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User.Items = DatabaseOperations.OphalenItems();
            Wissen();
            
            if (dbItems.SelectedItem is SpecialItem si)
            {
                txtNaam.Text = si.Item.Naam;
                txtDurability.Text = si.Durability.ToString();
                txtBoost.Text = si.Boost.ToString();
                cmbDbCategorie.SelectedItem = si.SpecialItemCategorie;
                
                /*//int count = -1;
                foreach (SpecialItemCategorie ic in categorieLijst2)
                {
                    //count++;
                    if (ic.Naam == si.SpecialItemCategorie.Naam)
                    {
                        //cmbDbCategorie.SelectedItem = categorieLijst2[count];
                        cmbDbCategorie.SelectedItem = ic;
                        Debug.WriteLine(ic.Naam == si.SpecialItemCategorie.Naam);
                    }
                }*/
                cmbDbZeldzaamheid.SelectedItem = si.Item.Zeldzaamheid;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst voor te zoeken op zeldzaamheid
            List<string> zeldzaamheidlijst1 = new List<string>() { "All", "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbZeldzaamheid.ItemsSource = zeldzaamheidlijst1;
            cmbZeldzaamheid.SelectedItem = "All";

            //Lijst van zeldzaamheden om een item toe te voegen
            List<string> zeldzaamheidlijst2 = new List<string>() { "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbDbZeldzaamheid.ItemsSource = zeldzaamheidlijst2;

            //lijst van categoriën om items op te zoeken
            List<SpecialItemCategorie> categorieLijst = DatabaseOperations.OphalenSpecialItemCategories();
            SpecialItemCategorie categorie = new SpecialItemCategorie();
            categorie.Naam = "All";
            categorieLijst.Insert(0, categorie);
            cmbCategorie.ItemsSource = categorieLijst;
            cmbCategorie.DisplayMemberPath = "Naam";
            cmbCategorie.SelectedIndex = 0;

            //lijst van categoriën om een item toe te voegen
            cmbDbCategorie.ItemsSource = DatabaseOperations.OphalenSpecialItemCategories(); 
            cmbDbCategorie.DisplayMemberPath = "Naam";

            ZoekenItems();
        }

        private void tbZoekItem_KeyUp(object sender, KeyEventArgs e)
        {
            ZoekenItems();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            User.Items = DatabaseOperations.OphalenItems();
            string foutmeldingen = Valideer("cmbDbZeldzaamheid");
            foutmeldingen += Valideer("cmbDbCategorie");
            foutmeldingen += Valideer("Boost");
            foutmeldingen += Valideer("Durability");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                string zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
                Item i = new Item();
                SpecialItem si = new SpecialItem();
                SpecialItemCategorie sic = cmbDbCategorie.SelectedItem as SpecialItemCategorie;
                i.Naam = txtNaam.Text;
                i.Zeldzaamheid = zeldzaamheid;
                si.id = i.id;
                si.CategorieId = sic.id;
                si.Boost = ConversieToInt(txtBoost.Text);
                si.Durability = ConversieToInt(txtDurability.Text);
                if (i.IsGeldig())
                {
                    if (!User.Items.Contains(i))
                    {
                        int ok = DatabaseOperations.ToevoegenItem(i, si);
                        if (ok > 0)
                        {
                            ZoekenItems();
                            Wissen();
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

        private void btnChangeItem_Click(object sender, RoutedEventArgs e)
        {
            User.Items = DatabaseOperations.OphalenItems();

            string foutmeldingen = Valideer("Boost");
            foutmeldingen += Valideer("Durability");
            foutmeldingen += Valideer("cmbDbZeldzaamheid");
            foutmeldingen += Valideer("cmbDbCategorie");
            foutmeldingen += Valideer("dbItems");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                SpecialItemCategorie c = cmbDbCategorie.SelectedItem as SpecialItemCategorie;
                SpecialItem si = dbItems.SelectedItem as SpecialItem;
                Item i = si.Item;
                User.Items.Remove(i);
                si.Boost = ConversieToInt(txtBoost.Text);
                si.Durability = ConversieToInt(txtDurability.Text);

                si.CategorieId = c.id;
                si.SpecialItemCategorie = c;

                i.Naam = txtNaam.Text;
                i.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;

                Debug.WriteLine(c.Naam + " " + si.id + "-" + si.CategorieId + "-" + si.Boost + "-" + si.Durability + "-" + i.Naam + "-" + i.Zeldzaamheid);

                if (i.IsGeldig())
                {
                    if (!User.Items.Contains(i))
                    {
                        int ok1 = DatabaseOperations.AanpassenItems(i);
                        if (ok1 > 0)
                        {
                            int ok = DatabaseOperations.AanpassenSpecialItems(si);
                            if (ok > 0)
                            {
                                Debug.WriteLine(c.Naam + " " + si.id + "-" + si.CategorieId + "-" + si.Boost + "-" + si.Durability + "-" + i.Naam + "-" + i.Zeldzaamheid);
                                ZoekenItems();
                                Wissen();
                            }
                            else
                            {
                                MessageBox.Show("SpecialItem is niet gewijzigd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                        else
                        {
                            MessageBox.Show("Item is niet gewijzigd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
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

            Wissen();
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("dbItems");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                SpecialItem si = dbItems.SelectedItem as SpecialItem;
                int ok = DatabaseOperations.VerwijderenSpecialItem(si.Item, si);
                if (ok > 0)
                {
                    ZoekenItems();
                    Wissen();
                }
                else
                {
                    MessageBox.Show("Item is niet verwijderd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private string Valideer(string columnName)
        {
            if (columnName == "dbItems" && dbItems.SelectedItem == null)
            {
                return "Selecteer een item!" + Environment.NewLine;
            }
            if (columnName == "cmbDbZeldzaamheid" && cmbDbZeldzaamheid.SelectedItem == null)
            {
                return "Selecteer een zeldzaamheid!" + Environment.NewLine;
            }
            if (columnName == "cmbDbCategorie" && cmbDbCategorie.SelectedItem == null)
            {
                return "Selecteer een categorie!" + Environment.NewLine;
            }
            if (columnName == "Boost" && !string.IsNullOrWhiteSpace(txtBoost.Text) && int.TryParse(txtBoost.Text, out int boost) && boost < 0)
            {
                return "Boost moet een positief nummeriek getal zijn!" + Environment.NewLine;
            }
            if (columnName == "Durability" && !string.IsNullOrWhiteSpace(txtDurability.Text) && int.TryParse(txtDurability.Text, out int durability) && durability < 0)
            {
                return "Durability moet een positief nummeriek getal zijn!" + Environment.NewLine;
            }
            return "";
        }
        private void Wissen()
        {
            txtNaam.Text = "";
            txtDurability.Text = "";
            txtBoost.Text = "";
            cmbDbZeldzaamheid.SelectedIndex = -1;
            cmbDbCategorie.SelectedIndex = -1;

        }
        private void ZoekenItems()
        {
            SpecialItemCategorie categorie = cmbCategorie.SelectedItem as SpecialItemCategorie;
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;

            if (categorie != null && cmbCategorie.SelectedIndex != 0 && cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaCategorieEnZeldzaamheid(tbZoekItem.Text, categorie.id, zeldzaamheid);
            }
            else if (categorie != null && cmbCategorie.SelectedIndex != 0)
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaCategorie(tbZoekItem.Text, categorie.id);
            }
            else if (cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaZeldzaamheid(tbZoekItem.Text, zeldzaamheid);
            }
            else
            {
                dbItems.ItemsSource = DatabaseOperations.OphalenSpecialItemsViaNaam(tbZoekItem.Text);
            }
        }
        private int ConversieToInt(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && int.TryParse(text, out int number))
            {
                return number;
            }
            return null;
        }
    }
}
