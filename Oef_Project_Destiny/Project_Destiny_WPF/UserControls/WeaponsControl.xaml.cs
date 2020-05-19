﻿using Destiny_DAL;
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
    /// Interaction logic for Weapons.xaml
    /// </summary>
    public partial class WeaponsControl : UserControl
    {
        public WeaponsControl()
        {
            InitializeComponent();
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ZoekenWapens();
            }
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst voor te zoeken op zeldzaamheid
            List<string> zeldzaamheidlijst = new List<string>() { "All", "Common", "Uncommon", "Rare", "Legendary", "Exotic" };
            cmbZeldzaamheid.ItemsSource = zeldzaamheidlijst;
            cmbZeldzaamheid.SelectedItem = "All";

            List<Wapen> categorieLijst = DatabaseOperations.OphalenCategorie();
            Wapen categorie = new Wapen();
            categorie.Soort = "All";
            cmbCategorie.ItemsSource = categorieLijst;
            cmbCategorie.SelectedIndex = 0;

            cmbCategorie.DisplayMemberPath = "Soort";


            ZoekenWapens();

        }

        private void cmbCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbZeldzaamheid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dbWapens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ZoekenWapens()
        {
            string categorie = cmbCategorie.SelectedItem as String;
            string zeldzaamheid = cmbZeldzaamheid.SelectedItem as string;
            
            //kijken als "All" geselecteerd is in lijst 
            /*
            if (cmbCategorie.SelectedIndex != 0 && cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenArmorViaArmorSlotEnZeldzaamheid(tbZoekItem.Text, armorslot, zeldzaamheid);
            }
            else if (cmbCategorie.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenArmorViaArmorslot(tbZoekItem.Text, armorslot);
            }
            else if (cmbZeldzaamheid.SelectedIndex != 0)
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenArmorViaZeldzaamheid(tbZoekItem.Text, zeldzaamheid);
            }
            else
            {
                dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaNaam(tbZoekItem.Text);
            }
            */

            dbWapens.ItemsSource = DatabaseOperations.OphalenWapensViaNaam(tbZoekWapen.Text);
        }


    }
}
