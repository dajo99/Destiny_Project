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
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class WeaponsControl : UserControl
    {
        public WeaponsControl()
        {
            InitializeComponent();
        }
        List<Item> lijstItems = new List<Item>();
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ZoekenWapens();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst voor te zoeken op zeldzaamheid
            cmbZeldzaamheid.ItemsSource = GeneralItems.ZeldzaamheidLijstVoorZoeken;
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
            //Lijst maken van items voor equals (item controleren op zeldzaamheid en naam)
            lijstItems = DatabaseOperations.OphalenItems();
            string foutmeldingen = Valideer("cmbDbZeldzaamheid");
            foutmeldingen += Valideer("cmbDbCategorie");
            foutmeldingen += Valideer("cmbDbDamageType");
            foutmeldingen += Valideer("Impact");
            foutmeldingen += Valideer("Magazine");
            foutmeldingen += Valideer("Light");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Item it = new Item();
                Wapen wa = new Wapen();

                OpvullenWapen(it, wa);


                if (it.IsGeldig()) 
                {
                    if (!lijstItems.Contains(it))//Kijken als er al een item bestaat met dezelfde naam en als ze allebei exotic zijn
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
            lijstItems = DatabaseOperations.OphalenItems();

            //plaats in datagrid van geselecteerd wapen
            int initialW = dbWapens.SelectedIndex;

            string foutmeldingen = Valideer("dbWapens");
            foutmeldingen += Valideer("Impact");
            foutmeldingen += Valideer("Magazine");
            foutmeldingen += Valideer("Light");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Wapen w = dbWapens.SelectedItem as Wapen;

                lijstItems.Remove(w.Item);//Origineel item uit de lijst verwijderen voor equals

                OpvullenWapen(w.Item, w);

                if (w.Item.IsGeldig())
                {

                    if (!lijstItems.Contains(w.Item))
                    {
                        int ok = DatabaseOperations.AanpassenWapens(w.Item, w);

                        if (ok > 0)
                        {
                            initialW = -1;
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
            dbWapens.SelectedIndex = initialW;
        }

        private void btnRemoveWeapon_Click(object sender, RoutedEventArgs e)
        {
            //Checken als er iets geselecteerd is in datagrid
            string foutmeldingen = Valideer("dbWapens");
            string errors = "";

            int initialW = dbWapens.SelectedIndex;

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //Zorgen dat men meerdere items kan verwijderen uit database
                for (int i = 0; i < dbWapens.SelectedItems.Count; i++)
                {
                    Wapen w = dbWapens.SelectedItems[i] as Wapen;
                    int ok = DatabaseOperations.VerwijderenWapen(w.Item, w);
                    if (ok > 0)
                    {
                        initialW = -1;
                        WissenVelden();
                    }
                    else
                    {
                        //string opvullen met itemnaam als verwijderen niet gelukt is
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
            dbWapens.SelectedIndex = initialW;
        }

        private string Valideer(string columnName)
        {
            int max = 100;

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
            if (columnName == "Impact" && !string.IsNullOrWhiteSpace(txtImpact.Text) 
                && (!int.TryParse(txtImpact.Text, out int impact) || impact < 0 || impact > max))
            {
                return "Impact moet een positief nummeriek getal zijn onder de " + max + "!" + Environment.NewLine;
            }
            if (columnName == "Magazine" && !string.IsNullOrWhiteSpace(txtMagazine.Text) 
                && (!int.TryParse(txtMagazine.Text, out int magazine) || magazine < 0 || magazine > max))
            {
                return "Magazine moet een positief nummeriek getal zijn onder de " + max + "!" + Environment.NewLine;
            }
            if (columnName == "Light" && !string.IsNullOrWhiteSpace(txtLight.Text) 
                && (!int.TryParse(txtLight.Text, out int light) || light < 0))
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

        private void OpvullenWapen(Item i, Wapen w)
        {
            Wapenklasse wk = cmbDbCategorie.SelectedItem as Wapenklasse;
            Damagetype dt = cmbDbDamageType.SelectedItem as Damagetype;

            i.id = w.id;
            i.Naam = txtNaam.Text;
            i.Zeldzaamheid = cmbDbZeldzaamheid.SelectedItem as string;
            w.Impact = GeneralItems.ConversieToInt(txtImpact.Text);
            w.Magazine = GeneralItems.ConversieToInt(txtMagazine.Text);
            w.LightAmount = GeneralItems.ConversieToInt(txtLight.Text);
            w.WapenklasseId = wk.id;
            w.DamagetypeId = dt.id;
        }
    }
}
