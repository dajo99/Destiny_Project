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
    public partial class Character_Wijzigen : UserControl
    {
        public Character_Wijzigen()
        {
            InitializeComponent();
        }
        public Window Parent
        {
            get;
            set;
        }
        public Character_Wijzigen(Window parent)
        {
            this.Parent = parent;
            InitializeComponent();
        }
        
       
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            

        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        { 
              
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = (MainWindow)Parent;

            w.GridMain.Children.Clear();
            UserControl usc = new Character(w);
            w.GridMain.Children.Add(usc);
        }
    }
}
