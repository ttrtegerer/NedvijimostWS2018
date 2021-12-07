using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace esoft.Pages
{
    /// <summary>
    /// Логика взаимодействия для Nedvijimost.xaml
    /// </summary>
    public partial class Nedvijimost : Page
    {
        public Nedvijimost()
        {
            InitializeComponent();
            RBHouse.IsChecked = true;
            UpdateNedvijimost();
            CBSortByType.Items.Add("Все типы");
            CBSortByType.Items.Add("Дом");
            CBSortByType.Items.Add("Квартира");
            CBSortByType.Items.Add("Земля");
            CBSortByAdress.ItemsSource = App.Context.RealEstateSet.ToList();
        }

        private void LVNedvijimost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVNedvijimost.SelectedItem != null) { 
            entitis.RealEstateSet Nedvijimost = (entitis.RealEstateSet)LVNedvijimost.SelectedItem;
            TBoxCity.Text = Nedvijimost.Address_City;
            TBoxStreet.Text = Nedvijimost.Address_Street;
            TBoxHouse.Text = Nedvijimost.Address_House;
            TBoxRoom.Text = Nedvijimost.Address_Number;
            TBoxLatitude.Text =Convert.ToString(Nedvijimost.Coordinate_latitude);
            TBoxLongitude.Text = Convert.ToString(Nedvijimost.Coordinate_longitude);
            var NedvijimostHouse = App.Context.RealEstateSet_House.Where(p => p.Id == Nedvijimost.Id).Count();
            var NedvijimostRoom = App.Context.RealEstateSet_Apartment.Where(p => p.Id == Nedvijimost.Id).Count();
            var NedvijimostLand = App.Context.RealEstateSet_Land.Where(p => p.Id == Nedvijimost.Id).Count();
            if(NedvijimostHouse != 0){
                RBHouse.IsChecked = true;
                TBoxArea.Text = Convert.ToString(Nedvijimost.z3_RealEstateSet_House.TotalArea);
                TBoxAddEdit2.Text = Convert.ToString(Nedvijimost.z3_RealEstateSet_House.TotalFloors);
                }
            if (NedvijimostRoom != 0)
            {
                RBRoom.IsChecked = true;
                TBoxArea.Text = Convert.ToString(Nedvijimost.z3_RealEstateSet_Apartment.TotalArea);
                TBoxAddEdit2.Text = Convert.ToString(Nedvijimost.z3_RealEstateSet_Apartment.Floor);
                TBoxAddEdit3.Text = Convert.ToString(Nedvijimost.z3_RealEstateSet_Apartment.Rooms);
                }
            if (NedvijimostLand != 0)
            {
                RBLand.IsChecked = true;
                TBoxArea.Text = Convert.ToString(Nedvijimost.z3_RealEstateSet_Land.TotalArea);
            }
            }
            else
            {
                TBoxCity.Text = "";
                TBoxStreet.Text = "";
                TBoxHouse.Text = "";
                TBoxRoom.Text = "";
                TBoxLatitude.Text = "";
                TBoxLongitude.Text = "";
                TBoxArea.Text = "";
                TBoxAddEdit2.Text = "";
                TBoxAddEdit3.Text = "";
                RBHouse.IsChecked = true;
            }
        }

        private void RBRoom_Checked(object sender, RoutedEventArgs e)
        {
            TBlocAddEdit2.Visibility = Visibility.Visible;
            TBlocAddEdit3.Visibility = Visibility.Visible;
            TBoxAddEdit2.Visibility = Visibility.Visible;
            TBoxAddEdit3.Visibility = Visibility.Visible;
            TBlocAddEdit2.Text = "этаж: ";
            TBlocAddEdit3.Text = "Количество комнат: ";

        }

        private void RBLand_Checked(object sender, RoutedEventArgs e)
        {
            TBlocAddEdit2.Visibility = Visibility.Hidden;
            TBlocAddEdit3.Visibility = Visibility.Hidden;
            TBoxAddEdit2.Visibility = Visibility.Hidden;
            TBoxAddEdit3.Visibility = Visibility.Hidden;
        }

        private void RBHouse_Checked(object sender, RoutedEventArgs e)
        {
            TBlocAddEdit2.Visibility = Visibility.Visible;
            TBlocAddEdit3.Visibility = Visibility.Hidden;
            TBoxAddEdit2.Visibility = Visibility.Visible;
            TBoxAddEdit3.Visibility = Visibility.Hidden;
            TBlocAddEdit2.Text = "Количество комнат: ";
           
        }

        private void BtnAddK_Click(object sender, RoutedEventArgs e)
        {
            string Latitude = TBoxLatitude.Text;
            string Longitude = TBoxLongitude.Text;
            string Area = TBoxArea.Text;
            string AddEdit2 = TBoxAddEdit2.Text;
            string AddEdit3 = TBoxAddEdit3.Text;


            if (Latitude == "" || !Regex.IsMatch(Latitude, "^[0-9]+$"))
            {
                Latitude = null;
            }
            if (Longitude == "" || !Regex.IsMatch(Longitude, "^[0-9]+$"))
            {
                Longitude = null;
            }
            if (Area == "" || !Regex.IsMatch(Area, "^[0-9]+$"))
            {
                Area = null;
            }
            if (AddEdit2 == "" || !Regex.IsMatch(AddEdit2, "^[0-9]+$"))
            {
                AddEdit2 = null;
            }
            if (AddEdit3 == "" || !Regex.IsMatch(AddEdit3, "^[0-9]+$"))
            {
                AddEdit3 = null;
            }


            var Nedvijimost = new entitis.RealEstateSet
             {
                Address_City = TBoxCity.Text,
                Address_Street = TBoxStreet.Text,
                Address_House = TBoxHouse.Text,
                Address_Number = TBoxRoom.Text, 
                Coordinate_latitude = Convert.ToInt32(Latitude),
                Coordinate_longitude = Convert.ToInt32(Longitude)
             };
            App.Context.RealEstateSet.Add(Nedvijimost);
            App.Context.SaveChanges();
            var i = App.Context.RealEstateSet.Max(p => p.Id);
            if (RBHouse.IsChecked == true)
             {
                var House = new entitis.RealEstateSet_House
                {
                    Id = i,
                    TotalArea=Convert.ToInt32(Area),
                    TotalFloors=Convert.ToInt32(AddEdit2)
                };
                App.Context.RealEstateSet_House.Add(House);
                App.Context.SaveChanges();
                MessageBox.Show("Дом успешно добавлен");
                UpdateNedvijimost();
            }
            else if(RBRoom.IsChecked == true)
             {
                var Room = new entitis.RealEstateSet_Apartment
                {
                    Id = i,
                    TotalArea = Convert.ToInt32(Area),
                    Floor = Convert.ToInt32(AddEdit2),
                    Rooms=Convert.ToInt32(AddEdit3)
                };
                App.Context.RealEstateSet_Apartment.Add(Room);
                App.Context.SaveChanges();
                MessageBox.Show("Квартира успешна добавлена");
                UpdateNedvijimost();
            }
            else if (RBLand.IsChecked == true)
             {
                var Land = new entitis.RealEstateSet_Land { 
                    Id = i,
                    TotalArea = Convert.ToInt32(Area)
                    
                };
                App.Context.RealEstateSet_Land.Add(Land);
                App.Context.SaveChanges();
                MessageBox.Show("Земля успешна добавлена");
                UpdateNedvijimost();
            }

        }

        private void BtnEditK_Click(object sender, RoutedEventArgs e)
        {
            string Latitude = TBoxLatitude.Text;
            string Longitude = TBoxLongitude.Text;
            string Area = TBoxArea.Text;
            string AddEdit2 = TBoxAddEdit2.Text;
            string AddEdit3 = TBoxAddEdit3.Text;


            if (Latitude == "" || !Regex.IsMatch(Latitude, "^[0-9]+$"))
            {
                Latitude = null;
            }
            if (Longitude == "" || !Regex.IsMatch(Longitude, "^[0-9]+$"))
            {
                Longitude = null;
            }
            if (Area == "" || !Regex.IsMatch(Area, "^[0-9]+$"))
            {
                Area = null;
            }
            if (AddEdit2 == "" || !Regex.IsMatch(AddEdit2, "^[0-9]+$"))
            {
                AddEdit2 = null;
            }
            if (AddEdit3 == "" || !Regex.IsMatch(AddEdit3, "^[0-9]+$"))
            {
                AddEdit3 = null;
            }
            if (LVNedvijimost.SelectedItem != null)
            {


                entitis.RealEstateSet Nedvijimost = (entitis.RealEstateSet)LVNedvijimost.SelectedItem;
                if (Nedvijimost != null)
                {
                    Nedvijimost.Address_City = TBoxCity.Text;
                    Nedvijimost.Address_Street = TBoxStreet.Text;
                    Nedvijimost.Address_House = TBoxHouse.Text;
                    Nedvijimost.Address_Number = TBoxRoom.Text;
                    Nedvijimost.Coordinate_latitude = Convert.ToInt32(Latitude);
                    Nedvijimost.Coordinate_longitude = Convert.ToInt32(Longitude);
                    if (RBHouse.IsChecked == true)
                    {
                        Nedvijimost.z3_RealEstateSet_House.Id = Nedvijimost.Id;
                        Nedvijimost.z3_RealEstateSet_House.TotalArea = Convert.ToInt32(Area);
                        Nedvijimost.z3_RealEstateSet_House.TotalFloors = Convert.ToInt32(AddEdit2);
                        App.Context.SaveChanges();
                        MessageBox.Show("Обновление недвижимости успешно");
                        UpdateNedvijimost();
                    }
                    else if (RBRoom.IsChecked == true)
                    {
                        Nedvijimost.z3_RealEstateSet_Apartment.Id = Nedvijimost.Id;
                        Nedvijimost.z3_RealEstateSet_Apartment.TotalArea = Convert.ToInt32(Area);
                        Nedvijimost.z3_RealEstateSet_Apartment.Floor = Convert.ToInt32(AddEdit2);
                        Nedvijimost.z3_RealEstateSet_Apartment.Rooms = Convert.ToInt32(AddEdit3);
                        App.Context.SaveChanges();
                        MessageBox.Show("Обновление недвижимости успешно");
                        UpdateNedvijimost();
                    }
                    else if (RBLand.IsChecked == true)
                    {
                        Nedvijimost.z3_RealEstateSet_Land.Id = Nedvijimost.Id;
                        Nedvijimost.z3_RealEstateSet_Land.TotalArea = Convert.ToInt32(Area);
                        App.Context.SaveChanges();
                        MessageBox.Show("Обновление недвижимости успешно");
                        UpdateNedvijimost();
                    }

                }
                else
                {
                    MessageBox.Show("Вы не выбрали недвижимость прежде чем её обновить");
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали недвижимость прежде чем её обновить");
            }
        }

        private void BtnDeletK_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (LVNedvijimost.SelectedItem != null) 
            { 
            entitis.RealEstateSet Nedvijimost = (entitis.RealEstateSet)LVNedvijimost.SelectedItem;
            if (Nedvijimost != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить Недвижимость :" + $"{Nedvijimost.Adress}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    App.Context.RealEstateSet.Remove(Nedvijimost);
                    App.Context.SaveChanges();
                    MessageBox.Show("Удаление успешно");
                    LVNedvijimost.SelectedItem = null;
                    UpdateNedvijimost();
                }
               

            }
                else
                {
                    MessageBox.Show("Вы не выбрали недвижимость прежде чем её удалить");
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали недвижимость прежде чем её удалить");
            }
        }
            catch
            {
                MessageBox.Show("Невозможно удалить");
            }
    }
        private void UpdateNedvijimost()
        {
            IEnumerable<entitis.RealEstateSet> Nedvijimost = App.Context.RealEstateSet;
            if (CBSortByAdress.SelectedItem!=null)
            {
                
                Nedvijimost = Nedvijimost.Where(p => p.Id == CBSortByAdress.SelectedIndex+1);
            }
            if(CBSortByType.SelectedIndex == 1){
                Nedvijimost = Nedvijimost.Where(p => p.Proverka == 1);
            }
            if (CBSortByType.SelectedIndex == 2)
            {
                Nedvijimost = Nedvijimost.Where(p => p.Proverka == 2);
            }
            if (CBSortByType.SelectedIndex == 3)
            {
                Nedvijimost = Nedvijimost.Where(p => p.Proverka == 3);

            }
            Nedvijimost = Nedvijimost.Where(p => p.Adress.ToLower().Contains(TBoxSerch.Text.ToLower()));
            var Nedvijimosti = Nedvijimost.ToList();
            LVNedvijimost.ItemsSource = Nedvijimosti;

        }

        private void TBoxSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateNedvijimost();
        }

        private void CBSortByType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNedvijimost();
        }

        private void CBSortByAdress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNedvijimost();
        }

        private void BtnPredlozenie_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Predlozenie());
        }

        private void BtnPotribnost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Potrebnost());
        }

        private void BtnClientRieltor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientRieltor());
        }

        private void BtnSdelka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sdelka());
        }
    }
}
