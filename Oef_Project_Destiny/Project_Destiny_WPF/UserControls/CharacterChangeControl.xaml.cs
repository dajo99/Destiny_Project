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
    /// Interaction logic for Character_Wijzigen.xaml
    /// </summary>
    public partial class CharacterChangeControl : UserControl
    {
        public CharacterChangeControl()
        {
            InitializeComponent();
        }
        MainWindow w = (MainWindow)Application.Current.MainWindow;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            

        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        { 
              
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            w.GridMain.Children.Clear();
            UserControl usc = new CharacterControl();
            w.GridMain.Children.Add(usc);
        }
    }
}
