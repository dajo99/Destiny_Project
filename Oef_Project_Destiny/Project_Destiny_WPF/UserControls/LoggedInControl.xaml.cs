using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            SolidColorBrush s;

            switch (User.Acc.ThemaColor)//Kijken welk thema de user gebruikt volgens settingscherm
            {
                case "DeepPurple":
                    s = new SolidColorBrush(Colors.IndianRed);
                    break;

                default :
                    s = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00C7A3"));//Teal
                    break;
            }

            lblAccountnaam.Content = User.Acc.Accountnaam;
            lblAccountnaam.Foreground = s;
        }
    }
}
