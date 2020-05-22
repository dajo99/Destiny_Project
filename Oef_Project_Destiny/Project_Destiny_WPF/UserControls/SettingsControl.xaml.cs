using Destiny_DAL;
using Destiny_Models;
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
    /// Interaction logic for Instellingen.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private void BtnOpslaanInstellingen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = (MainWindow)Application.Current.MainWindow;
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            var bc = new BrushConverter();
            Uri uri = null;
            Account a;
            string fontKeuze = "";
            string layoutKeuze = "";

            int font = cbFont.SelectedIndex;
            switch (font)
            {
                case 0:
                    //gaat aanpassingen doen in app.xaml
                    App.Current.Resources["font"] = new FontFamily("Segoe UI");
                    fontKeuze = "Segoe UI";
                    break;

                case 1:
                    //gaat aanpassingen doen in app.xaml
                    App.Current.Resources["font"] = new FontFamily("Comic Sans MS");
                    fontKeuze = "Comic Sans MS";
                    break;
                
            }

            
            int layout = cbLayout.SelectedIndex;
            switch (layout)
            {
                case 0:
                    //Zo kan ik met Hex - kleurwaarden werken
                    mw.GridNav.Background = (Brush)bc.ConvertFrom("#FF00C7A3");
                    mw.GridMenu.Background = (Brush)bc.ConvertFrom("#FF00C7A3");


                    uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");
                    layoutKeuze = "Teal";
                    break;

                case 1:
                    mw.GridNav.Background = Brushes.IndianRed;
                    mw.GridMenu.Background = Brushes.IndianRed;

                    uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml");
                    layoutKeuze = "DeepPurple";
                    break;

            }


            //gaat aanpassingen doen in app.xaml
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(3);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });

            //Account van user ophalen uit database
            a = DatabaseOperations.OphalenAccount(User.Acc.Accountnaam);

            //Account opvullen met nieuwe invoer
            a.ThemaFont = fontKeuze;
            a.ThemaColor = layoutKeuze;

            if (a.IsGeldig())
            {
                List<Account> accounts = DatabaseOperations.CheckLogin(User.Acc);
                if (MessageBox.Show("Bent u zeker dat u deze wijzigingen wilt uitvoeren?", "Waarschuwing", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
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
                    
            }
            else
            {
                MessageBox.Show(a.Error, "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           

            

        }
    }
}