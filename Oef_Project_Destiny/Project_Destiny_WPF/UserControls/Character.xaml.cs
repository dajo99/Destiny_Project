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
    public partial class Character : UserControl
    {
        public Character()
        {
            InitializeComponent();
        }

        public Window Parent { get; set; }

        public Character(Window parent)
        {
            this.Parent = parent;
            InitializeComponent();
        }
        private void btnAanpassen_Click(object sender, RoutedEventArgs e)
        {

            MainWindow w = (MainWindow)Parent;
            w.GridMain.Children.Clear();
            UserControl usc = new Character_Wijzigen(w);
            w.GridMain.Children.Add(usc);


        }
    }
}
