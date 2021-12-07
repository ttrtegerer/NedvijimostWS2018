using esoft.entitis;
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
    /// Логика взаимодействия для Predlozenie.xaml
    /// </summary>
    public partial class Predlozenie : Page
    {
        public Predlozenie()
        {
            InitializeComponent();
            UpdatePredlozenie();
            CBoxKlient.ItemsSource = App.Context.PersonSet_Client.ToList();
            CBoxRieltor.ItemsSource = App.Context.PersonSet_Agent.ToList();
            CBoxNedvijimost.ItemsSource = App.Context.RealEstateSet.ToList();
        }

        private void LVPredlozenie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVPredlozenie.SelectedItem != null)
            {
                entitis.SupplySet Predlozenie = (entitis.SupplySet)LVPredlozenie.SelectedItem;
                TBoxPrise.Text =Convert.ToString(Predlozenie.Price);
                CBoxKlient.SelectedItem =Predlozenie.z3_PersonSet_Client;
                CBoxRieltor.SelectedItem = Predlozenie.z3_PersonSet_Agent;
                CBoxNedvijimost.SelectedItem = Predlozenie.z3_RealEstateSet;
            }
            

        }

        private void BtnAddK_Click(object sender, RoutedEventArgs e)
        {
            string Prise = TBoxPrise.Text;
            if(Prise==""|| !Regex.IsMatch(Prise, "^[0-9]+$"))
            {
                Prise = null;
            }
            if (CBoxKlient.SelectedItem != null && CBoxRieltor.SelectedItem != null && CBoxNedvijimost.SelectedItem != null)
            {
                var Predlojenie = new entitis.SupplySet
                {
                    Price = Convert.ToInt32(TBoxPrise.Text),
                    z3_PersonSet_Client = (PersonSet_Client)CBoxKlient.SelectedItem,
                    z3_PersonSet_Agent = (PersonSet_Agent)CBoxRieltor.SelectedItem,
                    z3_RealEstateSet = (RealEstateSet)CBoxNedvijimost.SelectedItem
                };
                App.Context.SupplySet.Add(Predlojenie);
                App.Context.SaveChanges();
                MessageBox.Show("Предложение успешно Добавлено");
                UpdatePredlozenie();
            }
            else MessageBox.Show("Выберите клиента,риелтора и недвижимость в выпадающем списке");
        }

        private void BtnEditK_Click(object sender, RoutedEventArgs e)
        {
            string Prise = TBoxPrise.Text;
            if (Prise == "" || !Regex.IsMatch(Prise, "^[0-9]+$"))
            {
                Prise = null;
            }

            if (LVPredlozenie.SelectedItem != null)
            {
                entitis.SupplySet Predlozenie = (entitis.SupplySet)LVPredlozenie.SelectedItem;
                if (Predlozenie != null)
                {
                    Predlozenie.Price = Convert.ToInt32(TBoxPrise.Text);
                    Predlozenie.z3_PersonSet_Client = (PersonSet_Client)CBoxKlient.SelectedItem;
                    Predlozenie.z3_PersonSet_Agent = (PersonSet_Agent)CBoxRieltor.SelectedItem;
                    Predlozenie.z3_RealEstateSet = (RealEstateSet)CBoxNedvijimost.SelectedItem;
                    App.Context.SaveChanges();
                    MessageBox.Show("Предложение успешно обновлено");
                    UpdatePredlozenie();

                }
            }
            else MessageBox.Show("Выберите предложение");
        }

        private void BtnDeletK_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (LVPredlozenie.SelectedItem != null)
            {
                entitis.SupplySet Predlozenie = (entitis.SupplySet)LVPredlozenie.SelectedItem;
                if (Predlozenie != null)
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить Предложение :" + $"{Predlozenie.Id}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        App.Context.SupplySet.Remove(Predlozenie);
                        App.Context.SaveChanges();
                        MessageBox.Show("Удаление успешно");
                        LVPredlozenie.SelectedItem = null;
                        UpdatePredlozenie();
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
                MessageBox.Show("Невозможно удалить предложение");
            }
        }


        private void UpdatePredlozenie()
        {
            LVPredlozenie.ItemsSource = App.Context.SupplySet.ToList();
        }

        private void BtnPotrebnost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Potrebnost());
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
