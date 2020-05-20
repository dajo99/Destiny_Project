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
using Destiny_Models;

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
        List<SpecialItemCategorie> categorieLijst2 = new List<SpecialItemCategorie>();
        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenItems();
        }

        private void cmbZeldzaamheid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenItems();
        }

        private void tbZoekItem_KeyUp(object sender, KeyEventArgs e)
        {
            ZoekenItems();
        }

        private void dbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbItems.SelectedItem is SpecialItem i)
            {
                txtNaam.Text = i.Item.Naam;
                txtDurability.Text = i.Durability.ToString();
                txtBoost.Text = i.Boost.ToString();
                cmbDbCategorie.SelectedItem = i.SpecialItemCategorie;

                //int count = -1;
                /*foreach (SpecialItemCategorie ic in categorieLijst2)
                {
                    //count++;
                    if (ic.Naam == i.SpecialItemCategorie.Naam)
                    {
                        //cmbDbCategorie.SelectedItem = categorieLijst2[count];
                        cmbDbCategorie.SelectedItem = ic;
                        Debug.WriteLine(ic.Naam == i.SpecialItemCategorie.Naam);
                    }
                }*/
                cmbDbZeldzaamheid.SelectedItem = i.Item.Zeldzaamheid;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst voor te zoeken op zeldzaamheid
            List<string> zeldzaamheidlijst = new List<string>() { "All", "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbZeldzaamheid.ItemsSource = zeldzaamheidlijst;
            cmbZeldzaamheid.SelectedItem = "All";

            //Lijst van zeldzaamheden om een item toe te voegen
            GeneralItems.ZeldzaamheidLijst = new List<string>() { "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbDbZeldzaamheid.ItemsSource = GeneralItems.ZeldzaamheidLijst;

            //lijst van categoriën om items op te zoeken
            List<SpecialItemCategorie> categorieLijst = DatabaseOperations.OphalenSpecialItemCategories();
            SpecialItemCategorie categorie = new SpecialItemCategorie();
            categorie.Naam = "All";
            categorieLijst.Insert(0, categorie);
            cmbCategorie.ItemsSource = categorieLijst;
            cmbCategorie.DisplayMemberPath = "Naam";
            cmbCategorie.SelectedIndex = 0;

            //lijst van categoriën om een item toe te voegen
            categorieLijst2 = DatabaseOperations.OphalenSpecialItemCategories();
            cmbDbCategorie.ItemsSource = categorieLijst2;
            cmbDbCategorie.DisplayMemberPath = "Naam";

            ZoekenItems();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            GeneralItems.Items = DatabaseOperations.OphalenItems();
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
                si.Boost = GeneralItems.ConversieToInt(txtBoost.Text);
                si.Durability = GeneralItems.ConversieToInt(txtDurability.Text);
                if (i.IsGeldig())
                {
                    if (!GeneralItems.Items.Contains(i))
                    {
                        int ok = DatabaseOperations.ToevoegenItem(i, si);
                        if (ok > 0)
                        {
                            ZoekenItems();
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

        private void btnChangeItem_Click(object sender, RoutedEventArgs e)
        {
            GeneralItems.Items = DatabaseOperations.OphalenItems();

            string foutmeldingen = Valideer("Boost");
            foutmeldingen += Valideer("Durability");
            foutmeldingen += Valideer("cmbDbZeldzaamheid");
            foutmeldingen += Valideer("cmbDbCategorie");
            foutmeldingen += Valideer("dbItems");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                SpecialItemCategorie c = cmbDbCategorie.SelectedItem as SpecialItemCategorie;
                SpecialItem si = dbItems.SelectedItem as SpecialItem;
                GeneralItems.Items.Remove(si.Item);

                si.Item.Naam = txtNaam.Text;
                si.Item.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
                si.Boost = GeneralItems.ConversieToInt(txtBoost.Text);
                si.Durability = GeneralItems.ConversieToInt(txtDurability.Text);

                    si.CategorieId = c.id;
                    si.SpecialItemCategorie = c;

                Debug.WriteLine(c.Naam + " " + si.id + "====" + si.Item.id + "-" + si.CategorieId + "===" + si.SpecialItemCategorie.id + "-" + si.SpecialItemCategorie.Naam + "===" + c.Naam);

                if (si.Item.IsGeldig())
                {
                    
                    if (!GeneralItems.Items.Contains(si.Item))
                    {
                        int ok = DatabaseOperations.AanpassenSpecialItems(si.Item, si);

                        if (ok > 0)
                        {
                            Debug.WriteLine(c.Naam + " " + si.id + "-" + si.CategorieId + "-" + si.Boost + "-" + si.Durability + "-" + si.Item.Naam + "-" + si.Item.Zeldzaamheid);
                            ZoekenItems();
                            WissenVelden();
                        }

                        else
                        {
                            MessageBox.Show("SpecialItem is niet gewijzigd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Een exotic item met dezelfde naam kan niet 2x voorkomen!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show(si.Item.Error, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ZoekenItems();
            WissenVelden();
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
                    WissenVelden();
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
        private void WissenVelden()
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

    }
}
