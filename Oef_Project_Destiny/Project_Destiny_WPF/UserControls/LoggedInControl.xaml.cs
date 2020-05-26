using System;
using System.Windows;
using System.Windows.Controls;
using Destiny_Models;

namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Ingelogd.xaml
    /// </summary>
    public partial class LoggedInControl : UserControl
    {
        public LoggedInControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbWelkom.Text += Environment.NewLine + User.Acc.Accountnaam;
        }
    }
}
