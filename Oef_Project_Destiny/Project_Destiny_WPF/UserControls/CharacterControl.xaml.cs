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
using Destiny_DAL;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<Character> karakters = DatabaseOperations.CharactersOphalenViaAccountId(User.Acc.id);

            if (karakters.Count > 0)
            {
                lbCharacters.Items.Clear();
                lbCharacters.Items.Add(User.Acc.Accountnaam + "," + Environment.NewLine
                       + "u beschikt over volgende karakters" + Environment.NewLine + new string('*', 1000) + Environment.NewLine);
                foreach (var item in karakters)
                {
                    lbCharacters.Items.Add("karakter " +  ": " + item.Ras + " " +
                    item.Gender + " " + item.HeadOption + " " + item.Face + " " + item.Marking + " "
                    + item.CharacterKlasse.Naam +  "\n\n"+"Level van het karakter: "
                    + item.Level + "\n\n" + new string('*', 1000));
                   
                }
            }
            
        }

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

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (lbCharacters.SelectedItem is Character character)
            {
                
              int ok = DatabaseOperations.CharacterVerwijderen(character);
                if (ok > 0)
                {
                    MessageBox.Show("Karakter is succesvol verwijderd!", "succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbCharacters.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("niet verwijderd");
                }
            }
            */
        }
    }
}
