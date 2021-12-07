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
    /// Логика взаимодействия для ClientRieltor.xaml
    /// </summary>
    public partial class ClientRieltor : Page
    {


        public ClientRieltor()
        {
            InitializeComponent();
            UpdateKlient();
            UpdateRieltor();
           
        }


        private void BtnDeletK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                entitis.PersonSet_Client Klient = (entitis.PersonSet_Client)LVKlient.SelectedItem;
                if (Klient != null)
                {

                    if (MessageBox.Show($"Вы уверены, что хотите клиента :" +
                  $"{Klient.Sername + " " + Klient.Name + " " + Klient.Otchestvo}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        App.Context.PersonSet_Client.Remove(Klient);
                        App.Context.SaveChanges();
                        MessageBox.Show("Удаление успешно");
                        LVKlient.SelectedItem = null;
                        UpdateKlient();

                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали клиента прежде чем его удалить");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }


        private void BtnDeletR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                entitis.PersonSet_Agent Rieltor = (entitis.PersonSet_Agent)LVRieltor.SelectedItem;
                if (Rieltor != null)
                {

                    if (MessageBox.Show($"Вы уверены, что хотите риелтора :" +
                  $"{Rieltor.Sername + " " + Rieltor.Name + " " + Rieltor.Otchestvo}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {


                        App.Context.PersonSet_Agent.Remove(Rieltor);


                        App.Context.SaveChanges();
                        MessageBox.Show("Удаление успешно");
                        LVRieltor.SelectedItem = null;
                        UpdateRieltor();

                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали риелтора прежде чем его удалить");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void BtnAddR_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(TBoxNameR.Text, @"^[a-zA-Z]+$")
                   && Regex.IsMatch(TBoxSernemeR.Text, @"^[a-zA-Z]+$")
                   && Regex.IsMatch(TBoxOtcestvoR.Text, @"^[a-zA-Z]+$")
                   && Regex.IsMatch(TBoxDealShearR.Text, "^[0-9]+$"))
            {

            
                try
            {
                var RieltorSet = new entitis.PersonSet
                {
                    FirstName = TBoxNameR.Text,
                    LastName = TBoxSernemeR.Text,
                    MiddleName = TBoxOtcestvoR.Text
                };

                App.Context.PersonSet.Add(RieltorSet);
                App.Context.SaveChanges();
                var j = App.Context.PersonSet.Max(p => p.Id);
                var Rieltor = new entitis.PersonSet_Agent
                {
                    DealShare = Convert.ToInt32(TBoxDealShearR.Text),
                    Id = j
                };
                App.Context.PersonSet_Agent.Add(Rieltor);
                App.Context.SaveChanges();
                MessageBox.Show("Риелтор успешно добавлен");
                UpdateRieltor();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
            }
            else
            {
                MessageBox.Show("Поля имеют не верный формат");
            }

        }

        private void BtnEditR_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(TBoxNameR.Text, @"^[a-zA-Z]+$")
                      && Regex.IsMatch(TBoxSernemeR.Text, @"^[a-zA-Z]+$")
                      && Regex.IsMatch(TBoxOtcestvoR.Text, @"^[a-zA-Z]+$")
                      && Regex.IsMatch(TBoxDealShearR.Text, "^[0-9]+$"))
            {
                try
                {
                    entitis.PersonSet_Agent Rieltor = (entitis.PersonSet_Agent)LVRieltor.SelectedItem;
                    if (Rieltor != null)
                    {
                        Rieltor.z3_PersonSet.FirstName = TBoxNameR.Text;
                        Rieltor.z3_PersonSet.LastName = TBoxSernemeR.Text;
                        Rieltor.z3_PersonSet.MiddleName = TBoxOtcestvoR.Text;
                        Rieltor.DealShare = Convert.ToInt32(TBoxDealShearR.Text);

                        App.Context.SaveChanges();
                        MessageBox.Show("Обновление риелтора успешно");
                        UpdateRieltor();
                    }
                    else MessageBox.Show("Выберите риелтора прежде чем его редактировать");
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Поля имеют не верный формат");
            }
        }

        private void BtnAddK_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(TBoxNameK.Text, @"^[a-zA-Z]+$")
                && Regex.IsMatch(TBoxSernemeK.Text, @"^[a-zA-Z]+$")
                && Regex.IsMatch(TBoxOtcestvoK.Text, @"^[a-zA-Z]+$")
                && Regex.IsMatch(TBoxPochtaK.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                && Regex.IsMatch(TBoxPhoneK.Text, "^[0-9]+$"))
            {
                try
                {
                    var KlientSet = new entitis.PersonSet
                    {
                        FirstName = TBoxNameK.Text,
                        LastName = TBoxSernemeK.Text,
                        MiddleName = TBoxOtcestvoK.Text
                    };

                    App.Context.PersonSet.Add(KlientSet);
                    App.Context.SaveChanges();
                    var i = App.Context.PersonSet.Max(p => p.Id);

                    var Klient = new entitis.PersonSet_Client
                    {
                        Phone = TBoxPhoneK.Text,
                        Email = TBoxPochtaK.Text,
                        Id = i
                    };
                    App.Context.PersonSet_Client.Add(Klient);
                    App.Context.SaveChanges();
                    MessageBox.Show("Клиент успешно добавлен");
                    UpdateKlient();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Поля имеют не верный формат");
            }

        }

        private void BtnEditK_Click(object sender, RoutedEventArgs e)
        {
            if (Regex.IsMatch(TBoxNameK.Text, @"^[a-zA-Z]+$")
                   && Regex.IsMatch(TBoxSernemeK.Text, @"^[a-zA-Z]+$")
                   && Regex.IsMatch(TBoxOtcestvoK.Text, @"^[a-zA-Z]+$")
                   && Regex.IsMatch(TBoxPochtaK.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                   && Regex.IsMatch(TBoxPhoneK.Text, "^[0-9]+$"))
            {
                try
                {
                    entitis.PersonSet_Client Klient = (entitis.PersonSet_Client)LVKlient.SelectedItem;
                    if (Klient != null)
                    {
                        Klient.z3_PersonSet.FirstName = TBoxNameK.Text;
                        Klient.z3_PersonSet.LastName = TBoxSernemeK.Text;
                        Klient.z3_PersonSet.MiddleName = TBoxOtcestvoK.Text;
                        Klient.Phone = TBoxPhoneK.Text;
                        Klient.Email = TBoxPochtaK.Text;
                        App.Context.SaveChanges();
                        MessageBox.Show("Обновление клиента успешно");
                        UpdateKlient();
                    }
                    else MessageBox.Show("Выберите клиента прежде чем его редактировать");
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            else
            {
                MessageBox.Show("Поля имеют не верный формат");
            }
        }

        private void LVKlient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            entitis.PersonSet_Client Klient = (entitis.PersonSet_Client)LVKlient.SelectedItem;
            if (Klient != null)
            {
                TBoxNameK.Text = Klient.Name;
                TBoxSernemeK.Text = Klient.Sername;
                TBoxOtcestvoK.Text = Klient.Otchestvo;
                TBoxPhoneK.Text = Klient.Phone;
                TBoxPochtaK.Text = Klient.Email;
            }
        }

        private void LVRieltor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            entitis.PersonSet_Agent Rieltor = (entitis.PersonSet_Agent)LVRieltor.SelectedItem;
            if (Rieltor != null)
            {
                TBoxNameR.Text = Rieltor.Name;
                TBoxSernemeR.Text = Rieltor.Sername;
                TBoxOtcestvoR.Text = Rieltor.Otchestvo;
                TBoxDealShearR.Text = Rieltor.DealShare.ToString();
            }

        }
        private void UpdateKlient()
        {
            IEnumerable<entitis.PersonSet_Client> Klients = App.Context.PersonSet_Client;


            //MessageBox.Show(StringDistance.LevenshteinDistance(App.Context.PersonSet_Client.Where(p=>p.Fio.ToLower()!="").ToString(),TBoxSerсhK.Text.ToLower()).ToString());
            
            Klients = Klients.Where(p => p.Fio.ToLower().Contains(TBoxSerсhK.Text.ToLower()));

            var klient = Klients.ToList();
            LVKlient.ItemsSource = klient;

        }
        private void UpdateRieltor()
        {
            IEnumerable<entitis.PersonSet_Agent> Rieltors = App.Context.PersonSet_Agent;
            Rieltors = Rieltors.Where(p => p.Fio.ToLower().Contains(TBoxSerсhR.Text.ToLower()));
            var Rieltor = Rieltors.ToList();
            LVRieltor.ItemsSource = Rieltor;
        }

        private void TBoxSerсhK_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateKlient();
        }

        private void TBoxSerсhR_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateRieltor();
        }

        private void BtnNedvijimost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Nedvijimost());
        }

        private void BtnPredlozenie_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Predlozenie());
        }

        private void BtnPotribnost_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Potrebnost());
        }

        private void BtnSdelka_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sdelka());
        }

        public static class StringDistance
        {
            /// <summary>
            /// Compute the distance between two strings.
            /// </summary>
            public static int LevenshteinDistance(string s, string t)
            {
                int n = s.Length;
                int m = t.Length;
                int[,] d = new int[n + 1, m + 1];

                // Step 1
                if (n == 0)
                {
                    return m;
                }

                if (m == 0)
                {
                    return n;
                }

                // Step 2
                for (int i = 0; i <= n; d[i, 0] = i++)
                {
                }

                for (int j = 0; j <= m; d[0, j] = j++)
                {
                }

                // Step 3
                for (int i = 1; i <= n; i++)
                {
                    //Step 4
                    for (int j = 1; j <= m; j++)
                    {
                        // Step 5
                        int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                        // Step 6
                        d[i, j] = Math.Min(
                            Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                            d[i - 1, j - 1] + cost);
                    }
                }
                // Step 7
                return d[n, m];
            }
        }

        
    }
}



