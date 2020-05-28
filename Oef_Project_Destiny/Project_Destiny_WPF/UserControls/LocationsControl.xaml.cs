using Destiny_DAL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Locations.xaml
    /// </summary>
    public partial class LocationsUserControl : UserControl
    {
        public LocationsUserControl()
        {
            InitializeComponent();
        }
        List<Locatie> locaties = new List<Locatie>();
        string areaOorspronkelijk = "";
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbWorld.ItemsSource = DatabaseOperations.OphalenWerelden();
            cmbWorld.DisplayMemberPath = "Wereld";

            List<string> toegang = new List<string>() { "False", "True" };
            cmbArea.ItemsSource = toegang;
        }

        private void cmbWorld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string foutmelding = Valideer("cmbUitgevers");
            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                Map map = cmbWorld.SelectedItem as Map;
                dbLocations.ItemsSource = DatabaseOperations.OphalenLocaties(map.id);
            }
            else
            {
                MessageBox.Show(foutmelding);
            }
        }
        private string Valideer(string columnName)
        {
            if (columnName == "cmbWorld" && cmbWorld.SelectedItem == null)
            {
                return "Selecteer een wereld!" + Environment.NewLine;
            }

            if (columnName == "cmbArea" && cmbArea.SelectedItem == null)
            {
                return "Verboden ja of nee!" + Environment.NewLine;
            }

            if (columnName == "Locatie" && dbLocations.SelectedItem == null)
            {
                return "Selecteer een Locatie!" + Environment.NewLine;
            }
            return "";
        }

        private void dbLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dbLocations.SelectedItem is Locatie locatie)
            {
                string omgeving = cmbArea.SelectedItem as string;
                txtLocations.Text = locatie.Naam;
                cmbArea.SelectedItem = locatie.RestrictedArea.ToString();
                omgeving = locatie.RestrictedArea.ToString();
                areaOorspronkelijk = omgeving;
            }
        }

        private void BtnWijzig_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("Locatie");
            foutmelding += Valideer("cmbArea");

            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                Locatie locatie = dbLocations.SelectedItem as Locatie;
                string omgeving = cmbArea.SelectedItem as string;

                locatie.Naam = txtLocations.Text;
                locatie.RestrictedArea = bool.Parse(omgeving);

                if (locatie.IsGeldig())
                {
                    locaties = DatabaseOperations.OphalenLocaties(locatie.MapId);
                    if (!locaties.Contains(locatie) || locatie.RestrictedArea != bool.Parse(areaOorspronkelijk))
                    {
                        int ok = DatabaseOperations.AanpassenLocatie(locatie);
                        if (ok > 0)
                        {
                            dbLocations.ItemsSource = DatabaseOperations.OphalenLocaties(locatie.MapId);
                            Wissen();
                        }
                        else
                        {
                            MessageBox.Show("Locatie is niet aangepast!","Foutmelding",MessageBoxButton.OK,MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Locatie bestaat al!","Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show(locatie.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmelding, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnToeevogen_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("cmbWorld");
            foutmelding += Valideer("cmbArea");

            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                string omgeving = cmbArea.SelectedItem as string;
                Map map = cmbWorld.SelectedItem as Map;

                Locatie locatie = new Locatie();

                locatie.Naam = txtLocations.Text;
                locatie.RestrictedArea = bool.Parse(omgeving);
                locatie.MapId = map.id;

                if (locatie.IsGeldig())
                {

                    locaties = DatabaseOperations.OphalenLocaties(locatie.MapId);

                    if (!locaties.Contains(locatie))
                    {
                        int ok = DatabaseOperations.ToevoegenLocatie(locatie);
                        if (ok > 0)
                        {
                            
                            dbLocations.ItemsSource = DatabaseOperations.OphalenLocaties(locatie.MapId);
                            Wissen();
                        }
                        else
                        {
                            MessageBox.Show("Locatie is niet toegevoegd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Locatie bestaat al!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
                else
                {
                    MessageBox.Show(locatie.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmelding, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("Locatie");
            foutmelding += Valideer("cmbArea");

            if (string.IsNullOrWhiteSpace(foutmelding))
            {

                Locatie locatie = dbLocations.SelectedItem as Locatie;
                int MapId = locatie.MapId;

                int ok = DatabaseOperations.VerwijderenLocatie(locatie);
                if (ok > 0)
                {
                    dbLocations.ItemsSource = DatabaseOperations.OphalenLocaties(MapId);
                    Wissen();
                }
                else
                {
                    MessageBox.Show("Locatie is niet verwijderd!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
            else
            {
                MessageBox.Show(foutmelding, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Wissen()
        {
            txtLocations.Text = "";
            cmbArea.SelectedIndex = -1;
            dbLocations.SelectedIndex = -1;
        }
    }
}
