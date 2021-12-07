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
    /// Логика взаимодействия для Potrebnost.xaml
    /// </summary>
    public partial class Potrebnost : Page
    {
        public Potrebnost()
        {
            InitializeComponent();
            UpdatePotrebnost();
            CBoxKlient.ItemsSource = App.Context.PersonSet_Client.ToList();
            CBoxRieltor.ItemsSource = App.Context.PersonSet_Agent.ToList();
            RBHouse.IsChecked = true;
        }

        private void LVPotrebnost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            entitis.DemandSet Potribnosti = (entitis.DemandSet)LVPotrebnost.SelectedItem;
            if(Potribnosti != null)
            {
                TBoxCity.Text = Potribnosti.Address_City;
                TBoxStreet.Text = Potribnosti.Address_Street;
                TBoxHouse.Text = Potribnosti.Address_House;
                TBoxRoom.Text = Potribnosti.Address_Number;
                TBoxMinPrise.Text =Convert.ToString(Potribnosti.MinPrice);
                TBoxMaxPrise.Text = Convert.ToString(Potribnosti.MaxPrice);
                CBoxKlient.SelectedItem = Potribnosti.z3_PersonSet_Client;
                CBoxRieltor.SelectedItem = Potribnosti.z3_PersonSet_Agent;
                var PotribnostiHouse = App.Context.RealEstateFilterSet_HouseFilter.Where(p => p.Id == Potribnosti.RealEstateFilter_Id).Count();
                var PotribnostiRoom = App.Context.RealEstateFilterSet_ApartmentFilter.Where(p => p.Id == Potribnosti.RealEstateFilter_Id).Count();
                var PotribnostiLand = App.Context.RealEstateFilterSet_LandFilter.Where(p => p.Id == Potribnosti.RealEstateFilter_Id).Count();
                if (PotribnostiHouse != 0)
                {
                    RBHouse.IsChecked = true;
                    TBoxMinArea.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MinArea);
                    TBoxMaxArea.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MaxArea);
                    TBoxAddEdit1.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MinRooms);
                    TBoxAddEdit2.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MaxRooms);
                    TBoxAddEdit3.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MinFloors);
                    TBoxAddEdit4.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MaxFloors);
                }
                if (PotribnostiRoom != 0)
                {
                    RBRoom.IsChecked = true;
                    TBoxMinArea.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MinArea);
                    TBoxMaxArea.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MaxArea);
                    TBoxAddEdit1.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MinRooms);
                    TBoxAddEdit2.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MaxRooms);
                    TBoxAddEdit3.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MinFloor);
                    TBoxAddEdit4.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MaxFloor);
                }
                if (PotribnostiLand != 0)
                {
                    RBLand.IsChecked = true;
                    TBoxMinArea.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_LandFilter.MinArea);
                    TBoxMaxArea.Text = Convert.ToString(Potribnosti.z3_RealEstateFilterSet.z3_RealEstateFilterSet_LandFilter.MaxArea);
                }
            }
        }

        private void BtnAddK_Click(object sender, RoutedEventArgs e)
        {

            string MinArea =   TBoxMinArea.Text;
            string MaxArea =   TBoxMinArea.Text;
            string AddEdit1 = TBoxAddEdit1.Text;
            string AddEdit2 = TBoxAddEdit2.Text;
            string AddEdit3 = TBoxAddEdit3.Text;
            string AddEdit4 = TBoxAddEdit4.Text;
            string MinPrise = TBoxMinPrise.Text;
            string MaxPrise = TBoxMaxPrise.Text;

            if (MinArea == "" || !Regex.IsMatch(MinArea, "^[0-9]+$"))
            {
                MinArea = null;
            }
            if (MaxArea == "" || !Regex.IsMatch(MaxArea, "^[0-9]+$"))
            {
                MaxArea = null;
            }
            if (AddEdit1 == "" || !Regex.IsMatch(AddEdit1, "^[0-9]+$"))
            {
                AddEdit1 = null;
            }
            if (AddEdit2 == "" || !Regex.IsMatch(AddEdit2, "^[0-9]+$"))
            {
                AddEdit2 = null;
            }
            if (AddEdit3 == "" || !Regex.IsMatch(AddEdit3, "^[0-9]+$"))
            {
                AddEdit3 = null;
            }
            if (AddEdit4 == "" || !Regex.IsMatch(AddEdit4, "^[0-9]+$"))
            {
                AddEdit4 = null;
            }
            if (MinPrise == "" || !Regex.IsMatch(MinPrise, "^[0-9]+$"))
            {
                MinPrise = null;
            }
            if (MaxPrise == "" || !Regex.IsMatch(MaxPrise, "^[0-9]+$"))
            {
                MaxPrise = null;
            }

            if(CBoxKlient.SelectedItem !=null && CBoxRieltor.SelectedItem != null) 
            { 


            var MaxCodAddPotrebnost = App.Context.DemandSet.Max(p => p.Id);

            var Filter = new entitis.RealEstateFilterSet
            {
                Id = MaxCodAddPotrebnost + 1
            };
            App.Context.RealEstateFilterSet.Add(Filter);
            App.Context.SaveChanges();
            var MaxCodFilterSet = App.Context.RealEstateFilterSet.Max(p => p.Id);
            var Potrebnost = new entitis.DemandSet
            {
                Address_City=TBoxCity.Text,
                Address_Street=TBoxStreet.Text,
                Address_House=TBoxHouse.Text,
                Address_Number=TBoxRoom.Text,
                MinPrice=Convert.ToInt32(MinPrise),
                MaxPrice=Convert.ToInt32(MaxPrise),
                z3_PersonSet_Client=(entitis.PersonSet_Client)CBoxKlient.SelectedItem,
                z3_PersonSet_Agent=(entitis.PersonSet_Agent)CBoxRieltor.SelectedItem,
                RealEstateFilter_Id= MaxCodFilterSet
            };
            App.Context.DemandSet.Add(Potrebnost);
            App.Context.SaveChanges();
            if (RBHouse.IsChecked == true)
            {
                var PotrebnostHouse = new entitis.RealEstateFilterSet_HouseFilter
                {
                    Id= MaxCodFilterSet,
                    MinArea=Convert.ToInt32(MinArea),
                    MaxArea = Convert.ToInt32(MaxArea),
                    MinRooms=Convert.ToInt32(AddEdit1),
                    MaxRooms = Convert.ToInt32(AddEdit2),
                    MinFloors=Convert.ToInt32(AddEdit3),
                    MaxFloors = Convert.ToInt32(AddEdit4)

                };
                App.Context.RealEstateFilterSet_HouseFilter.Add(PotrebnostHouse);
                App.Context.SaveChanges();
                MessageBox.Show("Запись успешно добавлена");
                UpdatePotrebnost();

            }
            if (RBRoom.IsChecked == true)
            {
                var PotrebnostRoom = new entitis.RealEstateFilterSet_ApartmentFilter
                {
                    Id = MaxCodFilterSet,
                    MinArea = Convert.ToInt32(MinArea),
                    MaxArea = Convert.ToInt32(MaxArea),
                    MinRooms = Convert.ToInt32(AddEdit1),
                    MaxRooms = Convert.ToInt32(AddEdit2),
                    MinFloor = Convert.ToInt32(AddEdit3),
                    MaxFloor = Convert.ToInt32(AddEdit4)

                };
                App.Context.RealEstateFilterSet_ApartmentFilter.Add(PotrebnostRoom);
                App.Context.SaveChanges();
                MessageBox.Show("Запись успешно добавлена");
                UpdatePotrebnost();
            }
            if (RBLand.IsChecked == true)
            {
                var PotrebnostLand = new entitis.RealEstateFilterSet_LandFilter
                {
                    Id = MaxCodFilterSet,
                    MinArea = Convert.ToInt32(MinArea),
                    MaxArea = Convert.ToInt32(MaxArea)
                };
                App.Context.RealEstateFilterSet_LandFilter.Add(PotrebnostLand);
                App.Context.SaveChanges();
                MessageBox.Show("Запись успешно добавлена");
                UpdatePotrebnost();

            }
            }
            else
            {
                MessageBox.Show("Выберите клиента и риелтора в выпадающем списке");
            }
        }

        private void BtnEditK_Click(object sender, RoutedEventArgs e)
        {
            string MinArea = TBoxMinArea.Text;
            string MaxArea = TBoxMinArea.Text;
            string AddEdit1 = TBoxAddEdit1.Text;
            string AddEdit2 = TBoxAddEdit2.Text;
            string AddEdit3 = TBoxAddEdit3.Text;
            string AddEdit4 = TBoxAddEdit4.Text;
            string MinPrise = TBoxMinPrise.Text;
            string MaxPrise = TBoxMaxPrise.Text;

            if (MinArea == "" || !Regex.IsMatch(MinArea, "^[0-9]+$"))
            {
                MinArea = null;
            }
            if (MaxArea == "" || !Regex.IsMatch(MaxArea, "^[0-9]+$"))
            {
                MaxArea = null;
            }
            if (AddEdit1 == "" || !Regex.IsMatch(AddEdit1, "^[0-9]+$"))
            {
                AddEdit1 = null;
            }
            if (AddEdit2 == "" || !Regex.IsMatch(AddEdit2, "^[0-9]+$"))
            {
                AddEdit2 = null;
            }
            if (AddEdit3 == "" || !Regex.IsMatch(AddEdit3, "^[0-9]+$"))
            {
                AddEdit3 = null;
            }
            if (AddEdit4 == "" || !Regex.IsMatch(AddEdit4, "^[0-9]+$"))
            {
                AddEdit4 = null;
            }
            if (MinPrise == "" || !Regex.IsMatch(MinPrise, "^[0-9]+$"))
            {
                MinPrise = null;
            }
            if (MaxPrise == "" || !Regex.IsMatch(MaxPrise, "^[0-9]+$"))
            {
                MaxPrise = null;
            }
            if (LVPotrebnost.SelectedItem != null) { 
            entitis.DemandSet Potrebnost = (entitis.DemandSet)LVPotrebnost.SelectedItem;
            if (Potrebnost != null)
            {
                Potrebnost.Address_City = TBoxCity.Text;
                Potrebnost.Address_Street = TBoxStreet.Text;
                Potrebnost.Address_House = TBoxHouse.Text;
                Potrebnost.Address_Number = TBoxRoom.Text;
                Potrebnost.MinPrice = Convert.ToInt32(MinPrise);
                Potrebnost.MaxPrice = Convert.ToInt32(MaxPrise);
                Potrebnost.z3_PersonSet_Client = (entitis.PersonSet_Client)CBoxKlient.SelectedItem;
                Potrebnost.z3_PersonSet_Agent = (entitis.PersonSet_Agent)CBoxRieltor.SelectedItem;
                if (RBHouse.IsChecked == true)
                {
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MinArea = Convert.ToInt32(MinArea);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MaxArea = Convert.ToInt32(MaxArea);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MinRooms = Convert.ToInt32(AddEdit1);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MaxRooms = Convert.ToInt32(AddEdit2);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MinFloors = Convert.ToInt32(AddEdit3);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_HouseFilter.MaxFloors = Convert.ToInt32(AddEdit4);
                    App.Context.SaveChanges();
                    MessageBox.Show("Запись успешно отредактирована");
                    UpdatePotrebnost();

                }
                if (RBRoom.IsChecked == true)
                {
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MinArea = Convert.ToInt32(MinArea);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MaxArea = Convert.ToInt32(MaxArea);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MinRooms = Convert.ToInt32(AddEdit1);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MaxRooms = Convert.ToInt32(AddEdit2);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MinFloor = Convert.ToInt32(AddEdit3);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_ApartmentFilter.MaxFloor = Convert.ToInt32(AddEdit4);
                    App.Context.SaveChanges();
                    MessageBox.Show("Запись успешно отредактирована");
                    UpdatePotrebnost();
                }
                if (RBLand.IsChecked == true)
                {
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_LandFilter.MinArea = Convert.ToInt32(MinArea);
                    Potrebnost.z3_RealEstateFilterSet.z3_RealEstateFilterSet_LandFilter.MaxArea = Convert.ToInt32(MaxArea);
                    App.Context.SaveChanges();
                    MessageBox.Show("Запись успешно отредактирована");
                    UpdatePotrebnost();
                }
            }
            else
            {
                MessageBox.Show("Выберите потребность для редактирования");
            }
            }
            else
            {
                MessageBox.Show("Выберите потребность для редактирования");
            }
        }

        private void BtnDeletK_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if(LVPotrebnost.SelectedItem != null) 
            { 
            entitis.DemandSet Potrebnosti = (entitis.DemandSet)LVPotrebnost.SelectedItem;
            if (Potrebnosti != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить Потребность :" + $"{Potrebnosti.Address_City+","+Potrebnosti.Address_Street+","+Potrebnosti.Address_House+","+Potrebnosti.Address_Number}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    App.Context.DemandSet.Remove(Potrebnosti);
                    App.Context.SaveChanges();
                    MessageBox.Show("Удаление успешно");
                    LVPotrebnost.SelectedItem = null;
                    UpdatePotrebnost();
                }


            }
            else
            {
                MessageBox.Show("Вы не выбрали предложение прежде чем её удалить");
            }
            }
            else
            {
                MessageBox.Show("Вы не выбрали предложение прежде чем её удалить");
            }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить");
            }

        }

        private void RBRoom_Checked(object sender, RoutedEventArgs e)
        {
            TBlocAddEdit1.Visibility = Visibility.Visible;
            TBlocAddEdit2.Visibility = Visibility.Visible;
            TBlocAddEdit3.Visibility = Visibility.Visible;
            TBlocAddEdit4.Visibility = Visibility.Visible;
            TBoxAddEdit1.Visibility = Visibility.Visible;
            TBoxAddEdit2.Visibility = Visibility.Visible;
            TBoxAddEdit3.Visibility = Visibility.Visible;
            TBoxAddEdit4.Visibility = Visibility.Visible;
            TBlocAddEdit1.Text = "Мин. комнат:";
            TBlocAddEdit2.Text = "Макс. комнат:";
            TBlocAddEdit3.Text = "Мин. этаж:";
            TBlocAddEdit4.Text = "Макс. этаж:";
        }

        private void RBHouse_Checked(object sender, RoutedEventArgs e)
        {
            TBlocAddEdit1.Visibility = Visibility.Visible;
            TBlocAddEdit2.Visibility = Visibility.Visible;
            TBlocAddEdit3.Visibility = Visibility.Visible;
            TBlocAddEdit4.Visibility = Visibility.Visible;
            TBoxAddEdit1.Visibility = Visibility.Visible;
            TBoxAddEdit2.Visibility = Visibility.Visible;
            TBoxAddEdit3.Visibility = Visibility.Visible;
            TBoxAddEdit4.Visibility = Visibility.Visible;
            TBlocAddEdit1.Text = "Мин. комнат:";
            TBlocAddEdit2.Text = "Макс. комнат:";
            TBlocAddEdit3.Text = "Мин. этажность:";
            TBlocAddEdit4.Text = "Макс. этажность:";

        }

        private void RBLand_Checked(object sender, RoutedEventArgs e)
        {
            TBlocAddEdit1.Visibility = Visibility.Hidden;
            TBlocAddEdit2.Visibility = Visibility.Hidden; 
            TBlocAddEdit3.Visibility = Visibility.Hidden;
            TBlocAddEdit4.Visibility = Visibility.Hidden;
            TBoxAddEdit1.Visibility = Visibility.Hidden;
            TBoxAddEdit2.Visibility = Visibility.Hidden;
            TBoxAddEdit3.Visibility = Visibility.Hidden;
            TBoxAddEdit4.Visibility = Visibility.Hidden;

        }
        private void UpdatePotrebnost()
        {
            LVPotrebnost.ItemsSource = App.Context.DemandSet.ToList();
        }

        private void BtnPredlozenie_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Predlozenie());
        }

        private void BtnNedvijimost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Nedvijimost());
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
