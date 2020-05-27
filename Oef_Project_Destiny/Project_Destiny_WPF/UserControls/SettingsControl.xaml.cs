using Destiny_DAL;
using Destiny_Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Project_Destiny_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for Instellingen.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        string font = "";
        string themaColor = "";
        MainWindow w = (MainWindow)Application.Current.MainWindow;

        private void BtnOpslaanInstellingen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bent u zeker dat u deze wijzigingen wilt uitvoeren?", "Waarschuwing", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                Account a;
                int fontindex = cbFont.SelectedIndex;
                switch (fontindex)
                {
                    case 0:
                        font = "Segoe UI";
                        SetFont(font);
                        break;

                    case 1:
                        font = "Comic Sans MS";
                        SetFont(font);
                        break;

                }

                int layoutIndex = cbLayout.SelectedIndex;
                switch (layoutIndex)
                {
                    case 0:
                        themaColor = "Teal";
                        SetThemeColor(themaColor);
                        break;

                    case 1:
                        themaColor = "DeepPurple";
                        SetThemeColor(themaColor);
                        break;

                }

                //Account van user ophalen uit database
                a = DatabaseOperations.OphalenAccount(User.Acc.Accountnaam);

                //Account opvullen met nieuwe invoer
                a.ThemaFont = font;
                a.ThemaColor = themaColor;

                if (a.IsGeldig())
                {

                    int ok = DatabaseOperations.WijzigenAccount(a);
                    if (ok > 0)
                    {
                        // User terug updaten met nieuwe gegevens
                        User.Acc = a;
                        //UserControl terug refreshen
                        w.GridMain.Children.Clear();
                        UserControl usc = new SettingsControl();
                        w.GridMain.Children.Add(usc);
                    }
                    else
                    {
                        MessageBox.Show("De instellingen zijn niet aangepast!", "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show(a.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SetFont(string font)
        {
            
            switch (font)
            {
                case "Segoe UI":
                    //gaat aanpassingen doen in app.xaml
                    App.Current.Resources["font"] = new FontFamily(font);

                    break;

                case "Comic Sans MS":
                    //gaat aanpassingen doen in app.xaml
                    App.Current.Resources["font"] = new FontFamily(font);
                    break;

            }
        }

        private void SetThemeColor(string color)
        {
            Uri uri = null;
            var bc = new BrushConverter();
            switch (color)
            {
                case "Teal":
                    //Zo kan ik met Hex - kleurwaarden werken
                    w.GridNav.Background = (Brush)bc.ConvertFrom("#FF00C7A3");
                    w.GridMenu.Background = (Brush)bc.ConvertFrom("#FF00C7A3");

                    uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
                    break;

                case "DeepPurple":
                    w.GridNav.Background = Brushes.IndianRed;
                    w.GridMenu.Background = Brushes.IndianRed;

                    uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml");
                    break;

            }
            //gaat aanpassingen doen in app.xaml
            Application.Current.Resources.MergedDictionaries.RemoveAt(3);
            Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });
        }
    }
}