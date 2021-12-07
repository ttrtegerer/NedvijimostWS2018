using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventPage : ContentPage
    {
        string connectionString = @"Data Source=stud-mssql.sttec.yar.ru,38325;Database=user79_db;" + "User ID=user79_db;Password=user79;Connect Timeout=30;" + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" + "MultiSubnetFailover=False";
        SqlConnection connection;
        public int AgentId;
        protected internal ObservableCollection<Eventcs> Eventcs { get; set; }
        public EventPage()
        {
            
            InitializeComponent();
            

            connection = new SqlConnection(connectionString);
            string query = String.Format("SELECT * from [X-AgentEvent] ");
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            Eventcs = new ObservableCollection<Eventcs> { };
            while (rd.Read())
            {
                var eventcs = new Eventcs
                {
                    Id = rd.GetInt32(0),
                    DateTime = rd.GetString(2),
                    Duration = rd.GetInt32(4),
                    TypeEvent = rd.GetString(3),
                    Comment = rd.GetString(5)
                };
                Eventcs.Add(eventcs);
            }
            EventsList.BindingContext = Eventcs;
            connection.Close();
        }
        private async void AddButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EddetEvent());
        }
        private async void OnListViewItemSelectedAsync(object sender, SelectedItemChangedEventArgs args)
        {
            
              var choice= await DisplayAlert("Подтвердить действие", "Вы хотите удалить событие?", "Да", "Нет");
              if (choice) {
                Eventcs selectedEventcs = args.SelectedItem as Eventcs;
                if (selectedEventcs != null) {
                    connection = new SqlConnection(connectionString);
                    string query = String.Format("delete  from [X-AgentEvent] WHERE Id  = '{0}'", 21);
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    await Navigation.PushAsync(new EventPage());
                }

            }
              else 
              {
                  await DisplayAlert("Уведомление", "Вернутся к списку событий", "OK");
                  EventsList.SelectedItem = null;
              }
        }
        

    }
}