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

namespace esoft.Pages
{
    /// <summary>
    /// Логика взаимодействия для Sdelka.xaml
    /// </summary>
    public partial class Sdelka : Page
    {
        public Sdelka()
        {
            InitializeComponent();
            UpdateSdelka();
            CBoxPotrebnost.ItemsSource = App.Context.DemandSet.ToList();
            CBoxPredlozhenie.ItemsSource = App.Context.SupplySet.ToList();
            TBoxOtcislenieCompanii.IsEnabled = false;
            TBoxOtcislenieRieltorKlientBuy.IsEnabled = false;
            TBoxOtcislenieRieltorKlientSale.IsEnabled = false;
            TBoxStoimostYslugForClientBuy.IsEnabled = false;
            TBoxStoimostYslugForClientSale.IsEnabled = false;
            
        }

        private void BtnAddK_Click(object sender, RoutedEventArgs e)
        {if (CBoxPotrebnost.SelectedItem != null && CBoxPredlozhenie.SelectedItem != null)
            {
                var Sdelka = new entitis.DealSet
                {
                    z3_DemandSet = (entitis.DemandSet)CBoxPotrebnost.SelectedItem,
                    z3_SupplySet = (entitis.SupplySet)CBoxPredlozhenie.SelectedItem
                };
                App.Context.DealSet.Add(Sdelka);
                App.Context.SaveChanges();
                MessageBox.Show("Сделка успешно добавлена");
                UpdateSdelka();
            }
            else MessageBox.Show("Выберите потребность и предложение в выпадающем списке");
    }

        private void BtnEditK_Click(object sender, RoutedEventArgs e)
        {
            entitis.DealSet Sdelka = (entitis.DealSet)LVSdelka.SelectedItem;
            if (Sdelka != null) { 
            Sdelka.z3_DemandSet = (entitis.DemandSet)CBoxPotrebnost.SelectedItem;
            Sdelka.z3_SupplySet = (entitis.SupplySet)CBoxPredlozhenie.SelectedItem;
            App.Context.SaveChanges();
            MessageBox.Show("Обновление успешно");
            UpdateSdelka();
            }
            else
            {
                MessageBox.Show("Выберите сделку прежде чем её редактировать");
            }
        }

        private void BtnDeletK_Click(object sender, RoutedEventArgs e)
        {
            entitis.DealSet Sdelka = (entitis.DealSet)LVSdelka.SelectedItem;
            if (Sdelka != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить сделку :" + $"{Sdelka.Id}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {

                    App.Context.DealSet.Remove(Sdelka);
                    App.Context.SaveChanges();
                    MessageBox.Show("Удаление успешно");
                    LVSdelka.SelectedItem = null;
                    UpdateSdelka();
                }
            }
            else
            {
                MessageBox.Show("Выберите сделку прежде чем её удалить");
            }
        }

