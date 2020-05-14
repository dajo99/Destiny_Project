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
using Project_Destiny_WPF.UserControls;
namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Character.xaml
    /// </summary>
    public partial class CharacterControl : UserControl
    {
        public CharacterControl()
        {
            InitializeComponent();
        }

        MainWindow w = (MainWindow)Application.Current.MainWindow;

        private void btnAanpassen_Click(object sender, RoutedEventArgs e)
        {

            w.GridMain.Children.Clear();
            UserControl usc = new CharacterChangeControl();
            w.GridMain.Children.Add(usc);


        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            w.GridMain.Children.Clear();
            UserControl usc = new CharacterCreateControl();
            w.GridMain.Children.Add(usc);
        }
    }
}
