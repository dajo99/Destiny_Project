using System;
using System.Media;
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
using Destiny_Models;
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

            dtgKarakters.ItemsSource = karakters;


        }

        private void btnAanpassen_Click(object sender, RoutedEventArgs e)
        {


            if (dtgKarakters.SelectedItem is Character c)
            {
                if (c.Level <= 9)
                {
                    MessageBox.Show("Sorry," + "\n" + "de mogenlijkheid om het uiterlijk van je karakter te wijzigen is pas beschikbaar vanaf dat het geselecteerde karakter level 10 heeft bereikt");
                }

                else
                {
                    ///gaat het geselecteerde karakter opslagen in de statische klassen. 
                    ///dit vereenvoudigd de CRUD update in de andere user-control
                    User.Character = c;
                    w.GridMain.Children.Clear();
                    UserControl usc = new CharacterChangeControl();
                    w.GridMain.Children.Add(usc);
                }
            }

            else
            {
                MessageBox.Show("Selecteer eerst het karakter dat je wil wijzigen!");
            }
        }





        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {

            if (dtgKarakters.Items.Count < 3)
            {
                w.GridMain.Children.Clear();
                UserControl usc = new CharacterCreateControl();
                w.GridMain.Children.Add(usc);
            }

            else
            {
                MessageBox.Show("Een account kan maximaal 3 characters hebben!","limiet bereikt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {

            if (dtgKarakters.SelectedItem is Character character)
            {

                int ok = DatabaseOperations.CharacterVerwijderen(character);
                if (ok > 0)
                {
                    MessageBox.Show("Karakter is succesvol verwijderd!", "succes", MessageBoxButton.OK, MessageBoxImage.Information);
                    dtgKarakters.ItemsSource = DatabaseOperations.CharactersOphalenViaAccountId(User.Acc.id);
                }
                else
                {
                    MessageBox.Show("niet verwijderd");
                }
            }
            else
            {
                MessageBox.Show("Eerst een karakter selecteren om te verwijderen!", "halt!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }

        }

        private void btnStrijden_Click(object sender, RoutedEventArgs e)
        {
            if (dtgKarakters.SelectedItem is Character c)
            {
                User.Character = c;
                CustomMessageBox b = new CustomMessageBox();
                b.ShowDialog();
                c = User.Character;
                DatabaseOperations.CharacterUpdaten(c);
                dtgKarakters.ItemsSource = DatabaseOperations.CharactersOphalenViaAccountId(User.Acc.id);
            }
            else
            {
                MessageBox.Show("Selecteer een karakter dat ten strijde gaat!");
            }
        }
    }
}
