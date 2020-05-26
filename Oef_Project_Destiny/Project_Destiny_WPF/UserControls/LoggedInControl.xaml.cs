using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Destiny_DAL;
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
            Account a = DatabaseOperations.OphalenAccount(User.Acc.Accountnaam);
            User.Acc = a;
            SolidColorBrush s = new SolidColorBrush();

            switch (User.Acc.ThemaColor)
            {
                case "Teal":
                    s = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00C7A3"));
                    break;
                case "DeepPurple":
                    s = new SolidColorBrush(Colors.IndianRed);
                    break;
            }

            lblAccountnaam.Content = User.Acc.Accountnaam;
            lblAccountnaam.Foreground = s;
        }
    }
}
