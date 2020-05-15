using Destiny_DAL;
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
    /// Interaction logic for Locations.xaml
    /// </summary>
    public partial class LocationsUserControl : UserControl
    {
        public LocationsUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbWorld.ItemsSource = DatabaseOperations.OphalenWerelden();
            cmbWorld.DisplayMemberPath = "Wereld";
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
                return "Selecteer een uitgever!" + Environment.NewLine;
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
                txtLocations.Text = locatie.Naam;
                txtArea.Text = locatie.RestrictedArea.ToString();
                
                
            }
        }

        private void BtnWijzig_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("Locatie");

            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                Locatie locatie = dbLocations.SelectedItem as Locatie;

                locatie.Naam = txtLocations.Text;
                locatie.RestrictedArea = bool.Parse(txtArea.Text);

                if (locatie.IsGeldig())
                {
                    int ok = DatabaseOperations.AanpassenLocatie(locatie);
                    if (ok > 0)
                    {
                        dbLocations.ItemsSource = DatabaseOperations.OphalenLocaties(locatie.MapId);
                        Wissen();
                    }
                    else
                    {
                        MessageBox.Show("Locatie is niet aangepast!");
                    }
                }
                else
                {
                    MessageBox.Show(locatie.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmelding);
            }
        }

        private void BtnToeevogen_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("cmbWorld");
            
            if (string.IsNullOrWhiteSpace(foutmelding))
            {
                Map map = cmbWorld.SelectedItem as Map;

                Locatie locatie = new Locatie();

                locatie.Naam = txtLocations.Text;
                locatie.RestrictedArea = bool.Parse(txtArea.Text);
                locatie.MapId = map.id;

                if (locatie.IsGeldig())
                {
                    int ok = DatabaseOperations.ToevoegenLocatie(locatie);
                    if (ok > 0)
                    {
                        dbLocations.ItemsSource = DatabaseOperations.OphalenLocaties(locatie.MapId);
                        Wissen();
                    }
                    else
                    {
                        MessageBox.Show("Locatie is niet toegevoegd!");
                    }
                }
                else
                {
                    MessageBox.Show(locatie.Error);
                }

            }
            else
            {
                MessageBox.Show(foutmelding);
            }
        }

        

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            string foutmelding = Valideer("Locatie");

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
                    MessageBox.Show("Locatie is niet verwijderd!");
                }


            }
            else
            {
                MessageBox.Show(foutmelding);
            }

        }

        private void Wissen()
        {
            txtLocations.Text = "";
            txtArea.Text = "False";
            dbLocations.SelectedIndex = -1;
        }
    }
}
