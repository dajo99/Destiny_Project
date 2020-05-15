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
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class WeaponsControl : UserControl
    {
        public WeaponsControl()
        {
            InitializeComponent();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("Enter key pressed");
            }
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCategorie.ItemsSource = DatabaseOperations.OphalenCategorie();
            cmbCategorie.DisplayMemberPath = "Soort";
            cmbZeldzaamheid.ItemsSource = DatabaseOperations.OphalenZeldzaamheid();
            cmbZeldzaamheid.DisplayMemberPath = "Zeldzaamheid";

        }

        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbZeldzaamheid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dbWapens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
