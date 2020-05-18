using Destiny_DAL;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Armor.xaml
    /// </summary>
    public partial class ArmorControl : UserControl
    {
        public ArmorControl()
        {
            InitializeComponent();
        }
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
            Wissen();
            if (dbArmor.SelectedItem is Armor a)
            {
                txtDiscipline.Text = a.Discipline.ToString();
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
            GeneralItems.ZeldzaamheidLijst = new List<string>() { "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
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

            //kijken als "All" geselecteerd is in lijst 
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

        private void Wissen()
        {
            txtNaam.Text = "";
            txtDiscipline.Text = "";
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
            GeneralItems.Items = DatabaseOperations.OphalenItems();

            string foutmeldingen = ValideerSelectie("cmbDbZeldzaamheid");
            foutmeldingen += ValideerSelectie("cmbDbArmorSlot");
            foutmeldingen += ValideerTekstToInt(txtDiscipline.Text, "Discipline");
            foutmeldingen += ValideerTekstToInt(txtIntellect.Text, "Intellect");
            foutmeldingen += ValideerTekstToInt(txtMobility.Text, "Mobility");
            foutmeldingen += ValideerTekstToInt(txtRecovery.Text, "Recovery");
            foutmeldingen += ValideerTekstToInt(txtResilience.Text, "Resilience");
            foutmeldingen += ValideerTekstToInt(txtStrength.Text, "Strength");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                string zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
                string armorslot = cmbDbArmorSlot.SelectedItem as string;
                Item i = new Item();
                Armor a = new Armor();
                i.Naam = txtNaam.Text;
                i.Zeldzaamheid = zeldzaamheid;
                a.id = i.id;
                a.ArmorSlot = armorslot;
                a.Discipline = GeneralItems.ConversieToInt(txtDiscipline.Text);
                a.Intellect = GeneralItems.ConversieToInt(txtIntellect.Text);
                a.Mobility = GeneralItems.ConversieToInt(txtMobility.Text);
                a.Recovery = GeneralItems.ConversieToInt(txtRecovery.Text);
                a.Resilience = GeneralItems.ConversieToInt(txtResilience.Text);
                a.Strength = GeneralItems.ConversieToInt(txtStrength.Text);

                if (i.IsGeldig())
                {
                    if (!GeneralItems.Items.Contains(i))
                    {
                        int ok = DatabaseOperations.ToevoegenArmor(i, a);
                        if (ok > 0)
                        {
                            ZoekenArmor();
                            Wissen();
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
            GeneralItems.Items = DatabaseOperations.OphalenItems();

            string foutmeldingen = ValideerSelectie("dbArmor");
            foutmeldingen += ValideerSelectie("cmbDbZeldzaamheid");
            foutmeldingen += ValideerSelectie("cmbDbArmorSlot");
            foutmeldingen += ValideerTekstToInt(txtDiscipline.Text, "Discipline");
            foutmeldingen += ValideerTekstToInt(txtIntellect.Text, "Intellect");
            foutmeldingen += ValideerTekstToInt(txtMobility.Text, "Mobility");
            foutmeldingen += ValideerTekstToInt(txtRecovery.Text, "Recovery");
            foutmeldingen += ValideerTekstToInt(txtResilience.Text, "Resilience");
            foutmeldingen += ValideerTekstToInt(txtStrength.Text, "Strength");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Armor a = dbArmor.SelectedItem as Armor;
                a.Item.Naam = txtNaam.Text;
                a.Item.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
                a.ArmorSlot = cmbDbArmorSlot.SelectedItem as string;
                a.Discipline = GeneralItems.ConversieToInt(txtDiscipline.Text);
                a.Intellect = GeneralItems.ConversieToInt(txtIntellect.Text);
                a.Mobility = GeneralItems.ConversieToInt(txtMobility.Text);
                a.Recovery = GeneralItems.ConversieToInt(txtRecovery.Text);
                a.Resilience = GeneralItems.ConversieToInt(txtResilience.Text);
                a.Strength = GeneralItems.ConversieToInt(txtStrength.Text);

                if (a.Item.IsGeldig())
                {
                    if (!GeneralItems.Items.Contains(a.Item))
                    {
                        int ok = DatabaseOperations.AanpassenArmor(a, a.Item);
                        if (ok > 0)
                        {
                            ZoekenArmor();
                            Wissen();
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
            Wissen();
        }

        private void btnRemoveArmor_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = ValideerSelectie("dbArmor");
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Armor a = dbArmor.SelectedItem as Armor;
                int ok = DatabaseOperations.VerwijderenArmor(a.Item, a);
                if (ok > 0)
                {
                    ZoekenArmor();
                    Wissen();
                }
                else
                {
                    MessageBox.Show("Armor is niet verwijderd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            if (!string.IsNullOrWhiteSpace(tekst) && int.TryParse(tekst, out int number) && number < 0)
            {
                Debug.WriteLine(number + "---" + tekst);
                return columnName + " moet een positief nummeriek getal zijn!" + Environment.NewLine;
            }
            return "";
        }
    }
}
