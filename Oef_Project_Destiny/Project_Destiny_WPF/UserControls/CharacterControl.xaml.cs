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
            if (dtgKarakters.SelectedItem is Character karakter)
            {
                ///gaat het geselecteerde karakter opslagen in de statische klassen. 
                ///dit vereenvoudigd de CRUD update in de andere user-control
                User.Character = karakter;

                w.GridMain.Children.Clear();
                UserControl usc = new CharacterChangeControl();
                w.GridMain.Children.Add(usc);
            }
            


        }

        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            w.GridMain.Children.Clear();
            UserControl usc = new CharacterCreateControl();
            w.GridMain.Children.Add(usc);
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
            
        }

        private void btnStrijden_Click(object sender, RoutedEventArgs e)
        {
            if (dtgKarakters.SelectedItem is Character c)
            {
                Random trommel = new Random();
                int getal = trommel.Next(1, 7);
                if (getal <= 3)
                {
                    c.Level++;
                    SoundPlayer sound = new SoundPlayer("Short_triumphal_fanfare-John_Stracke-815794903.wav");
                    sound.Play();
                    MessageBox.Show("Het level van je karakter is gestegen!");
                    
                    
                }
                else
                {
                    SoundPlayer sound = new SoundPlayer("fail-trombone-01.wav");
                    sound.Play();
                    MessageBox.Show("je karakter heeft de strijd verloren, probeer het weer op een ander moment");
                }
            }
            else
            {
                MessageBox.Show("Selecteer een karakter dat ten strijde gaat!");
            }
        }
    }
}
