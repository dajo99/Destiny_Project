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
        //om usercontrols in de maingrid te kunnen inladen
        MainWindow w = (MainWindow)Application.Current.MainWindow;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //gaat characters ophalen uit de database aan de hand van de account id
            List<Character> karakters = DatabaseOperations.CharactersOphalenViaAccountId(User.Acc.id);
            
            //vult de datagrid van de usercontrol op 
            dtgKarakters.ItemsSource = karakters;


        }

        private void btnAanpassen_Click(object sender, RoutedEventArgs e)
        {

            //gaat na welk character er is geselecteerd om aan te passen
            if (dtgKarakters.SelectedItem is Character c)
            {
                //gaat het level van het character controleren
                if (c.Level <= 9)
                {
                    MessageBox.Show("Sorry," + "\n" + "de mogenlijkheid om het uiterlijk van je karakter te wijzigen is pas beschikbaar vanaf dat het geselecteerde karakter level 10 heeft bereikt");
                }

                else
                {
                    //gaat het geselecteerde karakter opslagen in de statische klassen. 
                    //dit vereenvoudigd de CRUD update in de andere user-control
                    User.Character = c;
                    w.GridMain.Children.Clear();
                    UserControl usc = new CharacterChangeControl();
                    w.GridMain.Children.Add(usc);
                }
            }
            //als er geen character is geselecteerd
            else
            {
                MessageBox.Show("Selecteer eerst het karakter dat je wil wijzigen!");
            }
        }





        private void btnAanmaken_Click(object sender, RoutedEventArgs e)
        {
            //gaat kijken hoeveel characters de datagrid al bevat
            if (dtgKarakters.Items.Count < 3)
            {
                w.GridMain.Children.Clear();
                UserControl usc = new CharacterCreateControl();
                w.GridMain.Children.Add(usc);
            }
            //als er al 3 characters zijn
            else
            {
                MessageBox.Show("Een account kan maximaal 3 characters hebben!","limiet bereikt", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            //gaat kijken of er een karakter is geselecteerd om te verwijderen
            if (dtgKarakters.SelectedItem is Character character)
            {
                //gaat de returnwaarde van de savechanges opvangen
                
                if (MessageBox.Show("bent u zeker dat u het karakter wilt verwijderen?", "bevestiging", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes )
                {

                    int ok = DatabaseOperations.CharacterVerwijderen(character);
                    //controleerd of de returnwaarde van savechanges groter is dan 0
                    if (ok > 0)
                {
                    MessageBox.Show("Karakter is succesvol verwijderd!", "succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    //gaat de datagrid herinstellen
                    dtgKarakters.ItemsSource = DatabaseOperations.CharactersOphalenViaAccountId(User.Acc.id);
                }
                //als het verwijderen niet is gelukt
                else
                {
                    MessageBox.Show("karakter is niet verwijderd!");
                }

                }
            }
            //als er geen karakter geselecteerd is
            else
            {
                MessageBox.Show("Eerst een karakter selecteren om te verwijderen!", "halt!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }

        }

        private void btnStrijden_Click(object sender, RoutedEventArgs e)
        {
            //gaat kijken of er een karakter geselecteerd in de datagrid
            if (dtgKarakters.SelectedItem is Character c)
            {
                //gaat de statische property character instellen op het geselecteerde karakter zodat deze in SteenPapierSchaar.xaml.cs kan worden gebruikt om het level te updaten
                User.Character = c;

                //gaat het steenpapierschaar scherm openen
                //dit was eerst bedoelt als een messagebox maar ik heb besloten om er een spelletje van te maken in de plaats
                CustomMessageBox b = new CustomMessageBox();
                b.ShowDialog();

                //gaat na de showdialog het geselecteerde karakter het nieuwe level geven
                c = User.Character;
                DatabaseOperations.CharacterUpdaten(c);

                //refreshed de database
                dtgKarakters.ItemsSource = DatabaseOperations.CharactersOphalenViaAccountId(User.Acc.id);
            }

            //als er geen karakter is geselecteerd
            else
            {
                MessageBox.Show("Selecteer een karakter dat ten strijde gaat!");
            }
        }
    }
}
