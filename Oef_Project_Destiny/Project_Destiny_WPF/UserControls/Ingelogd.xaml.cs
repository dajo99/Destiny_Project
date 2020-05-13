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
    /// Interaction logic for Ingelogd.xaml
    /// </summary>
    public partial class Ingelogd : UserControl
    {
        public Ingelogd()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbWelkom.Text += Environment.NewLine + User.Acc.Accountnaam;
        }
    }
}
