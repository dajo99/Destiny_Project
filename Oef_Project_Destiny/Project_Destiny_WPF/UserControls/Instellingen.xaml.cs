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
    public partial class Instellingen : UserControl
    {
        public Instellingen()
        {
            InitializeComponent();
        }

        private void BtnOpslaanInstellingen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            var bc = new BrushConverter();
            Uri uri = null;


            if (cbFont.SelectedIndex == 0)
            {
                //gaat aanpassingen doen in app.xaml
                App.Current.Resources["font"] = new FontFamily("Segoe UI");
                

            }

            if (cbFont.SelectedIndex == 1)
            { 
                //gaat aanpassingen doen in app.xaml
                App.Current.Resources["font"] = new FontFamily("Comic Sans MS");

            }

           

            if (cbLayout.SelectedIndex == 0)
            {
                //Zo kan ik met Hex - kleurwaarden werken
                mw.GridNav.Background = (Brush)bc.ConvertFrom("#FF00C7A3");
                mw.GridMenu.Background = (Brush)bc.ConvertFrom("#FF00C7A3");

                
                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Teal.xaml");

            }

            if (cbLayout.SelectedIndex == 1)
            {
                mw.GridNav.Background = Brushes.IndianRed;
                mw.GridMenu.Background = Brushes.IndianRed;

                uri = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml");
                
            }

            //gaat aanpassingen doen in app.xaml
            System.Windows.Application.Current.Resources.MergedDictionaries.RemoveAt(3);
            System.Windows.Application.Current.Resources.MergedDictionaries.Insert(3, new ResourceDictionary() { Source = uri });

        }
    }
}