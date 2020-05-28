using Destiny_DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        List<Item> lijstItems = new List<Item>();

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
                txtDurability.Text = i.Durability.ToString();
                txtBoost.Text = i.Boost.ToString();
                cmbDbCategorie.SelectedItem = i.SpecialItemCategorie;
                cmbDbZeldzaamheid.SelectedItem = i.Item.Zeldzaamheid;
                txtNaam.Text = i.Item.Naam;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst voor te zoeken op zeldzaamheid
            cmbZeldzaamheid.ItemsSource = GeneralItems.ZeldzaamheidLijstVoorZoeken;
            cmbZeldzaamheid.SelectedItem = "All";

            //Lijst van zeldzaamheden om een item toe te voegen
            cmbDbZeldzaamheid.ItemsSource = GeneralItems.ZeldzaamheidLijst;

            //lijst van categoriën om items op te zoeken
            List<SpecialItemCategorie> categorieLijstZoeken = DatabaseOperations.OphalenSpecialItemCategories();
            SpecialItemCategorie categorie = new SpecialItemCategorie();
            categorie.Naam = "All";
            categorieLijstZoeken.Insert(0, categorie);
            cmbCategorie.ItemsSource = categorieLijstZoeken;
            cmbCategorie.DisplayMemberPath = "Naam";
            cmbCategorie.SelectedIndex = 0;

            //lijst van categoriën om een item toe te voegen
            List<SpecialItemCategorie> categorieLijst = DatabaseOperations.OphalenSpecialItemCategories();
            cmbDbCategorie.ItemsSource = categorieLijst;
            cmbDbCategorie.DisplayMemberPath = "Naam";

            ZoekenItems();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            //Lijst maken van items voor equals (item controleren op zeldzaamheid en naam)
            lijstItems = DatabaseOperations.OphalenItems();

            //Valideren 
            string foutmeldingen = ValideerGegevens("cmbDbCategorie");
            foutmeldingen += ValideerGegevens("Boost");
            foutmeldingen += ValideerGegevens("Durability");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //objecten aanmaken om toe te voegen (item en special item)
                
                Item i = new Item();
                SpecialItem si = new SpecialItem();

                OpvullenItem(si, i);

                if (i.IsGeldig())//Kijken als naam en zeldzaamheid zijn ingevuld
                {
                    if (!lijstItems.Contains(i))//Kijken als er al een item bestaat met dezelfde naam en als deze exotic is
                    {
                        int ok = DatabaseOperations.ToevoegenSpecialItem(i, si);
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
            //Lijst maken van items voor equals (item controleren op zeldzaamheid en naam)
            lijstItems = DatabaseOperations.OphalenItems();
            
            //plaats in datagrid van geselecteerd item
            int initialSi = dbItems.SelectedIndex;

            //Valideren
            string foutmeldingen = ValideerGegevens("Boost");
            foutmeldingen += ValideerGegevens("Durability");
            foutmeldingen += ValideerGegevens("cmbDbCategorie");
            foutmeldingen += ValideerGegevens("dbItems");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //Objecten ophalen en andere values erin zetten
                SpecialItem si = dbItems.SelectedItem as SpecialItem;

                lijstItems.Remove(si.Item); //Origineel item uit de lijst verwijderen voor equals 

                ////Gegevens van objecten veranderen (item en special item)
                OpvullenItem(si,si.Item);

                if (si.Item.IsGeldig())//Kijken als naam en zeldzaamheid zijn ingevuld
                {
                    
                    if (!lijstItems.Contains(si.Item))//Kijken als er al een item bestaat met dezelfde naam en als ze allebei exotic zijn
                    {
                        int ok = DatabaseOperations.AanpassenSpecialItems(si.Item, si);

                        if (ok > 0)
                        {
                            //positie van datagrid op -1 zetten
                            initialSi = -1;
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
            //Zorgen dat item nog altijd geselecteerd is als wijzigen niet gelukt is
            dbItems.SelectedIndex = initialSi;
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            //Valideren als er iets geselecteerd is
            string foutmeldingen = ValideerGegevens("dbItems");
            string errors = "";
            int positie = -1;

            //plaats in datagrid van geselecteerd item
            int initialSi = dbItems.SelectedIndex;

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //Zorgen dat men meerdere items kan verwijderen uit database
                for (int i = 0; i < dbItems.SelectedItems.Count; i++)
                {
                    SpecialItem si = dbItems.SelectedItems[i] as SpecialItem;
                    int ok = DatabaseOperations.VerwijderenSpecialItem(si.Item, si);
                    //string opvullen met itemnaam als verwijderen niet gelukt is
                    if (ok > 0)
                    {
                        WissenVelden();
                    }
                    else
                    {
                        positie = initialSi;
                        errors += si.Item.Naam[i] + "is niet verwijderd!" + Environment.NewLine;
                    }
                }
                if (! string.IsNullOrWhiteSpace(errors))
                {
                    MessageBox.Show(errors, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZoekenItems();
            //Zorgen dat item nog altijd geselecteerd is als verwijderen niet gelukt is
            dbItems.SelectedIndex = positie;
        }
        private string ValideerGegevens(string columnName)
        {
            if (columnName == "dbItems" && dbItems.SelectedItem == null)
            {
                return "Selecteer een item!" + Environment.NewLine;
            }
            if (columnName == "cmbDbCategorie" && cmbDbCategorie.SelectedItem == null)
            {
                return "Selecteer een categorie!" + Environment.NewLine;
            }
            if (columnName == "Boost" && !string.IsNullOrWhiteSpace(txtBoost.Text) 
                && int.TryParse(txtBoost.Text, out int boost) && (boost < 0 || boost > 100))
            {
                return "Boost moet een positief nummeriek getal zijn onder de 100!" + Environment.NewLine;
            }
            if (columnName == "Durability" && !string.IsNullOrWhiteSpace(txtDurability.Text) 
                && int.TryParse(txtDurability.Text, out int durability) && (durability < 0 || durability > 100))
            {
                return "Durability moet een positief nummeriek getal zijn onder de 100!" + Environment.NewLine;
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

            //kijken als "All" geselecteerd is in de comboboxen of als er andere zoekcriteria zijn
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

        private void OpvullenItem(SpecialItem si, Item i)
        {
            SpecialItemCategorie sc = cmbDbCategorie.SelectedItem as SpecialItemCategorie;

            si.id = i.id;
            i.Naam = txtNaam.Text;
            i.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
            si.Boost = GeneralItems.ConversieToInt(txtBoost.Text);
            si.Durability = GeneralItems.ConversieToInt(txtDurability.Text);
            si.CategorieId = sc.id;
        }
    }
}