        private void LVSdelka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            entitis.DealSet Sdelka = (entitis.DealSet)LVSdelka.SelectedItem;
            if (Sdelka != null)
            {
                CBoxPotrebnost.SelectedItem = Sdelka.z3_DemandSet;
                CBoxPredlozhenie.SelectedItem = Sdelka.z3_SupplySet;
                var TypeNedvizimostiHouse = App.Context.RealEstateSet_House.Where(P => P.Id == Sdelka.z3_SupplySet.Id).Count();
                var TypeNedvizimostiRoom = App.Context.RealEstateSet_Apartment.Where(P => P.Id == Sdelka.z3_SupplySet.Id).Count();
                var TypeNedvizimostiLand = App.Context.RealEstateSet_Land.Where(P => P.Id == Sdelka.z3_SupplySet.Id).Count();
                if (TypeNedvizimostiHouse != 0)
                {
                    TBoxStoimostYslugForClientSale.Text = Convert.ToString(30000 + Sdelka.z3_SupplySet.Price * 1 / 100);
                }
                if (TypeNedvizimostiRoom != 0)
                {
                    TBoxStoimostYslugForClientSale.Text = Convert.ToString(36000 + Sdelka.z3_SupplySet.Price * 1 / 100);
                }
                if (TypeNedvizimostiLand != 0)
                {
                    TBoxStoimostYslugForClientSale.Text = Convert.ToString(30000 + Sdelka.z3_SupplySet.Price * 2 / 100);
                }

                TBoxStoimostYslugForClientBuy.Text = Convert.ToString(Sdelka.z3_SupplySet.Price*3/100);
                TBoxOtcislenieRieltorKlientBuy.Text = Convert.ToString(Sdelka.z3_SupplySet.z3_PersonSet_Agent.DealShare* Sdelka.z3_SupplySet.Price * 3 / 100/100);
                TBoxOtcislenieRieltorKlientSale.Text = Convert.ToString(Sdelka.z3_DemandSet.z3_PersonSet_Agent.DealShare * Convert.ToInt32(TBoxStoimostYslugForClientSale.Text)/100);
                TBoxOtcislenieCompanii.Text = Convert.ToString(Convert.ToInt32(TBoxStoimostYslugForClientSale.Text) + Convert.ToInt32(TBoxStoimostYslugForClientBuy.Text) - Convert.ToInt32(TBoxOtcislenieRieltorKlientBuy.Text) - Convert.ToInt32(TBoxOtcislenieRieltorKlientSale.Text));
            }
           
       }

        private void UpdateSdelka()
        {
            LVSdelka.ItemsSource = App.Context.DealSet.ToList();
        }

        private void CBoxPredlozhenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStoimost();
        }

        private void CBoxPotrebnost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStoimost();
        }
        private void UpdateStoimost()
        {
            entitis.SupplySet Predlozenie = (entitis.SupplySet)CBoxPredlozhenie.SelectedItem;
            entitis.DemandSet Potrebnost = (entitis.DemandSet)CBoxPotrebnost.SelectedItem;
            if (Predlozenie != null && Potrebnost != null)
            {
                var TypeNedvizimostiHouse = App.Context.RealEstateSet_House.Where(P => P.Id == Predlozenie.Id).Count();
                var TypeNedvizimostiRoom = App.Context.RealEstateSet_Apartment.Where(P => P.Id == Predlozenie.Id).Count();
                var TypeNedvizimostiLand = App.Context.RealEstateSet_Land.Where(P => P.Id == Predlozenie.Id).Count();
                if (TypeNedvizimostiHouse != 0)
                {
                    TBoxStoimostYslugForClientSale.Text = Convert.ToString(30000 + Predlozenie.Price * 1 / 100);
                }
                if (TypeNedvizimostiRoom != 0)
                {
                    TBoxStoimostYslugForClientSale.Text = Convert.ToString(36000 + Predlozenie.Price * 1 / 100);
                }
                if (TypeNedvizimostiLand != 0)
                {
                    TBoxStoimostYslugForClientSale.Text = Convert.ToString(30000 + Predlozenie.Price * 2 / 100);
                }

                TBoxStoimostYslugForClientBuy.Text = Convert.ToString(Predlozenie.Price * 3 / 100);
                TBoxOtcislenieRieltorKlientBuy.Text = Convert.ToString(Predlozenie.z3_PersonSet_Agent.DealShare * Predlozenie.Price * 3 / 100 / 100);
                TBoxOtcislenieRieltorKlientSale.Text = Convert.ToString(Potrebnost.z3_PersonSet_Agent.DealShare * Convert.ToInt32(TBoxStoimostYslugForClientSale.Text) / 100);
                TBoxOtcislenieCompanii.Text = Convert.ToString(Convert.ToInt32(TBoxStoimostYslugForClientSale.Text) + Convert.ToInt32(TBoxStoimostYslugForClientBuy.Text) - Convert.ToInt32(TBoxOtcislenieRieltorKlientBuy.Text) - Convert.ToInt32(TBoxOtcislenieRieltorKlientSale.Text));
            }
        }

        private void BtnPredlozenie_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Predlozenie());
        }

        private void BtnClientRieltor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientRieltor());
        }

        private void BtnNedvijimost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Nedvijimost());
        }

        private void BtnPotrebnost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Potrebnost());
        }
    }
}
