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
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        public AccountControl()
        {
            InitializeComponent();
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtProfielnaam.Text = "Kevin";
            ResetVelden();
        }


        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            ResetVelden();
        }

        private void ResetVelden()
        {
            txtAchternaam.IsEnabled = false;
            txtVoornaam.IsEnabled = false;
            txtMail.IsEnabled = false;         
            cmbRegio.IsEnabled = false;
            txtProfielnaam.IsEnabled = false;
            txtWachtwoord.IsEnabled = false;
            btnOpslaan.IsEnabled = false;
            btnWijzigen.IsEnabled = true;
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnWijzigen_Click_1(object sender, RoutedEventArgs e)
        {
            txtAchternaam.IsEnabled = true;
            txtVoornaam.IsEnabled = true;
            txtMail.IsEnabled = true;
            cmbRegio.IsEnabled = true;
            txtProfielnaam.IsEnabled = true;
            txtWachtwoord.IsEnabled = true;
            btnOpslaan.IsEnabled = true;
            btnWijzigen.IsEnabled = false;
        }
    }
}
