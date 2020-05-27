using Destiny_DAL;
using Destiny_Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Armor.xaml
    /// </summary>
    public partial class ArmorControl : UserControl
    {
        public ArmorControl()
        {
            InitializeComponent();
        }

        List<Item> lijstItems = new List<Item>();
        private void tbZoekArmor_KeyUp(object sender, KeyEventArgs e)
        {
            ZoekenArmor();
        }
        private void cmbZeldzaamheid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenArmor();
        }
        private void cmbArmorSlot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ZoekenArmor();
        }
        private void dbArmor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbArmor.SelectedItem is Armor a)
            {
                txtIntellect.Text = a.Intellect.ToString();
                txtMobility.Text = a.Mobility.ToString();
                txtRecovery.Text = a.Recovery.ToString();
                txtResilience.Text = a.Resilience.ToString();
                txtStrength.Text = a.Strength.ToString();

                txtNaam.Text = a.Item.Naam;
                cmbDbArmorSlot.SelectedItem = a.ArmorSlot;
                cmbDbZeldzaamheid.SelectedItem = a.Item.Zeldzaamheid;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> zeldzaamheidlijst = new List<string>() { "All", "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbZeldzaamheid.ItemsSource = zeldzaamheidlijst;
            cmbZeldzaamheid.SelectedItem = "All";

            //Lijst van zeldzaamheden om een item toe te voegen
            cmbDbZeldzaamheid.ItemsSource = GeneralItems.ZeldzaamheidLijst;

            //Lijst van armor slots 
            List<string> Armorslots = new List<string>() { "Helmet", "Gauntlets", "Chest Armor", "Leg Armor", "Class Item", "Artifact" };
            cmbDbArmorSlot.ItemsSource = Armorslots;

            List<string> ArmorslotsZoeken = new List<string>() { "All", "Helmet", "Gauntlets", "Chest Armor", "Leg Armor", "Class Item", "Artifact" };
            cmbArmorSlot.ItemsSource = ArmorslotsZoeken;
            cmbArmorSlot.SelectedItem = "All";

            ZoekenArmor();
        }

        private void ZoekenArmor()
        {
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;
            string armorslot = cmbArmorSlot.SelectedItem as string;

            //Checken als "All" geselecteerd is in de comboboxen of als er zoekcriteria zijn
            if (cmbArmorSlot.SelectedIndex != 0 && cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbArmor.ItemsSource = DatabaseOperations.OphalenArmorViaArmorSlotEnZeldzaamheid(tbZoekArmor.Text, armorslot, zeldzaamheid);
            }
            else if (cmbArmorSlot.SelectedIndex != 0)
            {
                dbArmor.ItemsSource = DatabaseOperations.OphalenArmorViaArmorslot(tbZoekArmor.Text, armorslot);
            }
            else if (cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbArmor.ItemsSource = DatabaseOperations.OphalenArmorViaZeldzaamheid(tbZoekArmor.Text, zeldzaamheid);
            }
            else
            {
                dbArmor.ItemsSource = DatabaseOperations.OphalenArmorViaNaam(tbZoekArmor.Text);
            }
        }

        private void VeldenWissen()
        {
            txtNaam.Text = "";
            txtIntellect.Text = "";
            txtMobility.Text = "";
            txtRecovery.Text = "";
            txtResilience.Text = "";
            txtStrength.Text = "";
            cmbDbArmorSlot.SelectedIndex = -1;
            cmbDbZeldzaamheid.SelectedIndex = -1;
        }

        private void btnAddArmor_Click(object sender, RoutedEventArgs e)
        {
            ////Lijst maken van items voor equals (item controleren op zeldzaamheid en naam)
            lijstItems = DatabaseOperations.OphalenItems();

            //valideren
            string foutmeldingen = ValideerSelectie("cmbDbZeldzaamheid");
            foutmeldingen += ValideerSelectie("cmbDbArmorSlot");
            foutmeldingen += ValideerTekstToInt(txtIntellect.Text, "Intellect");
            foutmeldingen += ValideerTekstToInt(txtMobility.Text, "Mobility");
            foutmeldingen += ValideerTekstToInt(txtRecovery.Text, "Recovery");
            foutmeldingen += ValideerTekstToInt(txtResilience.Text, "Resilience");
            foutmeldingen += ValideerTekstToInt(txtStrength.Text, "Strength");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //objecten aanmaken om toe te voegen (Armor en special item)
                Item i = new Item();
                Armor a = new Armor();

                OpvullenArmor(a, i);

                if (i.IsGeldig()) //Kijken als naam en zeldzaamheid zijn ingevuld
                {
                    if (!lijstItems.Contains(i))//Kijken als er al een item bestaat met dezelfde naam en als ze allebei exotic zijn
                    {
                        int ok = DatabaseOperations.ToevoegenArmor(i, a);
                        if (ok > 0)
                        {
                            ZoekenArmor();
                            VeldenWissen();
                        }
                        else
                        {
                            MessageBox.Show("Armor is niet toegevoegd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnChangeArmor_Click(object sender, RoutedEventArgs e)
        {
            //Lijst maken van items voor equals (item wordt gecontroleerd op zeldzaamheid en naam)
            lijstItems = DatabaseOperations.OphalenItems();

            //plaats van geselecteerd armor in datagrid
            int initialA = dbArmor.SelectedIndex;

            //valideren
            string foutmeldingen = ValideerSelectie("dbArmor");
            foutmeldingen += ValideerSelectie("cmbDbZeldzaamheid");
            foutmeldingen += ValideerSelectie("cmbDbArmorSlot");
            foutmeldingen += ValideerTekstToInt(txtIntellect.Text, "Intellect");
            foutmeldingen += ValideerTekstToInt(txtMobility.Text, "Mobility");
            foutmeldingen += ValideerTekstToInt(txtRecovery.Text, "Recovery");
            foutmeldingen += ValideerTekstToInt(txtResilience.Text, "Resilience");
            foutmeldingen += ValideerTekstToInt(txtStrength.Text, "Strength");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Armor a = dbArmor.SelectedItem as Armor;

                //Origineel item uit de lijst verwijderen voor equals
                lijstItems.Remove(a.Item);

                //Gegevens van objecten aanpassen (item en armor)
                OpvullenArmor(a, a.Item);

                if (a.Item.IsGeldig()) //Kijken als naam en zeldzaamheid zijn ingevuld
                {
                    if (!lijstItems.Contains(a.Item)) //Kijken als er al een item bestaat met dezelfde naam en als ze allebei exotic zijn
                    {
                        int ok = DatabaseOperations.AanpassenArmor(a, a.Item);
                        if (ok > 0)
                        {
                            //geselecteerde armor in dtaagrid terug op -1 zetten
                            initialA = -1;
                            VeldenWissen();
                        }
                        else
                        {
                            MessageBox.Show("Armor is niet gewijzigd!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Een exotic item met dezelfde naam kan niet 2x voorkomen!", "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(a.Item.Error, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen, "Foutmeldingen", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ZoekenArmor();
            dbArmor.SelectedIndex = initialA;

        }

        private void btnRemoveArmor_Click(object sender, RoutedEventArgs e)
        {
            //Checken als er iets geselecteerd is in datagrid
            string foutmeldingen = ValideerSelectie("dbArmor");
            string errors = "";

            //plaats in datagrid van geselecteerd armor
            int initialA = dbArmor.SelectedIndex;

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //Zorgen dat men meerdere items kan verwijderen uit database
                for (int i = 0; i < dbArmor.SelectedItems.Count; i++)
                {
                    Armor a = dbArmor.SelectedItems[i] as Armor;

                    int ok = DatabaseOperations.VerwijderenArmor(a.Item, a);
                    if (ok > 0)
                    {
                        // positie selectie resetten in datagrid
                        initialA = -1;
                        VeldenWissen();
                    }
                    else
                    {
                        //string opvullen met itemnaam als verwijderen niet gelukt is
                        errors += a.Item.Naam[i] +" is niet verwijderd!" + Environment.NewLine;
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

            ZoekenArmor();
            dbArmor.SelectedIndex = initialA;
        }

        private string ValideerSelectie(string columnName)
        {
            if (columnName == "dbArmor" && dbArmor.SelectedItem == null)
            {
                return "Selecteer een Armorstuk!" + Environment.NewLine;
            }
            if (columnName == "cmbDbZeldzaamheid" && cmbDbZeldzaamheid.SelectedItem == null)
            {
                return "Selecteer een zeldzaamheid!" + Environment.NewLine;
            }
            if (columnName == "cmbDbArmorSlot" && cmbDbArmorSlot.SelectedItem == null)
            {
                return "Selecteer een armorslot!" + Environment.NewLine;
            }

            return "";
        }
        private string ValideerTekstToInt(string tekst, string columnName)
        {
            if (!string.IsNullOrWhiteSpace(tekst) && int.TryParse(tekst, out int number)
                && (number < 0 || number > 100))
            {
                return columnName + " moet een positief nummeriek getal zijn onder de 100!" + Environment.NewLine;
            }

            return "";
        }

        private void OpvullenArmor(Armor a, Item i)
        {
            a.id = i.id;
            i.Naam = txtNaam.Text;
            i.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
            a.ArmorSlot = cmbDbArmorSlot.SelectedItem as string;
            a.Intellect = GeneralItems.ConversieToInt(txtIntellect.Text);
            a.Mobility = GeneralItems.ConversieToInt(txtMobility.Text);
            a.Recovery = GeneralItems.ConversieToInt(txtRecovery.Text);
            a.Resilience = GeneralItems.ConversieToInt(txtResilience.Text);
            a.Strength = GeneralItems.ConversieToInt(txtStrength.Text);
        }
    }
}
